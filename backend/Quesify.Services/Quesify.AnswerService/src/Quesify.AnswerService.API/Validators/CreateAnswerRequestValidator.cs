using FluentValidation;
using Quesify.AnswerService.API.Models;

namespace Quesify.AnswerService.API.Validators;

public class CreateAnswerRequestValidator : AbstractValidator<CreateAnswerRequest>
{
    public CreateAnswerRequestValidator()
    {
        RuleFor(o => o.QuestionId).NotNull();
        RuleFor(o => o.Body).NotEmpty();
    }
}
