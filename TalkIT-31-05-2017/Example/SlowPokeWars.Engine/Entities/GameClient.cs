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

        public override bool Equals(object obj)
        {
            var client = obj as GameClient;

            if (client == null)
            {
                return false;
            }

            if (client == this)
            {
                return true;
            }

            return Equals(client);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ConnectionId != null ? ConnectionId.GetHashCode() : 0) * 397;
            }
        }

        private bool Equals(GameClient other)
        {
            return string.Equals(ConnectionId, other.ConnectionId);
        }
    }
}