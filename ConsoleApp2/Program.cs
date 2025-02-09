using FootballHelper.DataBase;
using FootballHelper.Logic;
using FootballHelper.Logic.StrategyPattern;
using System.Numerics;

public class Program
{
    private static void Main(string[] args)
    {
        //IRepository data = new DataAccess();

        //List<Match> matches = data.GetMatches();

        //Match match = matches[0];

        //int[] score = match.CalculateFinalScore(data);

        //Console.WriteLine($"{score[0]} : {score[1]}");

        //int point = match.CalculatePointForHomeClub(data);
        //Console.WriteLine(point);

        // Использование стратегии общей статистики
        //var totalStatsCalculator = new StatisticsCalculator(new TotalStatisticsStrategy());
        //totalStatsCalculator.CalculatePlayerStatistics(player, playerStats);
        //totalStatsCalculator.CalculateClubStatistics(club, clubStats);

        //// Использование стратегии средней статистики
        //var averageStatsCalculator = new StatisticsCalculator(new AverageStatisticsStrategy());
        //averageStatsCalculator.CalculatePlayerStatistics(player, playerStats);
        //averageStatsCalculator.CalculateClubStatistics(club, clubStats);

        Console.ReadKey();
    }
}