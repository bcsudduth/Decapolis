using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Decapolis.Controllers
{
    [Route("decapolis")]
    public class DecapolisController : ControllerBase
    {
        public string[] GREEK_DECAPOLIS =
        {
            "Abila",
            "Capitolis",
            "Gadara",
            "Gerasa",
            "Hippos",
            "Konata",
            "Pella",
            "Philadelphia",
            "Philoterio",
            "Scythopolis"
        };

        public string[] MOST_POPULATED_US_CITIES =
        {
            "New York",
            "Los Angeles",
            "Chicago",
            "Houston",
            "Phoenix",
            "Philadelphia",
            "San Antonio",
            "San Diego",
            "Dallas",
            "San Jose"
        };


        [HttpGet]
        [Route("ancientgreece")]
        public async Task<ActionResult<string[]>> GetGreekDecapolis()
        {
            return Ok(GREEK_DECAPOLIS);
        }

        [HttpGet]
        [Route("usalargest")]
        public async Task<ActionResult<string[]>> GetUSALargest()
        {
            return Ok(MOST_POPULATED_US_CITIES);
        }
        
    }
}