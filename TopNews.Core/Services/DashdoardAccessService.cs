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
    internal class DashdoardAccessService : IDashdoardAccessService
    {
        private readonly IRepository<DashdoardAccess> _ipRepo;
        private readonly IMapper _mapper;
        public DashdoardAccessService(IRepository<DashdoardAccess> ipRepo, IMapper mapper)
        {
            _ipRepo = ipRepo;
            _mapper = mapper;
        }

        public async Task Create(DashdoardAccessDto model)
        {
            await _ipRepo.Insert(_mapper.Map<DashdoardAccess>(model));
            await _ipRepo.Save();
        }

        public async Task Update(DashdoardAccessDto model)
        {
            await _ipRepo.Update(_mapper.Map<DashdoardAccess>(model));
            await _ipRepo.Save();
        }

        public async Task Delete(int id)
        {
            await _ipRepo.Delete(id);
            await _ipRepo.Save();
        }

        public async Task<DashdoardAccessDto?> Get(string IpAddress)
        {
            return _mapper.Map<DashdoardAccessDto>(await _ipRepo.GetItemBySpec(new DashdoardAccesSpacification.GetByIpAddress(IpAddress)));
        }

        public async Task<DashdoardAccessDto?> Get(int id)
        {
            return _mapper.Map<DashdoardAccessDto>(await _ipRepo.GetByID(id));
        }

        public async Task<List<DashdoardAccessDto>> GetAll()
        {
            return _mapper.Map<List<DashdoardAccessDto>>(await _ipRepo.GetAll());
        }
    }
}
