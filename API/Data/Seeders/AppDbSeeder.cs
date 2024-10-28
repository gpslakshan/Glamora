using API.Models.Domain;

namespace API.Data.Seeders;

public class AppDbSeeder : IAppDbSeeder
{
    private readonly AppDbContext _context;

    public AppDbSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        if (await _context.Database.CanConnectAsync())
        {
            if (!_context.Products.Any())
            {
                var products = GetProducts();
                _context.Products.AddRange(products);
                await _context.SaveChangesAsync();
            }

            if (!_context.DeliveryMethods.Any())
            {
                var deliveryMethods = GetDeliveryMethods();
                _context.DeliveryMethods.AddRange(deliveryMethods);
                await _context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Product> GetProducts()
    {
        List<Product> products =
        [
            // src - https://www.maxfashion.com/ae/en/department/men (Max Fashion Middle East)
            new Product { Name = "Women's T-Shirt", Description = "Comfortable and breathable cotton t-shirt, perfect for casual wear during any season. Features a classic fit and a variety of colors to choose from.", Price = 19.99M, PictureUrl = "/images/products/womens-tshirt.webp", Type = "Women", Brand = "H&M", QuantityInStock = 50 },
            new Product { Name = "Men's Jeans", Description = "Stylish slim-fit jeans made from high-quality denim. These jeans offer a modern look with excellent comfort and durability. Available in multiple washes.", Price = 49.99M, PictureUrl = "/images/products/mens-jeans.webp", Type = "Men", Brand = "Levi's", QuantityInStock = 30 },
            new Product { Name = "Kids' Jacket", Description = "Warm and cozy winter jacket designed for kids. Made with water-resistant material and insulated lining to keep your child warm and dry during cold weather.", Price = 29.99M, PictureUrl = "/images/products/kids-jacket.webp", Type = "Kids", Brand = "Gap", QuantityInStock = 20 },
            new Product { Name = "Women's Dress", Description = "Elegant evening dress crafted from luxurious fabric. This dress features a flattering silhouette and intricate detailing, making it perfect for special occasions.", Price = 89.99M, PictureUrl = "/images/products/womens-dress.webp", Type = "Women", Brand = "Zara", QuantityInStock = 10 },
            new Product { Name = "Men's T-Shirt", Description = "Casual t-shirt made from soft, premium cotton. Ideal for everyday wear, this t-shirt provides a relaxed fit and comes in a range of colors to suit any style.", Price = 14.99M, PictureUrl = "/images/products/mens-tshirt.webp", Type = "Men", Brand = "Tommy Hilfiger", QuantityInStock = 60 },
            new Product { Name = "Kids' Shorts", Description = "Comfortable shorts perfect for active kids. Made from breathable fabric with an elastic waistband for a secure and comfortable fit. Available in fun colors.", Price = 12.99M, PictureUrl = "/images/products/kids-shorts.webp", Type = "Kids", Brand = "Disney", QuantityInStock = 40 },
            new Product { Name = "Women's Skirt", Description = "Stylish skirt with a flattering cut, suitable for both casual and formal occasions. Made from lightweight material for easy movement and comfort.", Price = 29.99M, PictureUrl = "/images/products/womens-skirt.webp", Type = "Women", Brand = "H&M", QuantityInStock = 15 },
            new Product { Name = "Men's Jacket", Description = "Warm winter jacket with a durable outer shell and insulated lining. Designed to provide maximum warmth and protection in cold weather conditions.", Price = 79.99M, PictureUrl = "/images/products/mens-jacket.webp", Type = "Men", Brand = "Nike", QuantityInStock = 25 },
            new Product { Name = "Kids' T-Shirt", Description = "Cute and comfortable t-shirt for kids. Made from soft, breathable cotton, this t-shirt is perfect for everyday wear and features playful designs.", Price = 9.99M, PictureUrl = "/images/products/kids-tshirt.webp", Type = "Kids", Brand = "Carter's", QuantityInStock = 35 },
            new Product { Name = "Women's Jeans", Description = "Skinny jeans with a flattering fit, crafted from stretchy denim for comfort and ease of movement. Available in various washes to match any outfit.", Price = 39.99M, PictureUrl = "/images/products/womens-jeans.webp", Type = "Women", Brand = "Zara", QuantityInStock = 20 },
            new Product { Name = "Men's Shorts", Description = "Casual shorts made from lightweight, breathable fabric. Perfect for summer days, these shorts offer a comfortable fit and come in various colors.", Price = 19.99M, PictureUrl = "/images/products/mens-shorts.webp", Type = "Men", Brand = "Nike", QuantityInStock = 45 },
            new Product { Name = "Kids' Dress", Description = "Adorable dress for kids, made from soft and comfortable fabric. Features charming designs and colors, perfect for any special occasion or everyday wear.", Price = 24.99M, PictureUrl = "/images/products/kids-dress.webp", Type = "Kids", Brand = "Disney", QuantityInStock = 15 },
            new Product { Name = "Women's Blouse", Description = "Chic blouse made from lightweight material, perfect for both office and casual wear. Features elegant detailing and comes in a variety of colors.", Price = 34.99M, PictureUrl = "/images/products/womens-blouse.webp", Type = "Women", Brand = "Mango", QuantityInStock = 25 },
            new Product { Name = "Men's Sweater", Description = "Warm and stylish sweater made from high-quality wool. Ideal for cooler weather, this sweater offers both comfort and a fashionable look.", Price = 39.99M, PictureUrl = "/images/products/mens-sweater.webp", Type = "Men", Brand = "Tommy Hilfiger", QuantityInStock = 30 },
            new Product { Name = "Kids' Pants", Description = "Comfortable pants made from durable fabric, perfect for active kids. Features an elastic waistband for a secure fit and easy movement.", Price = 19.99M, PictureUrl = "/images/products/kids-pants.webp", Type = "Kids", Brand = "Disney", QuantityInStock = 20 },
            new Product { Name = "Women's Coat", Description = "Elegant coat made from high-quality materials. Provides warmth and style, making it perfect for both formal and casual occasions.", Price = 99.99M, PictureUrl = "/images/products/womens-coat.webp", Type = "Women", Brand = "Calvin Klein", QuantityInStock = 5 },
            new Product { Name = "Men's Suit", Description = "Formal suit made from premium fabric. Offers a sharp and sophisticated look, perfect for business meetings and special occasions.", Price = 149.99M, PictureUrl = "/images/products/mens-suit.webp", Type = "Men", Brand = "Ralph Lauren", QuantityInStock = 10 },
            new Product { Name = "Kids' Sweater", Description = "Warm and cozy sweater for kids, made from soft material. Features fun designs and colors, perfect for keeping your child warm in style.", Price = 14.99M, PictureUrl = "/images/products/kids-sweater.webp", Type = "Kids", Brand = "Disney", QuantityInStock = 25 },
        ];

        return products;
    }

    private IEnumerable<DeliveryMethod> GetDeliveryMethods()
    {
        List<DeliveryMethod> deliveryMethods =
        [
            new DeliveryMethod { ShortName = "UPS1", Description = "Fastest delivery time", DeliveryTime = "1-2 Days", Price = 10 },
            new DeliveryMethod { ShortName = "UPS2", Description = "Get it within 5 days", DeliveryTime = "2-5 Days", Price = 5 },
            new DeliveryMethod { ShortName = "UPS3", Description = "Slower but cheap", DeliveryTime = "5-10 Days", Price = 2 },
            new DeliveryMethod { ShortName = "FREE", Description = "Free! You get what you pay for", DeliveryTime = "1-2 Weeks", Price = 0 }
        ];

        return deliveryMethods;
    }
}
