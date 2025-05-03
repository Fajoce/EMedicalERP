using Application.API.DTOs;
using Application.API.Repositories.Pacientes;
using Microsoft.AspNetCore.Mvc;

namespace EMedicalERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region constructor and properties
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService ?? throw new ArgumentNullException(nameof(pacienteService)); ;
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
            var (token, id) = await _pacienteService.AutenticarJwtAsync(loginDto);
            if (token == null)
                return Unauthorized("Credenciales inválidas");

            return Ok(new { token, pacienteId = id });
        }
    }
    #endregion public methods with their documentation 
}

