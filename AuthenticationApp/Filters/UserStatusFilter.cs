using AuthenticationApp.Application.Services.Persistance;
using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Domain.Models;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace AuthenticationApp.Filters
{
    public class UserStatusFilter : Attribute, IAsyncActionFilter
    {

        private readonly IUserRepository _userRepository;
        private readonly string _error;

        public UserStatusFilter(IUserRepository userRepository, string error)
        {
            _userRepository = userRepository;
            _error = error;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var email = context.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || user.Status != UserStatus.Active)
            {
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var errorBytes = Encoding.UTF8.GetBytes(_error);
                context.Result = new RedirectToActionResult("GetLoginPage", "Authentication", new { error = Convert.ToBase64String(errorBytes) });
            }
            else
                await next();
        }
    }
}
