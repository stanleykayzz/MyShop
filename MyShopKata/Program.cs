using MyShop.Command.Handler;
using MyShop.Core.Entities;
using MyShop.Core.Interface;
using MyShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using MyShop.Queries.Handler;

var builder = WebApplication.CreateBuilder(args);

//Add services to the app
var environment = builder.Environment.EnvironmentName;
var configuration = builder.Configuration.GetSection($"Environments:{environment}");

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
    var connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;

    builder.Services.AddDbContext<MyShopDbContext>(options =>
        options.UseSqlServer(connectionString));

}

builder.Services.AddScoped<IRepository<MyShop.Core.Entities.Product>, ProductRepository>();

builder.Services.AddMediatR(
    cfg => {
        cfg.RegisterServicesFromAssembly(typeof(AddProductCommandHandler).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(GetAllProductsQueryHandler).Assembly);
    });

var app = builder.Build();

// Initialise the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyShopDbContext>();
    if (useInMemoryDatabase)
    {
        dbContext.Database.EnsureCreated(); 
    }
}

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
