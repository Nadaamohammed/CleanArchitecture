
namespace Product.Domain.Entity;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
