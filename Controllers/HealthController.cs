using Microsoft.AspNetCore.Mvc;

namespace BeatGeneratorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private static bool _isReady = false;

        [HttpGet("livez")]
        public IActionResult GetLivez()
        {
            return Ok();
        }

        [HttpGet("readyz")]
        public IActionResult GetReadyz()
        {
            if (_isReady)
            {
                return Ok();
            }
            else
            {
                return StatusCode(503, "Not ready");
            }
        }

        public static void SetReady()
        {
            _isReady = true;
        }

        public static void SetNotReady()
        {
            _isReady = false;
        }
    }
}
