using System;

namespace SlowPokeWars.Engine.Game.Implementation
{
    public class GameCoordinator : IGameCoordinator
    {
        private readonly IConnectionsManager connectionManager;

        public GameCoordinator(IConnectionsManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public void Apply()
        {
            throw new NotImplementedException();
        }
    }
}
