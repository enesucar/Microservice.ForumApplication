using Microsoft.EntityFrameworkCore;
using Quesify.QuestionService.Core.Aggregates.Questions;
using Quesify.QuestionService.Core.Interfaces;
using Quesify.SharedKernel.Utilities.Exceptions;
using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Quesify.QuestionService.Core.Aggregates.Votes;

public class VoteService : IVoteService
{
    private readonly IQuestionContext _context;
    private readonly IDateTime _dateTime;

    public VoteService(
        IQuestionContext context,
        IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<Vote> CreateAsync(
        Question question, Guid userId, VoteType voteType)
    {
        if (await _context.Votes.AnyAsync(o => o.QuestionId == question.Id && o.UserId == userId))
        {
            throw new BusinessException("You have already voted on this question.");  
        }

        return new Vote(
            question.Id, userId, voteType, _dateTime.Now);
    }
}
