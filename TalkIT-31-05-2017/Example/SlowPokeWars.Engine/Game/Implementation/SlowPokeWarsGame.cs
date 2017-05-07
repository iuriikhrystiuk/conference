using System;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    internal class SlowPokeWarsGame : NotifiableBase, IGameInstance
    {
        private readonly Guid _gameGuid;
        private readonly IGameTicker _gameTicker;
        private readonly IGameField _slowPokeGameField;

        public SlowPokeWarsGame(IGameTicker gameTicker, IGameField slowPokeGameField)
        {
            _gameTicker = gameTicker;
            _gameGuid = Guid.NewGuid();
            _slowPokeGameField = slowPokeGameField;
        }

        public bool Initialized()
        {
            return !_slowPokeGameField.HasSpot();
        }

        public bool IsEmpty()
        {
            return _slowPokeGameField.IsEmpty();
        }

        public void AddPlayer(GameClient client)
        {
            var slowPoke = new SlowPoke(client);
            slowPoke.SubscribeNotifications(Notify);
            slowPoke.AcceptGameTicker(_gameTicker);
            _slowPokeGameField.Enter(slowPoke);
            if (Initialized())
            {
                Notify();
                _gameTicker.Start();
            }
        }

        public bool RemovePlayer(GameClient client)
        {
            return _slowPokeGameField.Exit(client);
        }

        public string GetIdentifier()
        {
            return _gameGuid.ToString();
        }

        public void SubscribeNotification(Action<string> callback)
        {
            SubscribeNotifications(() => callback(GetIdentifier()));
        }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("field", _slowPokeGameField.GetDescription());
            return description;
        }

        public void Dispose()
        {
            _gameTicker?.Dispose();
            ClearSubscriptions();
        }
    }
}
