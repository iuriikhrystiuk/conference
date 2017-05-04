using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Ninject;
using SlowPokeWars.Engine.Game;
using SlowPokeWars.Engine.Game.Implementation;
using SlowPokeWars.Web.Hubs;

namespace SlowPokeWars.Web.Bootstrapper
{
    public class SlowPokeWarsBootstrapper
    {
        private readonly IKernel _kernel;

        public SlowPokeWarsBootstrapper()
        {
            _kernel = new StandardKernel();
        }

        public IKernel GetKernel()
        {
            return _kernel;
        }

        public void Initialize()
        {
            _kernel.Bind<IGameCoordinator>().To<GameCoordinator>().InSingletonScope();
        }

        public IDependencyResolver GetSignalRDependencyResolver()
        {
            var resolver = new NinjectSignalRDependencyResolver(_kernel);

            // you have to use SignalR dependency resolver
            // since it uses not only your DI container but also internal mechanism
            // with additional dependencies to instanciate connection manager
            // the result of usage of virtual methods in constructors
            _kernel.Bind<IConnectionsManager>()
                .ToMethod(context => new ConnectionsManager(resolver.Resolve<IConnectionManager>().GetHubContext<GameHub>().Clients))
                .WhenInjectedInto<IGameCoordinator>();
            
            return resolver;
        }
    }
}