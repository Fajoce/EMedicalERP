using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.DTOs
{
    public class CitasReservadas
    {
        public int id { get; set; }
        public DateTime fechahora { get; set; }
        public string nombremedico { get; set; }
        public string especialidad { get; set; }

        public string estado { get; set; }
        public int MedicoId { get; set; }

        public int? PacienteId { get; set; }
        public string Patologia { get; set; }
        public string Tratamiento { get; set; }
    }
}
