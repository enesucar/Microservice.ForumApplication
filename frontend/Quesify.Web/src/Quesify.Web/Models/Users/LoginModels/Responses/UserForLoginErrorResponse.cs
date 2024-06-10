namespace Quesify.Web.Models.Users.LoginModels.Responses;

public class UserForLoginErrorResponse
{
    public bool Succeeded { get; set; }

    public bool Failed { get; set; }

    public bool IsLockedOut { get; set; }

    public bool RequiresTwoFactor { get; set; }

    public bool RequiresEmailConfirmation { get; set; }
}
