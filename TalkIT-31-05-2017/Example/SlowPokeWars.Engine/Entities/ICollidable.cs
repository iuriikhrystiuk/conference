namespace SlowPokeWars.Engine.Entities
{
    public interface ICollidable
    {
        bool Destroyed { get; set; }

        bool Collide(ICollidable target);

        void Destroy();
    }
}