using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic
{
    public interface IRepository
    {
        public int InsertClub(string name);
        public int InsertPlayer(Player player);
        public void InsertPlayerInClub(Club club, int playerID);
        public void DeletePlayer(Player player);
        public int InsertMatch(Match match);
        public void DeleteMatch(Match match);
        public void InsertPlayerStatistic(PlayerStatisticsPerMatch playerStatistics);
        public void InsertClubStatistics(ClubStatisticsPerMatch statistics);

        public Task<Club> UpdateClubAsync(Club myClub);
        public Task UpdateClubStatisticsAsync(ClubStatisticsPerMatch statistics);
        public Task UpdatePlayerStatisticAsync(PlayerStatisticsPerMatch plStats);
        public Task<List<Match>> GetMatchesAsync();
        public Task<List<Club>> GetClubsAsync();
        public Task<List<PlayerStatisticsPerMatch>> GetAllPlayersStatsAsync();
        public Task<List<Player>> GetPlayersForClubAsync(int clubId);
        public Task<List<PlayerStatisticsPerMatch>> GetGoalsForMatchAsync(Match match);
        public Task<List<ClubStatisticsPerMatch>> GetClubsStatsForMatchAsync(Match match);
        public Task<List<PlayerStatisticsPerMatch>> GetAllPlayerStatisticsForMatchAsync(Match match);
        public Task<List<Match>> GetMatchesForClubAsync(Club club);
        public Task<List<ClubStatisticsPerMatch>> GetAllStatsForClubAsync(Club club);
        public Task<List<PlayerStatisticsPerMatch>> GetAllPlayersStatsForClubAsync(Club club);
        public Task<List<PlayerStatisticsPerMatch>> GetAllStatisticForPlayerAsync(Player player);
        public Task<List<Player>> GetPlayersWithoutStatisticsInMatchForClubAsync(Match match, Club club);
    }
}
