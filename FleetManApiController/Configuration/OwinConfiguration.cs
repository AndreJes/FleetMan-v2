using FleetManApiController.Configuration;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(OwinConfiguration))]
namespace FleetManApiController.Configuration
{
    public class OwinConfiguration
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "API/{controller}/{action}");
            config.MapHttpAttributeRoutes();

            ConfigJson(config);

            ConfigOAuth(appBuilder);

            appBuilder.UseCors(CorsOptions.AllowAll);

            appBuilder.UseWebApi(config);
        }

        private void ConfigJson(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(JsonConfig.DefaultJsonMediaType);
            config.Formatters.JsonFormatter.SerializerSettings = JsonConfig.DefaultSettings;
        }

        private void ConfigOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions serverOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                Provider = new CustomAccessTokenProvider()
            };

            app.UseOAuthAuthorizationServer(serverOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
