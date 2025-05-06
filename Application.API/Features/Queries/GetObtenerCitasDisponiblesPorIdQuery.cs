using Application.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Features.Queries
{
    public record GetObtenerCitasDisponiblesPorIdQuery(int id): IRequest<CitaDisponibleDTO>
    {
    }
}
