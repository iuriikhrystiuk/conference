using System.Collections.Generic;
using System.Linq;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public class GameCoordinator : IGameCoordinator
    {
        private readonly IConnectionsManager _connectionManager;
        private readonly IGameInstanceFactory _gameInstanceFactory;

        private readonly ICollection<IGameInstance> _games;

        public GameCoordinator(IConnectionsManager connectionManager, IGameInstanceFactory gameInstanceFactory)
        {
            _connectionManager = connectionManager;
            _gameInstanceFactory = gameInstanceFactory;
            _games = new List<IGameInstance>();
        }

        public string Apply(GameClient client)
        {
            lock (_games)
            {
                var openGame = _games.FirstOrDefault(g => !g.Initialized());
                if (openGame == null)
                {
                    openGame = _gameInstanceFactory.CreateGameInstance();
                    openGame.SubscribeNotification(NotifyChanges);
                    _games.Add(openGame);
                }

                openGame.AddPlayer(client);
                return openGame.GetIdentifier();
            }
        }

        public string Leave(GameClient client)
        {
            lock (_games)
            {
                var openGame = _games.FirstOrDefault(g => g.Contains(client));
                if (openGame != null)
                {
                    openGame.RemovePlayer(client);
                    if (openGame.IsEmpty())
                    {
                        openGame.Dispose();
                        _games.Remove(openGame);
                    }

                    return openGame.GetIdentifier();
                }
            }

            return string.Empty;
        }

        private void NotifyChanges(string gameIdentifier)
        {
            lock (_games)
            {
                var game = _games.First(g => g.GetIdentifier() == gameIdentifier);
                _connectionManager.Group(gameIdentifier).gameUpdated(game.GetDescription());
            }
        }
    }
}
