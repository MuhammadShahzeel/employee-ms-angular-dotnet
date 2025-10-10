using System.Security.Claims;

namespace EMSBackend.Interfaces
{
    public interface IUserContextService
    {
        Task<int?> GetEmployeeIdFromClaimsAsync(ClaimsPrincipal user);
        Task<string?> GetUserIdFromClaimsAsync(ClaimsPrincipal User);
        bool IsAdmin(ClaimsPrincipal User);

    }
}
