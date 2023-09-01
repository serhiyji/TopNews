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
        public async Task<IActionResult> GetAll(int? page)
        {
            List<PostDto> posts = (await _postService.GetAll()).OrderByDescending(p => p.Id).ToList();
            int pageSize = 20;
            int pageNumber = (page ?? 1);
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id)
        {
            var posts = await _postService.Get(id);
            if (posts == null) return NotFound();
            await LoadCategories();
            return View(posts);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PostDto model)
        {
            var validationResult = await new CreatePostValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                model.File = files;
                await _postService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.CreatePostError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }
        #endregion

        #region Delete
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            PostDto? postDto = await _postService.Get(id);
            if (postDto == null)
            {
                ViewBag.AuthError = "Post not found.";
                return View();
            }
            return View(postDto);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _postService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        private async Task LoadCategories()
        {
            List<CategoryDto> result = await _categoryService.GetAll();
            @ViewBag.CategoryList = new SelectList((System.Collections.IEnumerable)result,
                nameof(CategoryDto.Id), nameof(CategoryDto.Name)
              );
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostsByCategory(int id)
        {
            List<PostDto> posts = await _postService.GetByCategory(id);
            int pageSize = 20;
            int pageNumber = 1;
            return View("GetAll", posts.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([FromForm] string searchString)
        {
            List<PostDto> posts = await _postService.Search(searchString);
            int pageSize = 20;
            int pageNumber = 1;
            return View("GetAll", posts.ToPagedList(pageNumber, pageSize));
        }
    }
}
