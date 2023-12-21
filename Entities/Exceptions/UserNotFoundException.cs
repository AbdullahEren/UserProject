using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userName) : base($"The user with username : {userName} could not found.")
        {
        }
        public UserNotFoundException(int userId) : base($"The user with id : {userId} could not found.")
        {
        }

    }
}
