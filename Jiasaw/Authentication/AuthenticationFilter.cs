using Jiasaw.Authorization;
using Jiasaw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Jiasaw.Authentication
{
    public class AuthenticationFilter : IAuthenticationFilter
    {
        public bool AllowMultiple => true;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                var actionAuthentication = context.ActionContext.ActionDescriptor.GetCustomAttributes<AuthenticationAttribute>();
                //验证
                if (
                    (actionAuthentication.Count > 0 && actionAuthentication.Last().Authenticate == true) ||
                    (actionAuthentication.Count == 0 && AuthenticationConfig.GetAuthenticateNoAttribute() == true)
                )
                {
                    var ticket = context.Request.Headers.GetValues(AuthenticationConfig.AuthenticationString).FirstOrDefault();
                    if (ticket == null)
                        throw new AuthenticationException("can not get  ticket !");
                    object obj = MemCache.Get(AuthenticationConfig.TicketKeyPrefix + ticket);
                    if (obj == null)
                    {
                        AuthorizationConfig.RemoveRoles(ticket);
                        throw new AuthenticationException("Ticket has Expired !");
                    }

                    if (AuthenticationConfig.GetRefreshTicket())
                        MemCache.Add(AuthenticationConfig.TicketKeyPrefix + ticket, obj, DateTime.Now.AddSeconds(AuthenticationConfig.GetTicketExpire()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
          
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Result);
        }

    
    }
}
