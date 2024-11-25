using AuthenticationApp.Application.Services.Persistance;
using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Domain.Models;
using AuthenticationApp.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStatusesAsync(List<string> EmailList, UserStatus status)
        {
            foreach (var email in EmailList)
            {
                if (await GetByEmailAsync(email) is User user)
                    user.Status = status;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOnlineAsync(string email, DateTime online)
        {
            if (await GetByEmailAsync(email) is User user)
                user.LastOnline = online;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(List<string> EmailList)
        {
            List<User> users = new List<User>();

            foreach (var email in EmailList)
            {
                if (await GetByEmailAsync(email) is User user)
                    users.Add(user);
            }

            _dbContext.Users.RemoveRange(users);
            await _dbContext.SaveChangesAsync();
        }
    }
}
