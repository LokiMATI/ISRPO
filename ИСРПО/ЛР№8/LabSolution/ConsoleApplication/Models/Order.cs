namespace ConsoleApplication.Models;

public class Order
{
    public int Id { get; set; }
    public double Total { get; set; }
    public bool IsExpress { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public string GetId() => "Order Id: " + Id;

    public string GetTotal() => "Total: " + Total;

    public string GetExpressInfo() => "Express Shipping: " + (IsExpress ? "Yes" : "No");
}
