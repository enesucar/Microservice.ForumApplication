using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quesify.QuestionService.API.IntegrationEvents.Events;
using Quesify.QuestionService.API.Mappers;
using Quesify.QuestionService.API.Models;
using Quesify.QuestionService.Core.Interfaces;
using Quesify.SharedKernel.AspNetCore.Controllers;
using Quesify.SharedKernel.AspNetCore.Filters;
using Quesify.SharedKernel.EventBus.Abstractions;
using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Security.Users;
using Quesify.SharedKernel.Utilities.Exceptions;
using Quesify.SharedKernel.Utilities.TimeProviders;

namespace Quesify.QuestionService.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class QuestionsController : BaseController
{
    private readonly IQuestionService _questionService;
    private readonly IVoteService _voteService;
    private readonly ICurrentUser _currentUser;
    private readonly IEventBus _eventBus;
    private readonly IQuestionContext _context;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IDateTime _dateTime;

    public QuestionsController(
        IQuestionService questionService,
        IVoteService voteService,
        ICurrentUser currentUser,
        IEventBus eventBus,
        IQuestionContext context,
        IGuidGenerator guidGenerator,
        IDateTime dateTime)
    {
        _questionService = questionService;
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
    public async Task<IActionResult> CreateAsync(CreateQuestionRequest request)
    {
        var question = await _questionService.CreateAsync(
            request.Title, request.Body, _currentUser.UserId!.Value);

        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

        await _eventBus.PublishAsync(
            new QuestionCreatedIntegrationEvent(
                question.Id,
                question.Title,
                question.Body,
                question.UserId,
                question.CreationDate,
                _guidGenerator.Generate(),
                _dateTime.Now
            )
        );

        return CreatedResponse(data: QuestionMapper.Map(question, new CreateQuestionResponse()));
    }

    [HttpPost("{id}/votes")]
    [Authorize]
    [Transaction]
    [Validation]
    public async Task<IActionResult> VoteAsync([FromRoute] Guid id, [FromBody] CreateVoteRequest request)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(o => o.Id == id);
        if (question == null)
        {
            throw new NotFoundException($"The question {id} was not found.");
        }

        var vote = await _voteService.CreateAsync(question, _currentUser.UserId!.Value, request.VoteType);
        await _context.Votes.AddAsync(vote);
        var score = await _questionService.SetScoreAsync(question, vote);
        await _context.SaveChangesAsync();

        await _eventBus.PublishAsync(
            new QuestionVotedIntegrationEvent(
                question.Id,
                question.UserId,
                question.Score,
                score,
                _guidGenerator.Generate(),
                _dateTime.Now
            )
        );

        return CreatedResponse(data: QuestionMapper.Map(vote, question, new CreateVoteResponse()));
    }
}
