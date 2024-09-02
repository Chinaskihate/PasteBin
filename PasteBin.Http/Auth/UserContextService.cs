using Microsoft.AspNetCore.Http;
using PasteBin.Contracts.Auth;
using System.Security.Claims;

namespace PasteBin.Http.Auth;
public class UserContextService : IUserContextService
{
    public string? UserId { get; private set; }

    public bool IsAuthenticated => string.IsNullOrEmpty(UserId);

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
