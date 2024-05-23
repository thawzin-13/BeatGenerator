using BeatGeneratorAPI.Const;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeatGeneratorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeatGeneratorController : ControllerBase
    {

        [HttpGet("GetConfiguration")]
        public IActionResult GetConfiguration()
        {
            return Ok(BeatGeneratorAPIConst.configuration);
        }

        [HttpGet("beatgenerator/{i:int}/{j:int}")]
        public IActionResult GetBeatSequence(int i, int j)
        {
            if (i <= j)
            {
                return BadRequest("i must be greater than j");
            }

            var result = new List<string>();

            for (int num = i; num >= j; num--)
            {
                if (num % 12 == 0)
                {
                    result.Add(BeatGeneratorAPIConst.configuration[12]);
                }
                else if (num % 4 == 0)
                {
                    result.Add(BeatGeneratorAPIConst.configuration[4]);
                }
                else if (num % 3 == 0)
                {
                    result.Add(BeatGeneratorAPIConst.configuration[3]);
                }
                else
                {
                    result.Add(BeatGeneratorAPIConst.configuration[1]);
                }
            }

            return Ok(result);

        }

        [HttpPost("configure/{i:int}/{text}")]
        public IActionResult Configure(int i, string text)
        {
            var validMultiples = new List<int> { 1,3,4,12};
            var validValues = new List<string> { "snare", "kick", "Hi-Hat", "Low Floor Tom", "cymbal", "Low-Mid Tom", "Bass Drum" };

            if (!validMultiples.Contains(i) || !validValues.Contains(text))
            {
                return BadRequest("Invalid Configuration");
            }

            BeatGeneratorAPIConst.configuration[i] = text;

            return Ok(text + " has been configured");
        }

        [HttpPost("Reset")]
        public IActionResult Reset()
        {
            BeatGeneratorAPIConst.configuration = new Dictionary<int, string>()
            {
                { 1, "Low Floor Tom" },
                { 3, "kick" },
                { 4, "snare" },
                { 12, "Hi-Hat" }
            };

            return Ok("Successfully Reset");
        }

    }
}
