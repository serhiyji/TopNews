using Microsoft.AspNetCore.Mvc;

namespace TopNews.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
