using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }

    }
}
