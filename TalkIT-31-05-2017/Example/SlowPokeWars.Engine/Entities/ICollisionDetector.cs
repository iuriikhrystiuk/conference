using System.Collections.Generic;

namespace SlowPokeWars.Engine.Entities
{
    public interface ICollisionDetector
    {
        IList<ICollidable> GetCollisions(IFieldObject targetObject, IEnumerable<IFieldObject> objectsOnField);
    }
}