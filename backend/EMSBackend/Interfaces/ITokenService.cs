using EMSBackend.Models;

namespace EMSBackend.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
