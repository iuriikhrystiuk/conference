using System.Net.Http;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public interface IGameCoordinator
    {
        IGameInstance Apply(GameClient client);

        IGameInstance Leave(GameClient client);

        IGameInstance GetGame(string gameIdentifier);
    }
}
