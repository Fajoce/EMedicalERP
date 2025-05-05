using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.API.Patients
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        [Column("nombres")]
        [Required(ErrorMessage ="This field is required")]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;
        [Column("apellidos")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;
        [Column("documento")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(20)]
        public string Documento { get; set; } = string.Empty;
        [Column("fechanacimiento")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(20)]
        public DateTime FechaNacimiento { get; set; }
        [Column("telefono")]
        [StringLength(20)]
        public string? Telefono { get; set; }
        [Column("email")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string? Email { get; set; }
    }
}
