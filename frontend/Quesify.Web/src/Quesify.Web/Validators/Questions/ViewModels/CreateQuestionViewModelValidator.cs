using FluentValidation;
using Quesify.Web.Constants;
using Quesify.Web.Models.Questions.CreateQuestionModels.ViewModels;

namespace Quesify.Web.Validators.Questions.ViewModels;

public class CreateQuestionViewModelValidator : AbstractValidator<CreateQuestionViewModel>
{
    public CreateQuestionViewModelValidator()
    {
        RuleFor(o => o.Title).NotEmpty().MaximumLength(QuestionConstants.MaxTitleLength);
        RuleFor(o => o.Body).NotEmpty();
    }
}
