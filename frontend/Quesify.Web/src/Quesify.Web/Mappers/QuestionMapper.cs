using Quesify.Web.Models.Questions.CreateQuestionModels.Requests;
using Quesify.Web.Models.Questions.CreateQuestionModels.ViewModels;

namespace Quesify.Web.Mappers;

public class QuestionMapper
{
    public static CreateQuestionRequest Map(CreateQuestionViewModel model, CreateQuestionRequest request)
    {
        request.Title = model.Title;
        request.Body = model.Body;
        return request;
    }
}
