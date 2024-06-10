using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Json;
using Quesify.SharedKernel.TimeProviders;
using Quesify.SharedKernel.Utilities.Guards;
using Quesify.Web.Clients;
using Quesify.Web.Interfaces;
using Quesify.Web.Services;
using System.Globalization;
using System.Reflection;
using System.Text;

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

        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

        services.AddControllersWithViews();

        services.AddFluentValidationAutoValidation(o => o.DisableDataAnnotationsValidation = true);
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddGuid(options => { options.UseSequentialGuidGenerator(services); });

        services.AddTimeProvider(options => { options.UseUtcDateTime(services); });

        services.AddJsonSerializer(options =>
        {
            options.UseNewtonsoft(services, settings =>
            {
                settings.Formatting = Formatting.Indented;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        });

        services.AddSecurity(o => { });

        services.AddHttpContextAccessor();
        services.AddHttpContextCurrentPrincipalAccessor();

        var backendBaseUrl = configuration.GetValue<string>("BackendOptions:BaseUrl")!;
        var identityServiceUrl = configuration.GetValue<string>("BackendOptions:IdentityServiceUrl")!;
        var searchServiceUrl = configuration.GetValue<string>("BackendOptions:SearchServiceUrl")!;
        var questionDetailServiceUrl = configuration.GetValue<string>("BackendOptions:QuestionDetailServiceUrl")!;
        var answerServiceUrl = configuration.GetValue<string>("BackendOptions:AnswerServiceUrl")!;
        var questionServiceUrl = configuration.GetValue<string>("BackendOptions:QuestionServiceUrl")!;
        var mediaServiceUrl = configuration.GetValue<string>("BackendOptions:MediaServiceUrl")!;

        services.AddScoped<IUserClient, UserClient>();
        services.AddScoped<ISearchClient, SearchClient>();
        services.AddScoped<IQuestionDetailClient, QuestionDetailClient>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMediaClient, MediaClient>();
        services.AddScoped<IUserService, UserService>();

        services.AddHttpClient<IUserClient, UserClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{identityServiceUrl}");
        });

        services.AddHttpClient<ISearchClient, SearchClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{searchServiceUrl}");
        });

        services.AddHttpClient<IQuestionDetailClient, QuestionDetailClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{questionDetailServiceUrl}");
        });

        services.AddHttpClient<IAnswerClient, AnswerClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{answerServiceUrl}");
        });

        services.AddHttpClient<IQuestionClient, QuestionClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{questionServiceUrl}");
        });

        services.AddHttpClient<IMediaClient, MediaClient>(options =>
        {
            options.BaseAddress = new Uri($"{backendBaseUrl}{mediaServiceUrl}");
        });

        services.AddAuthorization();
            
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/login");
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.Redirect("/");
                        return Task.CompletedTask;
                    },
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration.GetValue<string>("AccessTokenOptions:Issuer")!,
                    ValidAudience = configuration.GetValue<string>("AccessTokenOptions:Audience")!,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AccessTokenOptions:SecurityKey")!)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        return services;
    }
}
