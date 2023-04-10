using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwilightAssistantAPI.Data;
using TwilightAssistantAPI.Models;

namespace TwilightAssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerProfileController : ControllerBase
    {
        //Provide db context
        DataContext _context;
        //Constructor with DbContext injected
        public PlayerProfileController(DataContext context)
        {
            _context= context;
        }

        //Get method to display all player profiles
        [HttpGet]
        public async Task<ActionResult<List<PlayerProfile>>> AllPlayerProfiles()
        {
            var profiles = await _context.PlayerProfiles.ToListAsync();
            return Ok(profiles);
        }

        //Post method to create a new player profile
        [HttpPost]
        public async Task<ActionResult<List<PlayerProfile>>> CreatePlayerProfile(PlayerProfile profilePost)
        {
            //Take the posted profile and add it to the db. Save changes.
            _context.PlayerProfiles.Add(profilePost);
            await _context.SaveChangesAsync();

            //Get updated list.
            var profiles = await _context.PlayerProfiles.ToListAsync();

            //Return OK status code with updated list.
            return Ok(profiles);
        }

        //Delete a player based on the given GuidID.
        [HttpDelete("{guidid}")]
        public async Task<ActionResult<List<PlayerProfile>>> DeleteProfile(string guidid)
        {
            //Find the profile with the given Guid. (Guids are created in app when a profile is made)
            var profile = _context.PlayerProfiles.Where(p => p.GuidId == guidid).FirstOrDefault();
            if(profile == null)
            {
                return NotFound("Player profile Not Found.");
            }
            
            //Get the corresponding PrimaryKey value for the db item. Get the profile from the Db.
            int key = profile.Id;
            var result = await _context.PlayerProfiles.FindAsync(key);

            //If found, remove from the db and save changes.
            if(result == null)
            {
                return NotFound("Player profile Not Found.");
            }
            _context.PlayerProfiles.Remove(result);
            await _context.SaveChangesAsync();

            //Get updated list
            var profiles = await _context.PlayerProfiles.ToListAsync();

            //Return OK status code with updated list of profiles.
            return Ok(profiles);
        }

        //Find a player based on the given GuidID.
        [HttpGet("{guidid}")]
        public async Task<ActionResult<PlayerProfile>> GetSingleProfile(string guidid)
        {
            //Find the player with the given Guid.
            var profile = _context.PlayerProfiles.Where(p => p.GuidId == guidid).FirstOrDefault();
            if(profile == null)
            {
                return NotFound("Player profile not found.");
            }
            
            //Get the primary key value for the player with the given guid. Get the player from the Db.
            int key = profile.Id;
            var result = await _context.PlayerProfiles.FindAsync(key);

            //Return OK status code with specified player.
            return Ok(result);
        }

    }
}
