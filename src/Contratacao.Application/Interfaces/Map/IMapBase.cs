using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Application.Interfaces.Map
{
    public interface IMapBase<TDestination, TSource>
    {
        TDestination Map(TSource source);
    }
}
