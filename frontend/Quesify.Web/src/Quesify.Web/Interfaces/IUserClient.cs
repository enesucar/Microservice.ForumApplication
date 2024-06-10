using Quesify.Web.Models.Users.EditUserModels.Requests;
using Quesify.Web.Models.Users.EditUserModels.Responses;
using Quesify.Web.Models.Users.GetUserModels.Requests;
using Quesify.Web.Models.Users.GetUserModels.Responses;
using Quesify.Web.Models.Users.LoginModels.Requests;
using Quesify.Web.Models.Users.LoginModels.Responses;
using Quesify.Web.Models.Users.RegisterModels.Requests;
using Quesify.Web.Models.Users.RegisterModels.Responses;

namespace Quesify.Web.Interfaces;

public interface IUserClient
{
    Task<GetUserResponse> GetAsync(GetUserRequest request);

    Task<EditUserResponse> EditAsync(EditUserRequest request);

    Task<UserForLoginResponse> LoginAsync(UserForLoginRequest request);

    Task<UserForRegisterResponse> RegisterAsync(UserForRegisterRequest request);
}
