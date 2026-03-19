using Microsoft.EntityFrameworkCore;
using MineralCollection.API.Data;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Controller-Unterstützung
builder.Services.AddControllers();

// Swagger/OpenAPI konfigurieren 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite Datenbank-Anbindung
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=minerals.db");
    // Aktiviert die echten Werte in der Konsolenausgabe, hilfreich fürs schnelle Debuggen von SQL-Fehlern, 
    // da die tatsächlichen Werte in den Fehlermeldungen angezeigt werden.
    options.EnableSensitiveDataLogging();

    // Optional: Hilft beim Debuggen von Exceptions, indem EF Core 
    // detailliertere Fehlermeldungen ausgibt.
    options.EnableDetailedErrors();
});

// CORS-Policy hinzufügen, um Anfragen von Blazor WebAssembly zu erlauben
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Datenbank und die dazugehörigen Tabellen müssen erstellt werden falls nicht vorhanden. Wichtig für 
// die Tests weil die DB im temporären Build Ordner nicht existiert und die Tests fehlschlagen würden. 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    // Erstellt die Datenbank und Tabellen, falls sie nicht existieren
    context.Database.EnsureCreated();
}

// HTTP-Request-Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Standard-Sicherheitseinstellungen
app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseAuthorization();
app.MapControllers();

app.Run();