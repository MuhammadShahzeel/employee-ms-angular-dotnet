using EMSBackend.Data;
using EMSBackend.Interfaces;
using EMSBackend.Models;
using EMSBackend.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

// dependency inject for department
builder.Services.AddScoped<IRepository<Department>, Repository<Department>>();
// dependency inject for employee
builder.Services.AddScoped<IRepository<Employee>, Repository<Employee>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()                
    .AllowAnyHeader()                
    .SetIsOriginAllowed(origin => true) 
                                        
);



app.UseAuthorization();

app.MapControllers();

app.Run();
