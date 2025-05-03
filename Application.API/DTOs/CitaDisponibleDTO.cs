using System.Text.Json.Serialization;

namespace Application.API.DTOs
{
    public class CitaDisponibleDTO
    {
        [JsonIgnore]
        public int id { get; set; }
        public DateTime fechahora { get; set; }
        public string nombremedico { get; set; }
        public string especialidad { get; set; }

        public string estado { get; set; }
        public int MedicoId { get; set; }
        
        [JsonIgnore]
        public int? PacienteId { get; set; }
      
    }
}
