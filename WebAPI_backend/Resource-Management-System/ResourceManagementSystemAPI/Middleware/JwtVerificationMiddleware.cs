using Microsoft.IdentityModel.Tokens;

namespace ResourceManagementSystemAPI.Middleware
{
    public class JwtVerificationMiddleware
    {
        public static bool JwtLifetimeValidator(DateTime? notBefore,
                                         DateTime? expires,
                                         SecurityToken securityToken,
                                         TokenValidationParameters validationParameters)
        {
            if (!validationParameters.ValidateLifetime || (expires is null)) { return false; }
            return expires >= DateTime.UtcNow;
        }
    }
}
