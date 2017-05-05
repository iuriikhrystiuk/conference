using System;

namespace SlowPokeWars.Engine.Game
{
    public interface INotifiable
    {
        void SubscribeNotifications(Action callback);
    }
}