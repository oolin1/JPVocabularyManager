using System.Collections.Generic;
using System.IO;

using KanjiSheetHandler;
using WebScraper.Data;
using WebScraper.WebScrapers;
using DbHandler = DatabaseHandler.DatabaseHandler;

namespace JPVocabularyManager {
    public class Program {
        static void Main(string[] args) {
            //_ = new DbHandler();

            string filePath = Path.GetFullPath(@"..\..\..\..\KanjiSheetHandler\Resources\Kanji Sheet Sample.xlsx");
            string sheetName = "Kanji";

            KanjiSheetReader kanjiSheetReader = new KanjiSheetReader(filePath, sheetName);
            List<string> kanjis = kanjiSheetReader.ReadKanjisFromRange("A1", "T32"/*"T608"*/);
            
            JishoScraper jishoScraper = new JishoScraper();
            List<JishoData> jishoData = new List<JishoData>();
            foreach (string kanji in kanjis) {
                jishoData.Add(jishoScraper.ScrapeJishoData(kanji));
            }
        }
    }
}