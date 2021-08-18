using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatabaseHandler {
    public class KanjiDatabaseContext : DbContext {
        public KanjiDatabaseContext(DbContextOptions<KanjiDatabaseContext> options) : base(options) { }
        public DbSet<Kanji> Kanjis { get; set; }
        public void RemoveCollection<T>(ICollection<T> collection) {
            if (collection != null) {
                foreach (T item in collection) {
                    Remove(item);
                }
            }
        }
    }
}