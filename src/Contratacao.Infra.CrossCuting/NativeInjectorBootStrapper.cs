using Contratacao.Application;
using Contratacao.Application.Interfaces;
using Contratacao.Application.Interfaces.Service;
using Contratacao.Application.Service;
using Contratacao.Domain.Interfaces;
using Contratacao.Infra.Data.Contexto;
using Contratacao.Infra.Data.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratacao.Infra.CrossCuting
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repositorio

            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
            services.AddScoped<IApoliceRepoitorio, ApoliceRepositorio>();


            #endregion

            #region Aplicacao

            services.AddScoped(typeof(IAppBase<,,>), typeof(AppBase<,,>));
            services.AddScoped<IApoliceApp, ApoliceApp>();
            services.AddScoped<IApoliceService, ApoliceService>();
           
            #endregion

            services.AddScoped<ContratacaoDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        }
    }
}
