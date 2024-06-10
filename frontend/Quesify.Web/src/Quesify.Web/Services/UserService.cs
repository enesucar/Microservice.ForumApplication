using Quesify.Web.Interfaces;
using Quesify.Web.Mappers;
using Quesify.Web.Models.Users;
using Quesify.Web.Models.Users.GetUserModels.Requests;

namespace Quesify.Web.Services;

public class UserService : IUserService
{
    private readonly IUserClient _userClient;
    private string _mediaServiceUrl;

    public UserService(
        IUserClient userClient,
        IConfiguration configuration)
    {
        _userClient = userClient;
        var baseUrl = configuration.GetValue<string>("BackendOptions:BaseUrl");
        var mediaServiceUrl = configuration.GetValue<string>("BackendOptions:MediaServiceUrl");
        _mediaServiceUrl = $"{baseUrl}{mediaServiceUrl}";
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await _userClient.GetAsync(new GetUserRequest() { UserId = id });
        user.Data!.ProfileImageUrl = GetProfileImageUrl(user.Data.ProfileImageUrl);
        return UserMapper.Map(user.Data!, new User());
    }

    public string GetProfileImageUrl(string? url)
    {
        return url.IsNullOrWhiteSpace()
            ? $"{_mediaServiceUrl}attachments/default-avatar.png"
            : $"{_mediaServiceUrl}{url}";
    }
}
