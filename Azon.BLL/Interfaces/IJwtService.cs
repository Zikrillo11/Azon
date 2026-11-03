using Azon.Domain.Entities;

namespace Azon.BLL.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
}