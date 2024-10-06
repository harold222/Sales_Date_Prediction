using System.Reflection;
using Backend.Application;
using Backend.Infraestructure;
using BACKEND.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(360);
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

string env = app.Environment.EnvironmentName;

if (env == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.UseHsts();

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddJsonFile($"appsettings.{env}.json", true, true);

app.Use(async (context, next) =>
{
    string[] allowedOrigins = {
        "localhost"
    };

    string requestUrl = $"{context.Request.Scheme}://{context.Request.Host}";
    string origin = context.Request.Headers["Origin"].ToString();

    if (allowedOrigins.Any(domain => origin.Contains(domain)) || allowedOrigins.Any(domain => requestUrl.Contains(domain)))
    {
        context.Response.Headers.Append("Access-Control-Allow-Origin", origin);
        await next();
    }
    else
        context.Response.StatusCode = 403;
});

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Xss-Protection", "1");
    context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Append("Server", "None");
    await next();
});

app.UseCors(options =>
{
    options.WithOrigins("*");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }