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
                options =>
                {
                    options.SwaggerDoc
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

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Digite: Bearer {seu token}"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                     });

                }
            );
        }

    }
}
