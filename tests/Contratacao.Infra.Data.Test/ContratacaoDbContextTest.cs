using Contratacao.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Infra.Data.Test
{
    public class ContratacaoDbContextTest
    {
        public static ContratacaoDbContext Context { get; private set; }


        public static ContratacaoDbContext CreateContext()
        {


            var options = new DbContextOptionsBuilder<ContratacaoDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .EnableSensitiveDataLogging()
                                .Options;

            return new ContratacaoDbContext(options);
        }


    }
}
