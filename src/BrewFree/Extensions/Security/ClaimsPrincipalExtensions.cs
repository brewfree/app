using System.Linq;
using System.Security.Claims;

namespace BrewFree.Extensions.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetApplicationUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
    }
}
