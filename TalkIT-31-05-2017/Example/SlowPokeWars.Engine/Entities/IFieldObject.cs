using System;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public interface IFieldObject : ICollidable, IDescribable
    {
        Position Position { get; }

        AreaDescriptor GetArea();
    }

    public class AreaDescriptor : IDescribable
    {
        public AreaDescriptor(int length, int width)
        {
            Length = length;
            Width = width;
        }

        public int Length { get; private set; }

        public int Width { get; private set; }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("width", Width);
            description.Add("length", Length);
            return description;
        }
    }
}