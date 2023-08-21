using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopNews.Core.Entities.Site
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
