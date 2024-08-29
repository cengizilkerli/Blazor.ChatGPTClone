using ChatGPTClone.Application.Common.Interfaces;
using System.Security.Claims;

namespace ChatGPTClone.WebApi.Services;

public class CurrentUserManager : ICurrentUserServices
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserManager(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid UserId => GetUserId();

    public Guid GetUserId()
    {
        return Guid.Parse("2798212b-3e5d-4556-8629-a64eb70da4a8");

        //var userId = _contextAccessor
        //                .HttpContext?
        //                .User?
        //                .FindFirstValue("uid");

        //return string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
    }
}
