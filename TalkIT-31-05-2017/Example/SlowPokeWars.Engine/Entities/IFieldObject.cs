using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public interface IFieldObject : ICollidable, IDescribable, INotifiable, IHasArea
    {
        Position Position { get; set; }

        void UpdateState();
    }
}