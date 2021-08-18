using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHandler {
    public class LessonDatabaseContext : DbContext {
        public LessonDatabaseContext(DbContextOptions<LessonDatabaseContext> options) : base(options) { }
        public DbSet<Lesson> Lessons { get; set; }
    }
}