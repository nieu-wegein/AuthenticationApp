using AuthenticationApp.Application.Services.Persistance;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthenticationApp.Filters
{
    public class LastOnlineFilter : Attribute, IAsyncActionFilter
    {
        private readonly IUserRepository _userRepository;

        public LastOnlineFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var email = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (!String.IsNullOrEmpty(email))
                await _userRepository.UpdateOnlineAsync(email, DateTime.UtcNow);

            await next();
        }
    }
}
