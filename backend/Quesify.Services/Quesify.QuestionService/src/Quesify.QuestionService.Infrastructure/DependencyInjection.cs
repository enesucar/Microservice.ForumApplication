using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quesify.QuestionService.Core.Interfaces;
using Quesify.QuestionService.Infrastructure.Data.Contexts;
using Quesify.SharedKernel.EventBus;
using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Json;
using Quesify.SharedKernel.TimeProviders;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(connectionString, nameof(connectionString));
        
        services.AddGuid(options => { options.UseSequentialGuidGenerator(services); });

        services.AddTimeProvider(options => { options.UseUtcDateTime(services); });

        services.AddJsonSerializer(options => { options.UseNewtonsoft(services, settings => { }); });

        services.AddDbContext<QuestionContext>(options => 
            { options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); });

        services.AddScoped<IQuestionContext>(provider => provider.GetRequiredService<QuestionContext>());

        services.AddSecurity(options =>  { });

        services.AddEventBus(options =>
        {
            options.ProjectName = "Quesify";
            options.ServiceName = "QuestionService";
            options.UseKafka(services, kafkaOptions =>
            {
                kafkaOptions.ProducerConfig.BootstrapServers = "localhost:9092"; //TODO: Read from appsettings.json
                kafkaOptions.ConsumerConfig.BootstrapServers = "localhost:9092";
                kafkaOptions.ConsumerConfig.GroupId = "Quesify.QuestionService.Group";
                kafkaOptions.ConsumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
                kafkaOptions.ConsumerConfig.ClientId = "Quesify.QuestionService";
                kafkaOptions.ConsumerConfig.AllowAutoCreateTopics = true;
                kafkaOptions.AdminClientConfig.AllowAutoCreateTopics = true;
                kafkaOptions.AdminClientConfig.BootstrapServers = "localhost:9092";
            });
        });

        return services;
    }
}
