using Microsoft.AspNetCore.Mvc;

namespace CityTrivia.WebApi.Controllers {
    [Route("/")]
    public class HomeController : ControllerBase {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get() {
            return Ok(DateTime.UtcNow.ToString());
        }
    }
}
