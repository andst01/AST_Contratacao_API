using Contratacao.Infra.CrossCuting.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Infra.CrossCuting.Config
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMappingConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile),
                                   typeof(DTOToDomainMappingProfile));
        }
    }
}
