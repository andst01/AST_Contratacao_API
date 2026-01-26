using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Domain.Enums
{
    public enum EnumStatusApolice
    {
        

        [Description("Ativa")]
        Ativa = 0,

        [Description("Cancelada")]
        Cancelada = 1,

        [Description("Suspensa")]
        Suspensa = 2,

        [Description("Encerrada")]
        Encerrada = 3
    }
}
