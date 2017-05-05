namespace SlowPokeWars.Engine.Entities
{
    public class GameClient
    {
        public GameClient(string connectionId)
            : this(connectionId, string.Empty)
        {
        }

        public GameClient(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }

        public string ConnectionId { get; }
        public string Name { get; }
    }
}