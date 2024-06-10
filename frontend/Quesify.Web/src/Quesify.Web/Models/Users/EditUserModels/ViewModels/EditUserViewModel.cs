namespace Quesify.Web.Models.Users.EditUserModels.ViewModels;

public class EditUserViewModel
{
    public string? About { get; set; }

    public string? Location { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? WebSiteUrl { get; set; }

    public IFormFile? ProfileImage { get; set; }

    public bool RemoveProfileImage { get; set; }
}
