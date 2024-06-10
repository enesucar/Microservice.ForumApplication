using FluentValidation;
using Quesify.QuestionService.API.Models;
using Quesify.QuestionService.Core.Aggregates.Questions;

namespace Quesify.QuestionService.API.Validators;

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        RuleFor(o => o.Title).NotEmpty().MaximumLength(QuestionConstants.MaxTitleLength);
        RuleFor(o => o.Body).NotEmpty();
    }
}
