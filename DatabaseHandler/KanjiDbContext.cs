using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseHandler {
    public class KanjiDbContext : DbContext, IModifiable {
        public KanjiDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Kanji> Kanjis { get; set; }

        public IEntity GetEntity(string identifier) => Kanjis.Include(kanji => kanji.Meanings)
                                                             .Include(kanji => kanji.KunReadings)
                                                             .Include(kanji => kanji.OnReadings)
                                                             .Include(kanji => kanji.Parts)
                                                             .Where(kanji => kanji.Symbol == identifier)?.First();

        public void AddEntity(IEntity entity) => Kanjis.Add(entity as Kanji);

        public void RemoveEntity(IEntity entity) {
            Kanji kanji = entity as Kanji;
            RemoveCollection(kanji.Meanings);
            RemoveCollection(kanji.KunReadings);
            RemoveCollection(kanji.OnReadings);
            RemoveCollection(kanji.Parts);
            Remove(kanji);
        }

        private void RemoveCollection<T>(ICollection<T> collection) {
            if (collection != null) {
                foreach (T item in collection) {
                    Remove(item);
                }
            }
        }
    }
}