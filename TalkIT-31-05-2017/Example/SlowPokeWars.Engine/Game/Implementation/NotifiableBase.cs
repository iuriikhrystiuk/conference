using System;
using System.Collections.Generic;
using System.Linq;

namespace SlowPokeWars.Engine.Game
{
    public abstract class NotifiableBase : INotifiable
    {
        private ICollection<Action> _callbacks;
        private ICollection<Action> _callbacksToRemove;

        protected NotifiableBase()
        {
            _callbacks = new List<Action>();
            _callbacksToRemove = new List<Action>();
        }

        public void SubscribeNotifications(Action callback)
        {
            lock (_callbacks)
            {
                _callbacks.Add(callback);
            }
        }

        public void UnsubscribeNotifications(Action callback)
        {
            lock (_callbacksToRemove)
            {
                _callbacksToRemove.Add(callback);
            }
        }

        protected void Notify()
        {
            lock (_callbacksToRemove)
            {
                _callbacks = _callbacks.Except(_callbacksToRemove).ToList();
                _callbacksToRemove.Clear();
            }

            lock (_callbacks)
            {
                foreach (var callback in _callbacks)
                {
                    callback();
                }
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