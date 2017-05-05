namespace SlowPokeWars.Engine.Game
{
    public interface IConnectionsManager
    {
        dynamic Client(string connectionId);

        dynamic Group(string groupName);
    }
}
