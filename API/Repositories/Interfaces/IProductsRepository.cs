using API.Models.Domain;

namespace API.Repositories.Interfaces;

public interface IProductsRepository
{
    Task<(IEnumerable<Product>, int)> GetAllProductsAsync(string? searchTerm, string? brand,
        string? type, string? sort, int pageNumber, int pageSize);

    Task<Product?> GetProductByIdAsync(int id);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
    Task<IEnumerable<string>> GetBrandsAsync();
    Task<IEnumerable<string>> GetTypesAsync();
}
