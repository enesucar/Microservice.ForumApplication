using Microsoft.Net.Http.Headers;
using Quesify.SharedKernel.Json;
using Quesify.Web.Interfaces;
using Quesify.Web.Models.Answers.CreateAnswerModels.Requests;
using Quesify.Web.Models.Answers.CreateAnswerModels.Responses;
using Quesify.Web.Models.Answers.CreateVoteForAnswerModels.Requests;
using Quesify.Web.Models.Answers.CreateVoteForAnswerModels.Responses;
using System.Net.Http.Headers;

namespace Quesify.Web.Clients;

public class AnswerClient : IAnswerClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public AnswerClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer,
        IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<CreateAnswerResponse> CreateAnswerAsync(CreateAnswerRequest request)
    {
        SetAuthorization();
        var response = await _httpClient.PostAsJsonAsync("answers", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<CreateAnswerResponse>(responseContent)!;
    }

    public async Task<CreateVoteForAnswerResponse> CreateVoteAsync(CreateVoteForAnswerRequest request, Guid answerId)
    {
        SetAuthorization();
        var response = await _httpClient.PostAsJsonAsync($"answers/{answerId}/votes", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<CreateVoteForAnswerResponse>(responseContent)!;
    }

    private void SetAuthorization()
    {
        var token = _contextAccessor.HttpContext?.Request.Cookies["token"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
