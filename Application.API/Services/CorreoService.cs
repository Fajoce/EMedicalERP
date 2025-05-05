using Application.API.Repositories.Correo;
using Domain.API.Apointments;
using Domain.API.Patients;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Services
{
    public class CorreoService : ICorreoService
    {
        private readonly IConfiguration _config;

        public CorreoService(IConfiguration config)
        {
            _config = config;
        }
        public async Task EnviarConfirmacionCitaAsync(Paciente paciente, Cita cita)
        {
            if (paciente == null || cita == null || string.IsNullOrWhiteSpace(paciente.Email))
                return;

            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Clínica", _config["EmailSettings:From"]));
            mensaje.To.Add(new MailboxAddress(paciente.Nombres, paciente.Email));
            mensaje.Subject = "Confirmación de Cita Reservada";

            mensaje.Body = new TextPart("plain")
            {
                Text = $"Hola {paciente.Nombres},\n\nTu cita fue reservada para el {cita.fechahora:dddd, dd MMM yyyy HH:mm} con el Dr./Dra. {cita.Medico.Nombre}.\n\nGracias por confiar en nosotros."
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpHost"], int.Parse(_config["EmailSettings:SmtpPort"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailSettings:User"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(mensaje);
            await smtp.DisconnectAsync(true);
        }
    }
 
}
