using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwilightAssistantAPI.Data;
using TwilightAssistantAPI.Models;

namespace TwilightAssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        DataContext _context;
        public GameController(DataContext context)
        {
            _context = context;
        }

        //Get all basic game info
        [HttpGet]
        public async Task<ActionResult<List<Game>>> GetBasicGames()
        {
            var games = await _context.Games.ToListAsync();
            return Ok(games);
        }

        //Create game
        [HttpPost]
        public async Task<ActionResult<List<Game>>> CreateGame(Game gamePost)
        {
            //Add game to database and save changes
            await _context.Games.AddAsync(gamePost);
            await _context.SaveChangesAsync();

            //Get the updated list from the database to return
            var games = await _context.Games.ToListAsync();
            return Ok(games);
        }


        //Update game (active/inactive)
        [HttpPut("{GuidId}")]
        public async Task<ActionResult<List<Game>>> UpdateGame(string GuidId)
        {
            //Find the key of the game with the passed GuidId value
            var game = _context.Games.Where(g => g.GuidId == GuidId).FirstOrDefault();
            if (game == null)
            {
                return NotFound("Game not found.");
            }
            int key = game.Id;

            //Get the game from the database
            var result = await _context.Games.FindAsync(key);
            if (result == null)
            {
                return NotFound("Game not found.");
            }

            //Change the active status
            result.IsActive = false;

            //Save the Db
            await _context.SaveChangesAsync();

            //Get the updated list of Games from Db
            var games = await _context.Games.ToListAsync();

            return Ok(games);
        }

        //Delete Game
        [HttpDelete("{GuidId}")]
        public async Task<ActionResult<List<Game>>> DeleteGame(string GuidId)
        {
            //Find game with passed GuidId
            var game = _context.Games.Where(g => g.GuidId == GuidId).FirstOrDefault();
            if(game == null)
            {
                return NotFound("Game not found.");
            }
            
            //Remove game and save Db
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            //Get updated list and return it in the response body
            var games = await _context.Games.ToListAsync();
            return Ok(games);
        }


    }
}
