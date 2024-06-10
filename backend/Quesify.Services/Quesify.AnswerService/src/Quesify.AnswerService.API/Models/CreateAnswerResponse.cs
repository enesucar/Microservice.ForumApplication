namespace Quesify.AnswerService.API.Models;

public class CreateAnswerResponse
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public string Body { get; set; }

    public CreateAnswerResponse()
    {
        Body = null!;
    }
}
