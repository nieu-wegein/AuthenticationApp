using AuthenticationApp.Application.Services.Authentication;
using AuthenticationApp.Contracts;
using AuthenticationApp.Extensions;
using AuthenticationApp.Filters;
using AuthenticationApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Text;
using System.Web;
using IAuthenticationService = AuthenticationApp.Application.Services.Authentication.IAuthenticationService;

namespace AuthenticationApp.Controllers
{
    [Route("")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet("login")]
        public IActionResult GetLoginPage([FromQuery] string error)
        {
            string stringError;
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("GetIndexPage", "Home");

            try
            {
                byte[] data = Convert.FromBase64String(error ?? "");
                stringError = Encoding.UTF8.GetString(data);
            }
            catch
            {
                stringError = "Something wrong";
            }

            return View("Login", stringError);
        }

        [HttpGet("registration")]
        public IActionResult GetRegistrationPage()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("GetIndexPage", "Home");

            return View("Registration");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("GetLoginPage");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var claims = await _authService.LoginAsync(req.Email, req.Password);
            await HttpContext.SignInAsync("Cookies", claims);

            return RedirectToAction("GetIndexPage", "Home");
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegistrationRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = req.ToDomanModel();
            var claims = await _authService.RegisterAsync(user);
            await HttpContext.SignInAsync("Cookies", claims);

            return RedirectToAction("GetIndexPage", "Home");
        }
    }
}
