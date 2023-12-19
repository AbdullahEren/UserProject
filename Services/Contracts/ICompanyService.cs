using Entities.Dtos.CompanyDto;
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
        Task CreateCompanyAsync(CompanyForCreationDto companyDto);
        Task DeleteCompanyAsync(int companyId,Company company);
        Task UpdateCompanyAsync(int companyId,CompanyForUpdateDto companyDto);
    }
}
