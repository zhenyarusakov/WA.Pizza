using Microsoft.AspNetCore.Http;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Api;

public class AspNetUserInfoProvider: IUserInfoProvider
{
    public string? GetUserName()
    {
        var user = new HttpContextAccessor().HttpContext?.User;
        if (user?.Identity != null && !user.Identity.IsAuthenticated)
            return string.Empty;

        return user?.Identity?.Name;
    }
}