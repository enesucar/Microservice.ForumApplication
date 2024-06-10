using Quesify.SharedKernel.Json;
using Quesify.Web.Interfaces;
using Quesify.Web.Models.Attachments.CreateAttachmentModels.Requests;
using Quesify.Web.Models.Attachments.CreateAttachmentModels.Responses;
using Quesify.Web.Models.Questions.CreateQuestionModels.Responses;
using System.Net.Http;

namespace Quesify.Web.Clients;

public class MediaClient : IMediaClient
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public MediaClient(
        HttpClient httpClient,
        IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<CreateAttachmentResponse> CreateAttachmentAsync(CreateAttachmentRequest request)
    {
        using var binaryReader = new BinaryReader(request.File.OpenReadStream());
        byte[] data = binaryReader.ReadBytes((int)request.File.OpenReadStream().Length);
        ByteArrayContent bytes = new ByteArrayContent(data);
        MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent
        {
            { bytes, "file", request.File.FileName }
        };
        var response = await _httpClient.PostAsync($"attachments", multipartFormDataContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        return _jsonSerializer.Deserialize<CreateAttachmentResponse>(responseContent)!;
    }
}
