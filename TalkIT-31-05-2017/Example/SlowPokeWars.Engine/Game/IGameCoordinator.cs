using System.Net.Http;
using SlowPokeWars.Engine.Entities;

namespace SlowPokeWars.Engine.Game
{
    public interface IGameCoordinator
    {
        string Apply(GameClient client);

        string Leave(GameClient client);
    }
}
