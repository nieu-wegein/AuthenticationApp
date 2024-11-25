using AuthenticationApp.Application.Services.Authentication;
using AuthenticationApp.Application.Services.UserService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            return services.AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IUserService, UserService>();
        }
    }
}
