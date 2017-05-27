using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPoke : NotifiableBase, IFieldPlayer
    {
        private IGameField _field;
        private readonly int _height = 3;

        public GameClient Client { get; }
        public int Points { get; private set; }

        public Position Position { get; set; }

        public bool Destroyed { get; set; }

        public AreaDescriptor GetArea()
        {
            return new AreaDescriptor(Position, 6, _height);
        }

        public void UpdateState()
        {
        }

        public SlowPoke(GameClient client)
        {
            Client = client;
        }

        public void MoveLeft()
        {
            if (_field.TryMoveLeft(this))
            {
                Notify();
            }
            else
            {
                Position.Restore();
            }
        }

        public void MoveRight()
        {
            if (_field.TryMoveRight(this))
            {
                Notify();
            }
            else
            {
                Position.Restore();
            }
        }

        public void MoveDown()
        {
            if (_field.TryMoveDown(this))
            {
                Notify();
            }
            else
            {
                Position.Restore();
            }
        }

        public void MoveUp()
        {
            if (_field.TryMoveUp(this))
            {
                Notify();
            }
            else
            {
                Position.Restore();
            }
        }

        public void Fire()
        {
            var projectile = new Projectile(this);
            projectile.Position = new Position(
                Position.X, 
                Position.Y + Position.MovementActor.GetIncrement() * _height * 2, 
                new FasterMovementActor(Position.MovementActor, 5));
            projectile.AcceptField(_field);
            projectile.SubscribeNotifications(Notify);
            Notify();
        }

        public void Score()
        {
            Points++;
        }

        public void AcceptField(IGameField field)
        {
            _field = field;
        }

        public bool Collide(ICollidable target)
        {
            return false;
        }

        public void Destroy()
        {
            Destroyed = true;
            _field.Reset();
            Notify();
        }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("identifier", Client.ConnectionId);
            description.Add("name", Client.Name);
            description.Add("position", Position.GetDescription());
            description.Add("points", Points);
            description.Add("area", GetArea().GetDescription());
            return description;
        }

        public override bool Equals(object obj)
        {
            var player = obj as SlowPoke;

            if (player == null)
            {
                return false;
            }

            if (player == this)
            {
                return true;
            }

            return Equals(player);
        }

        private bool Equals(SlowPoke other)
        {
            return Equals(Client, other.Client) && Equals(Position, other.Position);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Client != null ? Client.GetHashCode() : 0) * 397) ^ (Position != null ? Position.GetHashCode() : 0);
            }
        }
    }
}
