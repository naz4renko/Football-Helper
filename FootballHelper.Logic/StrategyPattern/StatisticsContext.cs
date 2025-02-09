using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.StrategyPattern
{
    public class StatisticsContext
    {
        private readonly IStatisticsStrategy _strategy;

        public StatisticsContext(IStatisticsStrategy strategy)
        {
            _strategy = strategy;
        }

        public Dictionary<string, double> CalculatePlayerStatistics(Player player, List<PlayerStatisticsPerMatch> playerStats)
        {
            return _strategy.CalculatePlayerStatistics(player, playerStats);
        }

        public Dictionary<string, double> CalculateClubStatistics(Club club, List<ClubStatisticsPerMatch> clubStats, List<PlayerStatisticsPerMatch> playerStats)
        {
            return _strategy.CalculateClubStatistics(club, clubStats, playerStats);
        }
    }
}
