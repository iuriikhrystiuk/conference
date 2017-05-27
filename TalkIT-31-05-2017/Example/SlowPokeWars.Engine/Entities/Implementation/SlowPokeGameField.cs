using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPokeGameField : IGameField
    {
        private static readonly Position TopStartingPosition = new Position(50, 97, new ReverseMovementActor());
        private static readonly Position BottomStatingPosition = new Position(50, 3, new DefaultMovementActor());

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

        public void Reset()
        {
            for (int i = _fieldObjects.Count - 1; i >= 0; i--)
            {
                if (!_fieldObjects.ElementAt(i).Equals(_topPlayer) &&
                    !_fieldObjects.ElementAt(i).Equals(_bottomPlayer))
                {
                    RemoveObject(_fieldObjects.ElementAt(i));
                }
            }
            _topPlayer.Position = TopStartingPosition.Clone();
            _topPlayer.Destroyed = false;
            _bottomPlayer.Position= BottomStatingPosition.Clone();
            _topPlayer.Destroyed = false;
        }

        public void Enter(IFieldPlayer player)
        {
            if (_topPlayer == null)
            {
                _topPlayer = player;
                player.Position = TopStartingPosition.Clone();
                player.AcceptField(this);
                _fieldObjects.Add(_topPlayer);
            }
            else if (_bottomPlayer == null)
            {
                _bottomPlayer = player;
                player.Position = BottomStatingPosition.Clone();
                player.AcceptField(this);
                _fieldObjects.Add(_bottomPlayer);
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

        public AreaDescriptor GetArea()
        {
            return area;
        }

        private bool Collide(IMovableObject movable)
        {
            var succeeded = true;
            if (!CheckBoundaries(movable))
            {
                return false;
            }

            var collisions = _collisionDetector.GetCollisions(movable, _fieldObjects);
            foreach (var collidable in collisions)
            {
                if (movable.Collide(collidable))
                {
                    succeeded = false;
                    if (movable.Destroyed && collidable.Destroyed)
                    {
                        break;
                    }
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
    }
}