using System;
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
        private readonly ICollection<GameClient> _clients;

        public GameCoordinator(IConnectionsManager connectionManager, IGameInstanceFactory gameInstanceFactory)
        {
            _connectionManager = connectionManager;
            _gameInstanceFactory = gameInstanceFactory;
            _games = new List<IGameInstance>();
            _clients = new List<GameClient>();
        }

        public IGameInstance Apply(GameClient client)
        {
            lock (_games)
            {
                if (_clients.Contains(client))
                {
                    throw new Exception("Client already in game");
                }

                var openGame = _games.FirstOrDefault(g => g.HasSpot(client));
                if (openGame == null)
                {
                    openGame = _gameInstanceFactory.CreateGameInstance();
                    openGame.SubscribeNotification(NotifyChanges);
                    _games.Add(openGame);
                }

                openGame.AddPlayer(client);
                _clients.Add(client);
                return openGame;
            }
        }

        public IGameInstance Leave(GameClient client)
        {
            lock (_games)
            {
                var openGame = _games.FirstOrDefault(g => g.RemovePlayer(client));
                if (openGame != null)
                {
                    _clients.Remove(client);
                    if (openGame.IsEmpty())
                    {
                        openGame.Dispose();
                        _games.Remove(openGame);
                    }

                    return openGame;
                }
            }

            return null;
        }

        public IGameInstance GetGame(string gameIdentifier)
        {
            lock (_games)
            {
                return _games.FirstOrDefault(g => g.GetIdentifier() == gameIdentifier);
            }
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
