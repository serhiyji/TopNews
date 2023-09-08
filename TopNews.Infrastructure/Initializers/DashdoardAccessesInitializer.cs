using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Entities;

namespace TopNews.Infrastructure.Initializers
{
    internal static class DashdoardAccessesInitializer
    {
        public static void SeedDashdoardAccesses(this ModelBuilder model)
        {
            model.Entity<DashdoardAccess>().HasData(
                new DashdoardAccess()
                {
                    Id = 1,
                    IpAddress = "0.0.0.0",
                }
            );
        }
    }
}
