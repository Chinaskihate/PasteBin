using Microsoft.AspNetCore.Builder;
using PasteBin.Http.Middlewares;

namespace PasteBin.Http.Extensions;
public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
        builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}
