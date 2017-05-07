using System.Collections.Generic;

namespace SlowPokeWars.Engine.Entities
{
    public interface ICollisionDetector
    {
        IEnumerable<ICollidable> GetCollisions(IFieldObject targetObject, IEnumerable<IFieldObject> objectsOnField);
    }
}