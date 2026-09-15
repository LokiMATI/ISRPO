// Исходный код приложения для рефакторинга.
// Рефакторинг зафиксировать в текстовом документе со столбцами 
// Задание | Исходный код | Код после рефакторинга
// Разнести типы данных по разным файлам.

using ConsoleApplication.DbContexts;
using ConsoleApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication.Services;

public class CustomerService(AppDbContext dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;

    public void AddCustomer(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();
    }

    public void PrintCustomerInfo(int customerId)
    {
        var customer = _dbContext.Customers.Include(c => c.Orders).FirstOrDefault(c => c.Id == customerId);
        if (customer is null)
            return;

        Console.WriteLine(customer.GetName());
        Console.WriteLine(customer.GetEmail());
    }
}
