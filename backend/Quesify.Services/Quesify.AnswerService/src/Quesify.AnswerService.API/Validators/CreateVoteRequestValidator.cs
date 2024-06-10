using FluentValidation;
using Quesify.AnswerService.API.Models;

namespace Quesify.AnswerService.API.Validators;

public class CreateVoteRequestValidator : AbstractValidator<CreateVoteRequest>
{
    public CreateVoteRequestValidator()
    {
        RuleFor(o => o.VoteType).NotNull();
    }
}

