using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
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

        public async Task<ServiceResponse> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UsersDto> mappedUsers = users.Select(u => _mapper.Map<AppUser, UsersDto>(u)).ToList();
            
            for (int i = 0; i < mappedUsers.Count; i++)
            {
                mappedUsers[i].Role = string.Join(", ", _userManager.GetRolesAsync(users[i]).Result);
            }

            return new ServiceResponse()
            {
                Success = true,
                Message = "All Users loaded",
                Payload = mappedUsers
            };
        }
        public async Task<ServiceResponse> GetUserByIdAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "User or password incorect.",
                };
            }
            var mappedUser = _mapper.Map<AppUser, UpdateUserDto>(user);

            return new ServiceResponse()
            {
                Success = true,
                Message = "User succesfully loaded",
                Payload = mappedUser
            };
        }
    }
}
