using Application.API.DTOs;
using Application.API.Repositories.Pacientes;
using Application.API.Services;
using Domain.API.Patients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Features.Commands
{
    public class CreatePacienteCommnadHandler : IRequestHandler<CreatePacienteCommand, bool>
    {
        private readonly IPacienteService _service;
public CreatePacienteCommnadHandler(IPacienteService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreatePacienteCommand request, CancellationToken cancellationToken)
        {
            var dto = new CreatePacienteDTO
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Documento = request.Documento,
                FechaNacimiento = request.FechaNacimiento,
                Telefono = request.Telefono,
                Email = request.Email
            };

            return await _service.CreatePaciente(dto);
        }
    }
}
