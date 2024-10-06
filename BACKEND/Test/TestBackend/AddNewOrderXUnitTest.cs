using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace TestBackend;

public class AddNewOrderXUnitTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AddNewOrderXUnitTest(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Local");
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddNewOrder()
    {
        var newOrder = new
        {
            Empid = 1,
            Shipperid = 1,
            Shipname = "Speedy Express",
            Shipaddress = "525 S. Lexington Ave",
            Shipcity = "Seattle",
            Orderdate = "1996-07-04",
            Requireddate = "1996-08-01",
            Shippeddate = "1996-07-16",
            Freight = 32.38,
            Shipcountry = "USA",
            ProductId = 11,
            UnitPrice = 14,
            Quantity = 12,
            Discount = 0.15
        };

        var contentPost = new StringContent(JsonConvert.SerializeObject(newOrder), Encoding.UTF8, "application/json");

        HttpResponseMessage carga = _client.PostAsync($"/api/Services/CreateOrder", contentPost).Result;

        carga.EnsureSuccessStatusCode();

        Assert.NotNull(carga);

        string content = await carga.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrEmpty();
        Assert.Contains("id", content);
    }
}
