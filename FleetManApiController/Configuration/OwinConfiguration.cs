using FleetManApiController.Configuration;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using System;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Swashbuckle.Application;
using Microsoft.OpenApi.Models;
using FleetManApiController.Model;

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
            ConfigSwagger(config);

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
                AuthorizeEndpointPath = new PathString("/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),
                Provider = new CustomAccessTokenProvider()
            };
            app.UseOAuthAuthorizationServer(serverOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void ConfigSwagger(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "FleetMan.API");
                    c.DocumentFilter<AuthTokenOperation>();
                    c.ApiKey("Authorization")
                        .Name("Authorization")
                        .Description("Token de autenticação")
                        .In("header");
                }).EnableSwaggerUi(c =>
                {
                    c.EnableApiKeySupport("Authorization", "header");
                });
        }
    }
}
