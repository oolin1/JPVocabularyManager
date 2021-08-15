﻿using DatabaseHandler.Data;
using KanjiSheetHandler;
using System;
using System.Collections.Generic;
using System.IO;
using DbHandler = DatabaseHandler.DatabaseHandler;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            string filePath = Path.GetFullPath(@"..\..\..\..\KanjiSheetHandler\Resources\Kanji Sheet Sample.xlsx");
            string sheetName = "Kanji";

            KanjiSheetReader kanjiSheetReader = new KanjiSheetReader(filePath, sheetName);
            List<string> kanjis = kanjiSheetReader.ReadKanjisFromRange("A1", "T608");

            using DbHandler dbHandler = new DbHandler();
            foreach (string kanji in kanjis) {
                Kanji result = KanjiBuilder.BuildKanji(kanji);
                if (result != null) {
                    Console.WriteLine("adding kanji " + result.HeisingID + ", " + result.HeisingMeaning);
                    dbHandler.AddOrReplaceKanji(result);
                }
            }
        }
    }
}