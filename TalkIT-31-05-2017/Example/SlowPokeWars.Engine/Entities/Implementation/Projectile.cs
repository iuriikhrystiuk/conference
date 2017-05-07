namespace SlowPokeWars.Engine.Entities
{
    public class Projectile : IMovableObject
    {
        public bool Collide(ICollidable target)
        {
            if (!target.Destroyed)
            {
                target.Destroy();
            }

            Destroy();
            return true;
        }

        public void Destroy()
        {
            Destroyed = true;
        }

        public Position Position { get; set; }
        public AreaDescriptor GetArea()
        {
            return new AreaDescriptor(1, 1);
        }

        public void MoveUp()
        {
            throw new System.NotImplementedException();
        }

        public void MoveDown()
        {
            throw new System.NotImplementedException();
        }

        public void MoveLeft()
        {
            throw new System.NotImplementedException();
        }

        public void MoveRight()
        {
            throw new System.NotImplementedException();
        }

        public bool Destroyed { get; private set; }
    }
}