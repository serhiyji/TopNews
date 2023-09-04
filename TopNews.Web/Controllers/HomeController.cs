using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Web.Models;
using X.PagedList;

namespace TopNews.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        public HomeController(IPostService postService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            List<PostDto> posts = (await _postService.GetAll()).OrderByDescending(p => p.Id).ToList();
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View("Index", posts.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> PostsByCategory(int id)
        {
            List<PostDto> posts = (await _postService.GetByCategory(id)).OrderByDescending(p => p.Id).ToList();
            int pageSize = 20;
            int pageNumber = 1;
            return View("Index", posts.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromForm] string searchString)
        {
            List<PostDto> posts = (await _postService.Search(searchString)).OrderByDescending(p => p.Id).ToList();
            int pageSize = 20;
            int pageNumber = 1;
            return View("Index", posts.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> PostPage(int? id)
        {
            PostDto? post = await _postService.Get(id ?? 0);
            if (post == null)
            {
                return RedirectToAction(nameof(Index));
            }
            post.CategoryName = (await _categoryService.Get(post.CategoryId)).Name;
            return View(post);
        }

        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                default:
                    return View("Error");
            }
        }
    }
}