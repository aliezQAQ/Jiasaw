using Jiasaw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiasaw.Authorization
{
    public  class AuthorizationConfig
    {
        internal static readonly string RolePrefix = "role-";
        internal static void AddRoles(string ticket, List<string> roles) => MemCache.Add(RolePrefix + ticket, roles);
        internal static void RemoveRoles(string ticket) => MemCache.Remove(RolePrefix + ticket);

        public static List<string> GetRoles(string ticket)
        {
            return (List<string>)MemCache.Get(RolePrefix + ticket);
        }

        //没有注解是否需要验权
        private static bool AuthorizationNoAttribute = true;

        public static void SetAuthorizationNoAttribute(bool check) => AuthorizationNoAttribute = check;

        internal static bool GetAuthorizationNoAttribute()
        {
            return AuthorizationNoAttribute;
        }
    }
}
