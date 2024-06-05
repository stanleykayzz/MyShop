using MyShop.Command.Handler;
using MyShop.Core.Entities;
using MyShop.Core.Interface;
using MyShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Add services to the app
var environment = builder.Environment.EnvironmentName;
var configuration = builder.Configuration.GetSection("Environnments").GetSection(environment);

//Is inMemoryDatabase enable
var useInMemoryDatabase = configuration.GetValue<bool>("DatabaseSettings:UseInMemoryDatabase");

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (useInMemoryDatabase)
{
    builder.Services.AddDbContext<MyShopDbContext>(options =>
        options.UseInMemoryDatabase(environment)
    );
}
else
{
    builder.Services.AddDbContext<MyShopDbContext>(options =>
        options.UseSqlServer(
            configuration.GetValue<string>("DatabaseSettings:ConnectionString")
        ));
}

builder.Services.AddScoped<IRepository<MyShop.Core.Entities.Product>, ProductRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddProductCommandHandler).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyShop API V1");
        
        // To serve Swagger at root of the application
        c.RoutePrefix = string.Empty;
    });
}

//app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
