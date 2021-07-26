using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatabaseHandler {
    public class DatabaseHandler {
        public DatabaseHandler() {
            string connectionString = @"Filename=..\..\..\..\DatabaseHandler\Database\VocabularyDatabase.db";
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connectionString).Options;

            using DatabaseContext dataBase = new DatabaseContext(options);
            dataBase.Database.EnsureCreated();

            Kanji kanji = new Kanji {
                HeisingID = 1,
                HeisingMeaning = "one",
                Symbol = "一",
                Meanings = new List<Meaning>() { new Meaning() { Word = "one" }, new Meaning() { Word = "one radical(no.1)" } },
                KunReadings = new List<KunReading>() { new KunReading() { Reading = "ひと-" }, new KunReading() { Reading = "ひと.つ" } },
                OnReadings = new List<OnReading>() { new OnReading() { Reading = "イチ" }, new OnReading() { Reading = "イツ" } }
            };

            dataBase.Kanjis.Add(kanji);
            dataBase.SaveChanges();
        }
    }
}