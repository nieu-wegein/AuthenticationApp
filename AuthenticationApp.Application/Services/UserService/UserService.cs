using AuthenticationApp.Application.Services.Persistance;
using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user;
        }

        public async Task ChangeStatusesAsync(List<string> emailList, UserStatus status)
        {
            await _userRepository.UpdateStatusesAsync(emailList, status);
        }

        public async Task DeleteRangeAsync(List<string> emailList)
        {
            await _userRepository.DeleteRangeAsync(emailList);
        }
    }
}
