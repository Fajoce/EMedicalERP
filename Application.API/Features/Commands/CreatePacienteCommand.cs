using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Features.Commands
{
    public class CreatePacienteCommand: IRequest<bool>
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
