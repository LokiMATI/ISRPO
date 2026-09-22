using Models;

namespace DTO.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Category? Category { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
