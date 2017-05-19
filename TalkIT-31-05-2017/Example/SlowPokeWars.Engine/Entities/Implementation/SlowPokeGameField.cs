using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPokeGameField : IGameField
    {
        private AreaDescriptor area = new AreaDescriptor(new Position(50, 50, new DefaultMovementActor()), 50, 50);

        private IFieldPlayer _topPlayer;

        private IFieldPlayer _bottomPlayer;

        private readonly ICollection<IFieldObject> _fieldObjects = new List<IFieldObject>();

        private readonly ICollisionDetector _collisionDetector;

        private readonly IGameTicker _gameTicker;

        public SlowPokeGameField(ICollisionDetector collisionDetector, IGameTicker gameTicker)
        {
            _collisionDetector = collisionDetector;
            _gameTicker = gameTicker;
        }

        public bool TryMoveLeft(IMovableObject movable)
        {
            movable.Position.DecrementAbscissa();
            return Collide(movable);
        }

        public void AddObject(IFieldObject fieldObject)
        {
            _fieldObjects.Add(fieldObject);
            _gameTicker.SubscribeNotifications(fieldObject.UpdateState);
            _gameTicker.Start();
        }

        public bool TryMoveDown(IMovableObject movable)
        {
            movable.Position.DecrementOrdinata();
            return Collide(movable);
        }

        public bool TryMoveRight(IMovableObject movable)
        {
            movable.Position.IncrementAbscissa();
            return Collide(movable);
        }

        public bool TryMoveUp(IMovableObject movable)
        {
            movable.Position.IncrementOrdinata();
            return Collide(movable);
        }

        public bool HasSpot(GameClient client)
        {
            return (_bottomPlayer == null && !(_topPlayer?.Client.Equals(client) ?? false)) ||
                (_topPlayer == null && !(_bottomPlayer?.Client.Equals(client) ?? false));
        }

        public bool IsEmpty()
        {
            return _bottomPlayer == null && _topPlayer == null;
        }

        public IFieldPlayer GetPlayer(GameClient client)
        {
            if (_topPlayer.Client.Equals(client))
            {
                return _topPlayer;
            }

            if (_bottomPlayer.Client.Equals(client))
            {
                return _bottomPlayer;
            }

            return null;
        }

        public void RemoveObject(IFieldObject movable)
        {
            _gameTicker.UnsubscribeNotifications(movable.UpdateState);
            _fieldObjects.Remove(movable);

            if (!_fieldObjects.Any())
            {
                _gameTicker.Stop();
            }
        }

        public void Enter(IFieldPlayer player)
        {
            if (_topPlayer == null)
            {
                _topPlayer = player;
                _fieldObjects.Add(player);
                player.Position = new Position(50, 99, new ReverseMovementActor());
                player.AcceptField(this);
            }
            else if (_bottomPlayer == null)
            {
                _bottomPlayer = player;
                _fieldObjects.Add(player);
                player.Position = new Position(50, 1, new DefaultMovementActor());
                player.AcceptField(this);
            }
            else
            {
                throw new Exception("Field is full!");
            }
        }

        public bool Exit(GameClient client)
        {
            if (_topPlayer?.Client.Equals(client) ?? false)
            {
                _fieldObjects.Remove(_topPlayer);
                _topPlayer = null;
                return true;
            }

            if (_bottomPlayer?.Client.Equals(client) ?? false)
            {
                _fieldObjects.Remove(_bottomPlayer);
                _bottomPlayer = null;
                return true;
            }

            return false;
        }

        public JObject GetDescription()
        {
            var description = new JObject();
            description.Add("area", GetArea().GetDescription());
            description.Add("top", _topPlayer?.GetDescription());
            description.Add("bottom", _bottomPlayer?.GetDescription());

            var objects = _fieldObjects.Except(new List<IFieldObject> { _topPlayer, _bottomPlayer }).ToList();
            description.Add("objects", objects.Any() ? new JArray(objects.Select(o => o.GetDescription())) : null);
            return description;
        }

        private bool Collide(IMovableObject movable)
        {
            var succeeded = true;
            if (!CheckBoundaries(movable))
            {
                return false;
            }

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

        private bool CheckBoundaries(IFieldObject fieldObject)
        {
            var fieldArea = GetArea();
            var objectArea = fieldObject.GetArea();

            var intersection = fieldArea.Intersects(objectArea);

            return intersection == objectArea.GetAreaValue();
        }

        public AreaDescriptor GetArea()
        {
            return area;
        }
    }
}