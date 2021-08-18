using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DatabaseHandler {
    public class KanjiDatabaseHandler : IDisposable {
        private const string connectionString = @"Filename=..\..\..\..\DatabaseHandler\Databases\KanjiDatabase2.db";
        private DbContextOptions<KanjiDatabaseContext> options;
        private KanjiDatabaseContext dataBase;

        public KanjiDatabaseHandler() {
            options = new DbContextOptionsBuilder<KanjiDatabaseContext>().UseSqlite(connectionString).Options;
            dataBase = new KanjiDatabaseContext(options);
            dataBase.Database.EnsureCreated();
        }

        public Kanji GetKanji(string symbol) {
            try {
                return dataBase.Kanjis.Include(kanji => kanji.Meanings)
                                      .Include(kanji => kanji.KunReadings)
                                      .Include(kanji => kanji.OnReadings)
                                      .Include(kanji => kanji.Parts)
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
                    dataBase.RemoveCollection(foundKanji.Meanings);
                    dataBase.RemoveCollection(foundKanji.KunReadings);
                    dataBase.RemoveCollection(foundKanji.OnReadings);
                    dataBase.RemoveCollection(foundKanji.Parts);
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