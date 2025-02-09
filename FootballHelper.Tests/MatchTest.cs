using FootballHelper.Logic;
using FootballHelper.Logic.BuilderPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Tests
{
    public class MatchTest
    {
        ClubBuilder clubBuilder = new ClubBuilder();
        PlayerBuilder playerBuilder = new PlayerBuilder();
        MatchBuilder matchBuilder = new MatchBuilder();
        PlayerStatisticsPerMatchBuilder playerStatsBuilder = new PlayerStatisticsPerMatchBuilder();
        ClubStatisticsPerMatchBuilder clubStatsBuilder = new ClubStatisticsPerMatchBuilder();

        [Fact]
        public void Get_Player_Stats_For_Home_Club_In_Match()
        {
            //arrange
            var homePlayer1 = playerBuilder.WithID(1).WithName("Ronaldo").Build();
            var homePlayer2 = playerBuilder.WithID(2).WithName("Benzema").Build();
            var awayPlayer1 = playerBuilder.WithID(3).WithName("Neymar").Build();
            var awayPlayer2 = playerBuilder.WithID(4).WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer1, homePlayer2 };
            List<Player> awayPlayers = new List<Player> { awayPlayer1, awayPlayer2 };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var match = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();

            var homePl1Stats = playerStatsBuilder.WithPlayer(homePlayer1).WithShoots(10).WithMatch(match).Build();
            var homePl2Stats = playerStatsBuilder.WithPlayer(homePlayer2).WithShoots(15).WithMatch(match).Build();
            var awayPl1Stats = playerStatsBuilder.WithPlayer(awayPlayer1).WithShoots(5).WithMatch(match).Build();
            var awayPl2Stats = playerStatsBuilder.WithPlayer(awayPlayer2).WithShoots(7).WithMatch(match).Build();

            List<PlayerStatisticsPerMatch> allPlayerStatsList = new List<PlayerStatisticsPerMatch> { homePl1Stats, homePl2Stats, awayPl1Stats, awayPl2Stats };

            List<PlayerStatisticsPerMatch> expectedHomePlayerStats = new List<PlayerStatisticsPerMatch> { homePl1Stats, homePl2Stats };

            //act
            List<PlayerStatisticsPerMatch> actualHomePlayerStats = match.GetPlayerStatsForHomeClub(allPlayerStatsList);

            //assert
            Assert.Equal(expectedHomePlayerStats, actualHomePlayerStats);
        }

        [Fact]
        public void Calculate_Final_Score_In_Match_Where_Score_3_2()
        {
            //arrange
            var homePlayer1 = playerBuilder.WithID(1).WithName("Ronaldo").Build();
            var homePlayer2 = playerBuilder.WithID(2).WithName("Benzema").Build();
            var awayPlayer1 = playerBuilder.WithID(3).WithName("Neymar").Build();
            var awayPlayer2 = playerBuilder.WithID(4).WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer1, homePlayer2 };
            List<Player> awayPlayers = new List<Player> { awayPlayer1, awayPlayer2 };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var match = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();

            var homePl1Stats = playerStatsBuilder.WithPlayer(homePlayer1).WithGoals(1).WithMatch(match).Build();
            var homePl2Stats = playerStatsBuilder.WithPlayer(homePlayer2).WithGoals(2).WithMatch(match).Build();
            var awayPl1Stats = playerStatsBuilder.WithPlayer(awayPlayer1).WithGoals(1).WithMatch(match).Build();
            var awayPl2Stats = playerStatsBuilder.WithPlayer(awayPlayer2).WithGoals(1).WithMatch(match).Build();

            List<PlayerStatisticsPerMatch> allPlayerStatsList = new List<PlayerStatisticsPerMatch> { homePl1Stats, homePl2Stats, awayPl1Stats, awayPl2Stats };

            int[] expectedScore = new int[2] { 3, 2 };

            //act
            int[] actualScore = match.CalculateFinalScore(allPlayerStatsList);

            //assert
            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void Calculate_Point_For_HomeClub_In_Match_With_Final_Score_1_1()
        {
            //arrange
            var homePlayer1 = playerBuilder.WithID(1).WithName("Ronaldo").Build();
            var homePlayer2 = playerBuilder.WithID(2).WithName("Benzema").Build();
            var awayPlayer1 = playerBuilder.WithID(3).WithName("Neymar").Build();
            var awayPlayer2 = playerBuilder.WithID(4).WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer1, homePlayer2 };
            List<Player> awayPlayers = new List<Player> { awayPlayer1, awayPlayer2 };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var match = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();

            var homePl1Stats = playerStatsBuilder.WithPlayer(homePlayer1).WithGoals(1).WithMatch(match).Build();
            var homePl2Stats = playerStatsBuilder.WithPlayer(homePlayer2).WithGoals(1).WithMatch(match).Build();
            var awayPl1Stats = playerStatsBuilder.WithPlayer(awayPlayer1).WithGoals(1).WithMatch(match).Build();
            var awayPl2Stats = playerStatsBuilder.WithPlayer(awayPlayer2).WithGoals(1).WithMatch(match).Build();

            List<PlayerStatisticsPerMatch> allPlayerStatsList = new List<PlayerStatisticsPerMatch> { homePl1Stats, homePl2Stats, awayPl1Stats, awayPl2Stats };
            int[] score = match.CalculateFinalScore(allPlayerStatsList);

            int expectedPoint = 1;

            //act
            int actualPoint = match.CalculatePointForHomeClub(score);

            //assert
            Assert.Equal(expectedPoint, actualPoint);
        }

        [Fact]
        public void Calculate_Point_For_AwayClub_In_Match_With_Final_Score_1_3()
        {
            //arrange
            var homePlayer1 = playerBuilder.WithID(1).WithName("Ronaldo").Build();
            var homePlayer2 = playerBuilder.WithID(2).WithName("Benzema").Build();
            var awayPlayer1 = playerBuilder.WithID(3).WithName("Neymar").Build();
            var awayPlayer2 = playerBuilder.WithID(4).WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer1, homePlayer2 };
            List<Player> awayPlayers = new List<Player> { awayPlayer1, awayPlayer2 };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var match = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();

            var homePl1Stats = playerStatsBuilder.WithPlayer(homePlayer1).WithGoals(1).WithMatch(match).Build();
            var homePl2Stats = playerStatsBuilder.WithPlayer(homePlayer2).WithGoals(0).WithMatch(match).Build();
            var awayPl1Stats = playerStatsBuilder.WithPlayer(awayPlayer1).WithGoals(1).WithMatch(match).Build();
            var awayPl2Stats = playerStatsBuilder.WithPlayer(awayPlayer2).WithGoals(2).WithMatch(match).Build();

            List<PlayerStatisticsPerMatch> allPlayerStatsList = new List<PlayerStatisticsPerMatch> { homePl1Stats, homePl2Stats, awayPl1Stats, awayPl2Stats };
            int[] score = match.CalculateFinalScore(allPlayerStatsList);

            int expectedPoint = 3;

            //act
            int actualPoint = match.CalculatePointForAwayClub(score);

            //assert
            Assert.Equal(expectedPoint, actualPoint);
        }
    }
}
