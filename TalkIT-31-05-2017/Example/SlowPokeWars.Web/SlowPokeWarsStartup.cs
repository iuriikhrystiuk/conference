using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using SlowPokeWars.Web.Bootstrapper;
using System.Web.Http;
using SlowPokeWars.Web.Hubs;

[assembly: OwinStartup(typeof(SlowPokeWars.Web.SlowPokeWarsStartup))]

namespace SlowPokeWars.Web
{
    public class SlowPokeWarsStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var bootstrapper = new SlowPokeWarsBootstrapper();
            bootstrapper.Initialize();

            var webApiConfiguration = new HttpConfiguration();
            webApiConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}");

            // Server Setup
            app.MapSignalR(new HubConfiguration
            {
                EnableJavaScriptProxies = false, // true if you do not want to write proxies yourself
                EnableJSONP = true, // here b/c server starts on port that differes from UI server port.
                EnableDetailedErrors = true,
                // required, if you want your DI container to resolve hub dependencies.
                Resolver = bootstrapper.GetSignalRDependencyResolver()
            });

            app.UseNinjectMiddleware(bootstrapper.GetKernel);
            app.UseNinjectWebApi(webApiConfiguration);
        }
    }
}
