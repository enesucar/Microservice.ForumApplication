using Quesify.Web.Models.Answers.CreateAnswerModels.Requests;
using Quesify.Web.Models.Answers.CreateAnswerModels.ViewModels;

namespace Quesify.Web.Mappers;

public class AnswerMapper
{
    public static CreateAnswerRequest Map(CreateAnswerViewModel model, CreateAnswerRequest request)
    {
        request.QuestionId = model.QuestionId;
        request.Body = model.Body;
        return request;
    }
}
