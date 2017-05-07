using System;
using System.Collections.Generic;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPokeCollisionDetector : ICollisionDetector
    {
        public IEnumerable<ICollidable> GetCollisions(IFieldObject targetObject, IEnumerable<IFieldObject> objectsOnField)
        {
            throw new NotImplementedException();
        }
    }
}