using AuthenticationApp.Application;
using AuthenticationApp.Application.Services;
using AuthenticationApp.Application.Services.UserService;
using AuthenticationApp.Contracts;
using AuthenticationApp.Domain.Models;
using AuthenticationApp.Extensions;
using AuthenticationApp.Filters;
using AuthenticationApp.Infrastructure.EntityFramework;
using AuthenticationApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationApp.Controllers
{
    [Route("")]
    [Authorize]
    [TypeFilter(typeof(LastOnlineFilter))]

    public class HomeController : Controller
    {
        private readonly IUserService _userSevice;
        private const string errorMessage = "You need to be active user to manipulate the table";

        public HomeController(IUserService userSevice)
        {
            _userSevice = userSevice;
        }

        [HttpGet]
        public async Task<IActionResult> GetIndexPage()
        {
            var userList = await _userSevice.GetAllAsync();
            var currentUser = await _userSevice.GetByEmailAsync(User.FindFirst(c => c.Type == ClaimTypes.Email).Value);

            var model = new HomeIndexViewModel()
            {
                FirstName = currentUser?.FirstName,
                Users = userList.ConvertAll(u => u.ToViewModel())
            };
            model.Users.Sort();
            return View("Index", model);
        }

        [HttpPut]
        [TypeFilter(typeof(UserStatusFilter), Arguments = new[] { errorMessage })]
        public async Task<IActionResult> ChangeUsersStatus([FromBody] ChangeStatusRequest req)
        {
            await _userSevice.ChangeStatusesAsync(req.EmailList, req.Status);
            return Ok();
        }

        [HttpDelete]
        [TypeFilter(typeof(UserStatusFilter), Arguments = new[] { errorMessage })]
        public async Task<IActionResult> DeleteUsers([FromBody] List<string> EmailList)
        {
            await _userSevice.DeleteRangeAsync(EmailList);
            return Ok();
        }
    }
}
