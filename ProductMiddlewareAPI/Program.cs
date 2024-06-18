using ProductMiddlewareAPI.Interfaces;
using ProductMiddlewareAPI.Mapping;
using ProductMiddlewareAPI.Services;
using ProductMiddlewareDataAcces.Interfaces;
using ProductMiddlewareDataAcces.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ApiProductRepository>();
builder.Services.AddScoped<IProductRepository, ApiProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(ProductProfile));

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
