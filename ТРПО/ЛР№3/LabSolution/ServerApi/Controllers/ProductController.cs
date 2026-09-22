using DTO.Products;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServerApi.Services;

namespace ServerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductService productService, CategoryService categoryService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var products = await productService.GetProductsAsync();
            var dtos = products.Select(p => new ProductDto()
            {
                Id = p.Id,
                Title = p.Title,
                Category = categoryService.GetCategoryAsync(p.CategoryId).Result,
                Description = p.Description,
                Price = p.Price,
            });

            return Ok(dtos);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            var product = await productService.GetProductAsync(id);
            if (product is null)
                return NotFound("Товара с таким идентификатором нет.");

            var dto = new ProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                Category = await categoryService.GetCategoryAsync(product.CategoryId),
                Description = product.Description,
                Price = product.Price,
            };

            return Ok(dto);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto input)
    {
        try
        {
            var product = await productService.AddProductAsync(input);
            if (product is null)
                return BadRequest("Ошибка при создании товара.");

            var dto = new ProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                Category = await categoryService.GetCategoryAsync(product.CategoryId),
                Description = product.Description,
                Price = product.Price,
            };

            return Ok(dto);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct([FromBody] Product input)
    {
        try
        {
            var product = await productService.UpdateProductAsync(input);
            if (product is null)
                return BadRequest("Ошибка при обновлении товара.");

            var dto = new ProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                Category = await categoryService.GetCategoryAsync(product.CategoryId),
                Description = product.Description,
                Price = product.Price,
            };

            return Ok(dto);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            if (await productService.DeleteProductAsync(id))
                return NoContent();

            return NotFound();

        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}
