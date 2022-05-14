namespace HockeyGame.Models
{
    public class TeamWithPlayers
    {
        public int Id { get; }
        public string Coach { get; }
        public int Year { get; }
        public IReadOnlyList<Player> Players { get; }

        public TeamWithPlayers(Team team, IReadOnlyList<Player> players)
        {
            Id = team.Id;
            Coach = team.Coach;
            Year = team.Year;
            Players = players;
        }
    }
}
