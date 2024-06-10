using FluentValidation;
using Quesify.Web.Models.Users.LoginModels.ViewModels;

namespace Quesify.Web.Validators.Users.ViewModels;

public class UserLoginViewModelValidator : AbstractValidator<UserLoginViewModel>
{
    public UserLoginViewModelValidator()
    {
        RuleFor(o => o.Email).NotEmpty().EmailAddress();
        RuleFor(o => o.Password).NotEmpty();
    }
}
