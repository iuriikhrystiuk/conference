﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SlowPokeWars.Engine.Entities
{
    public class SlowPokeCollisionDetector : ICollisionDetector
    {
        public IList<ICollidable> GetCollisions(IFieldObject targetObject, IEnumerable<IFieldObject> objectsOnField)
        {
            var targetObjectArea = targetObject.GetArea();

            var objectsToCollide = new List<ICollidable>();

            foreach (var fieldObject in objectsOnField.ToList())
            {
                var fieldObjectArea = fieldObject.GetArea();
                if (fieldObjectArea.Intersects(targetObjectArea) > 0)
                {
                    objectsToCollide.Add(fieldObject);
                }
            }

            return objectsToCollide;
        }
    }
}