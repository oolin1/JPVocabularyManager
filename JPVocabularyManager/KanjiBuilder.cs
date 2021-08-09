using DatabaseHandler.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebScraper.Data;
using WebScraper.WebScrapers;

namespace JPVocabularyManager {
    public class KanjiBuilder {
        public static Kanji BuildKanji(string kanji) {
            Task<Kanji> kanjiTask = BuildKanjiByWebScraping(kanji);
            kanjiTask.Wait();

            return kanjiTask.Result;
        }

        private static async Task<Kanji> BuildKanjiByWebScraping(string kanji) {
            Task<object> jishoTask = new JishoScraper().ScrapeData(kanji);
            Task<object> koohiiTask = new KoohiiScraper().ScrapeData(kanji);
            Task<object> jitenonTask = new JitenonScraper().ScrapeData(kanji);

            await Task.WhenAll(jishoTask, koohiiTask, jitenonTask);
            return BuildKanjiFromWebScrapedData(kanji, jishoTask.Result as JishoData, koohiiTask.Result as KoohiiData, jitenonTask.Result as JitenonData);
        }

        private static Kanji BuildKanjiFromWebScrapedData(string kanji, JishoData jishoData, KoohiiData koohiiData, JitenonData jitenonData) {
            ICollection<Word> meanings = new List<Word>();
            ICollection<Word> kunReadings = new List<Word>();
            ICollection<Word> onReadings = new List<Word>();
            ICollection<Word> parts = new List<Word>();

            jishoData.Meanings.ForEach(meaning => meanings.Add(new Word() { Text = meaning }));
            jishoData.OnReadings.ForEach(onReading => kunReadings.Add(new Word() { Text = onReading }));
            jishoData.KunReadings.ForEach(kunReading => onReadings.Add(new Word() { Text = kunReading }));
            jitenonData.Parts.ForEach(part => parts.Add(new Word() { Text = part }));

            return new Kanji() {
                Symbol = kanji,
                HeisingID = koohiiData.HeisingId,
                HeisingMeaning = koohiiData.HeisingMeaning,
                Meanings = meanings,
                KunReadings = kunReadings,
                OnReadings = onReadings,
                Parts = parts
            }; ;
        }
    }
}