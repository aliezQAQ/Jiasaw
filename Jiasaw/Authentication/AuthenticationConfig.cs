using Jiasaw.Authorization;
using Jiasaw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Jiasaw.Authentication
{
    public static  class AuthenticationConfig
    {
        internal static readonly string TicketKeyPrefix = "ticket-";
        //是否每次请求都刷新Ticket
        private static bool RefreshTicket = false;

        private static bool NoRefreshTicket = true;
        public static void SetRefreshTicket(bool refresh) => RefreshTicket = refresh;

        internal static bool GetRefreshTicket()
        {
            return RefreshTicket ? RefreshTicket : NoRefreshTicket;
        }

        //票据默认过期时间1小时
        private static double TicketExpire = 3600;

        public static void SetTicketExpire(int seconds) => TicketExpire = seconds>0? seconds: TicketExpire;

        internal static double GetTicketExpire() { return TicketExpire; }

        //没有注解是否需要授权
        private static bool AuthenticateNoAttribute = true;

        public static void SetAuthenticateNoAttribute(bool check) => AuthenticateNoAttribute = check;

        internal static bool GetAuthenticateNoAttribute()
        {
            return AuthenticateNoAttribute;
        }


        internal static readonly string AuthenticationString = "ticket";

        public static void RegisterUserAndRole(object user,List<string> roles)
        {
            string ticket = Tools.ToShortMD5(Guid.NewGuid().ToString("N"));
            DateTime expired = DateTime.Now.AddSeconds(AuthenticationConfig.GetTicketExpire());
            HttpContext.Current.Response.Headers.Add(AuthenticationConfig.AuthenticationString, ticket);
            if (user != null)
                MemCache.Add(AuthenticationConfig.TicketKeyPrefix + ticket, user, expired);
            if (roles != null && roles.Count > 0)
                AuthorizationConfig.AddRoles(ticket, roles);
        }

        public static object GetUser(string ticket)
        {
            return  MemCache.Get(AuthenticationConfig.TicketKeyPrefix + ticket);
        }
    }
}
