using Quesify.Web.Models.Answers.CreateAnswerModels.Requests;
using Quesify.Web.Models.Answers.CreateAnswerModels.Responses;
using Quesify.Web.Models.Answers.CreateVoteForAnswerModels.Requests;
using Quesify.Web.Models.Answers.CreateVoteForAnswerModels.Responses;

namespace Quesify.Web.Interfaces;

public interface IAnswerClient
{
    Task<CreateAnswerResponse> CreateAnswerAsync(CreateAnswerRequest request);

    Task<CreateVoteForAnswerResponse> CreateVoteAsync(CreateVoteForAnswerRequest request, Guid answerId);
}
