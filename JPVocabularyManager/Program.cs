using ScriptParsers;
using System;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            ScriptParser scriptParser = new ScriptParser(Constants.PythonScriptPath);
            Console.Write(scriptParser.RunScript(""));
        }
    }
}