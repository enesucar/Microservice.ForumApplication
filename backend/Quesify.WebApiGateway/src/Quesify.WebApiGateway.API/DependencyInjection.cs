using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Eureka;
using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Json;
using Quesify.SharedKernel.TimeProviders;
using Quesify.SharedKernel.Utilities.Guards;
using Serilog;
using Serilog.Events;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ConfigureHostBuilder hostBuilder)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configuration, nameof(configuration));
        Guard.Against.Null(hostBuilder, nameof(hostBuilder));

        services.AddGuid(options => { options.UseSequentialGuidGenerator(services); });

        services.AddTimeProvider(options => { options.UseUtcDateTime(services); });

        services.AddJsonSerializer(options => { options.UseNewtonsoft(services, settings => { }); });

        services
            .AddOcelot()
            .AddEureka();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Quesify.WebApiGateway.API", Version = "v1" });
            options.AddLowercaseDocumentFilter();
        });

        services.AddEnableRequestBuffering();

        services.AddRequestTime();

        hostBuilder.UseSerilog((context, services, configuration) =>
        {
            configuration
            .WriteTo.Seq(context.Configuration.GetValue<string>("Seq:ServerUrl")!)
            .WriteTo.Console()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
            .AddCustomEnriches(services);
        });

        services.AddPushSerilogProperties();

        services.AddCustomHttpLogging(o => { });

        return services;
    }
}