using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic
{
    public class Match
    {
        public int ID { get; private set; }
        public DateOnly DateOfTheMatch { get; private set; }
        public Club HomeClub { get; private set; }
        public Club AwayClub { get; private set; }

        public Match(int id, DateOnly dateOfTheMatch, Club homeClub, Club awayClub)
        {
            ID = id;
            DateOfTheMatch = dateOfTheMatch;
            HomeClub = homeClub;
            AwayClub = awayClub;
        }
        public List<PlayerStatisticsPerMatch> GetPlayerStatsForHomeClub(List<PlayerStatisticsPerMatch> allPlayerStatsInMatch)
        {
            List<PlayerStatisticsPerMatch> plStatsList = new List<PlayerStatisticsPerMatch>();

            var playerIds = HomeClub.Players.Select(p => p.ID).ToHashSet();

            plStatsList = allPlayerStatsInMatch.Where(stat => playerIds.Contains(stat.Player.ID)).ToList();

            return plStatsList;
        }

        public List<PlayerStatisticsPerMatch> GetPlayerStatsForAwayClub(List<PlayerStatisticsPerMatch> allPlayerStatsInMatch)
        {
            List<PlayerStatisticsPerMatch> plStatsList = new List<PlayerStatisticsPerMatch>();

            var playerIds = AwayClub.Players.Select(p => p.ID).ToHashSet();

            plStatsList = allPlayerStatsInMatch.Where(stat => playerIds.Contains(stat.Player.ID)).ToList();
            return plStatsList;

        }   

        public int[] CalculateFinalScore(List<PlayerStatisticsPerMatch> allPlStats)
        {
            int[] score = new int[2];
            int homeGoals = 0;
            int awayGoals = 0;

            var homePlayers = HomeClub.Players;
            var awayPlayers = AwayClub.Players;

            foreach (var player in homePlayers)
            {
                var playerStats = allPlStats.FindAll(ps => ps.Match.ID == this.ID && ps.Player.ID == player.ID);
                foreach (var stats in playerStats)
                {
                    homeGoals += stats.Goals;
                }
            }

            foreach (var player in awayPlayers)
            {
                var playerStats = allPlStats.FindAll(ps => ps.Match.ID == this.ID && ps.Player.ID == player.ID);
                foreach (var stats in playerStats)
                {
                    awayGoals += stats.Goals;
                }
            }

            score[0] = homeGoals;
            score[1] = awayGoals;

            return score;
        }

        public int CalculatePointForHomeClub(int[] score)
        {
            if (score[0] > score[1])
                return 3;
            if (score[0] < score[1])
                return 0;
            else
                return 1;
        }

        public int CalculatePointForAwayClub(int[] score)
        {
            if (score[1] > score[0])
                return 3;
            if (score[1] < score[0])
                return 0;
            else
                return 1;
        }
    }
}
