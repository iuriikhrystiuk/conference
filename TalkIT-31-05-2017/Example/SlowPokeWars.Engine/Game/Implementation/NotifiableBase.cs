using System;
using System.Collections.Generic;

namespace SlowPokeWars.Engine.Game
{
    public abstract class NotifiableBase : INotifiable
    {
        private readonly ICollection<Action> _callbacks;

        protected NotifiableBase()
        {
            _callbacks = new List<Action>();
        }

        public void SubscribeNotifications(Action callback)
        {
            lock (_callbacks)
            {
                _callbacks.Add(callback);
            }
        }

        protected void Notify()
        {
            foreach (var callback in _callbacks)
            {
                callback();
            }
        }

        protected void ClearSubscriptions()
        {
            lock (_callbacks)
            {
                _callbacks.Clear();
            }
        }
    }
}