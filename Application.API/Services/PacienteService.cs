using Application.API.DTOs;
using Application.API.Repositories.Citas;
using Application.API.Repositories.Pacientes;
using Infraestructure.API.Data;
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
        public async Task<(string Token, int PacienteId)> AutenticarJwtAsync(PacienteLoginDTO loginDto)
        {
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p =>
               p.Documento == loginDto.Documento &&
               p.FechaNacimiento.Date == loginDto.FechaNacimiento.Date);

            if (paciente == null)
                return (null, 0);

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

            return (new JwtSecurityTokenHandler().WriteToken(token), paciente.Id);
        }
    }
}
  
