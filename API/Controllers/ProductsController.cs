using API.Models.Domain;
using API.Models.Dtos;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsRepository _productsRepository;

    public ProductsController(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productsRepository.GetAllProductsAsync();
        var response = MapToProductDto(products);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        var product = await _productsRepository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var response = MapToProductDto(product);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            PictureUrl = createProductDto.PictureUrl,
            Type = createProductDto.Type,
            Brand = createProductDto.Brand,
            QuantityInStock = createProductDto.QuantityInStock
        };

        await _productsRepository.CreateProductAsync(product);

        var response = MapToProductDto(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
    {
        var product = await _productsRepository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.PictureUrl = updateProductDto.PictureUrl;
        product.Type = updateProductDto.Type;
        product.Brand = updateProductDto.Brand;
        product.QuantityInStock = updateProductDto.QuantityInStock;

        await _productsRepository.UpdateProductAsync(product);

        var response = MapToProductDto(product);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        var product = await _productsRepository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        await _productsRepository.DeleteProductAsync(product);

        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands()
    {
        var brands = await _productsRepository.GetBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes()
    {
        var types = await _productsRepository.GetTypesAsync();
        return Ok(types);
    }

    // Reusable method to map a single Product to ProductDto
    private ProductDto MapToProductDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUrl = product.PictureUrl,
            Type = product.Type,
            Brand = product.Brand,
            QuantityInStock = product.QuantityInStock
        };
    }

    // Reusable method to map a collection of Products to ProductDtos
    private IEnumerable<ProductDto> MapToProductDto(IEnumerable<Product> products)
    {
        return products.Select(p => MapToProductDto(p)).ToList();
    }
}
