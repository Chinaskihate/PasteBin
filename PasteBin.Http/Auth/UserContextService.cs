using Microsoft.AspNetCore.Http;
using PasteBin.Backend.Auth;
using System.Security.Claims;

namespace PasteBin.Http.Auth;
public class UserContextService : IUserContextService
{
    public string? UserId { get; private set; }

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
