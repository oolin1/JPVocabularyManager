using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatabaseHandler {
    public class LessonDbContext : DbContext, IModifiable {
        public LessonDbContext(DbContextOptions<LessonDbContext> options) : base(options) { }
        public DbSet<Lesson> Lessons { get; set; }

        public IEntity GetEntity(string identifier) => Lessons.Include(lesson => lesson.Entries).Where(lesson => lesson.Name == identifier)?.First();

        public void AddEntity(IEntity entity) => Lessons.Add(entity as Lesson);
        
        public void RemoveEntity(IEntity entity) {
            Lesson lesson = entity as Lesson;
            if (lesson.Entries != null) {
                foreach (LessonEntry entry in lesson.Entries) {
                    Remove(entry);
                }
            }
            Remove(lesson);
        }
    }
}