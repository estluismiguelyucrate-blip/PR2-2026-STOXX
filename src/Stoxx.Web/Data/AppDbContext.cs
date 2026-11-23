using Microsoft.EntityFrameworkCore;

namespace Stoxx.Web.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<CashSession> CashSessions => Set<CashSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().HasIndex(x => x.UserName).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(x => x.Code).IsUnique();
        modelBuilder.Entity<Product>().Property(x => x.CostPrice).HasPrecision(12, 2);
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasPrecision(12, 2);
        modelBuilder.Entity<Sale>().Property(x => x.Total).HasPrecision(12, 2);
        modelBuilder.Entity<SaleItem>().Property(x => x.UnitPrice).HasPrecision(12, 2);
        modelBuilder.Entity<SaleItem>().Property(x => x.UnitCost).HasPrecision(12, 2);
        modelBuilder.Entity<Purchase>().Property(x => x.UnitCost).HasPrecision(12, 2);
        modelBuilder.Entity<Purchase>().Property(x => x.TotalCost).HasPrecision(12, 2);
        modelBuilder.Entity<CashSession>().Property(x => x.OpeningAmount).HasPrecision(12, 2);
        modelBuilder.Entity<CashSession>().Property(x => x.ClosingAmount).HasPrecision(12, 2);
        modelBuilder.Entity<SaleItem>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Sale>().HasOne(x => x.CashSession).WithMany().OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Purchase>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Purchase>().HasOne(x => x.Supplier).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}
