﻿using Microsoft.Extensions.DependencyInjection;
using Quesify.SharedKernel.Serilog.Enrichers;
using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Serilog;

public static class LoggerConfigurationExtensions
{
    public static LoggerConfiguration AddCustomEnriches(
        this LoggerConfiguration loggerConfiguration,
        IServiceProvider serviceProvider)
    {
        var dateTime = serviceProvider.GetRequiredService<IDateTime>();

        return loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.With(new BaseLogEventEnricher("Host"))
            .Enrich.With(new ApplicationNameEnricher())
            .Enrich.With(new BaseLogEventEnricher("UserId"))
            .Enrich.With(new BaseLogEventEnricher("ClientIPAddress"))
            .Enrich.With(new BaseLogEventEnricher("ClientName"))
            .Enrich.With(new BaseLogEventEnricher("CorrelationId"))
            .Enrich.With(new BaseLogEventEnricher("RequestQueryString"))
            .Enrich.With(new BaseLogEventEnricher("RequestMethod"))
            //.Enrich.With(new BaseLogEventEnricher("AcceptLanguage"))
            //.Enrich.With(new BaseLogEventEnricher("UserAgent"))
            //.Enrich.With(new BaseLogEventEnricher("TimeZoneInfo"))
            .Enrich.With(new BaseLogEventEnricher("RequestBody"))
            .Enrich.With(new BaseLogEventEnricher("StatusCode"))
            .Enrich.With(new EnvironmentEnricher())
            .Enrich.With(new ExecutionDurationEnricher(dateTime))
            .Enrich.With(new BaseLogEventEnricher("RequestExecutionTime"))
            .Enrich.With(new BaseLogEventEnricher("SourceMemberName"))
            .Enrich.With(new BaseLogEventEnricher("SourceFilePath"))
            .Enrich.With(new BaseLogEventEnricher("SourceLineNumber"))
            .Enrich.With(new BaseLogEventEnricher("Version"))
            .Enrich.With(new RemovePropertiesEnricher());
    }
}
