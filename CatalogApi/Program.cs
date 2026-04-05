using JWTAuth.Extenthions;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthExtention();
builder.Services.AddAuthorization();
builder.WebHost.UseUrls("http://localhost:5001");

var app = builder.Build();

app.MapGet("/api/products", (HttpRequest request) => new[]
{
    new { id= 1, name = $"Product 1(served by {request.Host})" },
    new { id= 2, name = "Product 2"}
}).RequireAuthorization(new AuthorizeAttribute { Roles = "User" });

app.UseAuthentication();
app.UseAuthorization();

app.Run();
