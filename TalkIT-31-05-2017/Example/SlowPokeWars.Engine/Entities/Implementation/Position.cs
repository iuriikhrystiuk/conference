using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class Position : IDescribable
    {
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
            X += MovementActor.GetIncrement();
        }

        public void DecrementAbscissa()
        {
            X += MovementActor.GetDecrement();
        }

        public void IncrementOrdinata()
        {
            Y += MovementActor.GetIncrement();
        }

        public void DecrementOrdinata()
        {
            Y += MovementActor.GetDecrement();
        }
    }
}