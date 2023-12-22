using AutoMapper;
using Entities.Dtos.CompanyDto;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RepositoryContext _context;
        private readonly ICacheService _cache;

        public CompanyManager(IRepositoryManager manager, IMapper mapper, RepositoryContext context, UserManager<ApplicationUser> userManager, ICacheService cache)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _cache = cache;
        }

        public async Task CreateCompanyAsync(CompanyForCreationDto companyDto)
        {
            if (companyDto == null) 
                throw new CompanyNotFoundException();

            var company = _mapper.Map<Company>(companyDto);
            await _manager.Company.CreateCompanyAsync(company);
            await _manager.SaveAsync();
            await _cache.RemoveAsync("AllCompanies"); 
            await _cache.SetAsync($"Company:{company.CompanyId}", company);
        }

        public async Task DeleteCompanyAsync(int companyId)
        {
            var companyEntity = await _cache.GetAsync($"Company:{companyId}", async () =>
            {
                var company = await _manager.Company.GetCompanyAsync(companyId, false);
                if (company == null) 
                    throw new CompanyNotFoundException(companyId);
                return company;
            });
            var usersWithCompany = await _context.Users.Where(u => u.CompanyId == companyId).ToListAsync();
            foreach (var user in usersWithCompany)
            {
                user.CompanyId = null;
                await _cache.RemoveAsync($"User:{user.UserName}");
            }
            companyEntity.CompanyId = companyId;
            await _manager.Company.DeleteCompanyAsync(companyEntity);
            await _manager.SaveAsync();
            await _cache.RemoveAsync($"Company:{companyId}");
            await _cache.RemoveAsync("AllCompanies");
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await _cache.GetAsync("AllCompanies", async () =>
            {
                var companies = await _manager.Company.GetAllCompaniesAsync(trackChanges);
                return companies;
            });
        }

        public async Task<Company> GetCompanyAsync(int companyId, bool trackChanges)
        {
            return await _cache.GetAsync($"Company:{companyId}", async () =>
            {
                var company = await _manager.Company.GetCompanyAsync(companyId, trackChanges);
                if (company == null) 
                    throw new CompanyNotFoundException(companyId);
                return company;
            });
        }

        public async Task UpdateCompanyAsync(int companyId, CompanyForUpdateDto companyDto)
        {
            var companyEntity = await _cache.GetAsync($"Company:{companyId}", async () =>
            {
                var company = await _manager.Company.GetCompanyAsync(companyId, false);
                if (company == null) 
                    throw new CompanyNotFoundException(companyId);
                return company;
            });
            var usersWithCompany = await _context.Users.Where(u => u.CompanyId == companyId).ToListAsync();
            foreach (var user in usersWithCompany)
                await _cache.RemoveAsync($"User:{user.UserName}");

            var company = _mapper.Map<Company>(companyDto);
            await _manager.Company.UpdateCompanyAsync(companyId, company);
            await _manager.SaveAsync();
            await _cache.RemoveAsync($"Company:{companyId}");
            await _cache.RemoveAsync("AllCompanies");
            await _cache.SetAsync($"Company:{company.CompanyId}", company);
        }

    }
}
