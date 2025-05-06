using Application.API.DTOs;
using Application.API.Repositories.Citas;
using Application.API.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Features.Queries
{
    public class GetObtenerCitasDisponiblesPorIdHandler : IRequestHandler<GetObtenerCitasDisponiblesPorId, CitaDisponibleDTO>
    {
        public readonly ICitaService _service;

        public GetObtenerCitasDisponiblesPorIdHandler(ICitaService service)
        {
            _service = service;
        }

        public async Task<CitaDisponibleDTO> Handle(GetObtenerCitasDisponiblesPorId request, CancellationToken cancellationToken)
        {
            return await _service.ObtenerCitasDisponiblesPorId(request.id);
        }
    }
}
