using AuthenticationApp.Application;
using AuthenticationApp.Application.Services.Authentication;
using AuthenticationApp.Application.Services.Persistance;
using AuthenticationApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            return services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
