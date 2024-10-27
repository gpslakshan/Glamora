using API.Models.Domain;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Stripe;

namespace API.Services.Implementation;

public class PaymentService(
    IConfiguration config,
    ICartService cartService,
    IProductsRepository productsRepo,
    IDeliveryMethodsRepository deliveryMethodsRepo) : IPaymentService
{
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
    {
        StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];

        var cart = await cartService.GetCartAsync(cartId);

        if (cart == null)
        {
            return null;
        }

        var shippingPrice = 0M;

        if (cart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await deliveryMethodsRepo.GetDeliveryMethodByIdAsync((int)cart.DeliveryMethodId);

            if (deliveryMethod == null)
            {
                return null;
            }

            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in cart.Items)
        {
            var productItem = await productsRepo.GetProductByIdAsync(item.ProductId);

            if (productItem == null)
            {
                return null;
            }

            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();
        PaymentIntent? intent = null;

        if (string.IsNullOrEmpty(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice,
                Currency = "usd",
                PaymentMethodTypes = ["card"]
            };

            intent = await service.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice,
            };

            intent = await service.UpdateAsync(cart.PaymentIntentId, options);
        }

        await cartService.SetCartAsync(cart);
        return cart;
    }
}
