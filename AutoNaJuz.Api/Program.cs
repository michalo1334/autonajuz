using System.Text.Json.Serialization;
using AutoNaJuz.DAL.Data;
using AutoNaJuz.Model.User;
using AutoNaJuz.Services;
using AutoNaJuz.Services.ConcreteServices;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.Web;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

// Configure database context
services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
    options.EnableSensitiveDataLogging();
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

//Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_apiOrigins",
        policy  =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

// Register application services
services.AddScoped<ICarsService, CarsService>();
services.AddScoped<ICarRentalsService, CarRentalsService>();
services.AddScoped<IRenterInfoService, RenterInfoService>();

// Add Email service
var smtpSettings = builder.Configuration.GetSection("SmtpSettings");
builder.Services.AddSingleton<EmailService>(provider => new EmailService(
    smtpSettings["Host"],
    int.Parse(smtpSettings["Port"]),
    bool.Parse(smtpSettings["EnableSsl"]),
    smtpSettings["Username"],
    smtpSettings["Password"]));

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.ConfigObject.TryItOutEnabled = true;
    });
}

app.UseHttpsRedirection();
app.UseRouting();

//Enable CORS
app.UseCors("_apiOrigins");


app.UseAuthorization(); // Enable authentication

app.MapControllers();

app.UseStaticFiles();
MapSimpleUI();

app.Run();
return;

void MapSimpleUI()
{
    app.MapGet("/", async context =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync("wwwroot/simple_ui.html");
    });
}