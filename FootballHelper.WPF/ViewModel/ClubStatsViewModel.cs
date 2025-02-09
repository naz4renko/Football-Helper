using FootballHelper.Logic;
using FootballHelper.Logic.StrategyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FootballHelper.WPF.ViewModel
{
    public class ClubStatsViewModel : ViewModelBase
    {
        private IRepository repo;
        private MainWindowViewModel mvVM;
        private Club myClub;

        private Dictionary<string, double> totalClubStatistic;
        private Dictionary<string, double> avarageClubStatistic;

        private List<PlayerStatisticsPerMatch> allPlStats;

        private int[] matchStats;
        private int points;

        public ClubStatsViewModel(IRepository repo, Club club, MainWindowViewModel mvVM)
        {
            myClub = club;
            this.repo = repo;
            this.mvVM = mvVM;
        }

        public async Task InitializeAsync()
        {
            var matches = await repo.GetMatchesForClubAsync(myClub);

            var allClubStats = await repo.GetAllStatsForClubAsync(myClub);
            var allPlayersStatsForClub = await repo.GetAllPlayersStatsForClubAsync(myClub);
            var allPlayersStats = await repo.GetAllPlayersStatsAsync();

            matchStats = myClub.CalculateStats(matches, allPlayersStats);
            points = myClub.CalculatePoints(matches, allPlayersStats);

            var totalContext = new StatisticsContext(new TotalStatisticsStrategy());
            var avarageContext = new StatisticsContext(new AverageStatisticsStrategy());

            totalClubStatistic = totalContext.CalculateClubStatistics(myClub, allClubStats, allPlayersStatsForClub);
            avarageClubStatistic = avarageContext.CalculateClubStatistics(myClub, allClubStats, allPlayersStatsForClub);
        }

        public string Name
        {
            get => myClub.Name;
        }

        public string Matches
        {
            get => $"Матчей: {totalClubStatistic.First().Value}";
        }

        public string Points
        {
            get => $"Очки: {points}";
        }

        public string MatchStats
        {
            get => $"W:{matchStats[0]} - D:{matchStats[1]} - L:{matchStats[2]}";
        }

        public string TotalStats
        {
            get
            {
                string totalStats = "";
                foreach (var stats in totalClubStatistic)
                {
                    totalStats += $"{stats.Key}: {stats.Value} \n";
                }
                return totalStats;
            }
        }

        public string AverageStats
        {
            get
            {
                string averageStats = "";
                foreach (var stats in avarageClubStatistic)
                {
                    averageStats += $"{stats.Key}: {stats.Value} \n";
                }
                return averageStats;
            }
        }


    }
}
