using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.Services;
using TopNews.Core.Validation.User;

namespace TopNews.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;

        public DashboardController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] // GET
        public IActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous] // HOST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(TopNews.Core.DTOs.User.UserLoginDTO model)
        {
            LoginUserValidation validator = new LoginUserValidation();
            ValidationResult valationReslt = validator.Validate(model);
            if (valationReslt.IsValid) 
            {
                ServiceResponse result = await _userService.LoginUserAsync(model);
                if (result.Success) 
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Message;
                return View(model);
            }
            ViewBag.AuthError = valationReslt.Errors[0];
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            ServiceResponse response = await _userService.SignOutAsync();
            if (response.Success)
            {
                return RedirectToAction(nameof(SignIn));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
