using Quesify.SharedKernel.Security.Tokens;

namespace Quesify.Web.Models.Users.LoginModels.Responses;

public class UserForLoginSuccessResponse
{
    public bool Succeeded { get; set; }

    public AccessToken? AccessToken { get; set; }
}
