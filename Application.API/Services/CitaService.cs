using Application.API.DTOs;
using Application.API.Extension;
using Application.API.Repositories.Citas;
using Application.API.Repositories.Correo;
using Domain.API.Exceptions;
using Infraestructure.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.API.Services
{
    public class CitaService : ICitaService
    {
        private readonly AppDbContext _context;
        private readonly ICorreoService _correoService;
        

        public CitaService(AppDbContext context, ICorreoService correoService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
            _correoService = correoService ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<CitaDisponibleDTO>> listCitasAsync()
        {
            var lst = from c in _context.Cita join m in _context.Medico 
                      on c.MedicoId equals m.Id
                      select new CitaDisponibleDTO
                      {
                          id = c.id,
                          nombremedico = c.Medico.Nombre,
                          especialidad = c.especialidad,
                          fechahora = c.fechahora,
                          estado = c.estado,
                          MedicoId = c.Medico.Id                          
                      };

            return await lst.ToListAsync();
        }

        public async Task<List<CitaDisponibleDTO>> ObtenerCitasDisponiblesAsync(string especialidad)
        {
            var citas = (from c in _context.Cita
                         where c.estado == "Disponible" && c.especialidad == especialidad
                         select new CitaDisponibleDTO
                         {
                             id = c.id,
                             MedicoId = c.Medico.Id,
                             nombremedico = c.Medico.Nombre,
                             especialidad = c.especialidad,
                             fechahora = c.fechahora,
                             estado = c.estado
                         })
           .OrderBy(c => c.fechahora)
           .Take(5)
           .ToListAsync();

            return await citas;
        }
       
        public async Task<List<CitaDisponibleDTO>> ObtenerCitasDisponiblesPorId(int id)
        {
            var spec = new CitaDisponiblePorIdSpecification(id);
            var citas = (from c in _context.Cita
                         .ApplySpecification(spec)
                         select new CitaDisponibleDTO
                         {
                             id = c.id,
                             MedicoId = c.Medico.Id,
                             nombremedico = c.Medico.Nombre,
                             especialidad = c.especialidad,
                             fechahora = c.fechahora,
                             estado = c.estado,
                             PacienteId = c.PacienteId

                         })
           .OrderBy(c => c.fechahora)
           .ToListAsync();
            if (citas is null)
            {
                throw new BusinessRuleException("Cita ya esta reservada");
            }
            else
            {
                return await citas;
            }

                
        }

        public async Task<bool> ReservarCitaAsync(int citaId, int pacienteId)
        {
            var cita = await _context.Cita.FirstOrDefaultAsync(c => c.id == citaId);

            if (cita == null )
                throw new NotFoundException("La cita no existe.");
            if (cita.estado != "Disponible")
                throw new BusinessRuleException("La cita ya fue reservada");

            cita.estado = "Reservada";
            cita.PacienteId = pacienteId;

            await _context.SaveChangesAsync();
            // 📨 Envío de correo en segundo plano
            // Enviar correo de forma asíncrona sin bloquear la lógica
            _ = Task.Run(async () =>
            {
                var paciente = await _context.Paciente.FindAsync(pacienteId);
                await _correoService.EnviarConfirmacionCitaAsync(paciente, cita);
            });
            return true;
        }
    }
}
