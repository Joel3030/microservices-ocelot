

namespace Middleware;

public interface IJwtBuilder
{
    string GetToken(string id);
    string ValidateToken(string token);
}