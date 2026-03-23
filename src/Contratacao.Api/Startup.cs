using Contratacao.Api.Filters;
using Contratacao.Infra.CrossCuting.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace Contratacao.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataBaseConfiguration(Configuration);
            services.AddAutoMappingConfig();
            services.AddSwaggerConfig();
            services.AddInjecaoDependeciaConfig();

            services.AddControllers().AddNewtonsoftJson();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.Authority = "https://localhost:5001";
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = "https://localhost:5001",
                     ValidateAudience = false, // Deixe false para teste inicial
                     ValidateLifetime = true,
                     RoleClaimType = "role",
                     NameClaimType = "name"
                     
                     
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("--- ERRO FATAL NA API ---");
                         Console.WriteLine($"Mensagem: {context.Exception.Message}");
                         if (context.Exception.InnerException != null)
                             Console.WriteLine($"Inner: {context.Exception.InnerException.Message}");
                         return Task.CompletedTask;
                     }
                 };
             });

            services.AddAuthorization();

            services.AddControllers(options =>
            {
                options.Filters.Add<GenericExceptionFilterAttribute>(); // Adiciona o filtro globalmente
            });

            services.AddMvc().AddJsonOptions(opt =>
            {
                // opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            }).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowAll");

           

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
