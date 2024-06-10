using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Quesify.Web.Clients;
using Quesify.Web.Enums;
using Quesify.Web.Interfaces;
using Quesify.Web.Mappers;
using Quesify.Web.Models.Questions.CreateQuestionModels.Requests;
using Quesify.Web.Models.Questions.CreateQuestionModels.ViewModels;
using Quesify.Web.Models.Questions.CreateVoteForQuestionModels.Requests;
using Quesify.Web.Models.Questions.QuestionDetailModels.Requests;
using Quesify.Web.Models.Questions.SearchQuestionModels.ViewModels;

namespace Quesify.Web.Controllers;

public class QuestionsController : Controller
{
    private readonly ISearchClient _searchClient;
    private readonly IQuestionDetailClient _questionDetailClient;
    private readonly IQuestionClient _questionClient;
    private readonly IQuestionService _questionService;

    public QuestionsController(
        ISearchClient searchClient,
        IQuestionDetailClient questionDetailClient,
        IQuestionClient questionClient,
        IQuestionService questionService)
    {
        _searchClient = searchClient;
        _questionDetailClient = questionDetailClient;
        _questionClient = questionClient;
        _questionService = questionService;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Index([FromQuery] SearchForQuestionViewModel model)
    {
        var data = await _questionService.SearchAsync(model);
        
        ViewBag.QuestionPage = data.Data!.Page;
        ViewBag.QuestionText = model.Text;

        return View(data.Data);
    }

    [HttpGet("get-questions")]
    public async Task<IActionResult> GetQuestions([FromQuery] SearchForQuestionViewModel model)
    {
        var data = await _questionService.SearchAsync(model);
        return Json(new { data = data.Data!});
    }

    [HttpGet("questions/detail")]
    public async Task<IActionResult> Details([FromQuery] Guid id)
    {
        var data = await _questionDetailClient.GetQuestionDetailAsync(new QuestionDetailRequest() { Id = id }); 
        return View(data.Data);
    }

    [HttpGet("questions/ask")]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("questions/ask")]
    [Authorize]
    public async Task<IActionResult> CreateAsync(CreateQuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var createQuestionRequest = QuestionMapper.Map(model, new CreateQuestionRequest());
        var createQuestionResponse = await _questionClient.CreateQuestionAsync(createQuestionRequest);

        if (createQuestionResponse.IsSuccess)
        {
            return RedirectToAction("Details", "Questions", new { id = createQuestionResponse.Data!.Id });
        }
        else
        {
            ViewData["Errors"] = createQuestionResponse.Detail;
        }

        return View(model);
    }


    [HttpGet("questions/vote")]
    [Authorize]
    public async Task<IActionResult> Vote([FromQuery] Guid questionId, [FromQuery] VoteType voteType)
    {
        var createVoteForQuestionResponse
            = await _questionClient.CreateVoteAsync(new CreateVoteForQuestionRequest() { VoteType = voteType }, questionId);

        if (createVoteForQuestionResponse.IsFail)
        {
            TempData["CreateVoteErrors"] = createVoteForQuestionResponse.Detail;
        }
        else
        {
            TempData["CreateVoteMessage"] = "Your vote for the question has been created successfully.";
            return RedirectToAction("Details", "Questions", new { id = questionId });
        }

        return RedirectToAction("Details", "Questions", new { id = questionId });
    }
}
