using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPoke : NotifiableBase, IFieldPlayer
    {
        private IGameField _field;
        private ICollection<Projectile> _projectiles;

        public GameClient Client { get; }

        public Position Position { get; set; }

        public bool Destroyed { get; private set; }

        public AreaDescriptor GetArea()
        {
            return new PlayerAreaDescriptor(Position);
        }

        public SlowPoke(GameClient client)
        {
            Client = client;
            _projectiles = new Collection<Projectile>();
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

        public void MoveDown()
        {
            if (_field.TryMoveDown(this))
            {
                Notify();
            }
        }

        public void MoveUp()
        {
            if (_field.TryMoveUp(this))
            {
                Notify();
            }
        }

        public void Fire()
        {
            var projectile = new Projectile();
            projectile.Position = new Position(1, 1, Position.MovementActor);
            _projectiles.Add(projectile);
            projectile.AcceptField(_field);
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
