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

        public async Task Create(CategoryDto model)
        {
            await _categoryRepo.Insert(_mapper.Map<Category>(model));
            await _categoryRepo.Save();
        }

        public async Task Delete(int id)
        {
            CategoryDto? model = await Get(id);
            if (model == null) return;
            await _categoryRepo.Delete(id);
            await _categoryRepo.Save();
        }

        public async Task<CategoryDto?> Get(int id)
        {
            if (id < 0) return null;
            Category? category = await _categoryRepo.GetByID(id);
            if(category == null) return null;
            return _mapper.Map<CategoryDto?>(category);
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var result = await _categoryRepo.GetAll();
            return _mapper.Map<List<CategoryDto>>(result);
        }

        public async Task Update(CategoryDto model)
        {
            await _categoryRepo.Update(_mapper.Map<Category>(model));
            await _categoryRepo.Save();
        }

        public async Task<bool> IsNameCategoryInAllCategories(string NameCategory)
        {
            return (await this.GetAll()).Where(c => c.Name == NameCategory).Any();
        }
    }
}
