using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Domain.Models;

namespace AuthenticationApp.Application.Services.UserService
{
    public interface IUserService
    {
        Task ChangeStatusesAsync(List<string> EmailList, UserStatus status);
        Task DeleteRangeAsync(List<string> EmailList);
        Task<List<User>> GetAllAsync();
        Task<User> GetByEmailAsync(string email);
    }
}