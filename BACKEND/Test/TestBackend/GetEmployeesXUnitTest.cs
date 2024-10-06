using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using static TestBackend.GetClientOrdersXUnitTest;

namespace TestBackend;

public class GetEmployeesXUnitTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GetEmployeesXUnitTest(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Local");
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetEmployees()
    {
        HttpResponseMessage carga = _client.GetAsync($"/api/Services/GetEmployees").Result;

        carga.EnsureSuccessStatusCode();

        Assert.NotNull(carga);

        string content = await carga.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrEmpty();

        var result = System.Text.Json.JsonSerializer.Deserialize<List<EmployeesTest>>(content);

        Assert.Equal(9, result.Count);
    }

    public class EmployeesTest
    {
        public int id { get; set; }
        public string nombreCompleto { get; set; }
    }
}
