using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quesify.Web.Interfaces;
using Quesify.Web.Mappers;
using Quesify.Web.Models.Answers.CreateAnswerModels.ViewModels;
using Quesify.Web.Models.Answers.CreateAnswerModels.Requests;
using Quesify.Web.Clients;
using Quesify.Web.Models.Questions.QuestionDetailModels.Requests;
using Quesify.Web.Models.Answers.CreateVoteForAnswerModels.Requests;
using Quesify.Web.Models.Answers.CreateAnswerModels.Responses;
using System.Reflection;
using Quesify.Web.Enums;

namespace Quesify.Web.Controllers;

public class AnswersController : Controller
{
    private readonly IAnswerClient _answerClient;

    public AnswersController(IAnswerClient answerClient)
    {
        _answerClient = answerClient;
    }

    [HttpPost("answers")]
    [Authorize]
    public async Task<IActionResult> Create(CreateAnswerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["CreateAnswerValidationErrors"] = ModelState.Values.SelectMany(o => o.Errors).Select(o => o.ErrorMessage).ToList();
            return RedirectToAction("Details", "Questions", new { id = model.QuestionId });
        }

        var createAnswerRequest = AnswerMapper.Map(model, new CreateAnswerRequest());
        var createAnswerResponse = await _answerClient.CreateAnswerAsync(createAnswerRequest);

        if (createAnswerResponse.IsFail)
        {
            TempData["CreateAnswerErrors"] = createAnswerResponse.Detail;
        }
        else
        {
            TempData["CreateAnswerMessage"] = "Your comment has been created successfully.";
            return RedirectToAction("Details", "Questions", new { id = model.QuestionId });
        }

        return RedirectToAction("Details", "Questions", new { id = model.QuestionId });
    }

    [HttpGet("answers/vote")]
    [Authorize]
    public async Task<IActionResult> Vote([FromQuery] Guid answerId, [FromQuery] Guid questionId, [FromQuery] VoteType voteType)
    {
        var createVoteForAnswerResponse
            = await _answerClient.CreateVoteAsync(new CreateVoteForAnswerRequest() { VoteType = voteType }, answerId);

        if (createVoteForAnswerResponse.IsFail)
        {
            TempData["CreateVoteErrors"] = createVoteForAnswerResponse.Detail;
        }
        else
        {
            TempData["CreateVoteMessage"] = "Your vote for the answer has been created successfully.";
            return RedirectToAction("Details", "Questions", new { id = questionId });
        }

        return RedirectToAction("Details", "Questions", new { id = questionId });
    }
}