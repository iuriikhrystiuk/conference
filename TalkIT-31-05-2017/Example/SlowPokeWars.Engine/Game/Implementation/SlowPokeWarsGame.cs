using System;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    internal class SlowPokeWarsGame : NotifiableBase, IGameInstance
    {
        private readonly Guid _gameGuid;
        private readonly IGameTicker _gameTicker;
        private readonly IGameField _gameField;

        public SlowPokeWarsGame(IGameTicker gameTicker, IGameField gameField)
        {
            _gameTicker = gameTicker;
            _gameGuid = Guid.NewGuid();
            _gameField = gameField;
        }

        public bool HasSpot(GameClient client)
        {
            return _gameField.HasSpot(client);
        }

        public bool IsEmpty()
        {
            return _gameField.IsEmpty();
        }

        public void AddPlayer(GameClient client)
        {
            var slowPoke = new SlowPoke(client);
            slowPoke.SubscribeNotifications(Notify);
            _gameField.Enter(slowPoke);
            Notify();
        }

        public bool RemovePlayer(GameClient client)
        {
            if (_gameField.Exit(client))
            {
                Notify();
                return true;
            }

            return false;
        }

        public string GetIdentifier()
        {
            return _gameGuid.ToString();
        }

        public void SubscribeNotification(Action<string> callback)
        {
            SubscribeNotifications(() => callback(GetIdentifier()));
        }

        public void MoveLeft(string connectionId)
        {
            var player = _gameField.GetPlayer(new GameClient(connectionId));
            player?.MoveLeft();
        }

        public void MoveRight(string connectionId)
        {
            var player = _gameField.GetPlayer(new GameClient(connectionId));
            player?.MoveRight();
        }

        public void MoveUp(string connectionId)
        {
            var player = _gameField.GetPlayer(new GameClient(connectionId));
            player?.MoveUp();
        }

        public void MoveDown(string connectionId)
        {
            var player = _gameField.GetPlayer(new GameClient(connectionId));
            player?.MoveDown();
        }

        public void Fire(string connectionId)
        {
            var player = _gameField.GetPlayer(new GameClient(connectionId));
            player?.Fire();
        }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("id", GetIdentifier());
            description.Add("field", _gameField.GetDescription());
            return description;
        }

        public void Dispose()
        {
            _gameTicker?.Dispose();
            ClearSubscriptions();
        }
    }
}
