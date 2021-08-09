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
            if (jishoData == null || koohiiData == null) {
                return null;
            }

            ICollection<Meaning> meanings = new List<Meaning>();
            ICollection<KunReading> kunReadings = new List<KunReading>();
            ICollection<OnReading> onReadings = new List<OnReading>();
            ICollection<KanjiPart> parts = new List<KanjiPart>();

            jishoData.Meanings.ForEach(meaning => meanings.Add(new Meaning() { Word = meaning }));
            jishoData.KunReadings.ForEach(kunReading => kunReadings.Add(new KunReading() { Reading = kunReading }));
            jishoData.OnReadings.ForEach(onReading => onReadings.Add(new OnReading() { Reading = onReading }));
            jitenonData.Parts.ForEach(part => parts.Add(new KanjiPart() { Part = part }));

            return new Kanji() {
                Symbol = kanji,
                HeisingID = koohiiData.HeisingId,
                HeisingMeaning = koohiiData.HeisingMeaning,
                Meanings = meanings,
                KunReadings = kunReadings,
                OnReadings = onReadings,
                Parts = parts
            };
        }
    }
}