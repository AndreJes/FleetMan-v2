using FleetManApiController.Services;
using FleetManServices.AuthServices;
using Microsoft.Owin.Security.OAuth;
using SimpleWPFLogger.Enums;
using SimpleWPFLogger.Model;
using SimpleWPFLogger.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FleetManApiController.Configuration
{
    public class CustomAccessTokenProvider : OAuthAuthorizationServerProvider
    {
        ClientLoginService clientLoginService = new ClientLoginService();
        Logger Logger = LoggerManager.GetInstance().Loggers["MainLogger"];

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            _ = context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                UserInfo user_data = await clientLoginService.ValidateLogin(context.UserName, context.Password);

                if (user_data.IsValid)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user_data.Role));

                    context.OwinContext.Set<string>("user_cnpj", user_data.Cnpj);

                    context.Validated(identity);
                    Logger.Dispatcher.Invoke(() => Logger.PrintText(new Run("Novo token de acesso p/ usuário: " + context.UserName), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD)));
                }
                else
                {
                    context.SetError("Acesso inválido", "Dados de login inválidos");
                    Logger.Dispatcher.Invoke(() => Logger.PrintText(new Run("Tentativa de login inválida detectada p/ usuário: " + context.UserName), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD))); return;
                }

            }
            catch (Exception e)
            {
                if (context != null)
                {
                    context.SetError("Exception", e.ToString());
                }
                else
                {
                    throw e;
                }
            }
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            string user_cnpj = context.OwinContext.Get<string>("user_cnpj");

            Logger.Dispatcher.Invoke(() => Logger.PrintText(new Run("CNPJ usuário: " + user_cnpj), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD)));

            context.AdditionalResponseParameters.Add("user_cnpj", user_cnpj);

            return base.TokenEndpointResponse(context);
        }
    }
}
