using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.StrategyPattern
{
    public class TotalStatisticsStrategy : IStatisticsStrategy
    {
        public Dictionary<string, double> CalculatePlayerStatistics(Player player, List<PlayerStatisticsPerMatch> playerStats)
        {
            Dictionary<string, double> stats = new Dictionary<string, double>();

            var totalMatches = playerStats.Count;

            var totalGoals = playerStats.Sum(s => s.Goals);
            var totalAssists = playerStats.Sum(s => s.Assists);
            var totalShoots = playerStats.Sum(s => s.Shoots);
            var totalTackling = playerStats.Sum(s => s.Tackling);
            var totalYellowCarts = playerStats.Count(s => s.YellowCart);
            var totalRedCarts = playerStats.Count(s => s.RedCart);

            stats["Матчи"] = totalMatches;
            stats["Голы"] = totalGoals;
            stats["Ассисты"] = totalAssists;
            stats["Удары"] = totalShoots;
            stats["Отборы"] = totalTackling;
            stats["Желтые карточки"] = totalYellowCarts;
            stats["Красные карточки"] = totalRedCarts;

            return stats;
        }

        public Dictionary<string, double> CalculateClubStatistics(Club club, List<ClubStatisticsPerMatch> clubStats, List<PlayerStatisticsPerMatch> playerStats)
        {
            Dictionary<string, double> stats = new Dictionary<string, double>();

            var totalMatches = clubStats.Count;

            if (totalMatches == 0)
            {
                totalMatches = 1;
            }

            var totalCorners = clubStats.Sum(s => s.Corners);
            var totalOffsides = clubStats.Sum(s => s.Offsides);

            var totalGoals = playerStats.Sum(s => s.Goals);
            var totalAssists = playerStats.Sum(s => s.Assists);
            var totalShoots = playerStats.Sum(s => s.Shoots);

            stats["Матчи"] = totalMatches;
            stats["Угловые"] = totalCorners;
            stats["Оффсайды"] = totalOffsides;
            stats["Голы"] = totalGoals;
            stats["Ассисты"] = totalAssists;
            stats["Удары"] = totalShoots;

            return stats;
        }
    }
}
