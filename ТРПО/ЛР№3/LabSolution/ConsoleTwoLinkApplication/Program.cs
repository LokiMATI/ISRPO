using ApplicationEntityFramework;
using DTO.Products;
using Microsoft.EntityFrameworkCore;

using ProductDbContext context = new();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
await context.Categories.AddRangeAsync(
    [
    new()
    {
        Name = "Хозтовары"
    },
    new()
    {
        Name = "Продукты питания"
    },
    new()
    {
        Name = "Электроника"
    }
    ]);
await context.SaveChangesAsync();
var categories = await context.Categories.ToListAsync();

await context.Products.AddRangeAsync(
    [
    new()
    {
        Title = "Яблоко",
        CategoryId = categories.First(c => c.Name == "Продукты питания").Id,
        Description = "Вкусное предвыборное яблоко",
        Price = 3.59
    },
    new()
    {
        Title = "Телефон",
        CategoryId = categories.First(c => c.Name == "Электроника").Id,
        Description = "Сотовый телефон с новейшей технологией 3G",
        Price = 1000
    },
    new()
    {
        Title = "Сода",
        CategoryId = categories.First(c => c.Name == "Хозтовары").Id,
        Description = "Сода",
        Price = 50000000
    },
    new()
    {
        Title = "Доместос",
        CategoryId = categories.First(c => c.Name == "Хозтовары").Id,
        Description = "Туалеты чистить надо",
        Price = 5032
    },
    new()
    {
        Title = "Галеты",
        CategoryId = categories.First(c => c.Name == "Продукты питания").Id,
        Description = "Можно гвозди ими забивать",
        Price = 0.50
    },
]);

await context.SaveChangesAsync();

while (true)
{
    Console.WriteLine("---Консольное приложение, по просмотру товаров---");

    Console.WriteLine("\n1) Просмотреть товары\n3) Добавить товар\n4) Обновить товар\n5) Удалить товар\n0) Выйти");

    Console.Write("Введите номер операции: ");
    if (int.TryParse(Console.ReadLine(), out int option))
    {
        switch (option)
        {
            case 0: return;
            case 1:
                await ShowProducts();
                break;
            default:
                Console.WriteLine("Данный номер опции не известен.");
                break;
        }
    }
    else
        Console.WriteLine("Неверный ввод.");
    Console.Clear();
}

async Task ShowProducts()
{
    string? filter = null;
    int? parameter = null;

    int page = 1;
    int size = 4;

    while (true)
    {
        var products = await context.Products.ToListAsync();

        if (filter is not null)
            products = products.Where(p => p.Title.Contains(filter)).ToList();

        if (parameter is not null)
            switch (parameter)
            {
                case 1:
                    products = products.OrderBy(p => p.Title).ToList();
                    break;
                case 2:
                    products = products.OrderBy(p => p.Description).ToList();
                    break;
                case 3:
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
            }

        int totalPages = (int)Math.Ceiling(products.Count / (decimal)size);

        var dtos = products.Skip(size * (page - 1)).Take(size).Select(p
        => new ProductDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            Category = context.Categories.FirstOrDefault(c => c.Id == p.CategoryId)
        }).ToList();

        Console.Clear();
        Console.WriteLine($"---Список товаров (Страница {page})---\nЧтобы перемещаться по страницам, нажимайте на Left или Right");

        dtos.ForEach(ShowProductInfo);

        Console.WriteLine("\nВыбрать опцию:\n1) Фильтрировать список\n2) Сортировать список\n3) Детальный просмотр\n0) Выйти");

        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.LeftArrow && page > 1)
            page--;
        else if (key.Key == ConsoleKey.RightArrow && page < totalPages)
            page++;
        else
        {
            if (int.TryParse(key.KeyChar.ToString(), out int option))
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Write("Введите название продукта: ");
                        filter = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(filter))
                            filter = null;
                        break;
                    case 2:
                        Console.Write("Выберите по какому параметру сортировать:\n" +
                            "1) Название\n" +
                            "2) Описание\n" +
                            "3) Цена\n" +
                            "Любой другой знак - сброс сортировки\n" +
                            "Выбор параметра сортировки: ");
                        if (int.TryParse(Console.ReadLine(), out int result) && result >= 1 && result <= 3)
                            parameter = result;
                        else
                            parameter = null;
                        break;
                    case 3:
                        Console.Write("Введите идентификатор продукта: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                            ShowDetalProduct(id);
                        else
                        {
                            Console.Write("Неверный ввод...");
                            Console.ReadKey(true);
                        }
                        break;
                }
            continue;
        }
    }

}

void ShowProductInfo(ProductDto dto)
    => Console.WriteLine($"{dto.Id}: {dto.Title}\n" +
        $"Описание: {dto.Description}\n" +
        $"Категория: {(dto.Category.Name is null ? "Не указано" : dto.Category.Name)}\n" +
        $"Цена: {dto.Price}\n");

async Task ShowDetalProduct(int id)
{
    Console.Clear();
    var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
    if (product is null)
    {
        Console.WriteLine("Товара с таким идентификатором нет");
        return;
    }

    Console.WriteLine("---Подробная инфромация о товаре---");
    ShowProductInfo(
        new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Category = context.Categories.FirstOrDefault(c => c.Id == product.CategoryId)
        });

    Console.WriteLine("\nНажмите Enter, чтобы выйти...");
    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
}