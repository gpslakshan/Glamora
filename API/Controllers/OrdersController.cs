using API.Extensions;
using API.Models.Domain.OrderAggregate;
using API.Models.Dtos;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController(
    ICartService cartService,
    IProductsRepository productsRepo,
    IOrdersRepository ordersRepo,
    IDeliveryMethodsRepository deliveryMethodsRepo) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
    {
        var buyerEmail = User.GetEmail();

        var cart = await cartService.GetCartAsync(createOrderDto.CartId);

        if (cart == null)
        {
            return NotFound("Cart not found");
        }

        if (cart.PaymentIntentId == null)
        {
            return BadRequest("No payment intent for this order.");
        }

        var items = new List<OrderItem>();

        foreach (var item in cart.Items)
        {
            var productItem = await productsRepo.GetProductByIdAsync(item.ProductId);

            if (productItem == null)
            {
                return BadRequest("Problem with the order");
            }

            var itemOrdered = new ProductItemOrdered
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                PictureUrl = item.PictureUrl
            };

            var orderItem = new OrderItem
            {
                ItemOrdered = itemOrdered,
                Price = productItem.Price,
                Quantity = item.Quantity
            };

            items.Add(orderItem);
        }

        var deliveryMethod = await deliveryMethodsRepo.GetDeliveryMethodByIdAsync(createOrderDto.DeliveryMethodId);

        if (deliveryMethod == null)
        {
            return NotFound("No delivery method selected");
        }

        var order = new Order
        {
            OrderItems = items,
            DeliveryMethod = deliveryMethod,
            ShippingAddress = createOrderDto.ShippingAddress,
            SubTotal = items.Sum(x => x.Price * x.Quantity),
            PaymentSummary = createOrderDto.PaymentSummary,
            PaymentIntentId = cart.PaymentIntentId,
            BuyerEmail = buyerEmail
        };

        await ordersRepo.CreateOrderAsync(order);

        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order.ToDto());
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersForUser()
    {
        var userEmail = User.GetEmail();
        var orders = await ordersRepo.GetOrdersForUserAsync(userEmail);
        var response = orders.Select(o => o.ToDto()).ToList();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var userEmail = User.GetEmail();

        var order = await ordersRepo.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        if (order.BuyerEmail != userEmail)
        {
            return Forbid();
        }

        return Ok(order.ToDto());
    }
}
