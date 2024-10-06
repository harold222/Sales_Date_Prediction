using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestBackend;

public class NextOrderXUnitTest : IClassFixture<WebApplicationFactory<Program>>

{
    private readonly HttpClient _client;

    public NextOrderXUnitTest(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Local");
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetNextOrder()
    {
        HttpResponseMessage carga = _client.GetAsync($"/api/Services/NextOrder").Result;

        carga.EnsureSuccessStatusCode();

        string content = await carga.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrEmpty();

        List<RespuestaConsultaDto>? result = System.Text.Json.JsonSerializer.Deserialize<List<RespuestaConsultaDto>>(content);

        var findCustomer = result.Find(x => x.nombre == "Customer AHPOP");

        findCustomer.Should().NotBeNull();

        findCustomer.ultimaCompra.Should().Be("2008-02-04 00:00:00.000");
        findCustomer.prediccion.Should().Be("2008-03-23 00:00:00.000");
    }

    public class RespuestaConsultaDto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string ultimaCompra { get; set; }
        public string prediccion { get; set; }
    }
}
