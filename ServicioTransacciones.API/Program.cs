using Microsoft.EntityFrameworkCore;
using ServicioTransacciones.API.Datos;
using ServicioTransacciones.API.Repositorios;
using ServicioTransacciones.API.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITransaccionRepositorio, TransaccionRepositorio>();
builder.Services.AddScoped<ITransaccionServicio, TransaccionServicio>();

builder.Services.AddDbContext<AplicacionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//agrego el host para el servicio de productos
builder.Services.AddHttpClient("Productos", client =>
{
    client.BaseAddress = new Uri("http://localhost:16937"); // Cambia el puerto
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
