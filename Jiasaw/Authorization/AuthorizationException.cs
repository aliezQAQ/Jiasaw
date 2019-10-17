using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiasaw.Authorization
{
    public class AuthorizationException:Exception
    {
        public AuthorizationException(string message) : base(message)
        {

        }
    }
}
