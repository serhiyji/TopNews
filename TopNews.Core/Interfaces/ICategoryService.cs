using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Entities.Site;
using TopNews.Core.Services;

namespace TopNews.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto?> Get(int id);
        Task<ServiceResponse> GetByName(CategoryDto model);
        Task Create(CategoryDto model);
        Task Update(CategoryDto model);
        Task Delete(int id);
        Task<bool> IsNameCategoryInAllCategories(string NameCategory);
    }
}
