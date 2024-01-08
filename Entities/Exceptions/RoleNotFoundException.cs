using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException() : base($"The role could not found.")
        {
        }

    }
}
