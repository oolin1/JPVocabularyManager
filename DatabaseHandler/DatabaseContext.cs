using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHandler {
    public class DatabaseContext : DbContext {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Kanji> Kanjis { get; set; }
    }
}