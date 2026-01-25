using Contratacao.Api.Filters;
using Contratacao.Infra.CrossCuting.Config;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Contratacao.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataBaseConfiguration(Configuration);
            services.AddAutoMappingConfig();
            services.AddSwaggerConfig();
            services.AddInjecaoDependeciaConfig();

            services.AddControllers().AddNewtonsoftJson();

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
