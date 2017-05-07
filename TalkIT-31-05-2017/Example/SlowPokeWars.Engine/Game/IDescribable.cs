using Newtonsoft.Json.Linq;

namespace SlowPokeWars.Engine.Game
{
    public interface IDescribable
    {
        JObject GetDescription();
    }
}