using AuthenticationApp.Domain.Models;
using System.Security.Claims;

namespace AuthenticationApp.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ClaimsPrincipal> LoginAsync(string email, string password);
        public Task<ClaimsPrincipal> RegisterAsync(User user);
    }
}