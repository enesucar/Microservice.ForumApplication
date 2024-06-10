using Microsoft.AspNetCore.Identity;

namespace Quesify.Web.Models.Users.RegisterModels.Responses;

public class UserForRegisterErrorResponse
{
    public bool Succeeded { get; set; }

    public IEnumerable<IdentityError>? Errors { get; set; }

    public UserForRegisterErrorResponse()
    {
        Errors = [];
    }
}