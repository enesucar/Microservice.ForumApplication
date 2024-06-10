using Quesify.SharedKernel.Json;
using Quesify.Web.Interfaces;
using Quesify.Web.Models.Questions.SearchQuestionModels.Requests;
using Quesify.Web.Models.Questions.SearchQuestionModels.Responses;
using System.Web;

namespace Quesify.Web.Clients;

public class SearchClient : ISearchClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public SearchClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<SearchForQuestionResponse> SearchQuestionAsync(SearchForQuestionRequest request)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["page"] = request.Page.ToString();
        query["size"] = request.Size.ToString();
        if (!request.Text.IsNullOrWhiteSpace()) { query["text"] = request.Text!.ToString(); }
        if (request.UserId.HasValue) { query["userId"] = request.UserId!.ToString(); }
        if (request.Score.HasValue) { query["score"] = request.Score!.ToString(); }

        var queryString = query.ToString();
        var response = await _httpClient.GetAsync($"questions?{queryString}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<SearchForQuestionResponse>(responseContent)!;
    }
}
