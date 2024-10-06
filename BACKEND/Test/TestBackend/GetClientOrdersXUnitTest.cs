using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using static TestBackend.NextOrderXUnitTest;

namespace TestBackend;

public class GetClientOrdersXUnitTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GetClientOrdersXUnitTest(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Local");
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("72")]
    public async Task GetClientOrders(string id)
    {
        HttpResponseMessage carga = _client.GetAsync($"/api/Services/GetOrderByClient/{id}").Result;

        carga.EnsureSuccessStatusCode();

        Assert.NotNull(carga);

        string content = await carga.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrEmpty();

        var result = System.Text.Json.JsonSerializer.Deserialize<List<OrdersResponse>>(content);

        var item = result[0];
        Assert.False(string.IsNullOrWhiteSpace(item.nombre));
    }

    public class OrdersResponse
    {
        public int id { get; set; }
        public DateTime? fechaRequerida { get; set; }
        public DateTime? fechaCompra { get; set; }
        public string? nombre { get; set; }
        public string? direccion { get; set; }
        public string? ciudad { get; set; }
    }
}
