namespace Quesify.Web.Models.Attachments.CreateAttachmentModels.Responses;

public class CreateAttachmentSuccessResponse
{
    public string Path { get; set; }

    public long Size { get; set; }

    public CreateAttachmentSuccessResponse()
    {
        Path = null!;
    }
}
