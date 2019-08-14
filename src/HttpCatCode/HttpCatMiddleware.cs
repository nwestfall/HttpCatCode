using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HttpCatCode
{
    internal class HttpCatMiddleware
    {
        readonly RequestDelegate _next;

        readonly bool _returnImageIfNotOK = false;

        readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://http.cat")
        };

        public HttpCatMiddleware(RequestDelegate next, bool returnImageIfNotOK)
        {
            _next = next;
            _returnImageIfNotOK = returnImageIfNotOK;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.OnStarting((state) =>
            {
                httpContext.Response.Headers.Add("x-cat-status", $"https://http.cat/{httpContext.Response.StatusCode}");
                
                return Task.FromResult(0);
            }, null);

            await _next.Invoke(httpContext);

            int statusCode = httpContext.Response.StatusCode;
            if(_returnImageIfNotOK && (statusCode < 200 || statusCode >= 300))
            {
                await httpContext.Response.WriteAsync($"<html><body><img src=\"https://http.cat/{statusCode}\" /></body></html>");
            }
        }
    }

    public static class HttpCatExtensions
    {
        public static IApplicationBuilder UseHttpCat(this IApplicationBuilder builder, bool returnImageIfNotOK = false)
        {
            return builder.UseMiddleware<HttpCatMiddleware>(returnImageIfNotOK);
        }
    }
}
