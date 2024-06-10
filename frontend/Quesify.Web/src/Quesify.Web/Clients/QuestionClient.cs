using Quesify.SharedKernel.Json;
using Quesify.Web.Interfaces;
using Quesify.Web.Models.Questions.CreateQuestionModels.Requests;
using Quesify.Web.Models.Questions.CreateQuestionModels.Responses;
using Quesify.Web.Models.Questions.CreateVoteForQuestionModels.Requests;
using Quesify.Web.Models.Questions.CreateVoteForQuestionModels.Responses;
using System.Net.Http.Headers;
 
namespace Quesify.Web.Clients;

public class QuestionClient : IQuestionClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public QuestionClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer,
        IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<CreateQuestionResponse> CreateQuestionAsync(CreateQuestionRequest request)
    {
        SetAuthorization();
        var response = await _httpClient.PostAsJsonAsync($"questions", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<CreateQuestionResponse>(responseContent)!;
    }

    public async Task<CreateVoteForQuestionResponse> CreateVoteAsync(CreateVoteForQuestionRequest request, Guid questionId)
    {
        SetAuthorization();
        var response = await _httpClient.PostAsJsonAsync($"questions/{questionId}/votes", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<CreateVoteForQuestionResponse>(responseContent)!;
    }

    private void SetAuthorization()
    {
        var token = _contextAccessor.HttpContext?.Request.Cookies["token"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
