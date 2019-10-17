using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiasaw.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationAttribute:Attribute
    {
        public bool Authorization { get; set; }

        public string Roles { get; set; }

        public Logical Logical { get; set; }
    }

    public enum Logical
    {
        AND,
        OR
    }
}
