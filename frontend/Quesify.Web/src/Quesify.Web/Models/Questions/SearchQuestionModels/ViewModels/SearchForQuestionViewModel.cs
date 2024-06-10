namespace Quesify.Web.Models.Questions.SearchQuestionModels.ViewModels;

public class SearchForQuestionViewModel
{
    public string Text { get; set; }

    public int Page { get; set; }

    //public int Size { get; set; }

    public SearchForQuestionViewModel()
    {
        Text = string.Empty;
        Page = 1;
        //Size = 10;
    }
}
