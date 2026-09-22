using DTO.Products;
using System.Net.Http.Json;

HttpClient client = new();

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
    var products = await client.GetFromJsonAsync<List<ProductDto>>("http://localhost:5289/api/Product");

    Console.Clear();
    Console.WriteLine("---Список товаров---\n");

    products.ForEach(ShowProductInfo);

    Console.WriteLine("\nВыбрать опцию:\n1)Сортировать");
}

void ShowProductInfo(ProductDto dto)
    => Console.WriteLine($"{dto.Id}: {dto.Title}\n" +
        $"Описание: {dto.Description}\n" +
        $"Категория: {(dto.Category.Name is null ? "Не указано" : dto.Category.Name)}\n" +
        $"Цена: {dto.Price}\n");