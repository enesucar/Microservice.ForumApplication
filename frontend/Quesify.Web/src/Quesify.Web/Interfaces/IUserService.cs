using Quesify.Web.Models.Users;

namespace Quesify.Web.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(Guid id);

    string GetProfileImageUrl(string? url);
}
