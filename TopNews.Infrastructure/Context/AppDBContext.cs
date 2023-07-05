using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Infrastructure.Context
{
    internal class AppDBContext : IdentityDbContext
    {
        public AppDBContext() : base() { }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
    }
}
