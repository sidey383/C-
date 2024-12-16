using Nsu.HackathonProblem;
using Xunit;
using Moq;

using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem {

  public class Hackathon_Tests
  {

      IEnumerable<Employee> teamLeads = [
        new Employee(1, "First Employee"),
        new Employee(2, "Second Employee"),
        new Employee(3, "Third Employee")
      ];
      IEnumerable<Employee> juniors = [
        new Employee(1, "First Desire"),
        new Employee(2, "Second Desire"),
        new Employee(3, "Third Desire")
      ];
      IEnumerable<Wishlist> teamLeadsWishlists = [
        new Wishlist(1, [3, 2, 1]),
        new Wishlist(2, [2, 3, 1]),
        new Wishlist(3, [1, 3, 2])
      ];
      IEnumerable<Wishlist> juniorsWishlists = [
        new Wishlist(1, [3, 2, 1]),
        new Wishlist(2, [2, 3, 1]),
        new Wishlist(3, [1, 3, 2])
      ];
      IEnumerable<Team> teams = [
        new Team(new Employee(1, "First Employee"), new Employee(1, "First Desire")),
        new Team(new Employee(2, "Second Employee"), new Employee(2, "Second Desire")),
        new Team(new Employee(3, "Third Employee"), new Employee(3, "Third Desire")),
      ];

    [Fact]
    public void HackatoneTest() {
      Hackathon hackathon = new Hackathon(new System.Random());

      var wishlist = hackathon.CreateWishlist(teamLeads, juniors);

      Assert.Equal(3, wishlist.Count());
      foreach (var wl in wishlist) {
        Assert.Equal(3, wl.DesiredEmployees.Count());
        foreach (var t in juniors) {
          Assert.Contains(t.Id, wl.DesiredEmployees);
        }
      }
    }

    [Fact]
    public void StrategyTest() {
        ITeamBuildingStrategy strategy = new SimpleTeamBuildingStrategy();
        IEnumerable<Team> resultTeams = strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        Assert.Equal(teams, resultTeams);
    }

    [Fact]
    public void HrManagerTest() {
      Mock<ITeamBuildingStrategy> _strategyMock = new Mock<ITeamBuildingStrategy>();
      _strategyMock.Setup(s => s.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists)).Returns(teams);
      HrManager hrManager =  new HrManager(_strategyMock.Object);
      IEnumerable<Team> resultTeams = hrManager.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
      Assert.Equal(teams, resultTeams);
      _strategyMock.Verify(s => s.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists), Times.Once);
    }

    [Fact]
    public void HrDirectorTest() {
        HrDirector hrDirector = new HrDirector(new MathUtils());
        double result = hrDirector.CalculateQuality(teams, teamLeadsWishlists, juniorsWishlists);
        Assert.Equal(18.0/11.0, result, 1e-5);
    }
    
    [Fact]
    public void MathUtilsTest() {
      MathUtils mathUtils = new MathUtils();
      Assert.Equal(3, mathUtils.harmonicMean([3, 3, 3]), 1e-5);
      Assert.Equal(10, mathUtils.harmonicMean([10, 10, 10]), 1e-5);
      Assert.Equal(3, mathUtils.harmonicMean([2, 6]), 1e-5);
    }

  }

}