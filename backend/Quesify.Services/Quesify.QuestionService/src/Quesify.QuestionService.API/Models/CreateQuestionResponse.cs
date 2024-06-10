namespace Quesify.QuestionService.API.Models;

public class CreateQuestionResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public CreateQuestionResponse()
    {
        Title = null!;
        Body = null!;
    }
}
