using Microsoft.EntityFrameworkCore;
using Quesify.AnswerService.Core.Aggregates.Questions;
using Quesify.AnswerService.Core.Aggregates.Votes;
using Quesify.AnswerService.Core.Interfaces;
using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Utilities.Exceptions;
using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Quesify.AnswerService.Core.Aggregates.Answers;

public class AnswerService : IAnswerService
{
    private readonly IAnswerContext _context;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IDateTime _dateTime;

    public AnswerService(
        IAnswerContext context,
        IGuidGenerator guidGenerator,
        IDateTime dateTime)
    {
        _context = context;
        _guidGenerator = guidGenerator;
        _dateTime = dateTime;
    }

    public async Task<Answer> CreateAsync(
        Question question, string body, Guid userId)
    {
        if (await _context.Answers.AnyAsync(o => o.QuestionId == question.Id && o.UserId == userId))
        {
            throw new BusinessException("You cannot answer on a question more than once.");
        }

        return new Answer(
            _guidGenerator.Generate(), question.Id, body, userId, _dateTime.Now);
    }

    public async Task<int> SetScoreAsync(
        Answer answer, Vote vote)
    {
        int score = 0;

        if (vote.VoteType == VoteType.Upvote)
        {
            answer.IncreaseScore(VoteConstants.UpvoteScore);
            score = VoteConstants.UpvoteUserScore;
        }
        else if (vote.VoteType == VoteType.Downvote)
        {
            answer.DecreaseScore(VoteConstants.DownvoteScore);
            score = -VoteConstants.DownvoteUserScore;
        }

        return await Task.FromResult(score);
    }
}
