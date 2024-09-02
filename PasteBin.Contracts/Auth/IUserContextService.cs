namespace PasteBin.Contracts.Auth;
public interface IUserContextService
{
    string? UserId { get; }

    bool IsAuthenticated { get; }
}

