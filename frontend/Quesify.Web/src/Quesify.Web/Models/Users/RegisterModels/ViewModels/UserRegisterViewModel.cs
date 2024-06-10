namespace Quesify.Web.Models.Users.RegisterModels.ViewModels;

public class UserRegisterViewModel
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UserRegisterViewModel()
    {
        UserName = null!;
        Email = null!;
        Password = null!;
    }
}
