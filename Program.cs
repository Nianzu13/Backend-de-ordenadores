using Microsoft.EntityFrameworkCore;
using Inventario.Model;

var builder = WebApplication.CreateBuilder(args);

// --- OBLIGATORIO: Configurar el puerto para la nube ---
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Servicios básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- OBLIGATORIO: Conexión a Base de Datos ---
// Nota: Railway leerá esto de tus "Variables de Entorno" automáticamente
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// --- OBLIGATORIO: CORS para que Angular pueda conectar ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFront", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// --- OBLIGATORIO PARA PRUEBAS: Swagger fuera del IF para verlo en la nube ---
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("PermitirFront");
app.UseAuthorization();
app.MapControllers();

app.Run();