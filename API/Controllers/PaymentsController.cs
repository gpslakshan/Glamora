using API.Extensions;
using API.Models.Domain;
using API.Models.Domain.OrderAggregate;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stripe;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(
        IPaymentService paymentService,
        IDeliveryMethodsRepository deliveryMethodsRepo,
        ILogger<PaymentsController> logger,
        IOrdersRepository ordersRepo,
        IConfiguration config,
        IHubContext<NotificationHub> hubContext) : ControllerBase
    {
        private readonly string _whSecret = config["StripeSettings:WhSecret"]!;


        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);

            if (cart == null)
            {
                return BadRequest("Problem with your cart");
            }

            return Ok(cart);
        }

        [HttpGet("delivery-methods")]
        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods()
        {
            var deliveryMethods = await deliveryMethodsRepo.GetAllDeliveryMethodsAsync();
            return deliveryMethods;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebHook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = ConstructStripeEvent(json);

                if (stripeEvent.Data.Object is not PaymentIntent intent)
                {
                    return BadRequest("Invalid event data");
                }

                await HandlePaymentIntentSucceeded(intent);

                return Ok();
            }
            catch (StripeException ex)
            {
                logger.LogError(ex, "Stripe webhook error");
                return StatusCode(StatusCodes.Status500InternalServerError, "Stripe webhook error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        private async Task HandlePaymentIntentSucceeded(PaymentIntent intent)
        {
            if (intent.Status == "succeeded")
            {
                var order = await ordersRepo.GetOrderByPaymentIntentIdAsync(intent.Id) ?? throw new Exception("Order not found");

                var orderTotalInCents = (long)Math.Round(order.GetTotal() * 100,
                MidpointRounding.AwayFromZero);

                if (orderTotalInCents != intent.Amount)
                {
                    order.Status = OrderStatus.PaymentMismatch;
                }
                else
                {
                    order.Status = OrderStatus.PaymentReceived;
                }

                await ordersRepo.UpdateOrderAsync(order);

                var connectionId = NotificationHub.GetConnectionIdByEmail(order.BuyerEmail);

                if (!string.IsNullOrEmpty(connectionId))
                {
                    await hubContext.Clients.Client(connectionId).SendAsync("OrderCompleteNotification", order.ToDto());
                }
            }
        }

        private Event ConstructStripeEvent(string json)
        {
            try
            {
                return EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to construct stripe event");
                throw new StripeException("Invalid signature");
            }
        }
    }
}
