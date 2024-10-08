using API.Data;
using API.Models.Domain;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation;

public class ProductsRepository : IProductsRepository
{
    private readonly AppDbContext _context;

    public ProductsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<(IEnumerable<Product>, int)> GetAllProductsAsync(string? searchTerm, string? brand,
        string? type, string? sort, int pageNumber, int pageSize)
    {
        var query = _context.Products.AsQueryable();

        // Searching
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));
        }

        // Filtering
        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(p => p.Brand == brand);
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(p => p.Type == type);
        }

        var totalItemsCount = await query.CountAsync(); // Taking the count after searching & filtering

        // Sorting
        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query
        }; // Switch Expression in C# 12 - https://medium.com/@mohammadbourm/switch-expression-c-12-343b82e236ae


        // Pagination
        query = query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);


        return (await query.ToListAsync(), totalItemsCount);
    }

    public async Task<IEnumerable<string>> GetBrandsAsync()
    {
        return await _context.Products
            .Select(p => p.Brand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<string>> GetTypesAsync()
    {
        return await _context.Products
            .Select(p => p.Type)
            .Distinct()
            .ToListAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}
