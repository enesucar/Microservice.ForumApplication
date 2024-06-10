using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quesify.AnswerService.API.IntegrationEvents.Events;
using Quesify.AnswerService.API.Mappers;
using Quesify.AnswerService.API.Models;
using Quesify.AnswerService.Core.Interfaces;
using Quesify.SharedKernel.AspNetCore.Controllers;
using Quesify.SharedKernel.AspNetCore.Filters;
using Quesify.SharedKernel.EventBus.Abstractions;
using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Security.Users;
using Quesify.SharedKernel.Utilities.Exceptions;
using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Quesify.AnswerService.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AnswersController : BaseController
{
    private readonly IAnswerService _answerService;
    private readonly IVoteService _voteService;
    private readonly ICurrentUser _currentUser;
    private readonly IEventBus _eventBus;
    private readonly IAnswerContext _context;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IDateTime _dateTime;

    public AnswersController(
        IAnswerService answerService,
        IVoteService voteService,
        ICurrentUser currentUser,
        IEventBus eventBus,
        IAnswerContext context,
        IGuidGenerator guidGenerator,
        IDateTime dateTime)
    {
        _answerService = answerService;
        _voteService = voteService;
        _currentUser = currentUser;
        _eventBus = eventBus;
        _context = context;
        _guidGenerator = guidGenerator;
        _dateTime = dateTime;
    }

    [HttpPost]
    [Authorize]
    [Transaction]
    [Validation]
    public async Task<IActionResult> CreateAsync(CreateAnswerRequest request)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(o => o.Id == request.QuestionId);
        if (question == null)
        {
            throw new NotFoundException($"The question {request.QuestionId} was not found.");
        }

        var answer = await _answerService.CreateAsync(
            question, request.Body, _currentUser.UserId!.Value);

        await _context.Answers.AddAsync(answer);
        await _context.SaveChangesAsync();

        await _eventBus.PublishAsync(
            new AnswerCreatedIntegrationEvent(
                question.Id,
                answer.Id,
                answer.Body,
                answer.UserId,
                answer.CreationDate,
                _guidGenerator.Generate(),
                _dateTime.Now
            )
        );

        return CreatedResponse(data: AnswerMapper.Map(answer, new CreateAnswerResponse()));
    }

    [HttpPost("{id}/votes")]
    [Authorize]
    [Transaction]
    [Validation]
    public async Task<IActionResult> VoteAsync([FromRoute] Guid id, [FromBody] CreateVoteRequest request)
    {
        var answer = await _context.Answers.FirstOrDefaultAsync(o => o.Id == id);
        if (answer == null)
        {
            throw new NotFoundException($"The answer {id} was not found.");
        }

        var vote = await _voteService.CreateAsync(answer, _currentUser.UserId!.Value, request.VoteType);
        await _context.Votes.AddAsync(vote);
        var score = await _answerService.SetScoreAsync(answer, vote);
        await _context.SaveChangesAsync();

        await _eventBus.PublishAsync(
            new AnswerVotedIntegrationEvent(
                answer.QuestionId,
                answer.Id,
                answer.UserId,
                answer.Score,
                score,
                _guidGenerator.Generate(),
                _dateTime.Now
            )
        );

        return CreatedResponse(data: AnswerMapper.Map(vote, answer, new CreateVoteResponse()));
    }
}
