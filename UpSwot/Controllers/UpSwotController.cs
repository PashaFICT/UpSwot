using Microsoft.AspNetCore.Mvc;
using UpSwot.Core;
using UpSwot.Core.Models;

namespace UpSwot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpSwotController : Controller
    {
        private IPersonManager _personManager;
        public UpSwotController(IPersonManager personManager)
        {
            _personManager = personManager;
        }
        [HttpPost("/exists/person")]
        public async Task<IActionResult> ExistsPersonInEpisodeAsync([FromBody] EpisodPersonRequest episodePerson)
        {
            var res = await _personManager.CheckPersonEpisodAsync(episodePerson.EpisodeName, episodePerson.CharacterName);
                if (res == null)
                    return NotFound();
                return Ok(res);
        }

        [HttpGet("/GetPerson")]
        public IActionResult GetPerson(string name)
        {
            Task<List<PersonDto>> person = _personManager.FindPersonAsync(name);
                return Ok(person.Result);
        }
    }
}
