using Application.API.DTOs;
using Application.API.Repositories.Citas;
using Application.API.Repositories.Pacientes;
using Domain.API.Exceptions;
using Domain.API.Patients;
using Infraestructure.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.API.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public PacienteService(AppDbContext context, IConfiguration config)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
            _config = config ?? throw new ArgumentNullException(nameof(config)); ;
        }
        public async Task<(string Token, int PacienteId, string PacienteNombre)> AutenticarJwtAsync(PacienteLoginDTO loginDto)
        {
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p =>
               p.Documento == loginDto.Documento &&
               p.FechaNacimiento.Date == loginDto.FechaNacimiento.Date);

            if (paciente == null)
                throw new NotFoundException("No existe este id");

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, paciente.Id.ToString()),
        new Claim(ClaimTypes.Name, paciente.Nombres)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), paciente.Id, paciente.Nombres+ ' ' + paciente.Apellidos);
        }

        public async Task<bool> CreatePaciente(CreatePacienteDTO paciente)
        {
            try
            {
                var pac = new Paciente
                {
                    Nombres = paciente.Nombres,
                    Apellidos = paciente.Apellidos,
                    Documento = paciente.Documento,
                    FechaNacimiento = paciente.FechaNacimiento,
                    Telefono = paciente.Telefono,
                    Email = paciente.Email
                };

                _context.Paciente.Add(pac);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Puedes loguear el error aquí
                return false;
            }
        }
        

        public async Task<PacienteDTO> VerMisDatos(int id)
        {
            var datos = from p in _context.Paciente
                        where p.Id == id
                                            select new PacienteDTO
                                            {
                                                Id = p.Id,
                                                Nombres = p.Nombres,
                                                Documento = p.Documento,
                                                FechaNacimiento = p.FechaNacimiento,
                                                Apellidos = p.Apellidos,
                                                Telefono = p.Telefono,
                                                Email = p.Email
                                            };
            return await datos.FirstOrDefaultAsync();
        }
    }
}
  
