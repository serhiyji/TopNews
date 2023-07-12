using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.Validation.User;

namespace TopNews.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
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
        public IActionResult SignIn(TopNews.Core.DTOs.User.UserLoginDTO model)
        {
            LoginUserValidation validator = new LoginUserValidation();
            ValidationResult valationReslt = validator.Validate(model);
            if (valationReslt.IsValid) 
            { 
                return View();
            }
            ViewBag.AuthError = valationReslt.Errors[0];
            return View(model); 
        }
    }
}
