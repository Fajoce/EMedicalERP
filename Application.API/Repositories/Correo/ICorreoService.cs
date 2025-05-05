using Domain.API.Apointments;
using Domain.API.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Repositories.Correo
{
    public interface ICorreoService
    {
        Task EnviarConfirmacionCitaAsync(Paciente paciente, Cita cita);
    }
}
