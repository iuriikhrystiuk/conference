using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class Position : IDescribable
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
        public JObject GetDescription()
        {
            return new JObject { { "x", X }, { "y", Y } };
        }
    }
}