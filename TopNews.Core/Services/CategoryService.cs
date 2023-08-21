using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Entities.Site;
using TopNews.Core.Interfaces;

namespace TopNews.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _categoryRepo;
        public CategoryService(IMapper mapper, IRepository<Category> categoryRepo)
        {
            this._mapper = mapper;
            this._categoryRepo = categoryRepo;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var result = await _categoryRepo.GetAll();
            return _mapper.Map<List<CategoryDto>>(result);
        }
    }
}
