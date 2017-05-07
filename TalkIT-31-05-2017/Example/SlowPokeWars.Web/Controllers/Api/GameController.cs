using System.Runtime.InteropServices;
using SlowPokeWars.Engine.Game;
using System.Web.Http;

namespace SlowPokeWars.Web.Controllers.Api
{
    public class GameController : ApiController
    {
        private readonly IGameCoordinator _gameCoordinator;

        public GameController(IGameCoordinator gameCoordinator)
        {
            _gameCoordinator = gameCoordinator;
        }

        [HttpGet]
        public IHttpActionResult GetDescription(string gameIdentifier)
        {
            var game = _gameCoordinator.GetGame(gameIdentifier);
            return Ok(game.GetDescription());
        }
    }
}