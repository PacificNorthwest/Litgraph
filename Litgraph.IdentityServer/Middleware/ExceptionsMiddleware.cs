using System;
using System.Net;
using System.Threading.Tasks;
using Litgraph.IdentityServer.Model.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Litgraph.IdentityServer.Middleware
{
    class ExceptionsMiddleware
    {
        private RequestDelegate _next;

        public ExceptionsMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try { await this._next(context); }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (ex is ServerException exception)
                {
                    await context.Response.WriteAsync(exception.Message);
                }
            }

            return;
        }
    }
}