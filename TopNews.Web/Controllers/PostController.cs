using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Entities.Site;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.Post;

namespace TopNews.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }
        #region Get All page
        public async Task<IActionResult> GetAll()
        {
            List<PostDto> posts = await _postService.GetAll();
            return View(posts);
        }
        #endregion

        #region Create page
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostDto model)
        {
            var validationResult = await new CreatePostValidation().ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                ViewBag.CreatePostError = validationResult.Errors.FirstOrDefault();
                return View();
            }
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
            var validationResult = await new CreatePostValidation().ValidateAsync(model);
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
    }
}
