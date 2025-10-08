using System.Security.Claims;

namespace EMSBackend.Interfaces
{
    public interface IUserContextService
    {
        Task<int?> GetEmployeeIdFromClaimsAsync(ClaimsPrincipal user);
    }
}
