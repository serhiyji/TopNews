using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Post;

namespace TopNews.Core.AutoMapper.Post
{
    public class AutoMapperPostProfile : Profile
    {
        public AutoMapperPostProfile()
        {
            CreateMap<Entities.Site.Post, PostDto>().ReverseMap();
        }
    }
}
