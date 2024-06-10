using Microsoft.AspNetCore.Http;
using Quesify.SharedKernel.Json;
using Quesify.Web.Interfaces;
using Quesify.Web.Models.Questions.SearchQuestionModels.Responses;
using Quesify.Web.Models.Users.EditUserModels.Requests;
using Quesify.Web.Models.Users.EditUserModels.Responses;
using Quesify.Web.Models.Users.GetUserModels.Requests;
using Quesify.Web.Models.Users.GetUserModels.Responses;
using Quesify.Web.Models.Users.LoginModels.Requests;
using Quesify.Web.Models.Users.LoginModels.Responses;
using Quesify.Web.Models.Users.RegisterModels.Requests;
using Quesify.Web.Models.Users.RegisterModels.Responses;
using System.Net.Http.Headers;

namespace Quesify.Web.Clients;

public class UserClient : IUserClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer,
        IHttpContextAccessor contextAccessor)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _contextAccessor = contextAccessor;
    }

    public async Task<GetUserResponse> GetAsync(GetUserRequest request)
    {
        var response = await _httpClient.GetAsync($"users/{request.UserId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<GetUserResponse>(responseContent)!;
    }

    public async Task<EditUserResponse> EditAsync(EditUserRequest request)
    {
        SetAuthorization();
        var response = await _httpClient.PutAsJsonAsync($"users", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<EditUserResponse>(responseContent)!;
    }

    public async Task<UserForLoginResponse> LoginAsync(UserForLoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<UserForLoginResponse>(responseContent)!;
    }

    public async Task<UserForRegisterResponse> RegisterAsync(UserForRegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/register", request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<UserForRegisterResponse>(responseContent)!;
    }

    private void SetAuthorization()
    {
        var token = _contextAccessor.HttpContext?.Request.Cookies["token"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
