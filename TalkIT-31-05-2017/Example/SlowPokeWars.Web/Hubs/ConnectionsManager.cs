using Microsoft.AspNet.SignalR.Hubs;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Web.Hubs
{
    public class ConnectionsManager : IConnectionsManager
    {
        private readonly IHubConnectionContext<dynamic> connections;

        public ConnectionsManager(IHubConnectionContext<dynamic> connections)
        {
            this.connections = connections;
        }
    }
}