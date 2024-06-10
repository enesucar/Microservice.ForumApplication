using Quesify.Web.Models.Users;
using Quesify.Web.Models.Users.EditUserModels.ViewModels;
using Quesify.Web.Models.Users.GetUserModels.Responses;
using Quesify.Web.Models.Users.LoginModels.Requests;
using Quesify.Web.Models.Users.LoginModels.ViewModels;
using Quesify.Web.Models.Users.RegisterModels.Requests;
using Quesify.Web.Models.Users.RegisterModels.ViewModels;

namespace Quesify.Web.Mappers;

public static class UserMapper
{
    public static User Map(GetUserSuccessResponse response, User user)
    {
        user.Id = response.Id;
        user.UserName = response.UserName;
        user.Score = response.Score;
        user.About = response.About;
        user.Location = response.Location;
        user.BirthDate = response.BirthDate;
        user.WebSiteUrl = response.WebSiteUrl;
        user.ProfileImageUrl = response.ProfileImageUrl;
        user.CreationDate = response.CreationDate;
        return user;
    }

    public static UserForRegisterRequest Map(UserRegisterViewModel model, UserForRegisterRequest request)
    {
        request.UserName = model.UserName;
        request.Email = model.Email;
        request.Password = model.Password;
        return request;
    }

    public static UserForLoginRequest Map(UserLoginViewModel model, UserForLoginRequest request)
    {
        request.Email = model.Email;
        request.Password = model.Password;
        return request;
    }

    public static EditUserViewModel Map(User user, EditUserViewModel model)
    {
        model.About = user.About;
        model.Location = user.Location;
        model.BirthDate = user.BirthDate;
        model.WebSiteUrl = user.WebSiteUrl;
        return model;
    }
}
