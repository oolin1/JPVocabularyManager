using Microsoft.Data.Sqlite;
using System;

namespace JPVocabularyDatabase {
    public class DbHandler {
        public DbHandler() {
            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = @"C:\Project\JPVocabularyManager\JPVocabularyDatabase\VocabularyDatabase.db";
            using (SqliteConnection connection = new SqliteConnection(connectionStringBuilder.ConnectionString)) {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "CREATE TABLE ...";
                tableCmd.ExecuteNonQuery();


                using (SqliteTransaction transaction = connection.BeginTransaction()) {
                    SqliteCommand instertCmd = connection.CreateCommand();
                    instertCmd.CommandText = "INSERT INTO Kanjis VALUES('id', 'kanji' ...";
                    instertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                SqliteCommand selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Kanjis";
                using (SqliteDataReader reader = selectCmd.ExecuteReader()) {
                    while (reader.Read()) {
                        string result = reader.GetString(0);
                        Console.WriteLine(result);
                    }
                }
            }
        }
    }
}