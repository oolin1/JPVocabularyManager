using ScriptParsers;
using System;
using System.Web;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            ScriptParser scriptParser = new ScriptParser(Constants.PythonScriptPath);


            string url = string.Format(@"https://jisho.org/search/{0}/%20%23kanji", HttpUtility.UrlEncode("出"));

            Console.Write(scriptParser.RunScript(url));
        }
    }
}