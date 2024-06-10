using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quesify.SharedKernel.Security.Users;
using Quesify.Web.Interfaces;
using Quesify.Web.Mappers;
using Quesify.Web.Models.Attachments.CreateAttachmentModels.Requests;
using Quesify.Web.Models.Users.EditUserModels.Requests;
using Quesify.Web.Models.Users.EditUserModels.ViewModels;
using Quesify.Web.Models.Users.LoginModels.Requests;
using Quesify.Web.Models.Users.LoginModels.ViewModels;
using Quesify.Web.Models.Users.RegisterModels.Requests;
using Quesify.Web.Models.Users.RegisterModels.ViewModels;

namespace Quesify.Web.Controllers;

public class UsersController : Controller
{
    private readonly IUserClient _userClient;
    private readonly IUserService _userService;
    private readonly ICurrentUser _currentUser;
    private readonly IMediaClient _mediaClient;

    public UsersController(
        IUserClient userClient,
        IUserService userService,
        ICurrentUser currentUser,
        IMediaClient mediaClient)
    {
        _userClient = userClient;
        _userService = userService;
        _currentUser = currentUser;
        _mediaClient = mediaClient;
    }

    [HttpGet("users")]
    public async Task<IActionResult> Index([FromQuery] Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return View(user);
    }

    [HttpGet("users/edit")]
    [Authorize]
    public async Task<IActionResult> Edit()
    {
        var user = await _userService.GetUserByIdAsync(_currentUser.UserId!.Value);
        var userViewModel = UserMapper.Map(user, new EditUserViewModel());
        return View(userViewModel);
    }

    [HttpPost("users/edit")]
    [Authorize]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        EditUserRequest request = new EditUserRequest();

        if (model.RemoveProfileImage)
        {
            request.ProfileImageUrl = null;
        }
        else if (model.ProfileImage != null)
        {
            var createAttachmentResponse =
                await _mediaClient.CreateAttachmentAsync(
                    new CreateAttachmentRequest() { File = model.ProfileImage! });
            if (createAttachmentResponse.IsSuccess)
            {
                request.ProfileImageUrl = createAttachmentResponse.Data!.Path;
            }
        }

        request.About = model.About;
        request.Location = model.Location;
        request.BirthDate = model.BirthDate;
        request.WebSiteUrl = model.WebSiteUrl;

        var editUserResponse = await _userClient.EditAsync(request);
        if (editUserResponse.IsSuccess)
        {
            TempData["EditUserMessage"] = "Your profile has been successfully updated.";
            return RedirectToAction("Index", new { id = _currentUser.UserId!.Value });
        }

        TempData["Errors"] = "Your profile could not be updated. Please try again later.";
        return View(model);
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userForLoginRequest = UserMapper.Map(model, new UserForLoginRequest());
        var loginResponse = await _userClient.LoginAsync(userForLoginRequest);

        if (loginResponse.IsSuccess)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = loginResponse.Data!.AccessToken!.Expiration.ToDatetimeOffsetFromUtc();
            options.HttpOnly = true;
            Response.Cookies.Append("token", loginResponse.Data!.AccessToken!.Token, options);
            return Redirect("/");
        }
        else if (loginResponse.Errors!.Failed)
        {
            ViewData["Errors"] = "Email or password is wrong.";
        }
        else if (loginResponse.Errors!.RequiresEmailConfirmation)
        {
            ViewData["Errors"] = "Email address is not confirmed.";
        }
        else if (loginResponse.Errors!.IsLockedOut)
        {
            ViewData["Errors"] = "Too many failed attempts. Please try again later.";
        }
        else if (loginResponse.Errors!.RequiresTwoFactor)
        {
            //two factor
        }
        else 
        {
            ViewData["Errors"] = "Something went wrong. Please try again later.";
        }

        return View(model);
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userForRegisterRequest = UserMapper.Map(model, new UserForRegisterRequest());
        var registerResponse = await _userClient.RegisterAsync(userForRegisterRequest);

        if (registerResponse.IsFail)
        {
            ViewData["Errors"] = registerResponse.Errors!.Errors!.Select(o => o.Description).ToList();
            return View(model);
        }

        return RedirectToAction("RegisterSuccessful");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        if (Request.Cookies["token"] != null)
        {
            Response.Cookies.Delete("token");
        }

        return Redirect("/");
    }

    [HttpGet("register-successful")]
    public IActionResult RegisterSuccessful()
    {
        return View();
    }
}
