using System;

namespace SlowPokeWars.Engine.Game
{
    public interface IGameTicker : INotifiable, IDisposable
    {
        void Start();

        void Stop();
    }
}
