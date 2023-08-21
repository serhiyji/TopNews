using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.Entities.Site
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDateTime { get; set; }
        public int IdCategory { get; set; }
        public Category Category { get; set; }
        public string NameImage { get; set; }
    }
}
