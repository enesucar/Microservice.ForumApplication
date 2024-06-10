using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Aggregates.Votes;
using Quesify.QuestionService.Core.Interfaces;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));

        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IVoteService, VoteService>();

        return services;
    }
}
