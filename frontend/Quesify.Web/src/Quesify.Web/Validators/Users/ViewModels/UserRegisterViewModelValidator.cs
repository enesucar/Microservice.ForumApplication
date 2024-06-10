using FluentValidation;
using Quesify.Web.Models.Users.RegisterModels.ViewModels;

namespace Quesify.Web.Validators.Users.ViewModels;

public class UserRegisterViewModelValidator : AbstractValidator<UserRegisterViewModel>
{
    public UserRegisterViewModelValidator()
    {
        RuleFor(o => o.Email).NotEmpty().EmailAddress();
        RuleFor(o => o.UserName).NotEmpty();
        RuleFor(o => o.Password).NotEmpty();
    }
}
