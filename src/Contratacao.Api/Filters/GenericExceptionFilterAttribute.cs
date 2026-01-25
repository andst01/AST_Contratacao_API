using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace Contratacao.Api.Filters
{
    public class GenericExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GenericExceptionFilterAttribute> _logger;

        public GenericExceptionFilterAttribute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GenericExceptionFilterAttribute>();
        }

        public override void OnException(ExceptionContext context)
        {
            var erro1 = context.Exception.Message;
            // Registre a exceção
            _logger.LogError(context.Exception, "Ocorreu uma exceção não tratada.");

            // Defina o código de status HTTP
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Formate e retorne a resposta de erro
            var errorResponse = new
            {
                Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                // Você pode adicionar mais detalhes se desejar
                Detalhes = context.Exception.Message
            };

            context.Result = new ContentResult
            {
                Content = JsonSerializer.Serialize(errorResponse),
                ContentType = "application/json"
            };

            // Opcional: para impedir que a exceção seja tratada em outros filtros, você pode definir
            // context.ExceptionHandled = true;
            // No entanto, ao retornar um ContentResult, o framework geralmente trata isso.

            base.OnException(context); // Chame o método base se necessário
        }
    }
}
