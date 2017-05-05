using System;
using System.Collections.Generic;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public interface IGameInstance: INotifiable, IDisposable
    {
        void AddPlayer(GameClient client);
        void RemovePlayer(GameClient client);
        bool Contains(GameClient client);
        string GetIdentifier();
        bool Initialized();
        bool IsEmpty();
        void SubscribeNotification(Action<string> callback);
        string GetDescription();
    }
}