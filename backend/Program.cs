using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DataBaseContext>();
builder.Services.AddScoped<IProductService,ProductService>();

builder.Configuration.AddJsonFile("appsettings.json");
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/products", async (IProductService _productService) =>
{
    var users = await _productService.GetAllProductAsync();
    return Results.Ok(users);
});

app.MapGet("/products/{id}", async (int id, IProductService _productService) =>
{
    var user = await _productService.GetProductByIdAsync(id);
    return Results.Ok(user);
});

app.MapPost("/product", async (IProductService _productervice, ProductCreateDto productCreateDto) =>
{
    var product = await _productervice.CreateProductAsync(productCreateDto);
    return Results.Ok(product);
});

app.MapPut("/product/{id}", async (int id, ProductUpdateDto product,IProductService _productService) =>
{
    
    product.Id = id;
    if (id != product.Id) 
        return Results.BadRequest("ID in URL does not match ID in request body");
    
    var existProduct = await _productService.GetProductByIdAsync(id);
    if (existProduct == null) 
        return Results.NotFound($"Product with ID {id} not found");
    
    var updatedProduct = await _productService.UpdateProductAsync(product);
    return Results.Ok(updatedProduct);

});

app.MapDelete("/product/{id}", async (int id, IProductService _productService) =>
{
    var product = await _productService.GetProductByIdAsync(id);
    if (product == null) return Results.NotFound();

    await _productService.DeleteProductAsync(id);
    return Results.NoContent();

});

app.Run();

