using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPoke : NotifiableBase
    {
        private Field _field;
        private ICollection<Projectile> _projectiles;

        public GameClient Client { get; }

        public Position Position { get; set; }

        public SlowPoke(GameClient client)
        {
            Client = client;
            _projectiles = new Collection<Projectile>();
        }

        public void MoveLeft()
        {
            if (_field.TryMoveLeft())
            {
                Position.X--;
            }
        }

        public void MoveRight()
        {
            if (_field.TryMoveRight())
            {
                Position.X++;
            }
        }

        public void MoveDown()
        {
            if (_field.TryMoveDown())
            {
                Position.Y--;
            }
        }

        public void MoveUp()
        {
            if (_field.TryMoveUp())
            {
                Position.Y++;
            }
        }

        public void Fire()
        {
            
        }

        public void AcceptGameTicker(IGameTicker ticker)
        {
            ticker.SubscribeNotifications(BumpState);
        }

        public void EnterField(Field field)
        {
            _field = field;
        }

        private void BumpState()
        {
            Notify();
        }
    }

    public class Projectile
    {
    }
}
