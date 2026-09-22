namespace DTO.Products;

public class ProductCreateDto
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
