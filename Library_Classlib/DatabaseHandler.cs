using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Classlib
{
    public class DatabaseHandler
    {
        private string connectionString = "Data Source=Leaderboard.sqlite;Version=3;";

        public DatabaseHandler()
        {
            if (!File.Exists("Leaderboard.sqlite"))
            {
                InitializeDatabase();
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                SQLiteConnection.CreateFile("Leaderboard.sqlite");
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "CREATE TABLE IF NOT EXISTS Leaderboard (UserTag TEXT, Score INTEGER)";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                Console.WriteLine("Database Initialization Error: " + ex.Message);
            }
        }


        public async Task SaveLeaderboardEntry(string userTag, int score)
        {
            await Task.Run(() =>
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the UserTag already exists
                    string checkSql = "SELECT Score FROM Leaderboard WHERE UserTag = @UserTag";
                    int existingScore = -1;
                    using (var command = new SQLiteCommand(checkSql, connection))
                    {
                        command.Parameters.AddWithValue("@UserTag", userTag);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                existingScore = Convert.ToInt32(reader["Score"]);
                            }
                        }
                    }

                    // If UserTag exists and new score is higher, update the score
                    if (existingScore != -1 && score > existingScore)
                    {
                        string updateSql = "UPDATE Leaderboard SET Score = @Score WHERE UserTag = @UserTag";
                        using (var updateCommand = new SQLiteCommand(updateSql, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@UserTag", userTag);
                            updateCommand.Parameters.AddWithValue("@Score", score);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    // If UserTag does not exist, insert new entry
                    else if (existingScore == -1)
                    {
                        string insertSql = "INSERT INTO Leaderboard (UserTag, Score) VALUES (@UserTag, @Score)";
                        using (var insertCommand = new SQLiteCommand(insertSql, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@UserTag", userTag);
                            insertCommand.Parameters.AddWithValue("@Score", score);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            });
        }



        public async Task<List<LeaderboardEntry>> GetLeaderboardData()
        {
            return await Task.Run(() =>
            {
                List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT UserTag, Score FROM Leaderboard ORDER BY Score DESC";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LeaderboardEntry entry = new LeaderboardEntry
                                {
                                    UserTag = reader["UserTag"].ToString(),
                                    Score = Convert.ToInt32(reader["Score"])
                                };
                                leaderboard.Add(entry);
                            }
                        }
                    }
                }
                return leaderboard;
            });
        }
    }

    public class LeaderboardEntry
    {
        public string UserTag { get; set; }
        public int Score { get; set; }
    }
}

