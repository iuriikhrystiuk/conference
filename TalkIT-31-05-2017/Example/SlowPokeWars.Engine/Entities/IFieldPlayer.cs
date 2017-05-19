namespace SlowPokeWars.Engine.Entities
{
    public interface IFieldPlayer : IMovableObject
    {
        GameClient Client { get; }

        int Points { get; }

        void Fire();

        void Score();
    }
}
