using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic.StrategyPattern
{
    public class AverageStatisticsStrategy : IStatisticsStrategy
    {
        public Dictionary<string, double> CalculatePlayerStatistics(Player player, List<PlayerStatisticsPerMatch> playerStats)
        {
            Dictionary<string, double> stats = new Dictionary<string, double>();

            var totalMatches = playerStats.Count;

            if (totalMatches == 0)
            {
                totalMatches = 1;
            }    

            var totalGoals = playerStats.Sum(s => s.Goals);
            var totalAssists = playerStats.Sum(s => s.Assists);
            var totalShoots = playerStats.Sum(s => s.Shoots);
            var totalTackling = playerStats.Sum(s => s.Tackling);
            var totalYellowCarts = playerStats.Sum(s => s.YellowCart ? 1 : 0);
            var totalRedCarts = playerStats.Sum(s => s.RedCart ? 1 : 0);

            stats["Ср.Голы"] = (double) totalGoals / totalMatches;
            stats["Ср.Ассисты"] = (double) totalAssists / totalMatches;
            stats["Ср.Удары"] = (double) totalShoots / totalMatches;
            stats["Ср.Отборы"] = (double) totalTackling / totalMatches;
            stats["Ср.Желтые карточки"] = Math.Round( (totalYellowCarts / (double)totalMatches),2);
            stats["Ср.Красные карточки"] = Math.Round( (totalRedCarts / (double)totalMatches), 2);

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

            var totalPossession = clubStats.Sum(s => s.Possession);
            var totalCorners = clubStats.Sum(s => s.Corners);
            var totalOffsides = clubStats.Sum(s => s.Offsides);

            var totalGoals = playerStats.Sum(s => s.Goals);
            var totalAssists = playerStats.Sum(s => s.Assists);
            var totalShoots = playerStats.Sum(s => s.Shoots);

            stats["Ср.Владение"] = (double) totalPossession / totalMatches;
            stats["Ср.Угловые"] = (double) totalCorners / totalMatches;
            stats["Ср.Оффсайды"] = (double) totalOffsides / totalMatches;
            stats["Ср.Голы"] = (double) totalGoals / totalMatches;
            stats["Ср.Ассисты"] = (double) totalAssists / totalMatches;
            stats["Ср.Удары"] = (double) totalShoots / totalMatches;

            return stats;
        }
    }
}

