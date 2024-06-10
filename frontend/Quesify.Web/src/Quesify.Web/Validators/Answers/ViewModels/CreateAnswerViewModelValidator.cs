using FluentValidation;
using Quesify.Web.Models.Answers.CreateAnswerModels.ViewModels;

namespace Quesify.Web.Validators.Answers.ViewModels;

public class CreateAnswerViewModelValidator : AbstractValidator<CreateAnswerViewModel>
{
    public CreateAnswerViewModelValidator()
    {
        RuleFor(o => o.QuestionId).NotEmpty();
        RuleFor(o => o.Body).NotEmpty();
    }
}
