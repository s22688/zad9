using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Zad8.Middelwares
{
    public class MyFantasticErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public MyFantasticErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                using (StreamWriter sw = File.AppendText(@"log.txt")) { sw.WriteLine(ex.Message); }
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
