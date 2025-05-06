using Application.API.DTOs;
using Application.API.Features.Queries;
using Application.API.Repositories.Citas;
using Application.API.Repositories.Correo;
using Application.API.Repositories.Pacientes;
using Application.API.Services;
using Application.API.Validations;
using Domain.API.Middleware;
using EMedicalERP.API.IOD;
using FluentValidation.AspNetCore;
using Infraestructure.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("myConection");

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
// Leer configuración desde appsettings.json
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

// 🔐 Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false, // Cambia a true si vas a validar la audiencia
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ClockSkew = TimeSpan.Zero // Opcional: sin margen de error de tiempo
    };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencies();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPacienteService, PacienteService>();

builder.Services.AddScoped<ICitaService, CitaService>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PacienteLoginDTOValidator>());

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReservaCitaDTO>());

builder.Services.AddScoped<ICorreoService, CorreoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("myPolicies", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("myPolicies");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();
