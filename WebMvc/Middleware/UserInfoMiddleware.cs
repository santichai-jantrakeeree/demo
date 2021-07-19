using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace WebMvc.Middleware
{
    public class UserInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userIdString = context.User.Claims.FirstOrDefault(o => o.Type == "UserId")?.Value;
                if(!string.IsNullOrEmpty(userIdString))
                {
                    unitOfWork.SetCurrentUserId(int.Parse(userIdString));
                }

                var email = context.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
                if (!string.IsNullOrEmpty(email))
                {
                    unitOfWork.SetCurrentUserEmail(email);
                }
            }

            await _next(context);
        }
    }
}