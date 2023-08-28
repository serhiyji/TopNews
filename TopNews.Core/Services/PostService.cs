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

namespace TopNews.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Post> _postRepo;
        public PostService(IMapper mapper, IRepository<Post> postRepo)
        {
            _mapper = mapper;
            _postRepo = postRepo;
        }
        public async Task Create(PostDto model)
        {
            var newPost = _mapper.Map<Post>(model);
            await _postRepo.Insert(newPost);
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
            var result = await _postRepo.GetListBySpec(new Posts.ByCategory(id));
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
            var result = await _postRepo.GetAll();
            return _mapper.Map<List<PostDto>>(result);
        }

        public async Task Update(PostDto model)
        {
            await _postRepo.Update(_mapper.Map<Post>(model));
            await _postRepo.Save();
        }
    }
}
