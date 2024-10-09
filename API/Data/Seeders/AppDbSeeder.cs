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
        }
    }

    private IEnumerable<Product> GetProducts()
    {
        List<Product> products = new List<Product>
        {
            // src - https://www.maxfashion.com/ae/en/department/men (Max Fashion Middle East)
            new Product { Name = "Women's T-Shirt", Description = "Comfortable and breathable cotton t-shirt, perfect for casual wear during any season. Features a classic fit and a variety of colors to choose from.", Price = 19.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24WCTFEKTRT122SGREENMEDIUM-B24WCTFEKTRT122S-MXSS24042924_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Women", Brand = "H&M", QuantityInStock = 50 },
            new Product { Name = "Men's Jeans", Description = "Stylish slim-fit jeans made from high-quality denim. These jeans offer a modern look with excellent comfort and durability. Available in multiple washes.", Price = 49.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24MDNMFEFJ302BLUEDARK-B24MDNMFEFJ302-MXSPR24020324_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Men", Brand = "Levi's", QuantityInStock = 30 },
            new Product { Name = "Kids' Jacket", Description = "Warm and cozy winter jacket designed for kids. Made with water-resistant material and insulated lining to keep your child warm and dry during cold weather.", Price = 29.99M, PictureUrl = "https://media.maxfashion.com/i/max/C24KBYFSVACAYAWAY102BROWNLIGHT-C24KBYFSVACAYAWAY102-MXWIN24040624_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Kids", Brand = "Gap", QuantityInStock = 20 },
            new Product { Name = "Women's Dress", Description = "Elegant evening dress crafted from luxurious fabric. This dress features a flattering silhouette and intricate detailing, making it perfect for special occasions.", Price = 89.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24WCTFSWT157GREENMEDIUM-B24WCTFSWT157-MXSS24042924_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Women", Brand = "Zara", QuantityInStock = 10 },
            new Product { Name = "Men's T-Shirt", Description = "Casual t-shirt made from soft, premium cotton. Ideal for everyday wear, this t-shirt provides a relaxed fit and comes in a range of colors to suit any style.", Price = 14.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24MBSBCKJOPP100GREYDARK-B24MBSBCKJOPP100-MXSPR24060424_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Men", Brand = "Tommy Hilfiger", QuantityInStock = 60 },
            new Product { Name = "Kids' Shorts", Description = "Comfortable shorts perfect for active kids. Made from breathable fabric with an elastic waistband for a secure and comfortable fit. Available in fun colors.", Price = 12.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24KBOFEATHC506GREENMEDIUM-B24KBOFEATHC506-MXSS24022924_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Kids", Brand = "Disney", QuantityInStock = 40 },
            new Product { Name = "Women's Skirt", Description = "Stylish skirt with a flattering cut, suitable for both casual and formal occasions. Made from lightweight material for easy movement and comfort.", Price = 29.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24WCTFSKS405BLACKDARK-B24WCTFSKS405-MXSS24042924_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Women", Brand = "H&M", QuantityInStock = 15 },
            new Product { Name = "Men's Jacket", Description = "Warm winter jacket with a durable outer shell and insulated lining. Designed to provide maximum warmth and protection in cold weather conditions.", Price = 79.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24MCSFEBRLS370CREAMLIGHT-B24MCSFEBRLS370-MXSPR24170124_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Men", Brand = "Nike", QuantityInStock = 25 },
            new Product { Name = "Kids' T-Shirt", Description = "Cute and comfortable t-shirt for kids. Made from soft, breathable cotton, this t-shirt is perfect for everyday wear and features playful designs.", Price = 9.99M, PictureUrl = "https://media.maxfashion.com/i/max/C24KGYFSVACAY100PURPLELIGHT-C24KGYFSVACAY100-MXWIN24040624_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Kids", Brand = "Carter's", QuantityInStock = 35 },
            new Product { Name = "Women's Jeans", Description = "Skinny jeans with a flattering fit, crafted from stretchy denim for comfort and ease of movement. Available in various washes to match any outfit.", Price = 39.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24WDNMFELUNA112WHITELIGHT-B24WDNMFELUNA112-MXSPR24220424_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Women", Brand = "Zara", QuantityInStock = 20 },
            new Product { Name = "Men's Shorts", Description = "Casual shorts made from lightweight, breathable fabric. Perfect for summer days, these shorts offer a comfortable fit and come in various colors.", Price = 19.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24MCSFESWM504BLUELIGHT-B24MCSFESWM504-MXSPR24250324_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Men", Brand = "Nike", QuantityInStock = 45 },
            new Product { Name = "Kids' Dress", Description = "Adorable dress for kids, made from soft and comfortable fabric. Features charming designs and colors, perfect for any special occasion or everyday wear.", Price = 24.99M, PictureUrl = "https://media.maxfashion.com/i/max/TGTNOOSDENIMJEGGING1BLUEMEDIUM-TGTNOOSDENIMJEGGING1-MXNOOS071_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Kids", Brand = "Disney", QuantityInStock = 15 },
            new Product { Name = "Women's Blouse", Description = "Chic blouse made from lightweight material, perfect for both office and casual wear. Features elegant detailing and comes in a variety of colors.", Price = 34.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24WCTFSWT126ABROWNLIGHT-B24WCTFSWT126A-MXSS24042924_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Women", Brand = "Mango", QuantityInStock = 25 },
            new Product { Name = "Men's Sweater", Description = "Warm and stylish sweater made from high-quality wool. Ideal for cooler weather, this sweater offers both comfort and a fashionable look.", Price = 39.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24MCSFEBDRLS401NAVYDARK-B24MCSFEBDRLS401-MXSS24022324_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Men", Brand = "Tommy Hilfiger", QuantityInStock = 30 },
            new Product { Name = "Kids' Pants", Description = "Comfortable pants made from durable fabric, perfect for active kids. Features an elastic waistband for a secure fit and easy movement.", Price = 19.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24KBYFSBSTGEN509BLUEDARK-B24KBYFSBSTGEN509-MXSPR24200324_02-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Kids", Brand = "Disney", QuantityInStock = 20 },
            new Product { Name = "Women's Coat", Description = "Elegant coat made from high-quality materials. Provides warmth and style, making it perfect for both formal and casual occasions.", Price = 99.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24WCTFSJKT305CREAMLIGHT-B24WCTFSJKT305-MXSS24010424_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Women", Brand = "Calvin Klein", QuantityInStock = 5 },
            new Product { Name = "Men's Suit", Description = "Formal suit made from premium fabric. Offers a sharp and sophisticated look, perfect for business meetings and special occasions.", Price = 149.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24MCSFEBDRSS103BROWNMEDIUM-B24MCSFEBDRSS103-MXSPR24041123-1_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Men", Brand = "Ralph Lauren", QuantityInStock = 10 },
            new Product { Name = "Kids' Sweater", Description = "Warm and cozy sweater for kids, made from soft material. Features fun designs and colors, perfect for keeping your child warm in style.", Price = 14.99M, PictureUrl = "https://media.maxfashion.com/i/max/B24KBOFSBTSH607GREENLIGHT-B24KBOFSBTSH607-MXSPR24250324_01-2100.jpg?$prodimg-d-prt-4-2x$&$quality-retina$&fmt=auto&sm=c", Type = "Kids", Brand = "Disney", QuantityInStock = 25 },
        };

        return products;
    }
}
