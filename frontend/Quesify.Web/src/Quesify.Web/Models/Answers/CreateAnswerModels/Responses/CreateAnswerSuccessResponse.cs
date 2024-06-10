namespace Quesify.Web.Models.Answers.CreateAnswerModels.Responses;

public class CreateAnswerSuccessResponse
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public string Body { get; set; }

    public CreateAnswerSuccessResponse()
    {
        Body = null!;
    }
}
