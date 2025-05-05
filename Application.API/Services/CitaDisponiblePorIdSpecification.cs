using Application.API.Repositories.Citas;
using Domain.API.Apointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Services
{
    public class CitaDisponiblePorIdSpecification : ISpecification<Cita>
    {
        private readonly int _id;

        public CitaDisponiblePorIdSpecification(int id)
        {
            _id = id;
        }

        public Expression<Func<Cita, bool>> ToExpression()
        {
            return c => c.estado == "Disponible" && c.id == _id;
        }
    }
}
