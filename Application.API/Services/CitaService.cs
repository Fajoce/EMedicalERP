using Application.API.DTOs;
using Application.API.Repositories.Citas;
using Infraestructure.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.API.Services
{
    public class CitaService : ICitaService
    {
        private readonly AppDbContext _context;

        public CitaService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
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
                             // id = c.id,
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

        public async Task<bool> ReservarCitaAsync(int citaId, int pacienteId)
        {
            var cita = await _context.Cita.FirstOrDefaultAsync(c => c.id == citaId);

            if (cita == null || cita.estado != "Disponible")
                return false;

            cita.estado = "Reservada";
            cita.PacienteId = pacienteId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
