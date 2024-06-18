using ProductMiddlewareAPI.Interfaces;
using ProductMiddlewareAPI.Mapping;
using ProductMiddlewareAPI.Services;
using ProductMiddlewareDataAcces.Interfaces;
using ProductMiddlewareDataAcces.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Product Middleware API",
        Description = "An API to manage products"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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
