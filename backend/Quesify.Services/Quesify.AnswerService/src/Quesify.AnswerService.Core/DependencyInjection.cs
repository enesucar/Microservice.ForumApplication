using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Aggregates.Votes;
using Quesify.AnswerService.Core.Interfaces;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IVoteService, VoteService>();

        return services;
    }
}
