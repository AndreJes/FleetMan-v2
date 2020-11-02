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

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                Logger Logger = LoggerManager.GetInstance().Loggers["MainLogger"];

                if (await clientLoginService.ValidateLogin(context.UserName, context.Password))
                {
                    ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Client"));

                    context.Validated(identity);
                    Logger.Dispatcher.Invoke(() => Logger.PrintText(new Run("Novo token de acesso p/ usuário: " + context.UserName), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD)));
                }
                else
                {
                    context.SetError("acesso inválido", "Dados de login inválidos...");
                    Logger.Dispatcher.Invoke(() => Logger.PrintText(new Run("Tentativa de login inválida detectada p/ usuário: " + context.UserName), new Run(" --> "), new DateOptions(TextDecorationOptions.BOLD))); return;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
