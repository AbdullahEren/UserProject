using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(ICompanyRepository companyRepository, RepositoryContext context)
        {
            _companyRepository = companyRepository;
            _context = context;
        }

        public ICompanyRepository Company => _companyRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
