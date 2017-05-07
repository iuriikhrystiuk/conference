using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public interface IFieldPlayer : IMovableObject, IDescribable
    {
        GameClient Client { get; }
    }
}
