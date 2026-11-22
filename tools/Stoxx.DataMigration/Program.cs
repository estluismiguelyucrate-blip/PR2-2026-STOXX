using Microsoft.EntityFrameworkCore;
using Npgsql;
using Stoxx.Web.Data;

var sqlitePath = Environment.GetEnvironmentVariable("STOXX_SQLITE_PATH");
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (string.IsNullOrWhiteSpace(sqlitePath) || string.IsNullOrWhiteSpace(databaseUrl))
{
    Console.Error.WriteLine("Define STOXX_SQLITE_PATH y DATABASE_URL en el entorno antes de ejecutar la migración.");
    return 2;
}
if (!File.Exists(sqlitePath))
{
    Console.Error.WriteLine("No se encontró el archivo SQLite indicado.");
    return 2;
}

var sourceOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite($"Data Source={sqlitePath};Mode=ReadOnly")
    .Options;
var targetOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql(BuildPostgresConnectionString(databaseUrl))
    .Options;

await using var source = new AppDbContext(sourceOptions);
await using var target = new AppDbContext(targetOptions);
await target.Database.EnsureCreatedAsync();

if (await target.Users.AnyAsync() || await target.Products.AnyAsync() || await target.Suppliers.AnyAsync()
    || await target.CashSessions.AnyAsync() || await target.Sales.AnyAsync()
    || await target.SaleItems.AnyAsync() || await target.Purchases.AnyAsync())
{
    Console.Error.WriteLine("La base PostgreSQL ya tiene datos. La importación se detuvo para no mezclarlos.");
    return 3;
}

var users = await source.Users.AsNoTracking().ToListAsync();
var products = await source.Products.AsNoTracking().ToListAsync();
var suppliers = await source.Suppliers.AsNoTracking().ToListAsync();
var cashSessions = await source.CashSessions.AsNoTracking().ToListAsync();
var sales = await source.Sales.AsNoTracking().ToListAsync();
var saleItems = await source.SaleItems.AsNoTracking().ToListAsync();
var purchases = await source.Purchases.AsNoTracking().ToListAsync();

await using var transaction = await target.Database.BeginTransactionAsync();
target.Users.AddRange(users);
target.Products.AddRange(products);
target.Suppliers.AddRange(suppliers);
target.CashSessions.AddRange(cashSessions);
await target.SaveChangesAsync();
target.Sales.AddRange(sales);
await target.SaveChangesAsync();
target.SaleItems.AddRange(saleItems);
target.Purchases.AddRange(purchases);
await target.SaveChangesAsync();

foreach (var table in new[] { "Users", "Products", "Suppliers", "CashSessions", "Sales", "SaleItems", "Purchases" })
{
    var quotedTable = "\"" + table + "\"";
    var command = $"SELECT setval(pg_get_serial_sequence('{quotedTable}', 'Id'), COALESCE(MAX(\"Id\"), 1), COUNT(*) > 0) FROM {quotedTable}";
    await target.Database.ExecuteSqlRawAsync(command);
}
await transaction.CommitAsync();

Console.WriteLine("Migración completada:");
Console.WriteLine($"  Usuarios: {users.Count}; productos: {products.Count}; proveedores: {suppliers.Count}");
Console.WriteLine($"  Cajas: {cashSessions.Count}; ventas: {sales.Count}; líneas: {saleItems.Count}; compras: {purchases.Count}");
Console.WriteLine("Las sesiones de inicio deberán volver a abrirse. Las contraseñas existentes se conservan.");
return 0;

static string BuildPostgresConnectionString(string databaseUrl)
{
    if (!Uri.TryCreate(databaseUrl, UriKind.Absolute, out var uri) || uri.Scheme is not ("postgres" or "postgresql"))
        throw new InvalidOperationException("DATABASE_URL debe ser una dirección postgresql:// válida.");
    var userInfo = uri.UserInfo.Split(':', 2);
    if (userInfo.Length != 2 || string.IsNullOrWhiteSpace(uri.AbsolutePath.Trim('/')))
        throw new InvalidOperationException("DATABASE_URL debe incluir usuario, contraseña y nombre de base.");
    return new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.IsDefaultPort ? 5432 : uri.Port,
        Database = Uri.UnescapeDataString(uri.AbsolutePath.Trim('/')),
        Username = Uri.UnescapeDataString(userInfo[0]),
        Password = Uri.UnescapeDataString(userInfo[1]),
        SslMode = SslMode.Require,
        Timeout = 15,
        CommandTimeout = 30,
        MaxPoolSize = 10
    }.ConnectionString;
}
