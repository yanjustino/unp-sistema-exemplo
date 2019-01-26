using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Unp.Sistema.Inscricao.Api.Middlewares
{

    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }


    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var timer = new Stopwatch();

            try
            {
                //TODO: tratar a request

                timer.Start();
                await this._next(context);
                timer.Stop();

                //TODO: Logar
            }
            catch (Exception ex)
            {
                // TODO: Logar Exceção
                await this._next(context);
            }
        }
    }
}