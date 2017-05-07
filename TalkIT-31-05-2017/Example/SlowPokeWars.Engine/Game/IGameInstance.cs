using System;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public interface IGameInstance: INotifiable, IDescribable, IDisposable
    {
        void AddPlayer(GameClient client);
        bool RemovePlayer(GameClient client);
        string GetIdentifier();
        bool Initialized();
        bool IsEmpty();
        void SubscribeNotification(Action<string> callback);
    }
}