using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.Entities;

namespace TopNews.Core.Validation.Id
{
    public class CreateDashdoardAccessesValidation : AbstractValidator<NetworkAddressDto>
    {
        public CreateDashdoardAccessesValidation()
        {
            RuleFor(da => da.IpAddress).NotEmpty();
        }
    }
}
