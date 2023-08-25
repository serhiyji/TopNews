using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Post;

namespace TopNews.Core.Validation.Post
{
    public class CreatePostValidation : AbstractValidator<PostDto>
    {
        public CreatePostValidation()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title must not be empty");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description must not be empty");
            RuleFor(p => p.Text).NotEmpty().WithMessage("Text must not be empty");
        }
    }
}
