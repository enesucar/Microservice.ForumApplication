using Microsoft.AspNetCore.Identity;

namespace Quesify.Web.Models.Users.RegisterModels.Responses;

public class UserForRegisterSuccessResponse
{
    public bool Succeeded { get; set; }

    public IEnumerable<IdentityError>? Errors { get; set; }

    public UserForRegisterSuccessResponse()
    {
        Errors = [];
    }
}
