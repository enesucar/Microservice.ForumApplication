namespace Quesify.Web.Models.Questions.QuestionDetailModels.Responses;

public class DetailForQuestionSuccessResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public int Score { get; set; }

    public DetailForQuestionUserSuccessResponse User { get; set; }

    public List<DetailForQuestionAnswerSuccessResponse> Answers { get; set; }

    public DateTime CreationDate { get; set; }

    public DetailForQuestionSuccessResponse()
    {
        Title = null!;
        Body = null!;
        User = null!;
        Answers = [];
    }
}

public class DetailForQuestionUserSuccessResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public int Score { get; set; }

    public string? ProfileImageUrl { get; set; }

    public DetailForQuestionUserSuccessResponse()
    {
        UserName = null!;
    }
}

public class DetailForQuestionAnswerSuccessResponse
{
    public Guid Id { get; set; }

    public string Body { get; set; }

    public int Score { get; set; }

    public DetailForQuestionAnswerUserSuccessResponse User { get; set; }

    public DateTime CreationDate { get; set; }

    public DetailForQuestionAnswerSuccessResponse()
    {
        Body = null!;
        User = null!;
    }
}

public class DetailForQuestionAnswerUserSuccessResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public int Score { get; set; }

    public string? ProfileImageUrl { get; set; }

    public DetailForQuestionAnswerUserSuccessResponse()
    {
        UserName = null!;
    }
}

