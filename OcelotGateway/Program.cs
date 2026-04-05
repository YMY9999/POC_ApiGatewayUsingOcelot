using JWTAuth.Extenthions;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthExtention();

builder.WebHost.UseUrls("http://localhost:5000");

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x => x.WithDictionaryHandle());



var app = builder.Build();

await app.UseOcelot();

app.Run();
