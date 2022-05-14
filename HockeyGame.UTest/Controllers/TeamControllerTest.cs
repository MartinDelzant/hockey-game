using System.Linq;
using NUnit.Framework;
using HockeyGame.Controllers;
using HockeyGame.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HockeyGame.UTest.Controllers
{
    public class TeamControllerTest
    {
        [Test]
        public void GetTeamPerYear()
        {
            var teams = new[]
            {
                new Team {Coach = "coach", Id = 123, Year = 345}
            }.AsQueryable();

            var mockedTeamDbSet = new Mock<DbSet<Team>>();
            mockedTeamDbSet.As<IQueryable<Team>>().Setup(c => c.Provider).Returns(teams.Provider);
            mockedTeamDbSet.As<IQueryable<Team>>().Setup(c => c.Expression).Returns(teams.Expression);
            mockedTeamDbSet.As<IQueryable<Team>>().Setup(c => c.ElementType).Returns(teams.ElementType);
            mockedTeamDbSet.As<IQueryable<Team>>().Setup(c => c.GetEnumerator()).Returns(teams.GetEnumerator());

            var players = new[]
            {
                new Player {IsCapitain = false, LastName = "foo", Name = "bar", Number = 10, Id = 99, Position = "goal"}
            }.AsQueryable();

            var mockedPlayerDbSet = new Mock<DbSet<Player>>();
            mockedPlayerDbSet.As<IQueryable<Player>>().Setup(c => c.Provider).Returns(players.Provider);
            mockedPlayerDbSet.As<IQueryable<Player>>().Setup(c => c.Expression).Returns(players.Expression);
            mockedPlayerDbSet.As<IQueryable<Player>>().Setup(c => c.ElementType).Returns(players.ElementType);
            mockedPlayerDbSet.As<IQueryable<Player>>().Setup(c => c.GetEnumerator()).Returns(players.GetEnumerator());

            var playerTeams = new[]
            {
                new PlayerTeam { Id = 12, PlayerId = 99, TeamId = 123}
            }.AsQueryable();

            var mockedPlayerTeamDbSet = new Mock<DbSet<PlayerTeam>>();
            mockedPlayerTeamDbSet.As<IQueryable<PlayerTeam>>().Setup(c => c.Provider).Returns(playerTeams.Provider);
            mockedPlayerTeamDbSet.As<IQueryable<PlayerTeam>>().Setup(c => c.Expression).Returns(playerTeams.Expression);
            mockedPlayerTeamDbSet.As<IQueryable<PlayerTeam>>().Setup(c => c.ElementType).Returns(playerTeams.ElementType);
            mockedPlayerTeamDbSet.As<IQueryable<PlayerTeam>>().Setup(c => c.GetEnumerator()).Returns(playerTeams.GetEnumerator());

            var mockedContext = new Mock<HockeyGameDbContext>();
            mockedContext.Setup(c => c.Team).Returns(mockedTeamDbSet.Object);
            mockedContext.Setup(c => c.Player).Returns(mockedPlayerDbSet.Object);
            mockedContext.Setup(c => c.PlayerTeams).Returns(mockedPlayerTeamDbSet.Object);

            var teamsController = new TeamsController(mockedContext.Object);

            var teamWithPlayers = teamsController.GetTeam(345);
            Assert.AreEqual("coach", teamWithPlayers.Value.Coach);
            Assert.AreEqual(123, teamWithPlayers.Value.Id);
            Assert.AreEqual(345, teamWithPlayers.Value.Year);

            Assert.AreEqual(1, teamWithPlayers.Value.Players.Count);
            var player = teamWithPlayers.Value.Players.First();

            Assert.IsFalse(player.IsCapitain);
            Assert.AreEqual("goal", player.Position);
            Assert.AreEqual("foo", player.LastName);
            Assert.AreEqual("bar", player.Name);
            Assert.AreEqual(10, player.Number);
            Assert.AreEqual(99, player.Id);
        }
    }
}