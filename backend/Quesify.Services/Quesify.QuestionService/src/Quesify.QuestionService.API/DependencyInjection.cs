using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Quesify.SharedKernel.AspNetCore.Swagger.Filters;
using Quesify.SharedKernel.Utilities.Guards;
using Serilog;
using Serilog.Events;
using Steeltoe.Discovery.Client;
using System.Reflection;
using System.Text.Json.Serialization;

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

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                var enumConverter = new JsonStringEnumConverter();
                options.JsonSerializerOptions.Converters.Add(enumConverter);
            })
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        services.AddFluentValidationAutoValidation(options => { options.DisableDataAnnotationsValidation = true; });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Quesify.QuestionService.API", Version = "v1" });
            options.AddSecurity();
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

        services.AddCustomAuthentication(
            configuration.GetValue<string>("AccessTokenOptions:Audience")!,
            configuration.GetValue<string>("AccessTokenOptions:Issuer")!,
            configuration.GetValue<string>("AccessTokenOptions:SecurityKey")!);

        services.AddHttpContextAccessor();
        services.AddHttpContextCurrentPrincipalAccessor();

        services.AddCustomExceptionHandler();

        services.AddDiscoveryClient(configuration);

        return services;
    }
}
