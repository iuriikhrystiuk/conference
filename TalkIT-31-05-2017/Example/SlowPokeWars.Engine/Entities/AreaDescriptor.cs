using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class AreaDescriptor : IDescribable
    {
        public AreaDescriptor(Position topLeft, Position bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Position TopLeft { get; }

        public Position BottomRight { get; }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("topLeft", TopLeft.GetDescription());
            description.Add("bottomRight", BottomRight.GetDescription());
            return description;
        }
    }
}