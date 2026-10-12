using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Stoxx.Web.Contracts;

namespace Stoxx.Web.Components.Pages;

public class HomeBase : ComponentBase
{
    [Inject] protected IJSRuntime JS { get; set; } = default!;
    protected bool Ready, Busy, IsError, SetupRequired;
    protected string Notice = "", View = "inicio", LoginUserName = "", LoginPassword = "", SetupName = "", SetupUserName = "", SetupPassword = "", SaleSearch = "", ProductSearch = "", PaymentMethod = "Efectivo", ReportDate = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
    protected UserDto? CurrentUser;
    protected DashboardDto? Dashboard;
    protected CashDto? Cash;
    protected decimal OpeningAmount, CountedAmount;
    protected List<ProductDto> Products = [], SaleProducts = [];
    protected List<SupplierDto> Suppliers = [];
    protected List<PurchaseDto> Purchases = [];
    protected List<UserDto> Users = [];
    protected List<CartLine> Cart = [];
    protected ReportDto? Report;
    protected ProductFormModel ProductForm = new();
    protected SupplierFormModel SupplierForm = new();
    protected PurchaseFormModel PurchaseForm = new();
    protected UserFormModel UserForm = new();
    protected bool IsAdmin => CurrentUser?.Role == "Admin";
    protected bool CanSell => CurrentUser?.Role is "Admin" or "Cajero";
    protected bool CanInventory => CurrentUser?.Role is "Admin" or "Inventario";
    protected string PageTitle => View switch { "ventas" => "Registrar venta", "productos" => "Productos", "compras" => "Compras y proveedores", "reportes" => "Reportes", "usuarios" => "Usuarios", _ => "Resumen" };
    protected string Initial => string.IsNullOrWhiteSpace(CurrentUser?.Name) ? "S" : CurrentUser.Name[..1].ToUpperInvariant();
    protected string RoleName => RoleLabel(CurrentUser?.Role ?? "");
    protected string TodayLabel => DateTime.Now.ToString("dddd, d 'de' MMMM 'de' yyyy", CultureInfo.GetCultureInfo("es-BO"));
    protected decimal CartTotal => Cart.Sum(x => x.UnitPrice * x.Quantity);
    protected bool ProductMatches(ProductDto p) => p.Active && p.Stock > 0 && $"{p.Code} {p.Name}".Contains(SaleSearch, StringComparison.CurrentCultureIgnoreCase);
    protected bool ProductMatchesSearch(ProductDto p) => $"{p.Code} {p.Name} {p.Category}".Contains(ProductSearch, StringComparison.CurrentCultureIgnoreCase);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        try { var status = await Get<StatusDto>("/auth/status"); SetupRequired = status.SetupRequired; if (status.Authenticated) { CurrentUser = status.User; View = "inicio"; Dashboard = await Get<DashboardDto>("/dashboard"); Cash = Dashboard.Cash ?? new CashDto(); } }
        catch (Exception ex) { SetNotice(ex.Message, true); }
        Ready = true; StateHasChanged();
    }
    protected async Task<T> Get<T>(string path) => await Request<T>(path, "GET", null) ?? throw new InvalidOperationException("Respuesta vacía del servidor.");
    protected async Task<T?> Send<T>(string path, HttpMethod method, object? body = null) => await Request<T>(path, method.Method, body);
    private async Task<T?> Request<T>(string path, string method, object? body)
    {
        var result = await JS.InvokeAsync<ApiResult>("stoxxApi.request", "/api" + path, method, body is null ? null : JsonSerializer.Serialize(body));
        if (result.Status < 200 || result.Status >= 300) { var msg = "No se pudo completar la operación."; try { msg = JsonDocument.Parse(result.Body).RootElement.GetProperty("message").GetString() ?? msg; } catch { } throw new InvalidOperationException(msg); }
        if (string.IsNullOrEmpty(result.Body)) return default;
        return JsonSerializer.Deserialize<T>(result.Body, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
    protected async Task Run(Func<Task> action) { Busy = true; Notice = ""; try { await action(); } catch (Exception ex) { SetNotice(ex.Message, true); } finally { Busy = false; } }
    protected void SetNotice(string text, bool error = false) { Notice = text; IsError = error; }
    protected async Task LoginAsync() => await Run(async () => { await Send<object>("/auth/login", HttpMethod.Post, new LoginRequest(LoginUserName, LoginPassword)); CurrentUser = (await Get<StatusDto>("/auth/status")).User; LoginPassword = ""; await ShowDashboardAsync(); });
    protected async Task CreateAdminAsync() => await Run(async () => { await Send<object>("/auth/setup", HttpMethod.Post, new SetupRequest(SetupName, SetupUserName, SetupPassword)); SetupRequired = false; SetupPassword = ""; SetNotice("Administrador creado. Inicia sesión."); });
    protected async Task ShowDashboardAsync() { View = "inicio"; if (CurrentUser is not null) await LoadDashboardAsync(); }
    protected async Task LoadDashboardAsync() { await Run(async () => { Dashboard = await Get<DashboardDto>("/dashboard"); Cash = Dashboard.Cash ?? new CashDto(); }); }
    protected async Task ShowSalesAsync() => await LoadView("ventas", async () => { SaleProducts = await Get<List<ProductDto>>("/products"); Cash = await Get<CashDto>("/cash"); });
    protected async Task ShowProductsAsync() => await LoadView("productos", async () => Products = await Get<List<ProductDto>>("/products?all=true"));
    protected async Task ShowPurchasesAsync() => await LoadView("compras", async () => { Suppliers = await Get<List<SupplierDto>>("/suppliers?all=true"); Products = await Get<List<ProductDto>>("/products?all=true"); Purchases = await Get<List<PurchaseDto>>("/purchases"); });
    protected async Task ShowReportsAsync() => await LoadView("reportes", LoadReportAsync);
    protected async Task ShowUsersAsync() => await LoadView("usuarios", async () => Users = await Get<List<UserDto>>("/users"));
    private async Task LoadView(string view, Func<Task> load) { View = view; await Run(load); }
    protected async Task LogoutAsync() { await Run(async () => { await Send<object>("/auth/logout", HttpMethod.Post); CurrentUser = null; Cart.Clear(); View = "inicio"; }); }
    protected async Task OpenCashAsync() => await Run(async () => { await Send<object>("/cash/open", HttpMethod.Post, new OpenCashRequest(OpeningAmount)); OpeningAmount = 0; await LoadDashboardAsync(); });
    protected async Task CloseCashAsync() => await Run(async () => { var result = await Send<CloseDto>("/cash/close", HttpMethod.Post, new CloseCashRequest(CountedAmount)); SetNotice($"Caja cerrada. Diferencia: {Money(result?.Difference ?? 0)}"); CountedAmount = 0; await LoadDashboardAsync(); });
    protected void AddToCart(ProductDto p) { var line = Cart.FirstOrDefault(x => x.ProductId == p.Id); if (line is null) Cart.Add(new(p.Id,p.Name,p.UnitPrice,1,p.Stock)); else if (line.Quantity < line.Stock) line.Quantity++; }
    protected void ChangeQuantity(CartLine line, int delta) { line.Quantity += delta; if (line.Quantity <= 0) Cart.Remove(line); else if (line.Quantity > line.Stock) line.Quantity = line.Stock; }
    protected async Task CompleteSaleAsync() => await Run(async () => { if (Cart.Count == 0) return; var sale = await Send<SaleCreated>("/sales", HttpMethod.Post, new SaleRequest(PaymentMethod, Cart.Select(x => new SaleLineRequest(x.ProductId,x.Quantity)).ToList())); Cart.Clear(); SetNotice($"Venta #{sale?.Id} registrada por {Money(sale?.Total ?? 0)}."); await ShowSalesAsync(); });
    protected async Task SaveProductAsync() => await Run(async () => { var req = new ProductRequest(ProductForm.Code,ProductForm.Name,ProductForm.Category,ProductForm.CostPrice,ProductForm.UnitPrice,ProductForm.Stock,ProductForm.MinimumStock); await Send<object>(ProductForm.Id == 0 ? "/products" : $"/products/{ProductForm.Id}", ProductForm.Id == 0 ? HttpMethod.Post : HttpMethod.Put, req); ResetProductForm(); SetNotice("Producto guardado."); await ShowProductsAsync(); });
    protected void ResetProductForm() => ProductForm = new();
    protected void EditProduct(ProductDto p) => ProductForm = new() { Id=p.Id, Code=p.Code, Name=p.Name, Category=p.Category, CostPrice=p.CostPrice, UnitPrice=p.UnitPrice, Stock=p.Stock, MinimumStock=p.MinimumStock };
    protected async Task ArchiveProductAsync(ProductDto p) => await Run(async () => { await Send<object>($"/products/{p.Id}", HttpMethod.Delete); await ShowProductsAsync(); });
    protected async Task SaveSupplierAsync() => await Run(async () => { await Send<object>("/suppliers",HttpMethod.Post,new SupplierRequest(SupplierForm.Name,SupplierForm.ContactName,SupplierForm.Phone,SupplierForm.Email,SupplierForm.Notes)); SupplierForm = new(); await ShowPurchasesAsync(); });
    protected async Task ArchiveSupplierAsync(SupplierDto s) => await Run(async () => { await Send<object>($"/suppliers/{s.Id}",HttpMethod.Delete); await ShowPurchasesAsync(); });
    protected async Task SavePurchaseAsync() => await Run(async () => { await Send<object>("/purchases",HttpMethod.Post,new PurchaseRequest(PurchaseForm.SupplierId,PurchaseForm.ProductId,PurchaseForm.Quantity,PurchaseForm.UnitCost)); PurchaseForm = new(); await ShowPurchasesAsync(); });
    protected async Task LoadReportAsync() { var day = string.IsNullOrWhiteSpace(ReportDate) ? DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd") : ReportDate; Report = await Get<ReportDto>($"/reports/daily?date={day}"); }
    protected async Task ReportDateChanged(ChangeEventArgs e) { ReportDate = e.Value?.ToString() ?? ""; await Run(LoadReportAsync); }
    protected async Task CreateUserAsync() => await Run(async () => { await Send<object>("/users",HttpMethod.Post,new CreateUserRequest(UserForm.Name,UserForm.UserName,UserForm.Password,UserForm.Role)); UserForm = new(); await ShowUsersAsync(); });
    protected static string Money(decimal value) => value.ToString("Bs #,##0.00", CultureInfo.GetCultureInfo("es-BO"));
    protected static string ShortDate(DateTime value) => value.ToLocalTime().ToString("g");
    protected static string RoleLabel(string role) => role switch { "Admin" => "Administrador", "Cajero" => "Cajero", "Inventario" => "Inventario", _ => role };
}

public sealed class UserDto { public int Id {get;set;} public string Name {get;set;}=""; public string UserName {get;set;}=""; public string Role {get;set;}=""; }
public sealed class StatusDto { public bool SetupRequired {get;set;} public bool Authenticated {get;set;} public UserDto? User {get;set;} }
public sealed class DashboardDto { public decimal SalesTotal {get;set;} public int SalesCount {get;set;} public decimal GrossProfit {get;set;} public int ActiveProducts {get;set;} public int LowStockCount {get;set;} public List<ProductDto> LowStock {get;set;}=[]; public CashDto? Cash {get;set;} public List<RecentSale> RecentSales {get;set;}=[]; }
public sealed class CashDto { public bool Open {get;set;} public decimal OpeningAmount {get;set;} public decimal ExpectedCash {get;set;} }
public sealed class RecentSale { public int Id {get;set;} public DateTime CreatedAtUtc {get;set;} public string PaymentMethod {get;set;}=""; public decimal Total {get;set;} public string CashierName {get;set;}=""; }
public sealed class ProductDto { public int Id {get;set;} public string Code {get;set;}=""; public string Name {get;set;}=""; public string Category {get;set;}=""; public decimal CostPrice {get;set;} public decimal UnitPrice {get;set;} public int Stock {get;set;} public int MinimumStock {get;set;} public bool Active {get;set;} public bool LowStock {get;set;} }
public sealed class SupplierDto { public int Id {get;set;} public string Name {get;set;}=""; public string ContactName {get;set;}=""; public string Phone {get;set;}=""; public bool Active {get;set;} }
public sealed class PurchaseDto { public DateTime CreatedAtUtc {get;set;} public string SupplierName {get;set;}=""; public string ProductName {get;set;}=""; public int Quantity {get;set;} public decimal TotalCost {get;set;} }
public sealed class ReportDto { public decimal TotalSales {get;set;} public int SaleCount {get;set;} public decimal CashTotal {get;set;} public decimal QrTotal {get;set;} public decimal CostOfGoods {get;set;} public decimal GrossProfit {get;set;} public List<ReportSale> Sales {get;set;}=[]; public List<TopProduct> TopProducts {get;set;}=[]; }
public sealed class ReportSale { public int Id {get;set;} public DateTime CreatedAtUtc {get;set;} public string PaymentMethod {get;set;}=""; public decimal Total {get;set;} }
public sealed class TopProduct { public string Product {get;set;}=""; public int Units {get;set;} }
public sealed class SaleCreated { public int Id {get;set;} public decimal Total {get;set;} }
public sealed class CloseDto { public decimal Difference {get;set;} }
public sealed class ApiResult { public int Status {get;set;} public string Body {get;set;}=""; }
public sealed class CartLine(int productId,string name,decimal unitPrice,int quantity,int stock) { public int ProductId {get;}=productId; public string Name {get;}=name; public decimal UnitPrice {get;}=unitPrice; public int Quantity {get;set;}=quantity; public int Stock {get;}=stock; }
public sealed class ProductFormModel { public int Id {get;set;} public string Code {get;set;}=""; public string Name {get;set;}=""; public string Category {get;set;}=""; public decimal CostPrice {get;set;} public decimal UnitPrice {get;set;} public int Stock {get;set;} public int MinimumStock {get;set;} }
public sealed class PurchaseFormModel { public int SupplierId {get;set;} public int ProductId {get;set;} public int Quantity {get;set;}=1; public decimal UnitCost {get;set;} }
public sealed class SupplierFormModel { public string Name {get;set;}=""; public string ContactName {get;set;}=""; public string Phone {get;set;}=""; public string Email {get;set;}=""; public string Notes {get;set;}=""; }
public sealed class UserFormModel { public string Name {get;set;}=""; public string UserName {get;set;}=""; public string Password {get;set;}=""; public string Role {get;set;}="Cajero"; }
