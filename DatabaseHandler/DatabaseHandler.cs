using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatabaseHandler {
    public class DatabaseHandler {
        private string connectionString = @"Filename=..\..\..\..\DatabaseHandler\Database\VocabularyDatabase.db";
        DbContextOptions<DatabaseContext> options;
        public DatabaseHandler() {
            options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connectionString).Options;
            using (DatabaseContext dataBase = new DatabaseContext(options)) {
                dataBase.Database.EnsureCreated();
            }
        }

        public Kanji GetKanjiBySymbol(string symbol) {
            using DatabaseContext dataBase = new DatabaseContext(options); 
            try {
                return dataBase.Kanjis.Include(m => m.Meanings)
                                      .Include(m => m.KunReadings)
                                      .Include(m => m.OnReadings)
                                      .Include(m => m.Parts)
                                      .Where(kanji => kanji.Symbol == symbol).First();
            }
            catch {
                return null;
            }
        }

        public bool AddKanji(Kanji kanji) {
            using DatabaseContext dataBase = new DatabaseContext(options);
            try {
                dataBase.Kanjis.Add(kanji);
                dataBase.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool RemoveKanji(Kanji kanji) {
            using DatabaseContext dataBase = new DatabaseContext(options);
            try {
                dataBase.Remove(kanji);
                dataBase.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }
    }
}