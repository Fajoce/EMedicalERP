using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Repositories.Citas
{
    public interface ISpecification<T>
    {
     Expression<Func<T, bool>> ToExpression();
       
    }
}
