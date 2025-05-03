using Application.API.DTOs;

namespace Application.API.Repositories.Pacientes
{
    public interface IPacienteService
    {
        Task<(string Token, int PacienteId)> AutenticarJwtAsync(PacienteLoginDTO loginDto);
    }
}
