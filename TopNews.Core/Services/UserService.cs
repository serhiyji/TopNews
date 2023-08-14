using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.User;
using TopNews.Core.Entities.User;

namespace TopNews.Core.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, EmailService emailService, IMapper mapper, IConfiguration configuration)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._emailService = emailService;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        #region Signin / Signup / Sign out

        public async Task<ServiceResponse> LoginUserAsync(UserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse(false, "User or password incorect.");
            }
            else
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return new ServiceResponse(true, "User successfully loged in.");
                }
                if (result.IsNotAllowed)
                {
                    return new ServiceResponse(false, "Confirm your email please");
                }
                if (result.IsLockedOut)
                {
                    return new ServiceResponse(false, "Useris locked. Connect with your site admistrator.");
                }
                return new ServiceResponse(false, "User or password incorect");
            }
        }
        public async Task<ServiceResponse> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return new ServiceResponse(true);
        }

        #endregion

        #region Get users mapped

        private async Task<(bool Success, AppUser user)> GetAppUserByIdAsyck(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return user == null ? (false, null) : (true, user);
        }
        private async Task<ServiceResponse<Mapped, object>> GetMappedUserById<Mapped>(string Id)
        {
            var result = await this.GetAppUserByIdAsyck(Id);
            return (result.Success) ?
                new ServiceResponse<Mapped, object>(true, "User succesfully loaded", payload: _mapper.Map<AppUser, Mapped>(result.user)) :
                new ServiceResponse<Mapped, object>(false, "User not found");
        }
        public async Task<ServiceResponse<List<UsersDto>, object>> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UsersDto> mappedUsers = users.Select(u => _mapper.Map<AppUser, UsersDto>(u)).ToList();

            for (int i = 0; i < mappedUsers.Count; i++)
            {
                mappedUsers[i].Role = string.Join(", ", _userManager.GetRolesAsync(users[i]).Result);
            }

            return new ServiceResponse<List<UsersDto>, object>(true, "All Users loaded", payload: mappedUsers);
        }
        public async Task<ServiceResponse<UpdateUserDto, object>> GetUpdateUserDtoByIdAsync(string Id) => this.GetMappedUserById<UpdateUserDto>(Id).Result;
        public async Task<ServiceResponse<DeleteUserDto, object>> GetDeleteUserDtoByIdAsync(string Id) => this.GetMappedUserById<DeleteUserDto>(Id).Result;

        #endregion

        #region Change data users

        public async Task<ServiceResponse<object, string>> ChangePasswordAsync(UpdatePasswordDto model)
        {
            AppUser user = _userManager.FindByIdAsync(model.Id).Result;
            if (user == null) return new ServiceResponse<object, string>(false, "User or password incorrect.");

            IdentityResult result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).Result;
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return new ServiceResponse<object, string>(true, "Password successfully updated");
            }
            return new ServiceResponse<object, string>(false, "Error.", errors: result.Errors.ToList().Select(i => i.Description));
        }

        public async Task<ServiceResponse<object, IdentityError>> ChangeMainInfoUserAsync(UpdateUserDto newinfo)
        {
            AppUser user = await _userManager.FindByIdAsync(newinfo.Id);

            if (user != null)
            {
                user.FirstName = newinfo.FirstName;
                user.LastName = newinfo.LastName;
                user.Email = newinfo.Email;
                user.PhoneNumber = newinfo.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(user);

                return (result.Succeeded) ?
                    new ServiceResponse<object, IdentityError>(true, "Information has been changed") :
                    new ServiceResponse<object, IdentityError>(false, "Something went wrong", errors: result.Errors);
            }
            return new ServiceResponse<object, IdentityError>(false, "Not found user");
        }

        #endregion

        #region Create, Delete, Edit user

        public async Task<ServiceResponse<string, object>> GetEmailById(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);
            return (user != null) ?
                new ServiceResponse<string, object>(true, "", payload: user.Email) :
                new ServiceResponse<string, object>(false);
        }

        public async Task<ServiceResponse<object, IdentityError>> CreateUserAsync(CreateUserDto model)
        {
            AppUser NewUser = _mapper.Map<CreateUserDto, AppUser>(model);
            IdentityResult result = await _userManager.CreateAsync(NewUser);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(NewUser, model.Role);
                await SendConfirmationEmailAsync(NewUser);
                return new ServiceResponse<object, IdentityError>(true, "User has been added");
            }
            else
            {
                return new ServiceResponse<object, IdentityError>(false, "Something went wrong", errors: result.Errors);
            }
        }

        public async Task<ServiceResponse<object, IdentityError>> DeleteUserAsync(DeleteUserDto model)
        {
            AppUser userdelete = await _userManager.FindByIdAsync(model.Id);
            if (userdelete != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(userdelete);
                if (result.Succeeded)
                {
                    return new ServiceResponse<object, IdentityError>(true);
                }
                else
                {
                    return new ServiceResponse<object, IdentityError>(false, "something went wrong", errors: result.Errors);
                }
            }
            return new ServiceResponse<object, IdentityError>(false, "User a was found");
        }

        #endregion

        #region Email
        public async Task SendConfirmationEmailAsync(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            var url = $"{_configuration["HostSettings:URL"]}/Dashboard/confirmemail?userid={user.Id}&token={validEmailToken}";

            string emailBody = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now!</a>";
            await _emailService.SendEmailAsync(user.Email, "Email confirmation.", emailBody);
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse(false, "User not found");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string narmalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, narmalToken);
            if (result.Succeeded)
            {
                return new ServiceResponse(true, "Email successfully confirmed.");
            }
            return new ServiceResponse(false, "Email not confirmed", errors: result.Errors.Select(e => e.Description));
        }
        #endregion
    }
}
