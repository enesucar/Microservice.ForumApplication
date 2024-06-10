using Microsoft.EntityFrameworkCore;
using Quesify.AnswerService.Core.Aggregates.Answers;
using Quesify.AnswerService.Core.Interfaces;
using Quesify.SharedKernel.Utilities.Exceptions;
using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Quesify.AnswerService.Core.Aggregates.Votes;

public class VoteService : IVoteService
{
    private readonly IAnswerContext _context;
    private readonly IDateTime _dateTime;

    public VoteService(
        IAnswerContext context,
        IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<Vote> CreateAsync(
        Answer answer, Guid userId, VoteType voteType)
    {
        if (await _context.Votes.AnyAsync(o => o.AnswerId == answer.Id && o.UserId == userId))
        {
            throw new BusinessException("You have already voted on this answer.");
        }

        return new Vote(
            answer.Id, userId, voteType, _dateTime.Now);
    }
}
