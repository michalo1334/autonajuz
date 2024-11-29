using System.Text.Json.Serialization;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model.User;
using AutoNaJuz.Services.ConcreteServices;
using AutoNaJuz.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

// Configure database context
services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"), 
        b => b.EnableRetryOnFailure(10, TimeSpan.FromSeconds(3), null));
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});

// Configure Identity with disabled password requirements
services.AddIdentity<User, IdentityRole>(options =>
    {
        // Disable password requirements
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 1; // Minimum length set to 1
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>();

// Add controllers and routing
services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

services.AddRouting();

// Add FluentValidation
services.AddFluentValidationAutoValidation();

// Add AutoMapper
services.AddAutoMapper(opt => opt.AddProfile<AutoNaJuz.Services.Mappers.MapperConfigurationProfile>());

// Configure Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoNaJuzAPI", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Cookie,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            []
        }
    });
});

// Register application services
services.AddScoped<ICarsService, CarsService>();
services.AddScoped<ICarRentalsService, CarRentalsService>();
services.AddScoped<IRenterInfoService, RenterInfoService>();
services.AddScoped<IImagesService, ImagesService>();
services.AddScoped<ISeederService, SeederService>();

// Add Email service
var smtpSettings = builder.Configuration.GetSection("SmtpSettings");
builder.Services.AddSingleton<EmailService>(_ => new EmailService(
    smtpSettings["Host"] ?? string.Empty,
    int.Parse(smtpSettings["Port"] ?? string.Empty),
    bool.Parse(smtpSettings["EnableSsl"] ?? string.Empty),
    smtpSettings["Username"],
    smtpSettings["Password"] ?? string.Empty));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors("AllowAllOrigins");

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    var blockAutoMigrations = app.Configuration.GetValue<bool>("BlockAutoMigrations");
    
    if (!blockAutoMigrations)
        db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.ConfigObject.TryItOutEnabled = true;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthorization(); // Enable authentication

app.MapControllers();
MapSimpleUi();

app.Run();
return;

void MapSimpleUi()
{
    app.MapGet("/", async context =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync("wwwroot/simple_ui.html");
    });
}