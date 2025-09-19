using EMSBackend.Models;

namespace EMSBackend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
