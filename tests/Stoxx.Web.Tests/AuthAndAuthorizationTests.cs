using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Stoxx.Web.Tests;

public sealed class AuthAndAuthorizationTests
{
    [Fact]
    public async Task Health_endpoint_is_public()
    {
        using var factory = new StoxxWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Initial_admin_setup_is_one_time_and_admin_can_sign_in()
    {
        using var factory = new StoxxWebApplicationFactory();
        using var client = factory.CreateClient();

        var setup = await client.PostAsJsonAsync("/api/auth/setup", new
        {
            name = "Admin de prueba",
            userName = "admin_test",
            password = "PruebaSegura123!"
        });
        Assert.Equal(HttpStatusCode.OK, setup.StatusCode);

        var secondSetup = await client.PostAsJsonAsync("/api/auth/setup", new
        {
            name = "Segundo admin",
            userName = "admin_test2",
            password = "OtraClaveSegura123!"
        });
        Assert.Equal(HttpStatusCode.Conflict, secondSetup.StatusCode);

        var login = await client.PostAsJsonAsync("/api/auth/login", new
        {
            userName = "admin_test",
            password = "PruebaSegura123!"
        });
        Assert.Equal(HttpStatusCode.OK, login.StatusCode);

        var users = await client.GetAsync("/api/users");
        Assert.Equal(HttpStatusCode.OK, users.StatusCode);
    }

    [Fact]
    public async Task Unauthenticated_user_cannot_read_products()
    {
        using var factory = new StoxxWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/products");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Cashier_cannot_manage_user_accounts()
    {
        using var factory = new StoxxWebApplicationFactory();
        using var adminClient = factory.CreateClient();

        var setup = await adminClient.PostAsJsonAsync("/api/auth/setup", new
        {
            name = "Admin de prueba",
            userName = "admin_test",
            password = "PruebaSegura123!"
        });
        Assert.Equal(HttpStatusCode.OK, setup.StatusCode);

        var adminLogin = await adminClient.PostAsJsonAsync("/api/auth/login", new
        {
            userName = "admin_test",
            password = "PruebaSegura123!"
        });
        Assert.Equal(HttpStatusCode.OK, adminLogin.StatusCode);

        var createCashier = await adminClient.PostAsJsonAsync("/api/users", new
        {
            name = "Cajero de prueba",
            userName = "cashier_test",
            password = "CajeroSeguro123!",
            role = "Cajero"
        });
        Assert.Equal(HttpStatusCode.Created, createCashier.StatusCode);

        using var cashierClient = factory.CreateClient();
        var cashierLogin = await cashierClient.PostAsJsonAsync("/api/auth/login", new
        {
            userName = "cashier_test",
            password = "CajeroSeguro123!"
        });
        Assert.Equal(HttpStatusCode.OK, cashierLogin.StatusCode);

        var usersResponse = await cashierClient.GetAsync("/api/users");
        Assert.Equal(HttpStatusCode.Forbidden, usersResponse.StatusCode);
    }

    private sealed class StoxxWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _dataDirectory = Path.Combine(
            Path.GetTempPath(), "stoxx-tests", Guid.NewGuid().ToString("N"));

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            builder.ConfigureAppConfiguration((_, configuration) =>
                configuration.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["STOXX_DATA_DIR"] = _dataDirectory
                }));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            try
            {
                if (Directory.Exists(_dataDirectory))
                    Directory.Delete(_dataDirectory, recursive: true);
            }
            catch (IOException)
            {
                // Los archivos temporales no deben hacer fallar la prueba al limpiarse.
            }
            catch (UnauthorizedAccessException)
            {
                // La limpieza es auxiliar; el resultado de la prueba es lo importante.
            }
        }
    }
}
