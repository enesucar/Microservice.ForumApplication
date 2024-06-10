namespace Quesify.Web.Models.Answers.CreateAnswerModels.ViewModels;

public class CreateAnswerViewModel
{
    public Guid QuestionId { get; set; }

    public string Body { get; set; }

    public CreateAnswerViewModel()
    {
        Body = null!;
    }
}
