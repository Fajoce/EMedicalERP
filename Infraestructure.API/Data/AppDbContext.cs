using Domain.API.Apointments;
using Domain.API.Doctor;
using Domain.API.Patients;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Cita> Cita { get; set; }
        public DbSet<Medico> Medico { get; set; }
    }
}
