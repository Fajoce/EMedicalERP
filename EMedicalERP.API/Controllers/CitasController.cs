using Application.API.DTOs;
using Application.API.Features.Queries;
using Application.API.Repositories.Citas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMedicalERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region constructor and properties
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;
        private readonly ISender _sender;

        public CitasController(ICitaService citaService, ISender sender)
        {
            _citaService = citaService ?? throw new ArgumentNullException(nameof(citaService)); 
            _sender = sender ?? throw new ArgumentNullException(nameof(citaService));
        }
        #endregion constructor and properties

        #region public methods with their documentation
        /// <summary>
        /// Obtiene las 5 citas más cercanas disponibles para una especialidad específica.
        /// </summary>
        /// <param name="especialidad">Especialidad médica (Ej: Medicina general, Examen odontológico).</param>
        /// <returns>Lista de citas disponibles.</returns>
        /// <response code="200">Devuelve la lista de citas disponibles.</response>
        /// <response code="400">Si no se especifica la especialidad.</response>
        [HttpGet("disponibles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerDisponibles([FromQuery] string especialidad)
        {
            if (string.IsNullOrEmpty(especialidad))
                return BadRequest("Debe especificar una especialidad");

            var citas = await _citaService.ObtenerCitasDisponiblesAsync(especialidad);
            return Ok(citas);
        }

        /// <summary>
        /// Obtiene todas las citas registradas en el sistema.
        /// </summary>
        /// <returns>Lista completa de citas.</returns>
        /// <response code="200">Devuelve la lista de todas las citas.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListaCitas()
        {

            var citas = await _citaService.listCitasAsync();
            return Ok(citas);
        }
        /// <summary>
        /// Obtiene las  más cercanas disponibles por un id específico.
        /// </summary>
        /// <param name="id">Especialidad médica (Ej: 1,2,3,....).</param>
        /// <returns>Lista de citas disponibles.</returns>
        /// <response code="200">Devuelve la lista de citas disponibles.</response>
        /// <response code="400">Si no se especifica la especialidad.</response>

        [HttpGet("BuscarCitaDisponiblesPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerDisponiblesById([FromQuery] int id)
        {
            if (id == 0)
                return BadRequest("Debe especificar un id");
            var response = await _sender.Send(new GetObtenerCitasDisponiblesPorIdQuery(id));
            return Ok(response);
        }
        /// <summary>
        /// Reserva una cita para un paciente.
        /// </summary>
        /// <param name="dto">Datos de la cita y el paciente.</param>
        /// <returns>Confirmación de reserva.</returns>
        /// <response code="200">Cita reservada exitosamente.</response>
        /// <response code="400">La cita ya ha sido reservada o no existe.</response>
        [HttpPost("reservar")]
        public async Task<IActionResult> Reservar([FromBody] ReservaCitaDTO dto)
        {
            var resultado = await _citaService.ReservarCitaAsync(dto.CitaId, dto.PacienteId);

            if (!resultado)
                return BadRequest("La cita ya ha sido reservada o no existe.");

            return Ok("Cita reservada correctamente.");
        }

        [HttpGet("MisCitas/{pacienteId}")]
        public async Task<IActionResult> VerMisCitasPorPacienteId(int pacienteId)
        {
            var resultado = await _citaService.VerMisCitasPorPacienteID(pacienteId);

            if (resultado is null)
                return BadRequest("No ha tenido citas aun");

            return Ok(resultado);
        }
    }
    #endregion public methods with their documentation
}

