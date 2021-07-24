using ExcelParser;
using ScriptParsers;
using System;
using System.Web;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            string kanji;
            using (ExcelReader excelReader = new ExcelReader(@"C:\Users\margy\Desktop\laptop\im agile.xlsx", "漢字")) {
                 kanji = excelReader.GetKanji("B4");
            }
            
            ScriptParser scriptParser = new ScriptParser(Constants.PythonScriptPath);
            string url = string.Format(@"https://jisho.org/search/{0}/%20%23kanji", HttpUtility.UrlEncode(kanji));

            Console.Write(scriptParser.RunScript(url));
        }
    }
}