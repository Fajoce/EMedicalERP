using Application.API.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Validations
{
    public class PacienteLoginDTOValidator : AbstractValidator<PacienteLoginDTO>
    {
        public PacienteLoginDTOValidator()
        {
            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("El documento es obligatorio.")
                .Length(6, 20).WithMessage("El documento debe tener entre 6 y 20 caracteres.");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThan(DateTime.Today).WithMessage("La fecha debe ser menor a hoy.");
        }
    }
}
