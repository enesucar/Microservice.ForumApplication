using Quesify.Web.Models.Questions.SearchQuestionModels.Requests;
using Quesify.Web.Models.Questions.SearchQuestionModels.Responses;

namespace Quesify.Web.Interfaces;

public interface ISearchClient
{
    Task<SearchForQuestionResponse> SearchQuestionAsync(SearchForQuestionRequest request);
}
