using FleetManServices.AuthServices;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FleetManApiController.Configuration
{
    public class CustomAccessTokenProvider : OAuthAuthorizationServerProvider
    {
        ClientLoginService clientLoginService = new ClientLoginService();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (await clientLoginService.ValidateLogin(context.UserName, context.Password))
            {
                ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Client"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("acesso inválido", "Credenciais informadas são invalidas...");
                return;
            }
        }
    }
}
