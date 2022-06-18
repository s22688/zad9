using Microsoft.AspNetCore.Builder;
using Zad8.Middelwares;

namespace Zad8.Extentions
{
    public static class MyFatnasticMiddlewareExtentions
    {
        public static IApplicationBuilder UseMyFantasticErrorLoggingMiddleware(this IApplicationBuilder bulider)
        {
            return bulider.UseMiddleware<MyFantasticErrorLoggingMiddleware>();

        }
    }
}
