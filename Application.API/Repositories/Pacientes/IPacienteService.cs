using Application.API.DTOs;

namespace Application.API.Repositories.Pacientes
{
    public interface IPacienteService
    {
        Task<(string Token, int PacienteId, string PacienteNombre)> AutenticarJwtAsync(PacienteLoginDTO loginDto);
        Task <PacienteDTO> VerMisDatos(int id);
        Task<bool> CreatePaciente(CreatePacienteDTO paciente);
    }
}
