using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Core.Entities.Site;
using TopNews.Core.DTOs.Category;
using Microsoft.Extensions.Hosting;
using TopNews.Core.Entities.Specifications;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace TopNews.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostService(IMapper mapper, IRepository<Post> postRepo,  IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IRepository<Category> categoryRepo)
        {
            _mapper = mapper;
            _postRepo = postRepo;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepo = categoryRepo;
        }
        public async Task Create(PostDto model)
        {
            if (model.File.Count > 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + _configuration.GetValue<string>("ImageSettings:ImagePath");
                IFormFileCollection files = model.File;
                string fileName = Guid.NewGuid().ToString();
                string extansions = Path.GetExtension(files[0].FileName);
                using (FileStream fileStream = new FileStream(Path.Combine(upload, fileName + extansions), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                model.ImagePath = fileName + extansions;
            }
            else
            {
                model.ImagePath = "Default.png";
            }

            DateTime currentDate = DateTime.Today;
            string formatedDate = currentDate.ToString("d");
            model.PublishDate = formatedDate;
            Post newpost = _mapper.Map<Post>(model);
            newpost.Category = null;
            await _postRepo.Insert(newpost);
            await _postRepo.Save();
        }

        public async Task Delete(int id)
        {
            PostDto? model = await Get(id);
            if (model == null) return;
            await _postRepo.Delete(id);
            await _postRepo.Save();
        }

        public async Task<List<PostDto>> GetByCategory(int id)
        {
            IEnumerable<Post> result = await _postRepo.GetListBySpec(new Posts.ByCategory(id));
            return _mapper.Map<List<PostDto>>(result);
        }

        public async Task<PostDto?> Get(int id)
        {
            if (id < 0) return null;
            Post? post = await _postRepo.GetByID(id);
            if (post == null) return null;
            return _mapper.Map<PostDto?>(post);
        }

        public async Task<List<PostDto>> GetAll()
        {
            IEnumerable<Post> result = await _postRepo.GetAll();
            return _mapper.Map<List<PostDto>>(result);
        }

        public async Task Update(PostDto model)
        {
            await _postRepo.Update(_mapper.Map<Post>(model));
            await _postRepo.Save();
        }
    }
}
