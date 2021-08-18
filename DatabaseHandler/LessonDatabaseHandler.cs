using DatabaseHandler.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DatabaseHandler {
    public class LessonDatabaseHandler : IDisposable {
        private const string connectionString = @"Filename=..\..\..\..\DatabaseHandler\Databases\LessonDatabase.db";
        private DbContextOptions<LessonDatabaseContext> options;
        private LessonDatabaseContext dataBase;

        public LessonDatabaseHandler() {
            options = new DbContextOptionsBuilder<LessonDatabaseContext>().UseSqlite(connectionString).Options;
            dataBase = new LessonDatabaseContext(options);
            dataBase.Database.EnsureCreated();
        }

        public Lesson GetLesson(string name) {
            try {
                return dataBase.Lessons.Include(lesson => lesson.Entries)
                                       .Where(lesson => lesson.Name == name).First();
            }
            catch {
                return null;
            }
        }

        public bool AddOrUpdateLesson(Lesson lesson) {
            try {
                if (!RemoveLesson(lesson.Name)) {
                    return false;
                }

                dataBase.Lessons.Add(lesson);
                dataBase.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool RemoveLesson(string name) {
            try {
                Lesson foundlesson = GetLesson(name);
                if (foundlesson != null) {
                    foreach (LessonEntry entry in foundlesson.Entries) {
                        dataBase.Remove(entry);
                    }
                    dataBase.Remove(foundlesson);
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
