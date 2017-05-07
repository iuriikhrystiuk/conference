using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public interface IGameField: IDescribable
    {
        void Enter(IFieldPlayer player);
        bool Exit(GameClient client);
        bool TryMoveDown(IMovableObject movable);
        bool TryMoveLeft(IMovableObject movable);
        bool TryMoveRight(IMovableObject movable);
        bool TryMoveUp(IMovableObject movable);
        bool HasSpot();
        bool IsEmpty();
    }
}