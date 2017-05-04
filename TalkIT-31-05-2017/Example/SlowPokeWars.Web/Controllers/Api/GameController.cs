using SlowPokeWars.Engine.Game;
using System.Web.Http;

namespace SlowPokeWars.Web.Controllers.Api
{
    public class GameController : ApiController
    {
        private readonly IGameCoordinator gameCoordinator;

        public GameController(IGameCoordinator gameCoordinator)
        {
            this.gameCoordinator = gameCoordinator;
        }

        [HttpGet]
        public IHttpActionResult Apply()
        {
            return Ok();
        }
    }
}