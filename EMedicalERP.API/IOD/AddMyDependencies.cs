using Application.API.DTOs;
using Application.API.Features.Queries;
using Application.API.Repositories.Citas;
using Application.API.Repositories.Correo;
using Application.API.Repositories.Pacientes;
using Application.API.Services;
using Application.API.Validations;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System.Reflection;

namespace EMedicalERP.API.IOD
{
    public static class AddMyDependencies
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped<IPacienteService, PacienteService>();

            services.AddScoped<ICitaService, CitaService>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PacienteLoginDTOValidator>());

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReservaCitaDTO>());

            services.AddScoped<ICorreoService, CorreoService>();
            return services;
        }
    }
}
