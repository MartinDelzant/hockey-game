using HockeyGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HockeyGame.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly HockeyGameDbContext _context;

        public PlayersController(HockeyGameDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all players
        /// </summary>
        /// <returns>A list of all players</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer()
        {
            return await _context.Player.ToListAsync();
        }

        /// <summary>
        /// Get a player from his ID
        /// </summary>
        /// <param name="id">The id of the player to get</param>
        /// <returns>The player</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            // Assuming player exists
            return await _context.Player.FindAsync(id);
        }

        /// <summary>
        /// Set a given player as capitain
        /// </summary>
        /// <example>PUT: api/player/5/capitain</example>
        /// <param name="id">Player ID</param>
        /// <returns>Modified player</returns>
        [HttpPut("{id}/capitain")]
        public async Task<ActionResult<Player>> PutPlayerCapitain(int id)
        {
            var player = await _context.Player.FindAsync(id);

            if (player.IsCapitain)
            {
                return player;
            }

            player.IsCapitain = true;

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return player;
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
    }
}
