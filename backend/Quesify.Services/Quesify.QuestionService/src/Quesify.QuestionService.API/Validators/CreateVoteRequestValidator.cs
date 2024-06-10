using FluentValidation;
using Quesify.QuestionService.API.Models;

namespace Quesify.QuestionService.API.Validators;

public class CreateVoteRequestValidator : AbstractValidator<CreateVoteRequest>
{
    public CreateVoteRequestValidator()
    {
        RuleFor(o => o.VoteType).NotNull();
    }
}
