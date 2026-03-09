using Owin;
using System.Web.Http;

namespace ECommerce.LegacyInventory
{
    /// <summary>
    /// Configures the OWIN pipeline for the legacy inventory service.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the web API and routing.
        /// </summary>
        /// <param name="appBuilder">The app builder instance.</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Enable camelCase formatting
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


            appBuilder.UseWebApi(config);
        }
    }
}
