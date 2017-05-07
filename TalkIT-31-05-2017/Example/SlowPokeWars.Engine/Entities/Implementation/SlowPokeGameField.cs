using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPokeGameField : IGameField
    {
        private Position _topLeft = new Position(0, 0);

        private Position _bottomRight = new Position(100, 100);

        private IFieldPlayer _topPlayer;

        private IFieldPlayer _bottomPlayer;

        private readonly ICollection<IFieldObject> _fieldObjects = new List<IFieldObject>();

        private readonly ICollisionDetector _collisionDetector;

        public SlowPokeGameField(ICollisionDetector collisionDetector)
        {
            _collisionDetector = collisionDetector;
        }

        public bool TryMoveLeft(IMovableObject movable)
        {
            movable.Position.X--;
            var succeeded = true;
            var collisions = _collisionDetector.GetCollisions(movable, _fieldObjects.Except(Enumerable.Repeat(movable, 1)));
            foreach (var collidable in collisions)
            {
                if (collidable.Collide(movable))
                {
                    succeeded = false;
                }
            }
            return succeeded;
        }

        public bool TryMoveRight(IMovableObject movable)
        {
            return true;
        }

        public bool TryMoveUp(IMovableObject movable)
        {
            return true;
        }

        public bool HasSpot()
        {
            return _bottomPlayer == null || _topPlayer == null;
        }

        public bool IsEmpty()
        {
            return _bottomPlayer == null && _topPlayer == null;
        }

        public bool TryMoveDown(IMovableObject movable)
        {
            return true;
        }

        public void Enter(IFieldPlayer player)
        {
            if (_topPlayer == null)
            {
                _topPlayer = player;
                _fieldObjects.Add(player);
            }
            else if (_bottomPlayer == null)
            {
                _bottomPlayer = player;
            }
            else
            {
                throw new Exception("Field is full!");
            }
        }

        public bool Exit(GameClient client)
        {
            if (_topPlayer.Client.ConnectionId == client.ConnectionId)
            {
                _topPlayer = null;
                _fieldObjects.Remove(_topPlayer);
                return true;
            }

            if (_bottomPlayer.Client.ConnectionId == client.ConnectionId)
            {
                _topPlayer = null;
                _fieldObjects.Remove(_bottomPlayer);
                return false;
            }

            return false;
        }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("top", _topPlayer?.GetDescription());
            description.Add("bottom", _bottomPlayer?.GetDescription());
            description.Add("objects", new JArray(_fieldObjects.Except(new List<IFieldObject> { _topPlayer, _bottomPlayer })));
            return description;
        }
    }
}