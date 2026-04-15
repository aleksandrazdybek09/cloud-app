using Azure.Identity;
using Backend.Data;
using Backend.Repositories;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Obsługa Key Vault - dodajemy logikę bezpieczeństwa
var keyVaultUri = builder.Configuration["KeyVaultUri"];
if (!string.IsNullOrEmpty(keyVaultUri) && keyVaultUri != "YOUR_KEY_VAULT_URI")
{
    try
    {
        builder.Configuration.AddAzureKeyVault(
            new Uri(keyVaultUri),
            new DefaultAzureCredential());
    }
    catch (Exception ex)
    {
        // Jeśli Key Vault zawiedzie, logujemy to, ale pozwalamy aplikacji próbować dalej
        Console.WriteLine($"Key Vault Error: {ex.Message}");
    }
}

// 2. Konfiguracja Bazy Danych
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DbConnectionString")
                           ?? builder.Configuration["DbConnectionString"];

    // Jeśli używasz SQL Server
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()); // Automatyczne ponowienie przy chwilowych błędach Azure
});

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// 3. Swagger ZAWSZE aktywny (usuwamy warunek IsDevelopment dla testów w Azure)
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // To sprawi, że Swagger otworzy się na głównej stronie!
});

app.UseCors();
app.UseAuthorization();
app.MapControllers();

// 4. Bezpieczna inicjalizacja bazy danych
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // EnsureCreated tworzy bazę, jeśli jej nie ma.
        // W chmurze może to zająć chwilę lub rzucić błąd połączenia.
        db.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        // Wyświetli błąd w Log Stream, ale nie wyłączy całej aplikacji
        Console.WriteLine($"Database Initialization Error: {ex.Message}");
    }
}

app.Run();