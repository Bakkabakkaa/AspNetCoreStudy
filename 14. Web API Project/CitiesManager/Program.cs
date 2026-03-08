using CitiesManager.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
}).AddXmlSerializerFormatters();
    
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer(); // Generates description for all endpoints
builder.Services.AddSwaggerGen( options => 
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CitiesManager.xml"))); // Generates OpenAPI specification

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger(); // Creates endpoint for swagger.json
app.UseSwaggerUI(); // Creates swagger UI for testing all Web API endpoints / action methods

app.UseAuthorization();

app.MapControllers();

app.Run();