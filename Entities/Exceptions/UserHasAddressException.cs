using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class UserHasAddressException : BadRequestException
    {
        public UserHasAddressException() : base("User has an address.")
        {
        }
    }
}
