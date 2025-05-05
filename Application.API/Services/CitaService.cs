using Application.API.DTOs;
using Application.API.Repositories.Citas;
using Domain.API.Exceptions;
using Infraestructure.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            var citas = (from c in _context.Cita
                         where c.estado == "Disponible" && c.id == id
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
            _ = Task.Run(async () =>
            {
                try
                {
                    var paciente = await _context.Paciente.FindAsync(pacienteId);
                    if (paciente == null || string.IsNullOrWhiteSpace(paciente.Email))
                        return;

                    var mensaje = new MimeKit.MimeMessage();
                    mensaje.From.Add(new MimeKit.MailboxAddress("Clínica", "fajoce@gmail.com"));
                    mensaje.To.Add(new MimeKit.MailboxAddress(paciente.Nombres, paciente.Email));
                    mensaje.Subject = "Confirmación de Cita Reservada";

                    mensaje.Body = new MimeKit.TextPart("plain")
                    {
                        Text = $"Hola {paciente.Nombres},\n\nTu cita ha sido reservada para el {cita.fechahora.ToString("f")} con el Dr./Dra. {cita.Medico.Nombre}.\n\n¡Gracias por confiar en nosotros!"
                    };

                    using var smtp = new MailKit.Net.Smtp.SmtpClient();
                    await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync("fajoce@gmail.com", "N&ck13_2018");
                    await smtp.SendAsync(mensaje);
                    await smtp.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar correo de confirmación: {ex.Message}");
                    // Aquí podrías loguear formalmente si usas Serilog, NLog, etc.
                }
            });
            return true;
        }
    }
}
