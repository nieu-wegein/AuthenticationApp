using AuthenticationApp.Application.Services.Persistance;
using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ClaimsPrincipal> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || password != user.Password)
                throw new Exception("Invalid email or password");
            if (user.Status != UserStatus.Active)
                throw new Exception("Your account has been blocked");

            return GetClaims(user);
        }

        public async Task<ClaimsPrincipal> RegisterAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                return GetClaims(user);
            }
            catch (Exception e)
            {
                throw new Exception("User with given email already exists");
            }
        }

        private ClaimsPrincipal GetClaims(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString()),
                new Claim(ClaimTypes.Name, user?.FirstName),
                new Claim(ClaimTypes.Surname, user?.LastName),
                new Claim(ClaimTypes.Email, user?.Email),
            };

            var identity = new ClaimsIdentity(claims, "Cookies");

            return new ClaimsPrincipal(identity);
        }
    }
}
