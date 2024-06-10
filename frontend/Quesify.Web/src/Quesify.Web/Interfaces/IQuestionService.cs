using Quesify.Web.Models.Questions.SearchQuestionModels.Responses;
using Quesify.Web.Models.Questions.SearchQuestionModels.ViewModels;

namespace Quesify.Web.Interfaces;

public interface IQuestionService
{
    Task<SearchForQuestionResponse> SearchAsync(SearchForQuestionViewModel model);
}
