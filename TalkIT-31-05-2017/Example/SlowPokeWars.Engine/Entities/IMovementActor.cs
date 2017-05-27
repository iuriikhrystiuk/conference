namespace SlowPokeWars.Engine.Entities
{
    public interface IMovementActor
    {
        int Speed { get; }

        int GetIncrement();

        int GetDecrement();
    }

    public class ReverseMovementActor : IMovementActor
    {
        public int Speed => 1;

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
        public virtual int Speed => 1;

        public int GetIncrement()
        {
            return 1;
        }

        public int GetDecrement()
        {
            return -1;
        }
    }

    public class StaticMovementActor : DefaultMovementActor
    {
        public override int Speed => 0;
    }

    public class FasterMovementActor : IMovementActor
    {
        private readonly IMovementActor _actor;
        private readonly int _speed;

        public FasterMovementActor(IMovementActor actor, int speed)
        {
            _actor = actor;
            _speed = speed;
        }

        public int Speed => _speed;

        public int GetIncrement()
        {
            return _actor.GetIncrement();
        }

        public int GetDecrement()
        {
            return _actor.GetDecrement();
        }
    }
}