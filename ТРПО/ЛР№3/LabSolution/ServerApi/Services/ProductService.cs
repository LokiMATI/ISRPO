using DTO.Products;
using Models;

namespace ServerApi.Services;

public class ProductService
{
    private readonly List<Product> products = new()
    {
        
    };

    public async Task<Product?> GetProductAsync(int id) => products.FirstOrDefault(c => c.Id == id);

    public async Task<List<Product>> GetProductsAsync() => products;

    public async Task<Product> AddProductAsync(ProductCreateDto dto)
    {
        Product product = new()
        {
            Id = products.Count + 1,
            Title = dto.Title,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            Price = dto.Price
        };

        products.Add(product);

        return product;
    }

    public async Task<Product?> UpdateProductAsync(Product dto)
    {
        var product = products.FirstOrDefault(c => c.Id == dto.Id);
        if (product is null)
            return null;

        product.Title = dto.Title;
        product.CategoryId = dto.CategoryId;
        product.Description = dto.Description;
        product.Price = dto.Price;

        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = products.FirstOrDefault(c => c.Id == id);
        if (product is null)
            return false;

        products.Remove(product);
        return true;
    }
}
