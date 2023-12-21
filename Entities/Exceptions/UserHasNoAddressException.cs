using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class UserHasNoAddressException : BadRequestException
    {
        public UserHasNoAddressException() : base("User has no address.")
        {
        }
    }
}
