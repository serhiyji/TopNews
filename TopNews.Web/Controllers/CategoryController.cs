using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;
using TopNews.Core.DTOs.User;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.User;
using X.PagedList;

namespace TopNews.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly UserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public CategoryController(UserService userService, ICategoryService categoryService, IPostService postService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _postService = postService;
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
            if (validationResult.IsValid)
            {
                var result = await _categoryService.GetByName(model);
                if (!result.Success)
                {
                    ViewBag.AuthError = "Category exists.";
                    return View(model);
                }
                await _categoryService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }
        #endregion

        #region Update page
        public async Task<IActionResult> Update(int id)
        {
            CategoryDto? categoryDto = await _categoryService.Get(id);
            if (categoryDto == null)
            {
                ViewBag.AuthError = "Category not found.";
                return View();
            }
            return View(categoryDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryDto model)
        {
            ServiceResponse result = await _categoryService.GetByName(model);
            if (!result.Success)
            {
                ViewBag.AuthError = "Category exists.";
                return View(model);
            }
            CreateCategoryValidation validator = new CreateCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _categoryService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            CategoryDto? categoryDto = await _categoryService.Get(id);

            if (categoryDto == null)
            {
                ViewBag.AuthError = "Category not found.";
                return RedirectToAction(nameof(GetAll));
            }

            List<PostDto> posts = await _postService.GetByCategory(id);
            ViewBag.CategoryName = categoryDto.Name;
            ViewBag.CategoryId = categoryDto.Id;

            int pageSize = 20;
            int pageNumber = 1;
            return View("Delete", posts.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(int Id)
        {
            await _categoryService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

    }
}
