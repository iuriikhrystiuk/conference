using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
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

        public async Task<string> Enter(string name)
        {
            var game = _gameCoordinator.Apply(new GameClient(Context.ConnectionId, name));
            await Groups.Add(Context.ConnectionId, game);
            return game;
        }

        public Task Leave()
        {
            var game = _gameCoordinator.Leave(new GameClient(Context.ConnectionId));
            if (!string.IsNullOrEmpty(game))
            {
                return Groups.Remove(Context.ConnectionId, game);
            }

            return Task.CompletedTask;
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await Leave();

            await base.OnDisconnected(stopCalled);
        }
    }
}