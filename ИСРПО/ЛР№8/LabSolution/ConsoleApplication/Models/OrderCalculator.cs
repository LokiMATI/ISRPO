namespace ConsoleApplication.Models;

public class OrderCalculator
{
    public double VatTax { get; set; } = 0;

    public double Total { get; set; } = 0;

    public double DiscountPercentage { get; set; } = 0;

    public double DiscountMinimumAmount { get; set; } = 0;

    public double Discount => Total > DiscountMinimumAmount ? Total * DiscountPercentage : 0;

    public double Tax => Total * VatTax;

    public double Result => Total - Discount + Tax;
}
