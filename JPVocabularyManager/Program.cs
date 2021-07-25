using ExcelParser;
using ScriptParsers;
using System;
using System.Collections.Generic;
using System.Web;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            List<string> kanjis;
            using (ExcelReader excelReader = new ExcelReader(@"C:\Users\margy\Desktop\laptop\im agile.xlsx", "漢字")) {
                kanjis = excelReader.GetKanjis("B4");
            }

            foreach (string kanji in kanjis) {
                ScriptParser scriptParser = new ScriptParser(Constants.PythonScriptPath);
                string url = string.Format(@"https://jisho.org/search/{0}/%20%23kanji", HttpUtility.UrlEncode(kanji));

                Console.WriteLine(scriptParser.RunScript(url));   
            }
        }
    }
}