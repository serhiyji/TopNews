using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;

namespace TopNews.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly UserService _userService;
        private readonly ICategoryService _categoryService;

        public CategoryController(UserService userService, ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), nameof(DashboardController));
        }

        #region Get All page
        public IActionResult GetAll()
        {
            List<CategoryDto> categories = _categoryService.GetAll().Result;
            return View(categories);
        }
        #endregion

        #region Create page
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(CategoryDto model) 
        {
            return View();
        }
        #endregion

        #region Update page
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Update(CategoryDto model)
        {
            return View();
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            return View();
        }
        #endregion

    }
}
