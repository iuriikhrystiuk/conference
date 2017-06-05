using System;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class AreaDescriptor : IDescribable
    {
        public AreaDescriptor(Position position, int widthIncrement, int heightIncrement)
        {
            var possibleTopLeft = new Position(position.X + position.MovementActor.GetDecrement() * widthIncrement, position.Y + position.MovementActor.GetIncrement() * heightIncrement);
            var possibleBottomRight = new Position(position.X + position.MovementActor.GetIncrement() * widthIncrement, position.Y + position.MovementActor.GetDecrement() * heightIncrement);

            if (possibleTopLeft.X > possibleBottomRight.X)
            {
                TopLeft = possibleBottomRight;
                BottomRight = possibleTopLeft;
            }
            else
            {
                TopLeft = possibleTopLeft;
                BottomRight = possibleBottomRight;
            }
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

        public int Intersects(AreaDescriptor other)
        {
            var abscissaOverlap = Math.Max(0, Math.Min(BottomRight.X, other.BottomRight.X) - Math.Max(TopLeft.X, other.TopLeft.X));
            var ordinataOverlap = Math.Max(0, Math.Min(TopLeft.Y, other.TopLeft.Y) - Math.Max(BottomRight.Y, other.BottomRight.Y));
            return abscissaOverlap * ordinataOverlap;
        }

        public int GetAreaValue()
        {
            return (BottomRight.X - TopLeft.X) * (TopLeft.Y - BottomRight.Y);
        }
    }
}