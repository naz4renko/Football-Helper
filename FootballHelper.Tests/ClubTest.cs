using FootballHelper.Logic;
using FootballHelper.Logic.BuilderPattern;

namespace FootballHelper.Tests
{
    public class ClubTest
    {
        ClubBuilder clubBuilder = new ClubBuilder();
        PlayerBuilder playerBuilder = new PlayerBuilder();
        MatchBuilder matchBuilder = new MatchBuilder();
        PlayerStatisticsPerMatchBuilder playerStatsBuilder = new PlayerStatisticsPerMatchBuilder();

        [Fact]
        public void Add_Player_In_Club()
        {
            //arrange
            var club = clubBuilder.Build();
            var player = playerBuilder.WithName("Ronaldo").Build();

            //act
            club.AddPlayer(player);

            //assert
            Assert.Contains(player, club.Players);
        }

        [Fact]
        public void Add_Dublicate_Player_And_No_Added()
        {
            //arrange
            var club = clubBuilder.Build();
            var player = playerBuilder.WithName("Ronaldo").WithNationality("Portugal").WithPosition("нападающий").Build();
            club.Players.Add(player);

            //act
            club.AddPlayer(player);

            //assert
            Assert.Equal(1, club.Players.Count);
        }

        [Fact]
        public void Remove_Player_In_Club()
        {
            //arrange
            var club = clubBuilder.Build();
            var player = playerBuilder.WithName("Ronaldo").Build();
            club.Players.Add(player);

            //act
            club.RemovePlayer(player);

            //assert
            Assert.False(club.Players.Contains(player));
        }

        [Fact]
        public void Calculate_Points_For_HomeClub_With_Only_One_Win()
        {
            //arrange
            ClubBuilder clubBuilder = new ClubBuilder();
            PlayerBuilder playerBuilder = new PlayerBuilder();
            MatchBuilder matchBuilder = new MatchBuilder();
            PlayerStatisticsPerMatchBuilder playerStatsBuilder = new PlayerStatisticsPerMatchBuilder();

            var homePlayer = playerBuilder.WithName("Ronaldo").Build();
            var awayPlayer = playerBuilder.WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer };
            List<Player> awayPlayers = new List<Player> { awayPlayer };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var match = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();

            var homePlayerStats = playerStatsBuilder.WithMatch(match).WithPlayer(homePlayer).WithGoals(2).Build();
            var awayPlayerStats = playerStatsBuilder.WithMatch(match).WithPlayer(awayPlayer).WithGoals(1).Build();

            List<Match> matchList = new List<Match> { match };
            List<PlayerStatisticsPerMatch> playersStatsList = new List<PlayerStatisticsPerMatch> { homePlayerStats, awayPlayerStats };

            int expectedPoints = 3;

            //act
            int actualPoints = homeClub.CalculatePoints(matchList, playersStatsList);

            //assert
            Assert.Equal(expectedPoints, actualPoints);
        }

        [Fact]
        public void Calculate_Points_For_Club_With_Only_One_Draw()
        {
            //arrange
            ClubBuilder clubBuilder = new ClubBuilder();
            PlayerBuilder playerBuilder = new PlayerBuilder();
            MatchBuilder matchBuilder = new MatchBuilder();
            PlayerStatisticsPerMatchBuilder playerStatsBuilder = new PlayerStatisticsPerMatchBuilder();

            var homePlayer = playerBuilder.WithName("Ronaldo").Build();
            var awayPlayer = playerBuilder.WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer };
            List<Player> awayPlayers = new List<Player> { awayPlayer };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var match = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();

            var homePlayerStats = playerStatsBuilder.WithMatch(match).WithPlayer(homePlayer).WithGoals(2).Build();
            var awayPlayerStats = playerStatsBuilder.WithMatch(match).WithPlayer(awayPlayer).WithGoals(2).Build();


            List<Match> matchList = new List<Match> { match };
            List<PlayerStatisticsPerMatch> playersStatsList = new List<PlayerStatisticsPerMatch> { homePlayerStats, awayPlayerStats };

            int expectedPoints = 1;

            //act
            int actualPoints = homeClub.CalculatePoints(matchList, playersStatsList);

            //assert
            Assert.Equal(expectedPoints, actualPoints);
        }

        [Fact]
        public void Calculate_Points_For_Club_With_One_Win_And_One_Draw()
        {
            //arrange
            var homePlayer = playerBuilder.WithName("Ronaldo").Build();
            var awayPlayer = playerBuilder.WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer };
            List<Player> awayPlayers = new List<Player> { awayPlayer };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var matchFirst = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();
            var homePlayerStatsFirst = playerStatsBuilder.WithMatch(matchFirst).WithPlayer(homePlayer).WithGoals(2).Build();
            var awayPlayerStatsFirst = playerStatsBuilder.WithMatch(matchFirst).WithPlayer(awayPlayer).WithGoals(1).Build();

            var matchSecond = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();
            var homePlayerStatsSecond = playerStatsBuilder.WithMatch(matchSecond).WithPlayer(homePlayer).WithGoals(2).Build();
            var awayPlayerStatsSecond = playerStatsBuilder.WithMatch(matchSecond).WithPlayer(awayPlayer).WithGoals(2).Build();


            List<Match> matchList = new List<Match> { matchFirst, matchSecond };
            List<PlayerStatisticsPerMatch> playersStatsList = new List<PlayerStatisticsPerMatch> 
            { 
                homePlayerStatsFirst, 
                awayPlayerStatsFirst,
                homePlayerStatsSecond,
                awayPlayerStatsSecond

            };

            int expectedPoints = 4;

            //act
            int actualPoints = homeClub.CalculatePoints(matchList, playersStatsList);

            //assert
            Assert.Equal(expectedPoints, actualPoints);
        }

        [Fact]
        public void Calculate_Stats_For_Club_With_1Win_2Draw_1Lose()
        {
            //arrange
            var homePlayer = playerBuilder.WithName("Ronaldo").Build();
            var awayPlayer = playerBuilder.WithName("Messi").Build();

            List<Player> homePlayers = new List<Player> { homePlayer };
            List<Player> awayPlayers = new List<Player> { awayPlayer };

            var homeClub = clubBuilder.WithPlayers(homePlayers).Build();
            var awayClub = clubBuilder.WithPlayers(awayPlayers).Build();

            var match1 = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();
            var homePlayerStats1 = playerStatsBuilder.WithMatch(match1).WithPlayer(homePlayer).WithGoals(2).Build();
            var awayPlayerStats1 = playerStatsBuilder.WithMatch(match1).WithPlayer(awayPlayer).WithGoals(1).Build();

            var match2 = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();
            var homePlayerStats2 = playerStatsBuilder.WithMatch(match2).WithPlayer(homePlayer).WithGoals(0).Build();
            var awayPlayerStats2 = playerStatsBuilder.WithMatch(match2).WithPlayer(awayPlayer).WithGoals(0).Build();

            var match3 = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();
            var homePlayerStats3 = playerStatsBuilder.WithMatch(match3).WithPlayer(homePlayer).WithGoals(1).Build();
            var awayPlayerStats3 = playerStatsBuilder.WithMatch(match3).WithPlayer(awayPlayer).WithGoals(1).Build();

            var match4 = matchBuilder.WithHomeClub(homeClub).WithAwayClub(awayClub).Build();
            var homePlayerStats4 = playerStatsBuilder.WithMatch(match4).WithPlayer(homePlayer).WithGoals(0).Build();
            var awayPlayerStats4 = playerStatsBuilder.WithMatch(match4).WithPlayer(awayPlayer).WithGoals(2).Build();

            List<Match> matchList = new List<Match> { match1, match2, match3, match4 };
            List<PlayerStatisticsPerMatch> playersStatsList = new List<PlayerStatisticsPerMatch>
            {
                homePlayerStats1,
                awayPlayerStats1,
                homePlayerStats2,
                awayPlayerStats2,
                homePlayerStats3,
                awayPlayerStats3,
                homePlayerStats4,
                awayPlayerStats4
            };

            int[] expectedStats = new int[3] { 1, 2, 1 };

            //act
            int[] actualStats = homeClub.CalculateStats(matchList, playersStatsList);

            //assert
            Assert.Equal(expectedStats, actualStats);
        }

    }
}