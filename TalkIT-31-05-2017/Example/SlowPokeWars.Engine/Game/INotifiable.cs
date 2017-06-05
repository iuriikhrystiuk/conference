using System;

namespace SlowPokeWars.Engine.Game
{
    public interface INotifiable
    {
        void SubscribeNotifications(Action callback);

        void UnsubscribeNotifications(Action callback);
    }
}