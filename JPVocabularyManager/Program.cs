using DatabaseHandler;
using DatabaseHandler.Data;
using DesktopClient;
using DesktopClient.View;
using ExcelReader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace JPVocabularyManager {
    public class Program {
        [STAThread]
        static void Main(string[] args) {
            DesktopClientApp app = new DesktopClientApp();
            app.MainWindow = new MainWindow();
            app.MainWindow.ShowDialog();

            //RunDbExample();

            //string filePath = Path.GetFullPath(@"..\..\..\..\ExcelReader\Resources\Kanji Sheet Sample.xlsx");
            //string sheetName = "Kanji";

            //KanjiSheetParser kanjiSheetReader = new KanjiSheetParser(filePath, sheetName);
            //List<string> kanjis = kanjiSheetReader.ReadKanjisFromRange("A1", "T608");

            //string connectionString = @"Filename=C:\Project\JPVocabularyManager\DatabaseHandler\Databases\KanjiDatabase.db";
            //DbContextOptions<KanjiDbContext> options = new DbContextOptionsBuilder<KanjiDbContext>().UseSqlite(connectionString).Options;
            //KanjiDbContext dataBase = new KanjiDbContext(options);
            //DatabaseHandler<KanjiDbContext> kanjiDatabaseHandler = new DatabaseHandler<KanjiDbContext>(dataBase);

            //foreach (string kanji in kanjis) {
            //    Kanji result = KanjiBuilder.BuildKanji(kanji);
            //    if (result != null) {
            //        Console.WriteLine("adding kanji " + result.HeisingID + ", " + result.HeisingMeaning);
            //        kanjiDatabaseHandler.AddOrUpdateEntity(result);
            //    }
            //}
        }

        static private void RunDbExample() {
            Lesson exampleLesson = new Lesson() {
                Name = "Lesson 1",
                Entries = new List<LessonEntry>() {
                    new LessonEntry() {
                        Phrase = "用意",
                        Reading = "ようい",
                        Meaning = "preparation",
                        Comment = "まだ用意ができていません"
                    }
                }
            };

            string connectionString = @"Filename=C:\Project\JPVocabularyManager\DatabaseHandler\Databases\LessonDatabase.db";
            DbContextOptions<LessonDbContext> options = new DbContextOptionsBuilder<LessonDbContext>().UseSqlite(connectionString).Options;
            LessonDbContext lessonDb = new LessonDbContext(options);
            DatabaseHandler<LessonDbContext> lessonDatabaseHandler = new DatabaseHandler<LessonDbContext>(lessonDb);
            lessonDatabaseHandler.AddOrUpdateEntity(exampleLesson);
            Lesson retrievedLesson = lessonDatabaseHandler.GetEntity("Lesson 1") as Lesson;
            retrievedLesson.Entries.Add(new LessonEntry() {
                Phrase = "仕業",
                Reading = "しわざ",
                Meaning = "work, someones doing",
                Comment = "妖怪の仕業"
            });
            lessonDatabaseHandler.AddOrUpdateEntity(retrievedLesson);
            retrievedLesson = lessonDatabaseHandler.GetEntity("Lesson 1") as Lesson;
            lessonDatabaseHandler.RemoveEntity("Lesson 1");
            retrievedLesson = lessonDatabaseHandler.GetEntity("Lesson 1") as Lesson;

            Kanji exampleKanji = new Kanji() {
                HeisingID = 3,
                HeisingMeaning = "three",
                Symbol = "三",
                Meanings = new List<KanjiMeaning>() { new KanjiMeaning() { Word = "three" } },
                KunReadings = new List<KunReading>() { new KunReading() { Reading = "み" } },
                OnReadings = new List<OnReading>() { new OnReading() { Reading = "サン" } },
                Parts = new List<KanjiPart>() { new KanjiPart() { Part = "一" }, new KanjiPart() { Part = "三" } }
            };

            string connectionString2 = @"Filename=C:\Project\JPVocabularyManager\DatabaseHandler\Databases\KanjiDatabase2.db";
            DbContextOptions<KanjiDbContext> options2 = new DbContextOptionsBuilder<KanjiDbContext>().UseSqlite(connectionString2).Options;
            KanjiDbContext kanjiDb = new KanjiDbContext(options2);
            DatabaseHandler<KanjiDbContext> kanjiDatabaseHandler = new DatabaseHandler<KanjiDbContext>(kanjiDb);
            kanjiDatabaseHandler.AddOrUpdateEntity(exampleKanji);
            Kanji retrievedKanji = kanjiDatabaseHandler.GetEntity("三") as Kanji;
            kanjiDatabaseHandler.RemoveEntity("三");
            retrievedKanji = kanjiDatabaseHandler.GetEntity("三") as Kanji;
        }
    }
}