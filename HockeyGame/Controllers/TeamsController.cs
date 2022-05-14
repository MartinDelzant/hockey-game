using HockeyGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HockeyGame.Controllers
{
    [Route("api/team")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly HockeyGameDbContext _context;

        public TeamsController(HockeyGameDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a team from its ID
        /// </summary>
        /// <example>GET: api/team/5</example>
        /// <param name="id"></param>
        /// <returns>Th given team</returns>
        [HttpGet("{year}")]
        public ActionResult<TeamWithPlayers> GetTeam(int year)
        {
            // No async here for the sake of simplicity in the associated unit test
            var team = _context.Team.FirstOrDefault(t => t.Year == year);
            // Assuming team exists

            var teamPlayers = GetTeamPlayers(team.Id);

            return new TeamWithPlayers(team, teamPlayers);
        }

        /// <summary>
        /// Create a team
        /// </summary>
        /// <returns>Created team</returns>
        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            var addedTeam = await _context.Team.AddAsync(team);

            await _context.SaveChangesAsync();

            return addedTeam.Entity;
        }

        /// <summary>
        /// Add a player to a team
        /// </summary>
        /// <param name="year">The year the team played</param>
        /// <param name="player">The player to add</param>
        /// <returns></returns>
        [HttpPost("{year}")]
        public async Task<ActionResult<Player>> AddPlayerToTeam(int year, Player player)
        {
            // Get team from year
            var team = await _context.Team.FirstAsync(t => t.Year == year);
            // Assuming team exists here.

            // Assuming player does not exist yet
            // Add player in DB
            var addedPlayer = await _context.Player.AddAsync(player);

            // Add player <> team link
            await _context.PlayerTeams.AddAsync(new PlayerTeam
            {
                PlayerId = addedPlayer.Entity.Id,
                TeamId = team.Id
            });

            await _context.SaveChangesAsync();

            return addedPlayer.Entity;
        }

        private IReadOnlyList<Player> GetTeamPlayers(int teamId)
        {
            return (from team in _context.Team
                join teamPlayer in _context.PlayerTeams on team.Id equals teamPlayer.TeamId
                join player in _context.Player on teamPlayer.PlayerId equals player.Id
                where team.Id == teamId
                select player).ToList();
        }
    }
}
