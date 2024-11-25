using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Application.Services.Persistance
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateStatusesAsync(List<string> emailList, UserStatus status);
        Task UpdateOnlineAsync(string email, DateTime dt);
        Task DeleteRangeAsync(List<string> emailList);
    }
}
