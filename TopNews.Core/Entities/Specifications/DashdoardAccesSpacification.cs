using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Entities.Site;

namespace TopNews.Core.Entities.Specifications
{
    internal class DashdoardAccesSpacification
    {
        public class GetByIpAddress : Specification<DashdoardAccess>
        {
            public GetByIpAddress(string ipAdress)
            {
                Query.Where(da => da.IpAddress == ipAdress);
            }
        }
    }
}
