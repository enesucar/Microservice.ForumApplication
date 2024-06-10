namespace Quesify.QuestionService.API.Models;

public class CreateQuestionRequest
{
    public string Title { get; set; }

    public string Body { get; set; }

    public CreateQuestionRequest()
    {
        Title = null!;
        Body = null!;
    }
}
