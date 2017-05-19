using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Engine.Entities
{
    public interface IGameField: IDescribable, IHasArea
    {
        void Enter(IFieldPlayer player);
        bool Exit(GameClient client);
        void AddObject(IFieldObject fieldObject);
        bool TryMoveDown(IMovableObject movable);
        bool TryMoveLeft(IMovableObject movable);
        bool TryMoveRight(IMovableObject movable);
        bool TryMoveUp(IMovableObject movable);
        bool HasSpot(GameClient client);
        bool IsEmpty();
        IFieldPlayer GetPlayer(GameClient client);
        void RemoveObject(IFieldObject movable);
    }
}