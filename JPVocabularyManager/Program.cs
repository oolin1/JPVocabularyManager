using ExcelParser;
using ScriptParsers;
using System;
using System.Web;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            ScriptParser scriptParser = new ScriptParser(Constants.PythonScriptPath);

            ExcelReader excelReader = new ExcelReader(@"C:\Users\margy\Desktop\laptop\im agile.xlsx", "漢字");
            string kanji = excelReader.GetKanji(4, 2);
            string url = string.Format(@"https://jisho.org/search/{0}/%20%23kanji", HttpUtility.UrlEncode(kanji));

            Console.Write(scriptParser.RunScript(url));
        }
    }
}