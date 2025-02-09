using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.DataBase
{
    public class DatabaseSeeder
    {
        private static readonly Random random = new Random();

        public DatabaseSeeder()
        {

        }

        public void SeedDatabase(DataAccess data)
        {
            var clubs = new List<Club>();
            for (int i = 0; i < 50; i++)
            {
                string clubName = RandomDataGenerator.RandomClubName();
                int clubID = data.InsertClub(clubName);
                var club = new Club(clubID, clubName, new List<Player>());
                clubs.Add(club);
            }

            var players = new List<Player>();
            for (int i = 0; i < 1000; i++)
            {
                string playerName = RandomDataGenerator.RandomName();
                string nationality = RandomDataGenerator.RandomNationality();
                DateOnly birth = RandomDataGenerator.RandomDateOfBirth();
                string position = RandomDataGenerator.RandomPosition();

                var player = new Player(i + 1, playerName, nationality, birth, position);
                int playerId = data.InsertPlayer(player);

                var workPlayer = new Player(playerId, playerName, nationality, birth, position);
                players.Add(workPlayer);

                var club = clubs[random.Next(clubs.Count)];
                data.InsertPlayerInClub(club, playerId);
                club.AddPlayer(workPlayer);
            }

            var matches = new List<Match>();

            for (int i = 0; i < 100; i++)
            {
                DateOnly date = RandomDataGenerator.RandomDateOfBirth();
                var homeClub = clubs[random.Next(clubs.Count)];
                var awayClub = clubs[random.Next(clubs.Count)];

                var match = new Match(i + 1, date, homeClub, awayClub);

                int matchId = data.InsertMatch(match);
                var workMatch = new Match(matchId, date, homeClub, awayClub);
                matches.Add(workMatch);

                int goals = 0;
                foreach (var player in homeClub.Players)
                {

                    int assist = 0;
                    int goal = RandomDataGenerator.RandomGoals();
                    goals += goal;

                    if (goal == 0)
                    {
                        assist = goals;
                        goals = 0;
                    }

                    var playerStatistics = new PlayerStatisticsPerMatch(i + 1, player, workMatch, goal, assist, RandomDataGenerator.RandomStatistic(), RandomDataGenerator.RandomStatistic(), RandomDataGenerator.RandomCard(), RandomDataGenerator.RandomCard());
                    data.InsertPlayerStatistic(playerStatistics);
                }

                goals = 0;
                foreach (var player in awayClub.Players)
                {

                    int assist = 0;
                    int goal = RandomDataGenerator.RandomGoals();
                    goals += goal;

                    if (goal == 0)
                    {
                        assist = goals;
                        goals = 0;
                    }

                    var playerStatistics = new PlayerStatisticsPerMatch(i + 1, player, workMatch, goal, assist, RandomDataGenerator.RandomStatistic(), RandomDataGenerator.RandomStatistic(), RandomDataGenerator.RandomCard(), RandomDataGenerator.RandomCard());
                    data.InsertPlayerStatistic(playerStatistics);
                }

                int homePossesion = RandomDataGenerator.RandomPossesion();
                int awayPossesion = 100 - homePossesion;

                var clubStatistics = new ClubStatisticsPerMatch(i + 1, homeClub, workMatch, homePossesion, RandomDataGenerator.RandomStatistic(), RandomDataGenerator.RandomStatistic());
                data.InsertClubStatistics(clubStatistics);

                clubStatistics = new ClubStatisticsPerMatch(i + 1, awayClub, workMatch, awayPossesion, RandomDataGenerator.RandomStatistic(), RandomDataGenerator.RandomStatistic());
                data.InsertClubStatistics(clubStatistics);
            }
        }
    }
}
