using Quesify.Web.Models.Questions.QuestionDetailModels.Requests;
using Quesify.Web.Models.Questions.QuestionDetailModels.Responses;

namespace Quesify.Web.Interfaces;

public interface IQuestionDetailClient
{
    Task<DetailForQuestionResponse> GetQuestionDetailAsync(QuestionDetailRequest request);
}
