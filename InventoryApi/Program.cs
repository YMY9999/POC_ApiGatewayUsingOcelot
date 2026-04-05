using JWTAuth.Extenthions;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddJwtAuthExtention();
builder.Services.AddAuthorization();
builder.WebHost.UseUrls("http://localhost:5002");

var app = builder.Build();

app.MapGet("/api/stock/{productId}", (int productId) =>
{
    return new { ProductId = productId, QuantityInStock = productId == 1 ? 50 : 12 };
}).RequireAuthorization(new AuthorizeAttribute { Roles = "Administrator" });


app.UseAuthentication();
app.UseAuthorization();
app.Run();
