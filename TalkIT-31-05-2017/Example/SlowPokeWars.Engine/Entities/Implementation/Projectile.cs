using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class Projectile : NotifiableBase, IMovableObject
    {
        private IGameField _field;

        private readonly IFieldPlayer _parent;

        public Projectile(IFieldPlayer parent)
        {
            _parent = parent;
        }

        public bool Collide(ICollidable target)
        {
            if (!target.Destroyed)
            {
                target.Destroy();
            }

            Destroy();
            return true;
        }

        public void Destroy()
        {
            _field.RemoveObject(this);
            Destroyed = true;
            Notify();
            ClearSubscriptions();
        }

        public Position Position { get; set; }

        public AreaDescriptor GetArea()
        {
            return new AreaDescriptor(Position, 1, 1);
        }

        public void UpdateState()
        {
            MoveUp();
        }

        public void MoveUp()
        {
            if (_field.TryMoveUp(this))
            {
                Notify();
            }
            else
            {
                Destroy();
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
                Destroy();
            }
        }

        public void MoveLeft()
        {
            if (_field.TryMoveLeft(this))
            {
                Notify();
            }
            else
            {
                Destroy();
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
                Destroy();
            }
        }

        public void AcceptField(IGameField field)
        {
            _field = field;
            _field.AddObject(this);
        }

        public bool Destroyed { get; private set; }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("position", Position.GetDescription());
            description.Add("area", GetArea().GetDescription());
            description.Add("parent", _parent.Client.ConnectionId);
            return description;
        }

        public override bool Equals(object obj)
        {
            var projectile = obj as Projectile;

            if (projectile == null)
            {
                return false;
            }

            return Equals(projectile);
        }

        private bool Equals(Projectile other)
        {
            return Equals(Position, other.Position);
        }

        public override int GetHashCode()
        {
            return Position != null ? Position.GetHashCode() : 0;
        }
    }
}