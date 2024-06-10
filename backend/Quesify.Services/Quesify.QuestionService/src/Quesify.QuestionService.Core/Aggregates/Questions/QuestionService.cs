using Microsoft.EntityFrameworkCore;
using Quesify.QuestionService.Core.Aggregates.Votes;
using Quesify.QuestionService.Core.Interfaces;
using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Utilities.Exceptions;
using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Quesify.QuestionService.Core.Aggregates.Questions;

public class QuestionService : IQuestionService
{
    private readonly IQuestionContext _context;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IDateTime _dateTime;

    public QuestionService(
        IQuestionContext context,
        IGuidGenerator guidGenerator,
        IDateTime dateTime)
    {
        _context = context;
        _guidGenerator = guidGenerator;
        _dateTime = dateTime;
    }

    public async Task<Question> CreateAsync(
        string title, string body, Guid userId)
    {
        if (await _context.Questions.AnyAsync(o => o.Title == title))
        {
            throw new ConflictException($"Question title {title} already exists.");
        }

        return new Question(
            _guidGenerator.Generate(), title, body, userId, _dateTime.Now);
    }

    public async Task<int> SetScoreAsync(
        Question question, Vote vote)
    {
        int score = 0;

        if (vote.VoteType == VoteType.Upvote)
        {
            question.IncreaseScore(VoteConstants.UpvoteScore);
            score = VoteConstants.UpvoteUserScore;
        }
        else if (vote.VoteType == VoteType.Downvote)
        {
            question.DecreaseScore(VoteConstants.DownvoteScore);
            score = -VoteConstants.DownvoteUserScore;
        }

        return await Task.FromResult(score);
    }
}
