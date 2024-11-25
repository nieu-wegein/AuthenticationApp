using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthenticationApp.Models
{
    public record RegistrationRequest(
        [BindRequired] string FirstName,
        [BindRequired] string LastName,
        [BindRequired] string Email,
        [BindRequired] string Password)
    {
        public User ToDomanModel()
        {
            return new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Status = UserStatus.Active
            };
        }
    }
}
