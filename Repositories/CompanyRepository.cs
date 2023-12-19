using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) => await FindAllAsync(trackChanges);

        public async Task<Company> GetCompanyAsync(int companyId, bool trackChanges)
        {
            var company = await FindByConditionAsync(c => c.CompanyId.Equals(companyId), trackChanges);
            return company.SingleOrDefault();
        }

        public async Task UpdateCompanyAsync(int companyId, Company company)
        {
          company.CompanyId = companyId;
          await UpdateAsync(company);
        }

        public async Task CreateCompanyAsync(Company company)=> await CreateAsync(company);

        public async Task DeleteCompanyAsync(Company company)=> await DeleteAsync(company);

    }
}
