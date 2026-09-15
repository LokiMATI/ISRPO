using ConsoleApplication.DbContexts;
using ConsoleApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication.Services;

public class OrderService(AppDbContext dbContext)
{
    private const double VAT_TAX = 0.2;
    private const double DISCOUNT_MINIMUM_AMOUNT = 10000;
    private const double DISCOUNT_PERCENTAGE = 0.1;
    private readonly AppDbContext _dbContext = dbContext;

    public void AddOrder(Order order)
    {
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

    public void PrintOrderDetails(int orderId)
    {
        var order = _dbContext.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);
        Console.WriteLine(order.GetId());
        Console.WriteLine(order.GetTotal());
        Console.WriteLine(order.GetExpressInfo());
        Console.WriteLine(order.Customer.GetEmail());
    }

    public double CalculateFinalPrice(Order order)
    {
        OrderCalculator calculator = new()
        {
            VatTax = VAT_TAX,
            Total = order.Total,
            DiscountMinimumAmount = DISCOUNT_MINIMUM_AMOUNT,
            DiscountPercentage = DISCOUNT_PERCENTAGE
        };

        return calculator.Result;
    }
}
