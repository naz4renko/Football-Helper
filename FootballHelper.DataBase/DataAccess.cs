using FootballHelper.Logic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace FootballHelper.DataBase
{
    public class DataAccess : IRepository
    {
        private string connectionString = $@"Data Source=.\NAZARENKOSQL;Initial Catalog=FootballHelper;Integrated Security=True";


        #region Player
        public int InsertPlayer(Player player)
        {
            int newPlayerId;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = "INSERT INTO Players (Name, Nationality, Birth, Position) OUTPUT INSERTED.ID VALUES (@PName, @PNation, @PBirth, @PPosition)";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.AddWithValue("@PName", player.Name);
                sqlCommand.Parameters.AddWithValue("@PNation", player.Nationality);
                sqlCommand.Parameters.Add("@PBirth", SqlDbType.Date).Value = player.Birth.ToDateTime(TimeOnly.MinValue); // Конвертация DateOnly в DateTime
                sqlCommand.Parameters.AddWithValue("@PPosition", player.Position);

                newPlayerId = (int)sqlCommand.ExecuteScalar();
            }
            return newPlayerId;
        }
        public void InsertPlayerInClub(Club club, int playerID)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = "INSERT INTO ClubsPlayers (ClubID, PlayerID) VALUES (@ClubIDs, @PlayerIDs)";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.AddWithValue("@ClubIDs", club.ID);
                sqlCommand.Parameters.AddWithValue("@PlayerIDs", playerID);

                sqlCommand.ExecuteNonQuery();
            }
        }
        public void DeletePlayer(Player player)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = "DELETE ClubsPlayers WHERE PlayerID = @PlID;DELETE Players WHERE ID = @PlID;";
                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.AddWithValue("@PlID", player.ID);

                sqlCommand.ExecuteNonQuery();
            }
        }
        
        public async Task<List<Player>> GetPlayersWithoutStatisticsInMatchForClubAsync(Match match, Club club)
        {
            List<Player> players = new List<Player>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetPlayersWithoutStatisticsInMatchForClub", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@MatchID", match.ID);
                command.Parameters.AddWithValue("@ClubID", club.ID);

                await connection.OpenAsync();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        string name = (String)reader["Name"];
                        string nationality = (String)reader["Nationality"];
                        DateOnly birth = DateOnly.FromDateTime(reader.GetDateTime("Birth"));
                        string position = (String)reader["Position"];
                        Player player = new Player(id, name, nationality, birth, position);

                        players.Add(player);
                    }
                }
            }

            return players;
        }

        public async Task<List<Player>> GetPlayersForClubAsync(int clubId)
        {
            List<Player> playerList = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT Players.ID, Players.[Name], Players.Nationality, Players.Birth, Players.Position FROM Players JOIN ClubsPlayers ON Players.ID = ClubsPlayers.PlayerID WHERE ClubsPlayers.ClubID = {clubId};";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        string name = (String)reader["Name"];
                        string nationality = (String)reader["Nationality"];
                        DateOnly birth = DateOnly.FromDateTime(reader.GetDateTime("Birth"));
                        string position = (String)reader["Position"];
                        Player player = new Player(id, name, nationality, birth, position);

                        playerList.Add(player);
                    }
                }
            }
            return playerList;
        }
        public async Task<List<Player>> GetPlayersAsync()
        {
            List<Player> playerList = new List<Player>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = "SELECT * FROM Players";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        string name = (String)reader["Name"];
                        string nationality = (String)reader["Nationality"];
                        DateOnly birth = DateOnly.FromDateTime(reader.GetDateTime("Birth"));
                        string position = (String)reader["Position"];
                        Player player = new Player(id, name, nationality, birth, position);

                        playerList.Add(player);
                    }
                }
            }
            return playerList;
        }
        #endregion

        #region Club
        public async Task<List<Club>> GetClubsAsync()
        {
            List<Club> clubList = new List<Club>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = "SELECT * FROM Clubs";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        string name = (String)reader["Name"];

                        List<Player> players = await GetPlayersForClubAsync(id);

                        Club club = new Club(id, name, players);
                        clubList.Add(club);
                    }
                }
            }
            return clubList;
        }
        public int InsertClub(string name)
        {
            int newClubID;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"INSERT INTO CLUBS (Name) OUTPUT INSERTED.ID VALUES (@ClubName)";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.AddWithValue("@ClubName", name);

                newClubID = (int)sqlCommand.ExecuteScalar();
            }
            return newClubID;
        }
        public async Task<Club> UpdateClubAsync(Club myClub)
        {
            Club resultClub = myClub;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM Clubs WHERE ID = @id";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.AddWithValue("@id", myClub.ID);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                if (await reader.ReadAsync())
                {
                    int id = reader.GetInt32("ID");
                    string name = (String)reader["Name"];
                    var players = await GetPlayersForClubAsync(id);
                    resultClub = new Club(id, name, players);
                }
            }
            return resultClub;
        }
        #endregion

        #region Match
        public async Task<List<Match>> GetMatchesAsync()
        {
            List<Match> matchList = new List<Match>();
            //var clubs = await GetClubsAsync().ConfigureAwait(false);
            var clubs = await GetClubsAsync();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM Matches";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        DateOnly date = DateOnly.FromDateTime(reader.GetDateTime("Date"));

                        int homeClubId = reader.GetInt32("HomeClub");
                        int awayClubId = reader.GetInt32("AwayClub");

                        Club homeClub = clubs.Find(x => x.ID == homeClubId);
                        Club awayClub = clubs.Find(x => x.ID == awayClubId);

                        Match match = new Match(id, date, homeClub, awayClub);
                        matchList.Add(match);
                    }
                }
            }
            return matchList;
        }
        public async Task<List<Match>> GetMatchesForClubAsync(Club club)
        {
            List<Match> matchList = new List<Match>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM Matches WHERE HomeClub = {club.ID} OR AwayClub = {club.ID}";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                var clubs = await GetClubsAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        DateOnly date = DateOnly.FromDateTime(reader.GetDateTime("Date"));

                        int homeClubId = reader.GetInt32("HomeClub");
                        int awayClubId = reader.GetInt32("AwayClub");

                        Club homeClub = clubs.Find(x => x.ID == homeClubId);
                        Club awayClub = clubs.Find(x => x.ID == awayClubId);

                        Match match = new Match(id, date, homeClub, awayClub);
                        matchList.Add(match);
                    }
                }
            }
            return matchList;
        }
        public int InsertMatch(Match match)
        {
            int newMatchID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = "INSERT INTO MATCHES (Date, HomeClub, AwayClub) OUTPUT INSERTED.ID VALUES (@MDate,@MHomeClub, @MAwayClub)";

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                sqlCommand.Parameters.Add("@MDate", SqlDbType.Date).Value = match.DateOfTheMatch.ToDateTime(TimeOnly.MinValue);
                sqlCommand.Parameters.AddWithValue("@MHomeClub", match.HomeClub.ID);
                sqlCommand.Parameters.AddWithValue("@MAwayClub", match.AwayClub.ID);

                newMatchID = (int)sqlCommand.ExecuteScalar();
            }
            return newMatchID;

        }
        public void DeleteMatch(Match match)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand sqlCommand = new SqlCommand("DeleteMatchAndStatistics", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@MatchID", match.ID);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region PlayerStats
        public async Task<List<PlayerStatisticsPerMatch>> GetAllStatisticForPlayerAsync(Player player)
        {
            List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM PlayerStatistic WHERE PlayerID = {player.ID};";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                var matches = await GetMatchesAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        int matchId = reader.GetInt32("MatchID");
                        Match match = matches.Find(x => x.ID == matchId);
                        int goals = reader.GetInt32("Goals");
                        int assists = reader.GetInt32("Assists");
                        int shoots = reader.GetInt32("Shoots");
                        int tackling = reader.GetInt32("Tackling");
                        bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
                        bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

                        PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

                        psList.Add(plSPM);
                    }
                }
            }
            return psList;
        }
        public async Task<List<PlayerStatisticsPerMatch>> GetAllPlayersStatsAsync()
        {
            List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM PlayerStatistic";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    var players = await GetPlayersAsync();
                    var matches = await GetMatchesAsync();

                    while (await reader.ReadAsync ())
                    {
                        int id = reader.GetInt32("ID");

                        int matchId = reader.GetInt32("MatchID");
                        Match match = matches.Find(x => x.ID == matchId);

                        int playerId = reader.GetInt32("PlayerID");
                        Player player = players.Find(p => p.ID == playerId);
                        int goals = reader.GetInt32("Goals");
                        int assists = reader.GetInt32("Assists");
                        int shoots = reader.GetInt32("Shoots");
                        int tackling = reader.GetInt32("Tackling");
                        bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
                        bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

                        PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

                        psList.Add(plSPM);
                    }
                }
            }
            return psList;
        }
        public async Task<List<PlayerStatisticsPerMatch>> GetGoalsForMatchAsync(Match match)
        {
            List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT Goals, PlayerID FROM PlayerStatistic WHERE MatchID = {match.ID} and Goals > 0";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    List<Player> players = await GetPlayersAsync();

                    while (reader.Read())
                    {
                        //int id = reader.GetInt32("ID");
                        int playerId = reader.GetInt32("PlayerID");
                        Player player = new Player(playerId, "-", "-", new DateOnly(1, 1, 1), "-");
                        int goals = reader.GetInt32("Goals");


                        PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(0, player, match, goals, 0, 0, 0, false, false);

                        psList.Add(plSPM);
                    }
                }
            }
            return psList;
        }
        public async Task<List<PlayerStatisticsPerMatch>> GetAllPlayerStatisticsForMatchAsync(Match match)
        {
            List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM PlayerStatistic WHERE MatchID = {match.ID};";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    List<Player> players = await GetPlayersAsync();

                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");
                        int playerId = reader.GetInt32("PlayerID");
                        Player player = players.Find(p => p.ID == playerId);
                        int goals = reader.GetInt32("Goals");
                        int assists = reader.GetInt32("Assists");
                        int shoots = reader.GetInt32("Shoots");
                        int tackling = reader.GetInt32("Tackling");
                        bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
                        bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

                        PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

                        psList.Add(plSPM);
                    }
                }
            }
            return psList;
        }
        public async Task<List<PlayerStatisticsPerMatch>> GetAllPlayersStatsForClubAsync(Club club)
        {
            List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string procedureName = "GetPlayerStatisticsForClub";

                using (SqlCommand sqlCommand = new SqlCommand(procedureName, connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@ClubID", club.ID));

                    var matches = await GetMatchesForClubAsync(club);

                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("PlayerStatisticID"));
                                int matchId = reader.GetInt32(reader.GetOrdinal("MatchID"));
                                int playerId = reader.GetInt32(reader.GetOrdinal("PlayerID"));
                                string playerName = reader.GetString(reader.GetOrdinal("PlayerName"));
                                string nationality = reader.GetString(reader.GetOrdinal("Nationality"));
                                DateTime birth = reader.GetDateTime(reader.GetOrdinal("Birth"));
                                string position = reader.GetString(reader.GetOrdinal("Position"));
                                int goals = reader.GetInt32(reader.GetOrdinal("Goals"));
                                int assists = reader.GetInt32(reader.GetOrdinal("Assists"));
                                int shoots = reader.GetInt32(reader.GetOrdinal("Shoots"));
                                int tackling = reader.GetInt32(reader.GetOrdinal("Tackling"));
                                bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
                                bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

                                Player player = new Player(playerId, playerName, nationality, new DateOnly(birth.Year, birth.Month, birth.Day), position);
                                Match match = matches.Find(x => x.ID == matchId);

                                PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

                                psList.Add(plSPM);
                            }
                        }
                    }
                }
            }
            return psList;
        }
        public void InsertPlayerStatistic(PlayerStatisticsPerMatch playerStatistics)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = @"INSERT INTO PlayerStatistic 
                                   (MatchID, PlayerID, Goals, Assists, Shoots, Tackling, YellowCart, RedCart) 
                                   VALUES 
                                   (@MatchIDs, @PlayerIDs, @Goalss, @Assistss, @Shootss, @Tacklings, @YellowCarts, @RedCarts)";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                sqlCommand.Parameters.AddWithValue("@MatchIDs", playerStatistics.Match.ID);
                sqlCommand.Parameters.AddWithValue("@PlayerIDs", playerStatistics.Player.ID);
                sqlCommand.Parameters.AddWithValue("@Goalss", playerStatistics.Goals);
                sqlCommand.Parameters.AddWithValue("@Assistss", playerStatistics.Assists);
                sqlCommand.Parameters.AddWithValue("@Shootss", playerStatistics.Shoots);
                sqlCommand.Parameters.AddWithValue("@Tacklings", playerStatistics.Tackling);
                sqlCommand.Parameters.AddWithValue("@YellowCarts", playerStatistics.YellowCart);
                sqlCommand.Parameters.AddWithValue("@RedCarts", playerStatistics.RedCart);

                sqlCommand.ExecuteNonQuery();
            }
        }
        public async Task UpdatePlayerStatisticAsync(PlayerStatisticsPerMatch plStats)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdatePlayerStatistic", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", plStats.ID);
                    command.Parameters.AddWithValue("@MatchID", plStats.Match.ID);
                    command.Parameters.AddWithValue("@PlayerID", plStats.Player.ID);
                    command.Parameters.AddWithValue("@Goals", plStats.Goals);
                    command.Parameters.AddWithValue("@Assists", plStats.Assists);
                    command.Parameters.AddWithValue("@Shoots", plStats.Shoots);
                    command.Parameters.AddWithValue("@Tackling", plStats.Tackling);
                    command.Parameters.AddWithValue("@YellowCart", plStats.YellowCart);
                    command.Parameters.AddWithValue("@RedCart", plStats.RedCart);

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        #endregion

        #region ClubStats
        public async Task<List<ClubStatisticsPerMatch>> GetClubsStatsForMatchAsync(Match match)
        {
            List<ClubStatisticsPerMatch> csList = new List<ClubStatisticsPerMatch>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM ClubStatistic WHERE MatchID = {match.ID}";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");

                        int clubId = reader.GetInt32("ClubID");
                        var clubs = await GetClubsAsync();
                        Club club = clubs.Find(c => c.ID == clubId);

                        int possesion = reader.GetInt32("Possession");
                        int corners = reader.GetInt32("Corners");
                        int offsides = reader.GetInt32("Offsides");

                        ClubStatisticsPerMatch cspm = new ClubStatisticsPerMatch(id, club, match, possesion, corners, offsides);

                        csList.Add(cspm);
                    }
                }
            }
            return csList;
        }
        public async Task<List<ClubStatisticsPerMatch>> GetAllStatsForClubAsync(Club club)
        {
            List<ClubStatisticsPerMatch> csList = new List<ClubStatisticsPerMatch>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = $"SELECT * FROM ClubStatistic WHERE ClubID = {club.ID}";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                var matches = await GetMatchesForClubAsync(club);

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ID");

                        int matchId = reader.GetInt32("MatchID");
                        Match match = matches.Find(m => m.ID == matchId);

                        int possesion = reader.GetInt32("Possession");
                        int corners = reader.GetInt32("Corners");
                        int offsides = reader.GetInt32("Offsides");

                        ClubStatisticsPerMatch cspm = new ClubStatisticsPerMatch(id, club, match, possesion, corners, offsides);

                        csList.Add(cspm);
                    }
                }
            }
            return csList;
        }
        public void InsertClubStatistics(ClubStatisticsPerMatch statistics)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string command = $"INSERT INTO ClubStatistic (ClubID, MatchID, Possession, Corners, Offsides) VALUES (@ClubID, @MatchID, @Possession, @Corners, @Offsides)";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                sqlCommand.Parameters.AddWithValue("@ClubID", statistics.Club.ID);
                sqlCommand.Parameters.AddWithValue("@MatchID", statistics.Match.ID);
                sqlCommand.Parameters.AddWithValue("@Possession", statistics.Possession);
                sqlCommand.Parameters.AddWithValue("@Corners", statistics.Corners);
                sqlCommand.Parameters.AddWithValue("@Offsides", statistics.Offsides);

                sqlCommand.ExecuteNonQuery();
            }
        }
        public async Task UpdateClubStatisticsAsync(ClubStatisticsPerMatch statistics)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string command = @"
                UPDATE ClubStatistic
                SET Possession = @Possession,
                    Corners = @Corners,
                    Offsides = @Offsides
                WHERE ID = @ID AND ClubID = @ClubID AND MatchID = @MatchID";

                SqlCommand sqlCommand = new SqlCommand(command, connection);

                sqlCommand.Parameters.AddWithValue("@ID", statistics.ID);
                sqlCommand.Parameters.AddWithValue("@ClubID", statistics.Club.ID);
                sqlCommand.Parameters.AddWithValue("@MatchID", statistics.Match.ID);
                sqlCommand.Parameters.AddWithValue("@Possession", statistics.Possession);
                sqlCommand.Parameters.AddWithValue("@Corners", statistics.Corners);
                sqlCommand.Parameters.AddWithValue("@Offsides", statistics.Offsides);

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }
        #endregion
    }


    #region NoUse
    //public List<ClubStatisticsPerMatch> GetAllClubsStats()
    //{
    //    List<ClubStatisticsPerMatch> csList = new List<ClubStatisticsPerMatch>();

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = $"SELECT * FROM ClubStatistic";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");

    //                int clubId = reader.GetInt32("ClubID");
    //                Club club = GetClubs().Find(c => c.ID == clubId);

    //                int matchId = reader.GetInt32("MatchID");
    //                Match match = GetMatches().Find(m => m.ID == matchId);

    //                int possesion = reader.GetInt32("Possession");
    //                int corners = reader.GetInt32("Corners");
    //                int offsides = reader.GetInt32("Offsides");

    //                ClubStatisticsPerMatch cspm = new ClubStatisticsPerMatch(id, club, match, possesion, corners, offsides);

    //                csList.Add(cspm);
    //            }
    //        }
    //    }
    //    return csList;
    //}
    //public List<ClubStatisticsPerMatch> GetAllStatsForClub(Club club)
    //{
    //    List<ClubStatisticsPerMatch> csList = new List<ClubStatisticsPerMatch>();

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = $"SELECT * FROM ClubStatistic WHERE ClubID = {club.ID}";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");

    //                int matchId = reader.GetInt32("MatchID");
    //                Match match = GetMatches().Find(m => m.ID == matchId);

    //                int possesion = reader.GetInt32("Possession");
    //                int corners = reader.GetInt32("Corners");
    //                int offsides = reader.GetInt32("Offsides");

    //                ClubStatisticsPerMatch cspm = new ClubStatisticsPerMatch(id, club, match, possesion, corners, offsides);

    //                csList.Add(cspm);
    //            }
    //        }
    //    }
    //    return csList;
    //}
    //public List<PlayerStatisticsPerMatch> GetAllStatisticForPlayer(Player player)
    //{
    //    List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = $"SELECT * FROM PlayerStatistic WHERE PlayerID = {player.ID};";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");
    //                int matchId = reader.GetInt32("MatchID");
    //                Match match = GetMatches().Find(x => x.ID == matchId);
    //                int goals = reader.GetInt32("Goals");
    //                int assists = reader.GetInt32("Assists");
    //                int shoots = reader.GetInt32("Shoots");
    //                int tackling = reader.GetInt32("Tackling");
    //                bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
    //                bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

    //                PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

    //                psList.Add(plSPM);
    //            }
    //        }
    //    }
    //    return psList;
    //}
    //public List<PlayerStatisticsPerMatch> GetAllPlayersStatsForClub(Club club)
    //{
    //    List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string procedureName = "GetPlayerStatisticsForClub";

    //        using (SqlCommand sqlCommand = new SqlCommand(procedureName, connection))
    //        {
    //            sqlCommand.CommandType = CommandType.StoredProcedure;
    //            sqlCommand.Parameters.Add(new SqlParameter("@ClubID", club.ID));

    //            using (SqlDataReader reader = sqlCommand.ExecuteReader())
    //            {
    //                if (reader.HasRows)
    //                {
    //                    while (reader.Read())
    //                    {
    //                        int id = reader.GetInt32(reader.GetOrdinal("PlayerStatisticID"));
    //                        int matchId = reader.GetInt32(reader.GetOrdinal("MatchID"));
    //                        int playerId = reader.GetInt32(reader.GetOrdinal("PlayerID"));
    //                        string playerName = reader.GetString(reader.GetOrdinal("PlayerName"));
    //                        string nationality = reader.GetString(reader.GetOrdinal("Nationality"));
    //                        DateTime birth = reader.GetDateTime(reader.GetOrdinal("Birth"));
    //                        string position = reader.GetString(reader.GetOrdinal("Position"));
    //                        int goals = reader.GetInt32(reader.GetOrdinal("Goals"));
    //                        int assists = reader.GetInt32(reader.GetOrdinal("Assists"));
    //                        int shoots = reader.GetInt32(reader.GetOrdinal("Shoots"));
    //                        int tackling = reader.GetInt32(reader.GetOrdinal("Tackling"));
    //                        bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
    //                        bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

    //                        Player player = new Player(playerId, playerName, nationality, new DateOnly(birth.Year, birth.Month, birth.Day), position);
    //                        Match match = GetMatches().Find(x => x.ID == matchId);

    //                        PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

    //                        psList.Add(plSPM);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return psList;
    //}
    //public List<Match> GetMatches()
    //{
    //    List<Match> matchList = new List<Match>();
    //    List<Club> clubs = GetClubs();

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = $"SELECT * FROM Matches";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");
    //                DateOnly date = DateOnly.FromDateTime(reader.GetDateTime("Date"));

    //                int homeClubId = reader.GetInt32("HomeClub");
    //                int awayClubId = reader.GetInt32("AwayClub");

    //                Club homeClub = clubs.Find(x => x.ID == homeClubId);
    //                Club awayClub = clubs.Find(x => x.ID == awayClubId);

    //                Match match = new Match(id, date, homeClub, awayClub);
    //                matchList.Add(match);
    //            }
    //        }
    //    }
    //    return matchList;
    //}
    //public List<Player> GetPlayers()
    //{
    //    List<Player> playerList = new List<Player>();
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = "SELECT * FROM Players";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");
    //                string name = (String)reader["Name"];
    //                string nationality = (String)reader["Nationality"];
    //                DateOnly birth = DateOnly.FromDateTime(reader.GetDateTime("Birth"));
    //                string position = (String)reader["Position"];
    //                Player player = new Player(id, name, nationality, birth, position);

    //                playerList.Add(player);
    //            }
    //        }
    //    }
    //    return playerList;
    //}
    //public List<Club> GetClubs()
    //{
    //    List<Club> clubList = new List<Club>();
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = "SELECT * FROM Clubs";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");
    //                string name = (String)reader["Name"];

    //                List<Player> players = GetPlayersForClub(id);

    //                Club club = new Club(id, name, players);
    //                clubList.Add(club);
    //            }
    //        }
    //    }
    //    return clubList;
    //}
    //public List<Player> GetPlayersForClub(int clubId)
    //{
    //    List<Player> playerList = new List<Player>();
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = $"SELECT Players.ID, Players.[Name], Players.Nationality, Players.Birth, Players.Position FROM Players JOIN ClubsPlayers ON Players.ID = ClubsPlayers.PlayerID WHERE ClubsPlayers.ClubID = {clubId};";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");
    //                string name = (String)reader["Name"];
    //                string nationality = (String)reader["Nationality"];
    //                DateOnly birth = DateOnly.FromDateTime(reader.GetDateTime("Birth"));
    //                string position = (String)reader["Position"];
    //                Player player = new Player(id, name, nationality, birth, position);

    //                playerList.Add(player);
    //            }
    //        }
    //    }
    //    return playerList;
    //}
    //public List<PlayerStatisticsPerMatch> GetAllPlayersStats()
    //{
    //    List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = $"SELECT * FROM PlayerStatistic";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            List<Player> players = GetPlayers();

    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");

    //                int matchId = reader.GetInt32("MatchID");
    //                Match match = GetMatches().Find(x => x.ID == matchId);

    //                int playerId = reader.GetInt32("PlayerID");
    //                Player player = players.Find(p => p.ID == playerId);
    //                int goals = reader.GetInt32("Goals");
    //                int assists = reader.GetInt32("Assists");
    //                int shoots = reader.GetInt32("Shoots");
    //                int tackling = reader.GetInt32("Tackling");
    //                bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
    //                bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

    //                PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

    //                psList.Add(plSPM);
    //            }
    //        }
    //    }
    //    return psList;
    //}
    //public List<PlayerStatisticsPerMatch> GetAllPlayerStatisticsForMatch(Match match)
    //{
    //    List<PlayerStatisticsPerMatch> psList = new List<PlayerStatisticsPerMatch>();

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        string command = $"SELECT * FROM PlayerStatistic WHERE MatchID = {match.ID};";

    //        SqlCommand sqlCommand = new SqlCommand(command, connection);

    //        SqlDataReader reader = sqlCommand.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            List<Player> players = GetPlayers();

    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("ID");
    //                int playerId = reader.GetInt32("PlayerID");
    //                Player player = players.Find(p => p.ID == playerId);
    //                int goals = reader.GetInt32("Goals");
    //                int assists = reader.GetInt32("Assists");
    //                int shoots = reader.GetInt32("Shoots");
    //                int tackling = reader.GetInt32("Tackling");
    //                bool yellowCart = reader.GetBoolean(reader.GetOrdinal("YellowCart"));
    //                bool redCart = reader.GetBoolean(reader.GetOrdinal("RedCart"));

    //                PlayerStatisticsPerMatch plSPM = new PlayerStatisticsPerMatch(id, player, match, goals, assists, shoots, tackling, yellowCart, redCart);

    //                psList.Add(plSPM);
    //            }
    //        }
    //    }
    //    return psList;
    //}
    #endregion

}