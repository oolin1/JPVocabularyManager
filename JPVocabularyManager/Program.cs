using KanjiSheetHandler;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

            TaskRunner runner = new TaskRunner();
            foreach (string kanji in kanjis) {
                Task kanjiTask = runner.TasksAsync(kanji);
                kanjiTask.Wait();
            }
        }

        private class TaskRunner {
            public async Task TasksAsync(string kanji) {
                Task<object> jishoTask = new JishoScraper().ScrapeData(kanji);
                Task<object> koohiiTask = new KoohiiScraper().ScrapeData(kanji);
                Task<object> jitenonTask = new JitenonScraper().ScrapeData(kanji);

                await Task.WhenAll(jishoTask, koohiiTask, jitenonTask);

                JishoData jishoData = jishoTask.Result as JishoData;
                KoohiiData koohiiData = koohiiTask.Result as KoohiiData;
                JitenonData jitenonData = jitenonTask.Result as JitenonData;

                return;
            }
        }
    }
}