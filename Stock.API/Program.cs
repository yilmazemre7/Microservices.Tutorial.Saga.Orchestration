using MassTransit;
using MongoDB.Driver;
using Scalar.AspNetCore;
using Stock.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, _configure) =>
    {
        _configure.Host(builder.Configuration["RabbitMQ"]);
    });
});

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mongoDbService = scope.ServiceProvider.GetRequiredService<MongoDbService>();
    var stocks = mongoDbService.GetCollection<Stock.API.Models.Stock>();
    if (!await stocks.Find(_ => true).AnyAsync())
    {
        await stocks.InsertManyAsync(
        [
            new Stock.API.Models.Stock { ProductId = 1, Count = 200 },
            new Stock.API.Models.Stock { ProductId = 2, Count = 300 },
            new Stock.API.Models.Stock { ProductId = 3, Count = 50 },
            new Stock.API.Models.Stock { ProductId = 4, Count = 10 },
            new Stock.API.Models.Stock { ProductId = 5, Count = 60 }
        ]);
    }
}

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.Run();
