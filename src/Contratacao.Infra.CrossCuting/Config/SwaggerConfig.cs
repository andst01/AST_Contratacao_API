using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Infra.CrossCuting.Config
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
          if (services == null) throw new ArgumentNullException(nameof(services));


           services.AddSwaggerGen
           (
               s =>
               {
                   s.SwaggerDoc
                   (
                       "v1"

                       , new OpenApiInfo
                       {
                           Version = "v1",
                           Title = "Contratacao API",
                           Description = "API voltada para a gestão de Contratação",
                           Contact = new OpenApiContact
                           {

                               Email = string.Empty
                           }
                       }

                   );

               }
           );
        }

    }
}
