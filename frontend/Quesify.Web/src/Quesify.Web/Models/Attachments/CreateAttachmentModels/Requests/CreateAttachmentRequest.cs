namespace Quesify.Web.Models.Attachments.CreateAttachmentModels.Requests;

public class CreateAttachmentRequest
{
    public IFormFile File { get; set; }

    public CreateAttachmentRequest()
    {
        File = null!;
    }
}
