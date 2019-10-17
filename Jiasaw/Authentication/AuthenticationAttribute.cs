using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiasaw.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthenticationAttribute:Attribute
    {
        public bool Authenticate { get; set; } = true;
    }
}
