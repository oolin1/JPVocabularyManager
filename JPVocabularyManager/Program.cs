﻿using KanjiSheetHandler;
using ScriptParsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            string filePath = Path.GetFullPath(@"..\..\..\..\KanjiSheetHandler\Resources\Kanji Sheet Sample.xlsx");
            Path.GetRelativePath(Directory.GetCurrentDirectory(), @"C:\Project\JPVocabularyManager\KanjiSheetHandler\Resources\Kanji Sheet Sample.xlsx");
            string sheetName = "Kanji";

            KanjiSheetReader kanjiSheetReader = new KanjiSheetReader(filePath, sheetName);
            List<string> kanjis = kanjiSheetReader.ReadKanjisFromRange("A1", "T608");

            foreach (string kanji in kanjis) {
                ScriptParser scriptParser = new ScriptParser(Constants.PythonScriptPath);
                string url = string.Format(@"https://jisho.org/search/{0}/%20%23kanji", HttpUtility.UrlEncode(kanji));

                Console.WriteLine(scriptParser.RunScript(url));   
            }
        }
    }
}