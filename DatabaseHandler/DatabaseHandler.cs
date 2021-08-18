using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DatabaseHandler {
    public class DatabaseHandler : IDisposable {
        private const string connectionString = @"Filename=..\..\..\..\DatabaseHandler\Database\KanjiDatabase.db";
        private DbContextOptions<DatabaseContext> options;
        private DatabaseContext dataBase;

        public DatabaseHandler() {
            options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connectionString).Options;
            dataBase = new DatabaseContext(options);
            dataBase.Database.EnsureCreated();
        }

        public Kanji GetKanji(string symbol) {
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

        public bool AddOrReplaceKanji(Kanji kanji) {
            try {
                if (!RemoveKanji(kanji.Symbol)) {
                    return false;
                }
                
                dataBase.Kanjis.Add(kanji);
                dataBase.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool RemoveKanji(string kanji) {
            try {
                Kanji foundKanji = GetKanji(kanji);
                if (foundKanji != null) {
                    dataBase.Remove(foundKanji);
                    dataBase.SaveChanges();
                }
                return true;
            }
            catch {
                return false;
            }
        }

        public virtual void Dispose() {
            dataBase.Dispose();
            dataBase = null;
            options = null;
        }
    }
}