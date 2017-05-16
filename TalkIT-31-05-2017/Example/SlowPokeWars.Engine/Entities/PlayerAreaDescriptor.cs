namespace SlowPokeWars.Engine.Entities
{
    public class PlayerAreaDescriptor : AreaDescriptor
    {
        private const int WidthIncrement = 2;

        public PlayerAreaDescriptor(Position playerPosition)
            : base(new Position(playerPosition.X + playerPosition.MovementActor.GetIncrement() * WidthIncrement, playerPosition.Y + playerPosition.MovementActor.GetIncrement()),
                new Position(playerPosition.X + playerPosition.MovementActor.GetDecrement() * WidthIncrement, playerPosition.Y + playerPosition.MovementActor.GetDecrement()))
        {
        }
    }
}