using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.Logic
{
    public class Club
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Player> Players { get; private set; }

        public Club(int id, string name, List<Player> players)
        {
            ID = id;
            Name = name;
            Players = players;
        }

        public int CalculatePoints(List<Match> matches, List<PlayerStatisticsPerMatch> allPlStats)
        {
            int finalPoint = 0;
            foreach (Match match in matches)
            {
                if (match.HomeClub.ID == ID)
                {
                    int[] score = match.CalculateFinalScore(allPlStats);
                    int point = match.CalculatePointForHomeClub(score);
                    finalPoint += point;
                }
                if (match.AwayClub.ID == ID)
                {
                    int[] score = match.CalculateFinalScore(allPlStats);
                    int point = match.CalculatePointForAwayClub(score);
                    finalPoint += point;
                }
            }
            return finalPoint;
        }

        public int[] CalculateStats(List<Match> matches, List<PlayerStatisticsPerMatch> allPlStats)
        {
            int[] matchStats = new int[3];
            foreach (Match match in matches)
            {
                int[] score = match.CalculateFinalScore(allPlStats);

                if (match.HomeClub.ID == ID)
                {
                    int point = match.CalculatePointForHomeClub(score);
                    if (point == 3)
                    {
                        matchStats[0]++;
                    }
                    else if (point == 1)
                    {
                        matchStats[1]++;
                    }
                    else
                    {
                        matchStats[2]++;
                    }
                }
                if (match.AwayClub.ID == ID)
                {
                    int point = match.CalculatePointForAwayClub(score);
                    if (point == 3)
                    {
                        matchStats[0]++;
                    }
                    else if (point == 1)
                    {
                        matchStats[1]++;
                    }
                    else
                    {
                        matchStats[2]++;
                    }
                }
            }
            return matchStats;

        }

        public void AddPlayer(Player player) 
        {
            if (CheckPlayer(Players, player))
            {
                Players.Add(player);
            }
        }

        public void RemovePlayer(Player player) 
        {
            if (!CheckPlayer(Players, player))
            {
                Players.Remove(player);
            }
        }

        public static bool CheckPlayer(List<Player> players, Player p)
        {
            bool result = true;

            foreach (var pl in players)
            {
                if (pl.Name == p.Name && pl.Nationality == p.Nationality && p.Birth == p.Birth)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
