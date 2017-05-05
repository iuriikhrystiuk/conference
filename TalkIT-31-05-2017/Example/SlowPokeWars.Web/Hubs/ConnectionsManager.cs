using Microsoft.AspNet.SignalR.Hubs;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Web.Hubs
{
    public class ConnectionsManager : IConnectionsManager
    {
        private readonly IHubConnectionContext<dynamic> _connections;

        public ConnectionsManager(IHubConnectionContext<dynamic> connections)
        {
            _connections = connections;
        }

        public dynamic Client(string connectionId)
        {
            return _connections.Client(connectionId);
        }

        public dynamic Group(string groupName)
        {
            return _connections.Group(groupName);
        }
    }
}