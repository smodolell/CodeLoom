namespace CodeLoom.Business.Inventory.Data;


[Entity("Products")]
public class Product
{
    public Guid Id { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }


    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;


    public bool IsActive { get; set; }

    [ManyToOne(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

}

[Entity("Categories")]
public class Category
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [OneToMany("Category")]
    public List<Product> Products { get; set; } = new();
}

[Entity("Orders")]
public class Order
{
    public int Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    [OneToMany("Order")]
    public List<OrderItem> OrderItems { get; set; } = new();

    // Relación OneToOne
    [OneToOne("OrderId", IsPrincipal = true)]
    public ShippingInfo ShippingInfo { get; set; }
}

[Entity("OrderItems")]
public class OrderItem
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [ManyToOne("ProductId")]
    public Product Product { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [ManyToOne("OrderId")]
    public Order Order { get; set; }
}

[Entity("ShippingInfo")]
public class ShippingInfo
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [OneToOne("OrderId")]
    public Order Order { get; set; }
}