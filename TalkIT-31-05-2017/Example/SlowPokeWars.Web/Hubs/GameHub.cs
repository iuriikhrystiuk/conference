using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using SlowPokeWars.Engine.Entities;
using SlowPokeWars.Engine.Game;

namespace SlowPokeWars.Web.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameCoordinator _gameCoordinator;

        public GameHub(IGameCoordinator gameCoordinator)
        {
            _gameCoordinator = gameCoordinator;
        }

        public async Task<JObject> Enter(string name)
        {
            var game = _gameCoordinator.Apply(new GameClient(Context.ConnectionId, name));
            await Groups.Add(Context.ConnectionId, game.GetIdentifier());

            return game.GetDescription();
        }

        public async Task<JObject> Leave()
        {
            var game = _gameCoordinator.Leave(new GameClient(Context.ConnectionId));
            if (game != null)
            {
                await Groups.Remove(Context.ConnectionId, game.GetIdentifier());
                return game.GetDescription();
            }

            return null;
        }

        public void MoveLeft(string gameIdentifier)
        {
            var game = _gameCoordinator.GetGame(gameIdentifier);
            game.MoveLeft(Context.ConnectionId);
        }

        public void MoveRight(string gameIdentifier)
        {
            var game = _gameCoordinator.GetGame(gameIdentifier);
            game.MoveRight(Context.ConnectionId);
        }

        public void MoveUp(string gameIdentifier)
        {
            var game = _gameCoordinator.GetGame(gameIdentifier);
            game.MoveUp(Context.ConnectionId);
        }

        public void MoveDown(string gameIdentifier)
        {
            var game = _gameCoordinator.GetGame(gameIdentifier);
            game.MoveDown(Context.ConnectionId);
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await Leave();

            await base.OnDisconnected(stopCalled);
        }
    }
}