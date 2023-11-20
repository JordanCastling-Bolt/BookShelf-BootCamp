using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Library_Classlib
{
    /// <summary>
    /// Handles database operations for managing leaderboard data.
    /// </summary>
    public class DatabaseHandler
    {
        // Connection string for SQLite database
        private string connectionString = "Data Source=Leaderboard.sqlite;Version=3;";

        /// <summary>
        /// Initializes a new instance of the DatabaseHandler class.
        /// </summary>
        /// <remarks>
        /// Ensures that the SQLite database file exists and initializes the database if not.
        /// </remarks>
        public DatabaseHandler()
        {
            if (!File.Exists("Leaderboard.sqlite"))
            {
                InitializeDatabase();
            }
        }

        /// <summary>
        /// Initializes the SQLite database by creating the necessary file and tables.
        /// </summary>
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
                // Log database initialization errors
                Console.WriteLine("Database Initialization Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Asynchronously saves a leaderboard entry to the database.
        /// </summary>
        /// <param name="userTag">The user tag to be saved.</param>
        /// <param name="score">The score associated with the user tag.</param>
        /// <remarks>
        /// Updates the user's score if it's higher than the existing one, or adds a new entry if the user tag doesn't exist.
        /// </remarks>
        public async Task SaveLeaderboardEntry(string userTag, int score)
        {
            await Task.Run(() =>
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the UserTag already exists in the database
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

                    // Update the score if it's higher than the existing one
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
                    else if (existingScore == -1) // Insert a new entry if the user tag doesn't exist
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

        /// <summary>
        /// Asynchronously retrieves leaderboard data from the database.
        /// </summary>
        /// <returns>A list of LeaderboardEntry objects representing the leaderboard data.</returns>
        public async Task<List<LeaderboardEntry>> GetLeaderboardData()
        {
            return await Task.Run(() =>
            {
                List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to retrieve leaderboard data ordered by score
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

    /// <summary>
    /// Represents an entry in the leaderboard.
    /// </summary>
    public class LeaderboardEntry
    {
        public string UserTag { get; set; }
        public int Score { get; set; }
    }
}

