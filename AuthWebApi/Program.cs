using JWTAuth.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5006");

builder.Services.AddControllers();
builder.Services.AddSingleton<JwtTokenHandler>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
