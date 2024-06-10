namespace Quesify.Web.Models.Users.LoginModels.Requests;

public class UserForLoginRequest
{
    public string Email { get; set; }

    public string Password { get; set; }

    public UserForLoginRequest()
    {
        Email = null!;
        Password = null!;
    }
}
