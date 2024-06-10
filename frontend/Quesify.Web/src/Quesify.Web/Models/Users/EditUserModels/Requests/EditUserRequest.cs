namespace Quesify.Web.Models.Users.EditUserModels.Requests;

public class EditUserRequest
{
    public string? About { get; set; }

    public string? Location { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? WebSiteUrl { get; set; }

    public string? ProfileImageUrl { get; set; }
}
