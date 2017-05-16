using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public interface IFieldPlayer : IMovableObject
    {
        GameClient Client { get; }

        void Fire();
    }
}
