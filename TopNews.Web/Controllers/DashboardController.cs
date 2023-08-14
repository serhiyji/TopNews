﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.User;
using TopNews.Core.Services;
using TopNews.Core.Validation.User;
using TopNews.Web.Models.ViewModels;

namespace TopNews.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;

        public DashboardController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Signin / Signup / Sign out

        [AllowAnonymous] // GET
        public IActionResult SignIn()
        {
            bool userAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (userAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [AllowAnonymous] // HOST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserLoginDto model)
        {
            LoginUserValidation validator = new LoginUserValidation();
            ValidationResult valationReslt = validator.Validate(model);
            if (valationReslt.IsValid) 
            {
                ServiceResponse result = await _userService.LoginUserAsync(model);
                if (result.Success) 
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Message;
                return View(model);
            }
            ViewBag.AuthError = valationReslt.Errors[0];
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            ServiceResponse response = await _userService.SignOutAsync();
            if (response.Success)
            {
                return RedirectToAction(nameof(SignIn));
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<List<UsersDto>, object> result = await _userService.GetAllAsync();
            return View(result.Payload);
        }

        #region Profile page
        public async Task<IActionResult> Profile(string Id)
        {
            ServiceResponse<UpdateUserDto, object> result = await _userService.GetUpdateUserDtoByIdAsync(Id);
            if (result.Success)
            {
                UpdateProfileVM profile = new UpdateProfileVM()
                {
                    UserInfo = result.Payload,
                };
                return View(profile);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMainInfo(UpdateUserDto model)
        {
            var validator = new UpdateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse<object, IdentityError> result = await _userService.ChangeMainInfoUserAsync(model);
                if (result.Success)
                {
                    return View("Profile", new UpdateProfileVM() { UserInfo = model });
                }
                ViewBag.UserUpdateError = result.GetFirstError;
                return View("Profile", new UpdateProfileVM() { UserInfo = model });
            }
            ViewBag.UserUpdateError = validationResult.Errors[0];
            return View("Profile", new UpdateProfileVM() { UserInfo = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UpdatePasswordDto model)
        {
            var validator = new UpdatePasswordValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse<object, string> result = await _userService.ChangePasswordAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                ViewBag.UpdatePasswordError = result.Errors;
                return View(new UpdateProfileVM() { UserInfo = _userService.GetUpdateUserDtoByIdAsync(model.Id).Result.Payload });
            }
            ViewBag.UpdatePasswordError = validationResult.Errors[0];
            return View(new UpdateProfileVM() { UserInfo = _userService.GetUpdateUserDtoByIdAsync(model.Id).Result.Payload });
        }

        #endregion

        #region Users create, delete, edit

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            var validaor = new CreateUserValidation();
            var validationResult = await validaor.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse<object, IdentityError> response = await _userService.CreateUserAsync(model);
                if (response.Success)
                {
                    return RedirectToAction(nameof(GetAll));
                }
                else
                {
                    ViewBag.CreateUserError = response.GetFirstError;
                    return View();
                }
            }
            ViewBag.CreateUserError = validationResult.Errors[0];
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            ServiceResponse<DeleteUserDto, object> result = await _userService.GetDeleteUserDtoByIdAsync(id);
            return View(result.Payload);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteUserDto model)
        {
            ServiceResponse<object, IdentityError> result = await _userService.DeleteUserAsync(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.GetAllError = result.GetFirstError;
            return RedirectToAction(nameof(GetAll));
        }

        #endregion

        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            var result = await _userService.ConfirmEmailAsync(userid, token);
            if (result.Success)
            {
                return Redirect(nameof(SignIn));
            }
            return Redirect(nameof(SignIn));
        }

    }
}
