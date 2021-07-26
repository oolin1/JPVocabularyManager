using DatabaseHandler;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Text;

namespace JPVocabularyDatabase {
    public class DbHandler {
        private SqliteConnectionStringBuilder connectionStringBuilder;
        public DbHandler() {
            connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = @"C:\Project\JPVocabularyManager\JPVocabularyDatabase\VocabularyDatabase.db";
        }

        public void ClearDb() {
            using (SqliteConnection connection = new SqliteConnection(connectionStringBuilder.ConnectionString)) {
                connection.Open();

                SqliteCommand clearCmd = connection.CreateCommand();
                clearCmd.CommandText = File.ReadAllText(@"C:\Project\JPVocabularyManager\JPVocabularyDatabase\clearDb.sql");
                clearCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AddKanji(Kanji kanji) {
            using (SqliteConnection connection = new SqliteConnection(connectionStringBuilder.ConnectionString)) {
                connection.Open();

                using (SqliteTransaction transaction = connection.BeginTransaction()) {
                    SqliteCommand insertCmd = connection.CreateCommand();
                    
                    insertCmd.CommandText = $"INSERT INTO Kanjis('HeisingID','Kanji') VALUES('{kanji.HeisingID}','{kanji.Text}');";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                connection.Close();
            }
        }

        public string GetKanji(string kanji) {
            using (SqliteConnection connection = new SqliteConnection(connectionStringBuilder.ConnectionString)) {
                connection.Open();

                SqliteCommand selectCmd = connection.CreateCommand();
                selectCmd.CommandText = $"SELECT * FROM Kanjis WHERE Kanji = '{kanji}'";

                StringBuilder stringBuilder = new StringBuilder();
                using (SqliteDataReader reader = selectCmd.ExecuteReader()) {
                    while (reader.Read()) {
                        stringBuilder.Append(reader.GetString(0));
                    }
                }

                connection.Close();
                return stringBuilder.ToString();
            }
        }
    }
}