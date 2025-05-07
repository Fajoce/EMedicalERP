using Application.API.DTOs;
using Application.API.Features.Commands;
using Application.API.Repositories.Pacientes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EMedicalERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region constructor and properties
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        private readonly ISender _sender;

        public PacientesController(IPacienteService pacienteService, ISender sender)
        {
            _pacienteService = pacienteService ?? throw new ArgumentNullException(nameof(pacienteService));
            _sender = sender;
        }
        #endregion constructor and properties

        #region public methods with their documentation 
        /// <summary>
        /// Autentica al paciente mediante documento y fecha de nacimiento, y devuelve un token JWT.
        /// </summary>
        /// <param name="loginDto">DTO con el documento de identidad y fecha de nacimiento del paciente.</param>
        /// <returns>Token JWT y el ID del paciente si la autenticación es exitosa.</returns>
        /// <response code="200">Autenticación exitosa. Retorna el token y el ID del paciente.</response>
        /// <response code="401">Credenciales inválidas. No autorizado.</response>
        [HttpPost("autenticar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Autenticar([FromBody] PacienteLoginDTO loginDto)
        {
            var (token, id, nombre) = await _pacienteService.AutenticarJwtAsync(loginDto);
            if (token == null)
                return Unauthorized("Credenciales inválidas");

            return Ok(new { token, pacienteId = id, Nombre = nombre });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> VermisDatos(int id)
        {
            var datos= await _pacienteService.VerMisDatos(id);
            if (datos == null)
                return Unauthorized("Credenciales inválidas");

            return Ok(datos);
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePacienteCommand paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sender.Send(paciente);

            //if (!result)
            //    return StatusCode(500, "Error al crear el paciente.");

            return Ok(new { message = "Paciente creado correctamente." });
        }
    }
    #endregion public methods with their documentation 
}

