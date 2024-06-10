namespace Quesify.Web.Models.Questions.CreateQuestionModels.Responses;

public class CreateQuestionSuccessResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public CreateQuestionSuccessResponse()
    {
        Title = null!;
        Body = null!;
    }
}
