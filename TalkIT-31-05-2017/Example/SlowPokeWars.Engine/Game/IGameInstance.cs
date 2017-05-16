using System;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public interface IGameInstance: INotifiable, IDescribable, IDisposable
    {
        void AddPlayer(GameClient client);
        bool RemovePlayer(GameClient client);
        string GetIdentifier();
        bool HasSpot(GameClient client);
        bool IsEmpty();
        void SubscribeNotification(Action<string> callback);
        void MoveLeft(string connectionId);
        void MoveRight(string connectionId);
        void MoveUp(string connectionId);
        void MoveDown(string connectionId);
        void Fire(string connectionId);
    }
}