﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using PasteBin.Common;
using PasteBin.Common.Exceptions;
using PasteBin.Contracts.Exceptions;
using PasteBin.Serialization.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PasteBin.Http.Middlewares;
internal class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception, logger);
        }
    }

    private Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        var code = HttpStatusCode.InternalServerError;
        switch (exception)
        {
            case ValidationException:
                code = HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case NoAvailableUrlException:
                code = HttpStatusCode.ServiceUnavailable;
                break;
        }

        context.Response.ContentType = "Application/json";
        context.Response.StatusCode = (int)code;

        logger.LogError("URL: {Method} {DiplayUrl}{Message}{StackTrace}",
            context.Request.Method,
            context.Request.GetDisplayUrl(),
            $"{Environment.NewLine}{exception.Message}",
            $"{Environment.NewLine}{exception.StackTrace}");

        return context.Response.WriteAsync(new ResponseDto()
        {
            IsSuccess = false,
            Message = exception.Message,
        }.ToJson());
    }
}
