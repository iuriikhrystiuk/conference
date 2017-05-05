using System;
using System.Collections.Generic;
using System.Linq;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    internal class SlowPokeWarsGame : NotifiableBase, IGameInstance
    {
        private readonly List<SlowPoke> _slowPokes;
        private readonly Guid _gameGuid;
        private readonly IGameTicker _gameTicker;
        private readonly Field _field;

        public SlowPokeWarsGame(IGameTicker gameTicker)
        {
            _gameTicker = gameTicker;
            _slowPokes = new List<SlowPoke>();
            _gameGuid = Guid.NewGuid();
            _field = new Field();
        }

        public bool Initialized()
        {
            return _slowPokes.Count == 2;
        }

        public bool IsEmpty()
        {
            return _slowPokes.Count == 0;
        }

        public void AddPlayer(GameClient client)
        {
            var slowPoke = new SlowPoke(client);
            slowPoke.SubscribeNotifications(Notify);
            slowPoke.AcceptGameTicker(_gameTicker);
            slowPoke.EnterField(_field);
            _slowPokes.Add(slowPoke);
            if (Initialized())
            {
                Notify();
                _gameTicker.Start();
            }
        }

        public void RemovePlayer(GameClient client)
        {
            var existingSlowPoke = _slowPokes.FirstOrDefault(s => s.Client.ConnectionId == client.ConnectionId);
            if (existingSlowPoke != null)
            {
                _slowPokes.Remove(existingSlowPoke);
            }
        }

        public string GetIdentifier()
        {
            return _gameGuid.ToString();
        }

        public void SubscribeNotification(Action<string> callback)
        {
            SubscribeNotifications(() => callback(GetIdentifier()));
        }

        public string GetDescription()
        {
            return string.Empty;
        }

        public bool Contains(GameClient client)
        {
            return _slowPokes.Any(s => s.Client.ConnectionId == client.ConnectionId);
        }

        public void Dispose()
        {
            _gameTicker?.Dispose();
            ClearSubscriptions();
        }
    }
}
