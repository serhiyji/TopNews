using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return RedirectToAction("Index", "Dashboard");
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
            if (await _categoryService.IsNameCategoryInAllCategories(model.Name))
            {
                ViewBag.CreateCategoryError = $"category named {model.Name} already exists";
                return View();
            }
            await _categoryService.Create(model);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Update page
        public async Task<IActionResult> Update(int id)
        {
            CategoryDto? categotyinfo = await _categoryService.Get(id);
            return View(categotyinfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryDto model)
        {
            var validationResult = await new CreateCategoryValidation().ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                ViewBag.UpdateCategoryError = validationResult.Errors.FirstOrDefault();
                CategoryDto? category = await _categoryService.Get(model.Id);
                return View(category);
            }
            if (await _categoryService.IsNameCategoryInAllCategories(model.Name))
            {
                ViewBag.UpdateCategoryError = $"category named {model.Name} already exists";
                CategoryDto? category = await _categoryService.Get(model.Id);
                return View(category);
            }
            await _categoryService.Update(model);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            CategoryDto? category = await _categoryService.Get(id);
            if (category == null) 
            { 
                return RedirectToAction(nameof(GetAll));
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryDto model)
        {
            await _categoryService.Delete(model.Id);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

    }
}
