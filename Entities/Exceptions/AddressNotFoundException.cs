using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class AddressNotFoundException : NotFoundException
    {
        public AddressNotFoundException(int applicationUserId) : base($"The address with id : {applicationUserId} could not found.")
        {
        }

        public AddressNotFoundException() : base($"The address field required")
        {
        }
    }
}
