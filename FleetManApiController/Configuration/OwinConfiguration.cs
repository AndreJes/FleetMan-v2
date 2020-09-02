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

[assembly: OwinStartup(typeof(OwinConfiguration))]
namespace FleetManApiController.Configuration
{
    public class OwinConfiguration
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "API/{controller}");
            config.MapHttpAttributeRoutes();
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            appBuilder.UseCors(new CorsOptions());

            appBuilder.UseWebApi(config);
        }
    }
}
