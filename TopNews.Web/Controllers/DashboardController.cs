using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.DTOs.User;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.User;
using TopNews.Web.Models.ViewModels;

namespace TopNews.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;
        private readonly INetworkAddressService _networkAddressService;

        public DashboardController(UserService userService, INetworkAddressService networkAddressService)
        {
            _userService = userService;
            _networkAddressService = networkAddressService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Sign In page
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync()
        {
            string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            NetworkAddressDto? networkAddressDto = await _networkAddressService.Get(ipAddress);
            if (networkAddressDto == null)
            {
                return NotFound();
            }
            bool userAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (userAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserLoginDto model)
        {
            LoginUserValidation validator = new LoginUserValidation();
            ValidationResult valationReslt = await validator.ValidateAsync(model);
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
            ViewBag.AuthError = valationReslt.Errors.FirstOrDefault();
            return View(model); 
        }
        #endregion

        #region Log out page
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            ServiceResponse response = await _userService.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Get all users page
        public async Task<IActionResult> GetAll()
        {
            ServiceResponse<List<UsersDto>, object> result = await _userService.GetAllAsync();
            return View(result.Payload);
        }
        #endregion

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
            UpdateUserValidation validator = new UpdateUserValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.ChangeMainInfoUserAsync(model);
                if (result.Success)
                {
                    return View("Profile", new UpdateProfileVM() { UserInfo = model });
                }
                ViewBag.UserUpdateError = result.Errors.FirstOrDefault();
                return View("Profile", new UpdateProfileVM() { UserInfo = model });
            }
            ViewBag.UserUpdateError = validationResult.Errors[0];
            return View("Profile", new UpdateProfileVM() { UserInfo = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswordInfo(UpdatePasswordDto model)
        {
            UpdatePasswordValidation validator = new UpdatePasswordValidation();
            ValidationResult validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.ChangePasswordAsync(model);
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

        #region Create user page
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserDto model)
        {
            CreateUserValidation validaor = new CreateUserValidation();
            ValidationResult validationResult = await validaor.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse response = await _userService.CreateUserAsync(model);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.CreateUserError = response.Errors.FirstOrDefault();
                return View();
            }
            ViewBag.CreateUserError = validationResult.Errors.FirstOrDefault();
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            ServiceResponse result = await _userService.ConfirmEmailAsync(userid, token);
            return Redirect(nameof(SignIn));
        }
        #endregion

        #region Delete user page
        public async Task<IActionResult> DeleteUser(string id)
        {
            ServiceResponse<DeleteUserDto, object> result = await _userService.GetDeleteUserDtoByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View(nameof(GetAll));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(DeleteUserDto model)
        {
            ServiceResponse result = await _userService.DeleteUserAsync(model);
            if (!result.Success)
            {
                ViewBag.GetAllError = result.Errors.FirstOrDefault();
            }
            return RedirectToAction(nameof(GetAll));
        }
        #endregion

        #region Forgot password page
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            ServiceResponse result = await _userService.ForgotPasswordAsync(email);
            if (result.Success)
            {
                ViewBag.AuthError = "Check your email.";
                return View(nameof(SignIn));
            }
            ViewBag.AuthError = "Something went wrong.";
            return View();
        }
        #endregion

        #region Reset password page
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(PasswordRecoveryDto model)
        {
            ServiceResponse result = await _userService.ResetPasswordAsync(model);
            ValidationResult resultValidation = await new PasswordRecoveryValidation().ValidateAsync(model);
            ViewBag.Email = model.Email;
            ViewBag.Token = model.Token;
            if (!resultValidation.IsValid)
            {
                ViewBag.AuthError = resultValidation.Errors.FirstOrDefault();
                return View(nameof(ResetPassword));
            }
            if (result.Success)
            {
                ViewBag.AuthError = result.Message;
                return View(nameof(SignIn));
            }
            ViewBag.AuthError = result.Errors.Any() ? result.Errors.FirstOrDefault() : result.Message;
            return View();
        }
        #endregion

        #region Edit other user page
        public async Task<IActionResult> EditUser(string id)
        {

            ServiceResponse<EditUserDto, object> result = await _userService.GetEditUserDtoByIdAsync(id);
            if (result.Success)
            {
                await LoadRoles();
                return View(result.Payload);
            }
            return View(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDto model)
        {
            ValidationResult validationResult = await new EditUserValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.EditUserAsync(model);
                if (result.Success)
                {
                    return View(nameof(Index));
                }
                return View(nameof(Index));
            }
            await LoadRoles();
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(nameof(EditUser));
        }

        private async Task LoadRoles()
        {
            List<IdentityRole> result = await _userService.GetAllRolesAsync();
            @ViewBag.RoleList = new SelectList((System.Collections.IEnumerable)result, 
                nameof(IdentityRole.Name), nameof(IdentityRole.Name)
              );
        }
        #endregion
    }
}
