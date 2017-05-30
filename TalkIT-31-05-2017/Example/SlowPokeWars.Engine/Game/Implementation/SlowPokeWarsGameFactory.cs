using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public class SlowPokeWarsGameFactory : IGameInstanceFactory
    {
        public IGameInstance CreateGameInstance()
        {
            return new SlowPokeWarsGame(new SlowPokeGameField(new SlowPokeCollisionDetector(), new TimerGameTicker(200)));
        }
    }
}