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
    options.UseSqlite("Data Source=minerals.db"));

var app = builder.Build();

// HTTP-Request-Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Standard-Sicherheitseinstellungen
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();