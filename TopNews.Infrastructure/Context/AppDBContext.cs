using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Entities.User;
using TopNews.Infrastructure.Initializers;

namespace TopNews.Infrastructure.Context
{
    internal class AppDBContext : IdentityDbContext
    {
        public AppDBContext() : base() { }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<AppUser> AppUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*var userManager = this.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
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
            this.Roles.AddAsync(
                new IdentityRole()
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
            this.SaveChanges();
            IdentityResult adminResult = userManager.CreateAsync(admin, "Qwerty-1").Result;
            if (adminResult.Succeeded)
            {
                userManager.AddToRoleAsync(admin, "Administrator").Wait();
            }*/
        }
    }
}
