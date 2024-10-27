using API.Models.Domain;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(
        IPaymentService paymentService,
        IDeliveryMethodsRepository deliveryMethodsRepo) : ControllerBase
    {
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
    }
}
