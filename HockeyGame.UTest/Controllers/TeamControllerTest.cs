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
        // TODO : This test should be done with different inputs & should be extended to other methods of the TeamController class
        [Test]
        public void GetTeamPerYear()
        {
            var teams = new[]
            {
                new Team {Coach = "coach", Id = 123, Year = 345}
            }.AsQueryable();

            var mockedTeamDbSet = MockDbSet(teams);

            var players = new[]
            {
                new Player {IsCapitain = false, LastName = "foo", Name = "bar", Number = 10, Id = 99, Position = "goal"}
            }.AsQueryable();

            var mockedPlayerDbSet = MockDbSet(players);

            var playerTeams = new[]
            {
                new PlayerTeam { Id = 12, PlayerId = 99, TeamId = 123}
            }.AsQueryable();

            var mockedPlayerTeamDbSet = MockDbSet(playerTeams);

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

        private Mock<DbSet<T>> MockDbSet<T>(IQueryable<T> input) where T : class
        {
            var mockedTeamDbSet = new Mock<DbSet<T>>();
            mockedTeamDbSet.As<IQueryable<T>>().Setup(c => c.Provider).Returns(input.Provider);
            mockedTeamDbSet.As<IQueryable<T>>().Setup(c => c.Expression).Returns(input.Expression);
            mockedTeamDbSet.As<IQueryable<T>>().Setup(c => c.ElementType).Returns(input.ElementType);
            mockedTeamDbSet.As<IQueryable<T>>().Setup(c => c.GetEnumerator()).Returns(input.GetEnumerator());

            return mockedTeamDbSet;
        }
    }
}