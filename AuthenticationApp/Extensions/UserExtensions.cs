using AuthenticationApp.Domain.Models;
using AuthenticationApp.ViewModels;

namespace AuthenticationApp.Extensions
{
    public static class UserExtensions
    {
        public static UserViewModel ToViewModel(this User user)
        {

            return new UserViewModel(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.LastOnline,
                user.Status
            );
        }
    }
}
