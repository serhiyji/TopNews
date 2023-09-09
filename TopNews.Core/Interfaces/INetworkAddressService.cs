using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.Entities;
using TopNews.Core.Services;

namespace TopNews.Core.Interfaces
{
    public interface INetworkAddressService
    {
        Task<List<NetworkAddressDto>> GetAll();
        Task Create(NetworkAddressDto model);
        Task Update(NetworkAddressDto model);
        Task Delete(int id);
        Task<NetworkAddressDto?> Get(string IpAddress);
        Task<NetworkAddressDto?> Get(int id);
    }
}
