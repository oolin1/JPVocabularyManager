using DatabaseHandler;
using DatabaseHandler.Data;
using KanjiSheetHandler;
using System;
using System.Collections.Generic;
using System.IO;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            //RunDbExample();

            string filePath = Path.GetFullPath(@"..\..\..\..\KanjiSheetHandler\Resources\Kanji Sheet Sample.xlsx");
            string sheetName = "Kanji";

            KanjiSheetReader kanjiSheetReader = new KanjiSheetReader(filePath, sheetName);
            List<string> kanjis = kanjiSheetReader.ReadKanjisFromRange("A1", "T608");

            using KanjiDatabaseHandler dbHandler = new KanjiDatabaseHandler();
            foreach (string kanji in kanjis) {
                Kanji result = KanjiBuilder.BuildKanji(kanji);
                if (result != null) {
                    Console.WriteLine("adding kanji " + result.HeisingID + ", " + result.HeisingMeaning);
                    dbHandler.AddOrReplaceKanji(result);
                }
            }
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

            LessonDatabaseHandler lessonDatabaseHandler = new LessonDatabaseHandler();
            lessonDatabaseHandler.AddOrUpdateLesson(exampleLesson);
            Lesson retrievedLesson = lessonDatabaseHandler.GetLesson("Lesson 1");
            retrievedLesson.Entries.Add(new LessonEntry() {
                Phrase = "仕業",
                Reading = "しわざ",
                Meaning = "work, someones doing",
                Comment = "妖怪の仕業"
            });
            lessonDatabaseHandler.AddOrUpdateLesson(retrievedLesson);
            retrievedLesson = lessonDatabaseHandler.GetLesson("Lesson 1");
            lessonDatabaseHandler.RemoveLesson("Lesson 1");
            retrievedLesson = lessonDatabaseHandler.GetLesson("Lesson 1");

            Kanji exampleKanji = new Kanji() {
                HeisingID = 3,
                HeisingMeaning = "three",
                Symbol = "三",
                Meanings = new List<KanjiMeaning>() { new KanjiMeaning() { Word = "three" } },
                KunReadings = new List<KunReading>() { new KunReading() { Reading = "み" } },
                OnReadings = new List<OnReading>() { new OnReading() { Reading = "サン" } },
                Parts = new List<KanjiPart>() { new KanjiPart() { Part = "一" }, new KanjiPart() { Part = "三" } }
            };
            KanjiDatabaseHandler kanjiDatabaseHandler = new KanjiDatabaseHandler();
            kanjiDatabaseHandler.AddOrReplaceKanji(exampleKanji);
            Kanji retrievedKanji = kanjiDatabaseHandler.GetKanji("三");
            kanjiDatabaseHandler.RemoveKanji("三");
            retrievedKanji = kanjiDatabaseHandler.GetKanji("三");


        }
    }
}