using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class Projectile : NotifiableBase, IMovableObject
    {
        private IGameField _field;

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
            Destroyed = true;
        }

        public Position Position { get; set; }

        public AreaDescriptor GetArea()
        {
            return new AreaDescriptor(new Position(1, 1), new Position(2, 2));
        }

        public void MoveUp()
        {
            if (_field.TryMoveUp(this))
            {
                Notify();
            }
        }

        public void MoveDown()
        {
            if (_field.TryMoveDown(this))
            {
                Notify();
            }
        }

        public void MoveLeft()
        {
            if (_field.TryMoveLeft(this))
            {
                Notify();
            }
        }

        public void MoveRight()
        {
            if (_field.TryMoveRight(this))
            {
                Notify();
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
            return description;
        }
    }
}