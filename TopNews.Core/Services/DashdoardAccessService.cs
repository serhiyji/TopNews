using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.Entities;
using TopNews.Core.Entities.Specifications;
using TopNews.Core.Interfaces;

namespace TopNews.Core.Services
{
    internal class DashdoardAccessService : INetworkAddressService
    {
        private readonly IRepository<NetworkAddress> _ipRepo;
        private readonly IMapper _mapper;
        public DashdoardAccessService(IRepository<NetworkAddress> ipRepo, IMapper mapper)
        {
            _ipRepo = ipRepo;
            _mapper = mapper;
        }

        public async Task Create(NetworkAddressDto model)
        {
            await _ipRepo.Insert(_mapper.Map<NetworkAddress>(model));
            await _ipRepo.Save();
        }

        public async Task Update(NetworkAddressDto model)
        {
            await _ipRepo.Update(_mapper.Map<NetworkAddress>(model));
            await _ipRepo.Save();
        }

        public async Task Delete(int id)
        {
            await _ipRepo.Delete(id);
            await _ipRepo.Save();
        }

        public async Task<NetworkAddressDto?> Get(string IpAddress)
        {
            return _mapper.Map<NetworkAddressDto>(await _ipRepo.GetItemBySpec(new DashdoardAccesSpacification.GetByIpAddress(IpAddress)));
        }

        public async Task<NetworkAddressDto?> Get(int id)
        {
            return _mapper.Map<NetworkAddressDto>(await _ipRepo.GetByID(id));
        }

        public async Task<List<NetworkAddressDto>> GetAll()
        {
            return _mapper.Map<List<NetworkAddressDto>>(await _ipRepo.GetAll());
        }
    }
}
