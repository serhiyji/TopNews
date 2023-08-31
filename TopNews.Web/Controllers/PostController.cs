using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Entities.Site;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.Post;
using X.PagedList;

namespace TopNews.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        public PostController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }
        #region Get All page
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            List<PostDto> posts = (await _postService.GetAll()).OrderByDescending(p => p.Id).ToList();
            int pageSize = 20;
            int pageNumber = 1;
            return View("GetAll", posts.ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region Create page
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            await LoadCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(PostDto model)
        {
            CreatePostValidation validator = new CreatePostValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
                return View();
            }
            IFormFileCollection files = HttpContext.Request.Form.Files;
            model.File = files;
            await _postService.Create(model);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Update page
        public async Task<IActionResult> Update(int id)
        {
            PostDto? postinfo = await _postService.Get(id);
            return View(postinfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PostDto model)
        {
            ValidationResult validationResult = await new CreatePostValidation().ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                ViewBag.UpdatePostError = validationResult.Errors.FirstOrDefault();
                PostDto? post = await _postService.Get(model.Id);
                return View(post);
            }
            await _postService.Update(model);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            PostDto? post = await _postService.Get(id);
            if (post == null)
            {
                return RedirectToAction(nameof(GetAll));
            }
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PostDto model)
        {
            await _postService.Delete(model.Id);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        private async Task LoadCategories()
        {
            List<CategoryDto> result = await _categoryService.GetAll();
            @ViewBag.CategoriesList = new SelectList((System.Collections.IEnumerable)result,
                nameof(CategoryDto.Id), nameof(CategoryDto.Name)
              );
        }
    }
}
