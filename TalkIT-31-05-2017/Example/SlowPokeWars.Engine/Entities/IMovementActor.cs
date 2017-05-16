namespace SlowPokeWars.Engine.Entities
{
    public interface IMovementActor
    {
        int GetIncrement();

        int GetDecrement();
    }

    public class ReverseMovementActor : IMovementActor
    {
        public int GetIncrement()
        {
            return -1;
        }

        public int GetDecrement()
        {
            return 1;
        }
    }

    public class DefaultMovementActor : IMovementActor
    {
        public int GetIncrement()
        {
            return 1;
        }

        public int GetDecrement()
        {
            return -1;
        }
    }

    public class StaticMovementActor : IMovementActor
    {
        public int GetIncrement()
        {
            return 0;
        }

        public int GetDecrement()
        {
            return 0;
        }
    }
}