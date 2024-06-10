using Quesify.SharedKernel.Json;
using Quesify.Web.Interfaces;
using Quesify.Web.Models.Questions.QuestionDetailModels.Requests;
using Quesify.Web.Models.Questions.QuestionDetailModels.Responses;

namespace Quesify.Web.Clients;

public class QuestionDetailClient : IQuestionDetailClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public QuestionDetailClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<DetailForQuestionResponse> GetQuestionDetailAsync(QuestionDetailRequest request)
    {
        var response = await _httpClient.GetAsync($"question-details/{request.Id}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<DetailForQuestionResponse>(responseContent)!;
    }
}
