using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.User;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.User;

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
        public async Task<IActionResult> GetAll()
        {
            List<CategoryDto> categories =  await _categoryService.GetAll();
            return View(categories);
        }
        #endregion

        #region Create page
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto model) 
        {
            CreateCategoryValidation validator = new CreateCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                ViewBag.CreateCategoryError = validationResult.Errors.FirstOrDefault();
                return View();
            }
            await _categoryService.Create(model);

            return RedirectToAction(nameof(GetAll));
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
