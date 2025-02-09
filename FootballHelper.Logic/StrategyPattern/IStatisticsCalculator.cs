using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.StrategyPattern
{
    public interface IStatisticsStrategy
    {
        Dictionary<string, double> CalculatePlayerStatistics(Player player, List<PlayerStatisticsPerMatch> playerStats);
        Dictionary<string, double> CalculateClubStatistics(Club club, List<ClubStatisticsPerMatch> clubStats, List<PlayerStatisticsPerMatch> playerStats);
    }
}
