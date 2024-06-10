using Quesify.Web.Models.Attachments.CreateAttachmentModels.Requests;
using Quesify.Web.Models.Attachments.CreateAttachmentModels.Responses;

namespace Quesify.Web.Interfaces;

public interface IMediaClient
{
    Task<CreateAttachmentResponse> CreateAttachmentAsync(CreateAttachmentRequest request);
}
