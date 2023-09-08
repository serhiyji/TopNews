using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.Id;
using X.PagedList;

namespace TopNews.Web.Controllers
{
    public class DashdoardAccessesController : Controller
    {
        private readonly IDashdoardAccessService _ipService;
        public DashdoardAccessesController(IDashdoardAccessService dashdoardAccessService)
        {
            _ipService = dashdoardAccessService;
        }
        public IActionResult Index()
        {
            return RedirectToAction(nameof(GetAll));
        }

        #region Get All page
        public async Task<IActionResult> GetAll()
        {
            List<DashdoardAccessDto> ips = await _ipService.GetAll();
            return View(ips);
        }
        #endregion

        #region Create page
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DashdoardAccessDto model)
        {
            var validationResult = await new CreateDashdoardAccessesValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                DashdoardAccessDto? result = await _ipService.Get(model.IpAddress);
                if (result == null)
                {
                    ViewBag.AuthError = "DashdoardAccesses exists.";
                    return View(model);
                }
                _ipService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            DashdoardAccessDto? model = await _ipService.Get(id);
            if (model == null)
            {
                ViewBag.AuthError = "Category not found.";
                return RedirectToAction(nameof(GetAll));
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(int Id)
        {
            _ipService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion
    }
}
