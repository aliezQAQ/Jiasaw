using Jiasaw.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Jiasaw.Authorization
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => true;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                var actionAuthorization = actionContext.ActionDescriptor.GetCustomAttributes<AuthorizationAttribute>();
                if (AuthorizationConfig.GetAuthorizationNoAttribute())
                {
                    if (actionAuthorization.Count > 0)
                    {
                        if (actionAuthorization.Last().Authorization)
                        {
                            string ticket = actionContext.Request.Headers.GetValues(AuthenticationConfig.AuthenticationString).FirstOrDefault();
                            List<string> userRoles = AuthorizationConfig.GetRoles(ticket);
                            List<string> attrRoles = actionAuthorization.Last().Roles.Split(',').ToList();
                            switch (actionAuthorization.Last().Logical)
                            {
                                case Logical.AND:
                                    if (attrRoles.Except(userRoles).Count() > 0)
                                        throw new AuthorizationException("Authorizate Fail");
                                    return continuation.Invoke();
                                case Logical.OR:
                                    if (attrRoles.Intersect(userRoles).Count() == 0)
                                        throw new AuthorizationException("Authorizate Fail");
                                    return continuation.Invoke();
                            }
                        }
                        else
                        {
                            return continuation.Invoke();
                        }
                    }
                    throw new AuthorizationException("Authorizate Fail");
                }
                return continuation.Invoke();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
