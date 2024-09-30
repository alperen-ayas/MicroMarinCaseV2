using MicroMarinCaseV2.Api.Strategies;
using MicroMarinCaseV2.Application;
using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using MicroMarinCaseV2.Infrastructure;
using MicroMarinCaseV2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<IOperation, Operation>();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServies();

var jsonOptions = new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    WriteIndented = true,
};

builder.Services.AddSingleton(jsonOptions);

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MicroMarinDbContext>();

    context.Database.EnsureCreated();

    if (!context.Products.Any())
    {
        context.Products.Add(Product.Create(Guid.Parse("04bf3911-9c6e-4960-80cf-81f865a784f0"), "Telefon", 17.12m));
        context.Products.Add(Product.Create(Guid.Parse("04bf3911-9c6e-4960-80cf-81f865a784f1"), "Televizyon", 9.99m));

        context.SaveChanges();
    }

    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
