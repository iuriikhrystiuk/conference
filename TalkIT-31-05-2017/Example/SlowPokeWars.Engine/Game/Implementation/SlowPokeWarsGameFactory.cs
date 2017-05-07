using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public class SlowPokeWarsGameFactory : IGameInstanceFactory
    {
        public IGameInstance CreateGameInstance()
        {
            return new SlowPokeWarsGame(new TimerGameTicker(1000), new SlowPokeGameField(new SlowPokeCollisionDetector()));
        }
    }
}