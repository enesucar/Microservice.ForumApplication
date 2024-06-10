namespace Quesify.Web.Models.Users.GetUserModels.Responses;

public class GetUserSuccessResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public int Score { get; set; }

    public string? About { get; set; }

    public string? Location { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? WebSiteUrl { get; set; }

    public string? ProfileImageUrl { get; set; }

    public DateTime CreationDate { get; set; }

    public GetUserSuccessResponse()
    {
        UserName = null!;
    }
}
