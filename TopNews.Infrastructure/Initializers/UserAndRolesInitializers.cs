﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Entities.User;
using TopNews.Infrastructure.Context;

namespace TopNews.Infrastructure.Initializers
{
    public class UserAndRolesInitializers
    {
        public static async Task SeedUserAndRole(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScore = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScore.ServiceProvider.GetService<AppDBContext>();
                UserManager<AppUser> userManager = serviceScore.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                if (userManager.FindByEmailAsync("admin@email.com").Result == null)
                {
                    AppUser admin = new AppUser()
                    {
                        FirstName = "John",
                        LastName = "Connor",
                        UserName = "admin@email.com",
                        Email = "admin@email.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+xx(xxx)xxx-xx-xx",
                        PhoneNumberConfirmed = true,
                    };

                    context.Roles.AddAsync(
                        new IdentityRole()
                        {
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        }
                    );
                    await context.SaveChangesAsync();

                    IdentityResult adminResult = userManager.CreateAsync(admin, "Qwerty-1").Result;
                    if (adminResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin, "Administrator").Wait();
                    }
                }
            }
        }
    }
}
