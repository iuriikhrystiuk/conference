using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public interface IFieldObject : ICollidable, IDescribable
    {
        Position Position { get; set; }

        AreaDescriptor GetArea();
    }
}