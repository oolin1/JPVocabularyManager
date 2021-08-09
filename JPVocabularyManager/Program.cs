using KanjiSheetHandler;
using System.Collections.Generic;
using System.IO;
using DbHandler = DatabaseHandler.DatabaseHandler;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            DbHandler dbHandler = new DbHandler();

            string filePath = Path.GetFullPath(@"..\..\..\..\KanjiSheetHandler\Resources\Kanji Sheet Sample.xlsx");
            string sheetName = "Kanji";

            KanjiSheetReader kanjiSheetReader = new KanjiSheetReader(filePath, sheetName);
            List<string> kanjis = kanjiSheetReader.ReadKanjisFromRange("F1", "F30"/*"T608"*/);
            
            foreach (string kanji in kanjis) {
                dbHandler.AddKanji(KanjiBuilder.BuildKanji(kanji));
            }
        }
    }
}