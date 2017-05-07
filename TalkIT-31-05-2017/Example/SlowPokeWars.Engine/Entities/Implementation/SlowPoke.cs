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

        public Position Position { get; private set; }

        public bool Destroyed { get; private set; }

        public AreaDescriptor GetArea()
        {
            return new AreaDescriptor(2, 1);
        }

        public SlowPoke(GameClient client)
        {
            Client = client;
            Position = new Position(0, 0);
            _projectiles = new Collection<Projectile>();
        }

        public void MoveLeft()
        {
            if (_field.TryMoveLeft(this))
            {
            }
        }

        public void MoveRight()
        {
            if (_field.TryMoveRight(this))
            {
            }
        }

        public void MoveDown()
        {
            if (_field.TryMoveDown(this))
            {
            }
        }

        public void MoveUp()
        {
            if (_field.TryMoveUp(this))
            {
            }
        }

        public void Fire()
        {

        }

        public void AcceptGameTicker(IGameTicker ticker)
        {
            ticker.SubscribeNotifications(BumpState);
        }

        public void EnterField(IGameField field)
        {
            _field = field;
        }

        private void BumpState()
        {
            Notify();
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
            description.Add("position", Position.GetDescription());
            description.Add("area", GetArea().GetDescription());
            return description;
        }
    }
}
