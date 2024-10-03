using API_REST_ADMIN_NOTAS.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Verificar si la aplicación está corriendo en un contenedor Docker
bool isDocker = (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true");

if (isDocker)
{
    builder.Services.AddDbContext<AdminNotasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionLocalDocker")));
}
else
{
    builder.Services.AddDbContext<AdminNotasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionLocal")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
