namespace Quesify.AnswerService.API.Models;

public class CreateAnswerRequest
{
    public Guid QuestionId { get; set; }

    public string Body { get; set; }

    public CreateAnswerRequest()
    {
        Body = null!;
    }
}
