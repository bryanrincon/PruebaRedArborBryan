using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PruebaRedArborBryan.Application.CommandHandler;
using PruebaRedArborBryan.Infrastructure.Interfaces;
using PruebaRedArborBryan.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using PruebaRedArborBryan.Application.QueryHandler;
using PruebaRedArborBryan.Infrastructure;
using Duende.IdentityServer.Validation;
using PruebaRedArborBryan.Application;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Añadir MediatR al contenedor de servicios
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateEmployeeCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteEmployeeCommandHandler).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEmployeesQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetEmployeeByIdQueryHandler).Assembly));

// Lee la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultReadConnection");

// Configurar el contexto de la base de datos para Entity Framework (si lo estás usando)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultWriteConnection")));



// Inyección de dependencias para los repositorios
builder.Services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>(provider => new EmployeeReadRepository(connectionString));
builder.Services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
builder.Services.AddScoped<IResourceOwnerPasswordValidator, EmployeeResourceOwnerPasswordValidator>();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(new List<ApiScope> {
        new ApiScope("api1", "My API")
    })
    .AddInMemoryClients(new List<Client> {
        new Client {
            ClientId = "client_id",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("client_secret".Sha256()) },
            AllowedScopes = { "api1" },
        }
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.UseIdentityServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
