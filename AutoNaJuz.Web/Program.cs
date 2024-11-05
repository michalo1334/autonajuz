using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoNaJuz.Web;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja EmailService
var smtpSettings = builder.Configuration.GetSection("SmtpSettings");
builder.Services.AddSingleton<EmailService>(provider => new EmailService(
    smtpSettings["Host"],
    int.Parse(smtpSettings["Port"]),
    bool.Parse(smtpSettings["EnableSsl"]),
    smtpSettings["Username"],
    smtpSettings["Password"]));
    
// Dodaj inne usługi
builder.Services.AddControllers(); // Rejestracja kontrolerów

var app = builder.Build();

// Konfiguracja potoku HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles(); // Włączenie obsługi statycznych plików

// Mapowanie domyślnej strony
app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("wwwroot/index.html");
    
});

// Mapowanie kontrolerów
app.MapControllers();

app.Run();
