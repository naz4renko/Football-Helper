using FootballHelper.Logic;
using FootballHelper.Logic.BuilderPattern;
using FootballHelper.Logic.StrategyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Tests
{
    public class StrategyPatternTest
    {
        StatisticsContext totalContext = new StatisticsContext(new TotalStatisticsStrategy());
        StatisticsContext avarageContext = new StatisticsContext(new AverageStatisticsStrategy());

        ClubBuilder clubBuilder = new ClubBuilder();
        PlayerBuilder playerBuilder = new PlayerBuilder();
        MatchBuilder matchBuilder = new MatchBuilder();
        PlayerStatisticsPerMatchBuilder playerStatsBuilder = new PlayerStatisticsPerMatchBuilder();
        ClubStatisticsPerMatchBuilder clubStatsBuilder = new ClubStatisticsPerMatchBuilder();
        [Fact]
        public void Calculate_Total_Club_Statistic()
        {
            //arrange
            Club club = clubBuilder.Build();
            Player player1 = playerBuilder.Build();
            Player player2 = playerBuilder.Build();
            Match match1 = matchBuilder.WithHomeClub(club).Build();
            Match match2 = matchBuilder.WithAwayClub(club).Build();



            ClubStatisticsPerMatch clubStatistics1 = clubStatsBuilder.WithClub(club).WithMatch(match1).WithCorners(10).WithOffsides(5).Build();
            ClubStatisticsPerMatch clubStatistics2 = clubStatsBuilder.WithClub(club).WithCorners(20).WithOffsides(15).Build();

            PlayerStatisticsPerMatch playerStatistics1 = playerStatsBuilder.WithPlayer(player1).WithMatch(match2).WithGoals(3).WithShoots(10).WithAssists(2).WithTackling(15).WithYellowCart(true).Build();

            PlayerStatisticsPerMatch playerStatistics2 = playerStatsBuilder.WithPlayer(player2).WithGoals(2).WithShoots(5).WithAssists(3).WithTackling(7).WithYellowCart(false).WithRedCart(true).Build();

            List<ClubStatisticsPerMatch> clubStatsList = new List<ClubStatisticsPerMatch>() { clubStatistics1, clubStatistics2 };
            List<PlayerStatisticsPerMatch> playerStatsList = new List<PlayerStatisticsPerMatch>() { playerStatistics1, playerStatistics2 };

            Dictionary<string, double> expectedTotalStats = new Dictionary<string, double>()
            {
                { "Матчи", 2 },
                { "Угловые", 30 },
                { "Оффсайды", 20 },
                { "Голы", 5 },
                { "Ассисты",5 },
                { "Удары", 15 },
            };

            //act
            Dictionary<string, double> actualTotalStats = totalContext.CalculateClubStatistics(club, clubStatsList, playerStatsList);

            //assert
            Assert.Equal(expectedTotalStats, actualTotalStats);
        }

        [Fact]
        public void Calculate_Avarage_Club_Stats()
        {
            //arrange
            Club club = clubBuilder.Build();
            Player player1 = playerBuilder.Build();
            Player player2 = playerBuilder.Build();
            Match match1 = matchBuilder.WithHomeClub(club).Build();
            Match match2 = matchBuilder.WithAwayClub(club).Build();


            ClubStatisticsPerMatch clubStatistics1 = clubStatsBuilder.WithClub(club).WithMatch(match1).WithCorners(10).WithOffsides(5).WithPossession(20).Build();

            ClubStatisticsPerMatch clubStatistics2 = clubStatsBuilder.WithClub(club).WithCorners(20).WithOffsides(15).WithPossession(60).Build();

            PlayerStatisticsPerMatch playerStatistics1 = playerStatsBuilder.WithPlayer(player1).WithMatch(match2).WithGoals(3).WithShoots(10).WithAssists(2).WithTackling(15).WithYellowCart(true).Build();

            PlayerStatisticsPerMatch playerStatistics2 = playerStatsBuilder.WithPlayer(player2).WithGoals(2).WithShoots(5).WithAssists(3).WithTackling(7).WithYellowCart(false).WithRedCart(true).Build();

            List<ClubStatisticsPerMatch> clubStatsList = new List<ClubStatisticsPerMatch>() { clubStatistics1, clubStatistics2 };
            List<PlayerStatisticsPerMatch> playerStatsList = new List<PlayerStatisticsPerMatch>() { playerStatistics1, playerStatistics2 };

            Dictionary<string, double> expectedAvarageStats = new Dictionary<string, double>()
            {
                { "Ср.Владение", 40 },
                { "Ср.Угловые", 15 },
                { "Ср.Оффсайды", 10 },
                { "Ср.Голы", 2.5 },
                { "Ср.Ассисты",2.5 },
                { "Ср.Удары", 7.5 },
            };

            //act
            Dictionary<string, double> actualAvarageStats = avarageContext.CalculateClubStatistics(club, clubStatsList, playerStatsList);

            //assert
            Assert.Equal(expectedAvarageStats, actualAvarageStats);
        }

        [Fact]
        public void Calculate_Total_Player_Stats()
        {
            //arrange
            Player player = playerBuilder.Build();

            PlayerStatisticsPerMatch playerStatistics1 = playerStatsBuilder.WithPlayer(player)
                                                                          .WithGoals(1)
                                                                          .WithAssists(2)
                                                                          .WithShoots(5)
                                                                          .WithTackling(10)
                                                                          .WithYellowCart(true)
                                                                          .WithRedCart(false)
                                                                          .Build();

            PlayerStatisticsPerMatch playerStatistics2 = playerStatsBuilder.WithPlayer(player)
                                                                           .WithGoals(2)
                                                                           .WithAssists(1)
                                                                           .WithShoots(15)
                                                                           .WithTackling(22)
                                                                           .WithYellowCart(false)
                                                                           .WithRedCart(true)
                                                                           .Build();

            PlayerStatisticsPerMatch playerStatistics3 = playerStatsBuilder.WithPlayer(player)
                                                                           .WithGoals(0)
                                                                           .WithAssists(1)
                                                                           .WithShoots(11)
                                                                           .WithTackling(12)
                                                                           .WithYellowCart(true)
                                                                           .WithRedCart(false)
                                                                          .Build();

            List<PlayerStatisticsPerMatch> playerStatsList = new List<PlayerStatisticsPerMatch>() { playerStatistics1, playerStatistics2, playerStatistics3 };

            Dictionary<string, double> expectedTotalStats = new Dictionary<string, double>()
            {
                { "Матчи", 3 },
                { "Голы", 3 },
                { "Ассисты", 4 },
                { "Удары", 31 },
                { "Отборы", 44 },
                { "Желтые карточки", 2 },
                { "Красные карточки", 1 }
            };

            //act
            Dictionary<string, double> actualTotalStats = totalContext.CalculatePlayerStatistics(player, playerStatsList);

            //assert
            Assert.Equal(expectedTotalStats, actualTotalStats);
        }

        [Fact]
        public void Calculate_Avarage_Player_Stats()
        {
            //arrange
            Player player = playerBuilder.Build();

            PlayerStatisticsPerMatch playerStatistics1 = playerStatsBuilder.WithPlayer(player)
                                                                          .WithGoals(1)
                                                                          .WithAssists(2)
                                                                          .WithShoots(5)
                                                                          .WithTackling(10)
                                                                          .WithYellowCart(true)
                                                                          .Build();

            PlayerStatisticsPerMatch playerStatistics2 = playerStatsBuilder.WithPlayer(player)
                                                                           .WithGoals(2)
                                                                           .WithAssists(2)
                                                                           .WithShoots(15)
                                                                           .WithTackling(22)
                                                                           .WithYellowCart(true)
                                                                           .Build();

            PlayerStatisticsPerMatch playerStatistics3 = playerStatsBuilder.WithPlayer(player)
                                                                           .WithGoals(0)
                                                                           .WithAssists(2)
                                                                           .WithShoots(13)
                                                                           .WithTackling(13)
                                                                           .WithYellowCart(true)
                                                                          .Build();

            List<PlayerStatisticsPerMatch> playerStatsList = new List<PlayerStatisticsPerMatch>() { playerStatistics1, playerStatistics2, playerStatistics3 };

            Dictionary<string, double> expectedAvarageStats = new Dictionary<string, double>()
            {
                { "Ср.Голы", 1 },
                { "Ср.Ассисты", 2 },
                { "Ср.Удары", 11 },
                { "Ср.Отборы", 15 },
                { "Ср.Желтые карточки", 1 },
                { "Ср.Красные карточки", 0 }
            };

            //act
            Dictionary<string, double> actualAvarageStats = avarageContext.CalculatePlayerStatistics(player, playerStatsList);

            //assert
            Assert.Equal(expectedAvarageStats, actualAvarageStats);
        }

    }
}
