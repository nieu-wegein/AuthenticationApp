using AuthenticationApp.Application;
using AuthenticationApp.Domain.Enums;
using AuthenticationApp.Infrastructure;
using AuthenticationApp.Infrastructure.EntityFramework;
using AuthenticationApp.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
    builder.Services.AddAuthorization();
    builder.Services.AddDbContext<UserDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    builder.Services.RegisterApplication();
    builder.Services.RegisterInfrastructure();
}


var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();
