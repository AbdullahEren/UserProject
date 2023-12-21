using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(int id) : base($"The company with id : {id} could not found.")
        {
        }

        public CompanyNotFoundException() : base($"The company field required")
        {
        }
    }
}
