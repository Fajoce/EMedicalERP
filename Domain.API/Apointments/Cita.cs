using Domain.API.Doctor;
using Domain.API.Patients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.API.Apointments
{
    public class Cita
    {
        [Key]
        public int id { get; set; }
        [Column("especialidad")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100)]
        public string especialidad { get; set; } = string.Empty;
        [Column("fechahora")]
        [Required(ErrorMessage = "This field is required")]
        public DateTime fechahora { get; set; }
        [Column("estado")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string estado { get; set; } = string.Empty;
        [Column("MedicoId")]
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        [Column("PacienteId")]
        [ForeignKey("PacienteId")]
        
        public int? PacienteId { get; set; } 
        public Paciente Paciente { get; set; }
        [Column("noConsultorio")]
        [StringLength(3)]
        public string Noconsultorio { get; set; }
    }
}
