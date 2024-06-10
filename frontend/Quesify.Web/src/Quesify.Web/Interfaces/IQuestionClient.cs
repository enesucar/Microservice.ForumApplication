using Quesify.Web.Models.Questions.CreateQuestionModels.Requests;
using Quesify.Web.Models.Questions.CreateQuestionModels.Responses;
using Quesify.Web.Models.Questions.CreateVoteForQuestionModels.Requests;
using Quesify.Web.Models.Questions.CreateVoteForQuestionModels.Responses;

namespace Quesify.Web.Interfaces;

public interface IQuestionClient
{
    Task<CreateQuestionResponse> CreateQuestionAsync(CreateQuestionRequest request);

    Task<CreateVoteForQuestionResponse> CreateVoteAsync(CreateVoteForQuestionRequest request, Guid questionId);
}
