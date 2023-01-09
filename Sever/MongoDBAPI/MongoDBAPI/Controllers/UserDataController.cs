using Microsoft.AspNetCore.Mvc;
using MongoDBAPI.Services;
using MongoDBAPI.Classes;
using System.Xml.Linq;

namespace MongoDBAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly UserDataService _userDataService;

        public UserDataController(UserDataService UserDatasService) =>
            _userDataService = UserDatasService;


        [HttpGet]
        public async Task<List<UserData>> Get() =>
            await _userDataService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserData>> GetByID(string id)
        {
            var UserData = await _userDataService.GetAsync(id);

            if (UserData is null)
            {
                return NotFound();
            }

            return UserData;
        }


        [HttpGet("GetUser/{username}")]
        public async Task<ActionResult<UserData>> GetByUser(string username)
        {
            var UserData = await _userDataService.GetAsyncByUser(username);

            if (UserData is null)
            {
                return NotFound();
            }

            return UserData;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserData newUserData)
        {
            await _userDataService.CreateAsync(newUserData);

            return CreatedAtAction(nameof(Get), new { id = newUserData.Id }, newUserData);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, UserData updatedUserData)
        {
            var UserData = await _userDataService.GetAsync(id);

            if (UserData is null)
            {
                return NotFound();
            }

            updatedUserData.Id = UserData.Id;

            await _userDataService.UpdateAsync(id, updatedUserData);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var UserData = await _userDataService.GetAsync(id);

            if (UserData is null)
            {
                return NotFound();
            }

            await _userDataService.RemoveAsync(id);

            return NoContent();
        }
    }
}
