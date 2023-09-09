using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.Id;
using TopNews.Core.Validation.Post;
using X.PagedList;

namespace TopNews.Web.Controllers
{
    public class NetworkAddressController : Controller
    {
        private readonly INetworkAddressService _ipService;
        public NetworkAddressController(INetworkAddressService ipService)
        {
            _ipService = ipService;
        }
        public IActionResult Index()
        {
            return RedirectToAction(nameof(GetAll));
        }

        #region Get All page
        public async Task<IActionResult> GetAll()
        {
            List<NetworkAddressDto> ips = await _ipService.GetAll();
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
        public async Task<IActionResult> Create(NetworkAddressDto model)
        {
            var validationResult = await new CreateDashdoardAccessesValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                NetworkAddressDto? result = await _ipService.Get(model.IpAddress);
                if (result != null)
                {
                    ViewBag.AuthError = "DashdoardAccesses exists.";
                    return View(model);
                }
                await _ipService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }
        #endregion

        #region Update page
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id)
        {
            var ip = await _ipService.Get(id);
            if (ip == null) return NotFound();
            return View(ip);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(NetworkAddressDto model)
        {
            var validationResult = await new CreateDashdoardAccessesValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _ipService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }
        #endregion

        #region Delete
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            NetworkAddressDto? model = await _ipService.Get(id);
            if (model == null)
            {
                ViewBag.AuthError = "IP not found.";
                return RedirectToAction(nameof(GetAll));
            }
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            await _ipService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }
        #endregion
    }
}
