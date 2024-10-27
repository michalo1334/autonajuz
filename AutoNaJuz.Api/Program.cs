using System.Reflection;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model;
using AutoNaJuz.Services;
using AutoNaJuz.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure database context
var services = builder.Services;

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")));

// Configure Identity
services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add controllers and routing
services.AddControllers();
services.AddRouting();

// Add FluentValidation
services.AddFluentValidationAutoValidation();

// Configure Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Register application services
services.AddScoped<ICarsService, CarsService>();
services.AddScoped<ICarRentalsService, CarRentalsService>();

var app = builder.Build();

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure routing is configured

app.UseAuthentication(); // Enable authentication
app.UseAuthorization();  // Enable authorization

app.MapControllers();

app.Run();
