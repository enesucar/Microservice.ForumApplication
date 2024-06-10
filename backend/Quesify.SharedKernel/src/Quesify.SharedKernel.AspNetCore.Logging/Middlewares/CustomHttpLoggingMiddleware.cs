﻿using Microsoft.AspNetCore.Diagnostics;
using Quesify.SharedKernel.AspNetCore.Logging.Attributes;
using Quesify.SharedKernel.AspNetCore.Logging.Models;
using Serilog.Context;

namespace Quesify.SharedKernel.AspNetCore.Logging.Middlewares;

public class CustomHttpLoggingMiddleware : IMiddleware
{
    private readonly ILogger<CustomHttpLoggingMiddleware> _logger;
    private readonly LoggingOptions _loggingOptions;

    public CustomHttpLoggingMiddleware(
        ILogger<CustomHttpLoggingMiddleware> logger,
        LoggingOptions loggingOptions)
    {
        _logger = logger;
        _loggingOptions = loggingOptions;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stream originalBody = context.Response.Body;

        try
        {
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await next(context);

                memoryStream.Position = 0;
                string responseBody = new StreamReader(memoryStream).ReadToEnd();

                await InvokeInternalAsync(context, responseBody);

                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBody);
            }
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    private async Task InvokeInternalAsync(HttpContext context, string responseBody)
    {
        var loggingAttribute = context.GetEndpoint()?.Metadata.GetMetadata<LoggingAttribute>();
        var options = GetLoggingOptions(loggingAttribute);

        if (context.Response.StatusCode < (int)options.LoggingLevel)
        {
            return;
        }

        if (!options.IgnoreRequestHeader) LogContext.PushProperty("RequestHeader", context.Request.GetHeaders());
        if (!options.IgnoreRequestBody) LogContext.PushProperty("RequestBody", await context.Request.GetBodyAsync());
        if (!options.IgnoreResponseHeader) LogContext.PushProperty("ResponseHeader", context.Response.GetHeaders());
        if (!options.IgnoreResponseBody) LogContext.PushProperty("ResponseBody", responseBody);

        LogContext.PushProperty("StatusCode", context.Response.StatusCode);

        var logMessage = "HTTP {RequestMethod} - {RequestPath} responded {StatusCode} in {ExecutionDuration}ms ({ApplicationName})";

        if (context.Response.StatusCode >= 400)
        {
            var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            _logger.Here().LogError(error, logMessage);
        }
        else
        {
            _logger.Here().LogInformation(logMessage);
        }
    }

    private LoggingOptions GetLoggingOptions(LoggingAttribute? loggingAttribute)
    {
        if (loggingAttribute == null)
        {
            return _loggingOptions;
        }

        return new LoggingOptions()
        {
            LoggingLevel = loggingAttribute.LoggingLevel ?? _loggingOptions.LoggingLevel,
            IgnoreRequestHeader = loggingAttribute.IgnoreRequestHeader ?? _loggingOptions.IgnoreRequestHeader,
            IgnoreRequestBody = loggingAttribute.IgnoreRequestBody ?? _loggingOptions.IgnoreRequestBody,
            IgnoreResponseHeader = loggingAttribute.IgnoreResponseHeader ?? _loggingOptions.IgnoreResponseHeader,
            IgnoreResponseBody = loggingAttribute.IgnoreResponseBody ?? _loggingOptions.IgnoreResponseBody
        };
    }
}
