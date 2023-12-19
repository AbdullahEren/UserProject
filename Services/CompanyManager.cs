using AutoMapper;
using Entities.Dtos.CompanyDto;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CompanyManager : ICompanyService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CompanyManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task CreateCompanyAsync(CompanyForCreationDto companyDto)
        {
            if (companyDto == null)
            {
                throw new ArgumentNullException(nameof(companyDto));
            }
            var company = _mapper.Map<Company>(companyDto);

            await _manager.Company.CreateCompanyAsync(company);

            await _manager.SaveAsync();
        }

        public async Task DeleteCompanyAsync(int companyId,Company company)
        {
            var companyEntity = await _manager.Company.GetCompanyAsync(companyId, false);
            if (companyEntity == null)
            {
                throw new ArgumentNullException(nameof(companyEntity));
            }
            await _manager.Company.DeleteCompanyAsync(company);
            await _manager.SaveAsync();
            
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            var companies = await _manager.Company.GetAllCompaniesAsync(trackChanges);
            return companies;

        }

        public async Task<Company> GetCompanyAsync(int companyId, bool trackChanges)
        {
            var company = await _manager.Company.GetCompanyAsync(companyId, trackChanges);
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            return company;
        }

        public async Task UpdateCompanyAsync(int companyId, CompanyForUpdateDto companyDto)
        {
            var companyEntity = await _manager.Company.GetCompanyAsync(companyId, false);
            if (companyEntity == null)
            {
                throw new ArgumentNullException(nameof(companyEntity));
            }
            var company = _mapper.Map<Company>(companyDto);
            await _manager.Company.UpdateCompanyAsync(companyId, company);
            await _manager.SaveAsync();
        }
    }
}
