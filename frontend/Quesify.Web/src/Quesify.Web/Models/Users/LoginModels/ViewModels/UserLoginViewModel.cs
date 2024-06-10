namespace Quesify.Web.Models.Users.LoginModels.ViewModels;

public class UserLoginViewModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public UserLoginViewModel()
    {
        Email = null!;
        Password = null!;
    }
}
