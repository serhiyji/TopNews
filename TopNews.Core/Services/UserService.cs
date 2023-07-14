using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ServiceResponse> LoginUserAsync(UserLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "User or password incorect.",
                };
            }
            else
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return new ServiceResponse() 
                    {
                        Success = true,
                        Message = "User successfully loged in."
                    };
                }
                if (result.IsNotAllowed)
                {
                    return new ServiceResponse()
                    {
                        Success = false,
                        Message = "Confirm your email please"
                    };
                }
                if (result.IsLockedOut)
                {
                    return new ServiceResponse()
                    {
                        Success = false,
                        Message = "Useris locked. Connect with your site admistrator."
                    };
                }
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "User or password incorect",
                };
            }
        }
        public async Task<ServiceResponse> SignOutAsync()
        {
            await _signInManager.SignOutAsync(); 
            return new ServiceResponse()
            {
                Success = true,
            };
        }
    }
}
