using System.Runtime;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class Position : IDescribable
    {
        private PositionMemento memento;

        public Position(int x, int y)
            : this(x, y, new StaticMovementActor())
        {
        }

        public Position(int x, int y, IMovementActor actor)
        {
            X = x;
            Y = y;
            MovementActor = actor;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public IMovementActor MovementActor { get; }

        public JObject GetDescription()
        {
            return new JObject { { "x", X }, { "y", Y } };
        }

        public void IncrementAbscissa()
        {
            memento = new PositionMemento(X, Y);
            X += MovementActor.GetIncrement();
        }

        public void DecrementAbscissa()
        {
            memento = new PositionMemento(X, Y);
            X += MovementActor.GetDecrement();
        }

        public void IncrementOrdinata()
        {
            memento = new PositionMemento(X, Y);
            Y += MovementActor.GetIncrement();
        }

        public void DecrementOrdinata()
        {
            memento = new PositionMemento(X, Y);
            Y += MovementActor.GetDecrement();
        }

        public void Restore()
        {
            if (memento != null)
            {
                X = memento.X;
                Y = memento.Y;
                memento = null;
            }
        }

        public override bool Equals(object obj)
        {
            var position = obj as Position;

            if (position == null)
            {
                return false;
            }

            return Equals(position);
        }

        private bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }

    public class PositionMemento
    {
        public PositionMemento(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }
    }
}