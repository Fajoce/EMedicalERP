using Application.API.DTOs;
using Application.API.Features.Queries;
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

            return services;
        }
    }
}
