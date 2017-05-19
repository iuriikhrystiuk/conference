using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPoke : NotifiableBase, IFieldPlayer
    {
        private IGameField _field;

        public GameClient Client { get; }

        public Position Position { get; set; }

        public bool Destroyed { get; private set; }

        public AreaDescriptor GetArea()
        {
            return new AreaDescriptor(Position, 2, 1);
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
            projectile.Position = new Position(Position.X, Position.Y, Position.MovementActor);
            projectile.AcceptField(_field);
            projectile.SubscribeNotifications(Notify);
            Notify();
        }

        public void AcceptField(IGameField field)
        {
            _field = field;
        }

        public bool Collide(ICollidable target)
        {
            return true;
        }

        public void Destroy()
        {
            Destroyed = true;
        }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("identifier", Client.ConnectionId);
            description.Add("position", Position.GetDescription());
            description.Add("area", GetArea().GetDescription());
            return description;
        }
    }
}
