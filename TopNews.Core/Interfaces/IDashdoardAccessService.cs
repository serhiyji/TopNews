﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.Entities;
using TopNews.Core.Services;

namespace TopNews.Core.Interfaces
{
    public interface IDashdoardAccessService
    {
        Task<List<DashdoardAccessDto>> GetAll();
        void Create(DashdoardAccessDto model);
        void Delete(int id);
        Task<DashdoardAccessDto?> Get(string IpAddress);
        Task<DashdoardAccessDto?> Get(int id);
    }
}