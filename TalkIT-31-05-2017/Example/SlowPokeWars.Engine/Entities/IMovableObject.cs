namespace SlowPokeWars.Engine.Entities
{
    public interface IMovableObject : IFieldObject
    {
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
    }
}