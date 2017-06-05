using System.Timers;

namespace SlowPokeWars.Engine.Game
{
    public class TimerGameTicker : NotifiableBase, IGameTicker
    {
        private readonly Timer _timer;

        public TimerGameTicker(int interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Notify();
        }

        public void Start()
        {
            if (!_timer.Enabled)
            {
                _timer.Start();
            }
        }

        public void Stop()
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }
        }

        public void Dispose()
        {
            _timer.Elapsed -= TimerOnElapsed;
            _timer?.Dispose();
            ClearSubscriptions();
        }
    }
}