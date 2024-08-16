using MyProje.Data;

namespace MyProje.Services;
public class UserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public UserDto? GetCurrentUser()
    {
        return httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true
        ? new UserDto { UserName = httpContextAccessor.HttpContext.User.Identity.Name }
        : null;



    }
}