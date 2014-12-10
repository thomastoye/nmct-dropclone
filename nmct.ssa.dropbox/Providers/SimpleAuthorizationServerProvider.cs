using Microsoft.Owin.Security.OAuth;
using nmct.ssa.dropbox.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace nmct.ssa.dropbox.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // try to log in
            if (DAExternalUser.TryLogin(context.UserName, context.Password) == null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("username", context.UserName));
            id.AddClaim(new Claim("role", "user"));
            context.Validated(id);
            return Task.FromResult(0);
        }
    }
}