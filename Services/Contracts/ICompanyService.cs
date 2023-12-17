using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(int companyId, bool trackChanges);
        Task CreateCompany(Company company);
        Task DeleteCompany(Company company);
        Task UpdateCompany(int companyId,Company company);
    }
}
