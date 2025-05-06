using Application.API.DTOs;

namespace Application.API.Repositories.Citas
{
    public interface ICitaService
    {
        Task<List<CitaDisponibleDTO>> ObtenerCitasDisponiblesAsync(string especialidad);
        Task<List<CitaDisponibleDTO>> listCitasAsync();
        Task<CitaDisponibleDTO> ObtenerCitasDisponiblesPorId(int id);
        Task<bool> ReservarCitaAsync(int citaId, int pacienteId);
        Task<List<CitasReservadas>> VerMisCitasPorPacienteID(int pacienteId);
    }
}
