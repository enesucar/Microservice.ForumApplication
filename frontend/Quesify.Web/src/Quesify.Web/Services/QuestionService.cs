using Quesify.Web.Interfaces;
using Quesify.Web.Models.Questions.SearchQuestionModels.Requests;
using Quesify.Web.Models.Questions.SearchQuestionModels.Responses;
using Quesify.Web.Models.Questions.SearchQuestionModels.ViewModels;

namespace Quesify.Web.Services;

public class QuestionService : IQuestionService
{
    private readonly ISearchClient _searchClient;

    public QuestionService(ISearchClient searchClient)
    {
        _searchClient = searchClient;
    }

    public async Task<SearchForQuestionResponse> SearchAsync(SearchForQuestionViewModel model)
    {
        List<string> texts = [];
        int? score = null;
        Guid? userId = null;

        if (model.Text == null)
        {
            model.Text = string.Empty;
        }

        var elements = model.Text.Split(' ');

        if (!model.Text.IsNullOrEmpty())
        {
            foreach (var element in elements)
            {
                var values = element.Split(':');
                if (values.Length == 1)
                {
                    texts.Add(values[0]);
                }
                else
                {
                    if (values[0] == "user")
                    {
                        if (Guid.TryParse(values[values.Length - 1], out Guid result))
                        {
                            userId = result;
                        }
                        else
                        {
                            texts.Add(element);
                        }
                    }
                    else if (values[0] == "score")
                    {
                        if (int.TryParse(values[values.Length - 1], out int result))
                        {
                            score = result;
                        }
                        else
                        {
                            texts.Add(element);
                        }
                    }
                }
            }
        }

        var text = string.Join(" ", texts);

        SearchForQuestionRequest request = new SearchForQuestionRequest()
        {
            Page = model.Page,
            Size = 10,
            Score = score,
            UserId = userId,
            Text = text.IsNullOrWhiteSpace() ? null : text
        };

        return await _searchClient.SearchQuestionAsync(request);
    }
}
