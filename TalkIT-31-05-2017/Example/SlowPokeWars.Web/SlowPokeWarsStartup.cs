using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using SlowPokeWars.Web.Bootstrapper;
using System.Web.Http;

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

            app.MapSignalR(new HubConfiguration
            {
                EnableJSONP = true,
                EnableDetailedErrors = true,
                Resolver = bootstrapper.GetSignalRDependencyResolver()
            });
            app.UseNinjectMiddleware(bootstrapper.GetKernel);
            app.UseNinjectWebApi(webApiConfiguration);
        }
    }
}
