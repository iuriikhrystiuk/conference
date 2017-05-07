namespace SlowPokeWars.Engine.Entities
{
    public interface ICollidable
    {
        bool Destroyed { get; }

        bool Collide(ICollidable target);

        void Destroy();
    }
}