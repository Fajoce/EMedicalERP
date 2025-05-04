using Application.API.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Validations
{
    public class ReservaCitaDTOValidator: AbstractValidator<ReservaCitaDTO>
    {
        public ReservaCitaDTOValidator()
        {
            RuleFor(x => x.CitaId)
                .GreaterThan(0).WithMessage("Debe seleccionar una cita válida.");

            RuleFor(x => x.PacienteId)
                .GreaterThan(0).WithMessage("Paciente inválido.");
        }
    }
}
