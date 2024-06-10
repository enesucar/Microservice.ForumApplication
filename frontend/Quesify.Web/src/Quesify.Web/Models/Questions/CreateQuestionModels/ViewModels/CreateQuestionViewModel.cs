namespace Quesify.Web.Models.Questions.CreateQuestionModels.ViewModels;

public class CreateQuestionViewModel
{
    public string Title { get; set; }

    public string Body { get; set; }

    public CreateQuestionViewModel()
    {
        Title = null!;
        Body = null!;
    }
}
