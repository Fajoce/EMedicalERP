using Domain.API.Apointments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.API.Doctor
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Column("nombre")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        [Column("especialidad")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100)]
        public string Especialidad { get; set; } = string.Empty;
        public ICollection<Cita>? Citas { get; set; }
    }
}
