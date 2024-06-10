namespace Quesify.Web.Models.Questions.CreateQuestionModels.Requests;

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
