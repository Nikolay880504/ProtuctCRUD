using System.Security.Claims;

namespace FIrstProductCRUD.Extensions
{
    public static class HttpContextExtension
    {
        public static int GetUserIdOrDefault(this HttpContext httpContext)
        {
            var user = httpContext.User;

            if (user.Identity.IsAuthenticated && int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int parseId))
            {
                return parseId;
            }

            return default;
        }
    }
}
