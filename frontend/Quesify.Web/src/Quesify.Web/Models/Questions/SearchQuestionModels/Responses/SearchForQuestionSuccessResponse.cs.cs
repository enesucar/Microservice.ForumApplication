namespace Quesify.Web.Models.Questions.SearchQuestionModels.Responses;

public class SearchForQuestionSuccessResponse
{
    public int Page { get; set; }

    public int Size { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public bool IsFirstPage { get; set; }

    public bool IsLastPage { get; set; }

    public List<SearchForQuestionSuccessResultResponse> Items { get; set; }

    public SearchForQuestionSuccessResponse()
    {
        Items = [];
    }
}

public class SearchForQuestionSuccessResultResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public int QuestionScore { get; set; }

    public SearchForQuestionUserSuccessResponse User { get; set; }

    public DateTime CreationDate { get; set; }

    public SearchForQuestionSuccessResultResponse()
    {
        Title = null!;
        Body = null!;
        User = null!;
    }
}

public class SearchForQuestionUserSuccessResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string? ProfileImageUrl { get; set; }

    public int Score { get; set; }

    public SearchForQuestionUserSuccessResponse()
    {
        UserName = null!;
    }
}