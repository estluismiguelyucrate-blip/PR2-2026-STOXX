namespace Stoxx.Web.Data;

public sealed class AppUser
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string UserName { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = Roles.Inventory;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public static class Roles
{
    public const string Admin = "Admin";
    public const string Cashier = "Cajero";
    public const string Inventory = "Inventario";
}

public sealed class Product
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal CostPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
    public int MinimumStock { get; set; }
    public bool Active { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public sealed class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string ContactName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public string Notes { get; set; } = "";
    public bool Active { get; set; } = true;
}

public sealed class Sale
{
    public int Id { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public string PaymentMethod { get; set; } = "Efectivo";
    public decimal Total { get; set; }
    public string CashierName { get; set; } = "";
    public int CashSessionId { get; set; }
    public CashSession CashSession { get; set; } = null!;
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}

public sealed class SaleItem
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitCost { get; set; }
}

public sealed class Purchase
{
    public int Id { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public string SupplierName { get; set; } = "";
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalCost { get; set; }
    public string RecordedBy { get; set; } = "";
}

public sealed class CashSession
{
    public int Id { get; set; }
    public DateTime OpenedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAtUtc { get; set; }
    public decimal OpeningAmount { get; set; }
    public decimal? ClosingAmount { get; set; }
    public string OpenedBy { get; set; } = "";
    public string? ClosedBy { get; set; }
}
