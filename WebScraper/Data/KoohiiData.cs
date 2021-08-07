namespace WebScraper.Data {
    public class KoohiiData {
        public int HeisingId { get; private set; }
        public string HeisingMeaning { get; private set; }

        public KoohiiData(int heisingId, string heisingMeaning) {
            HeisingId = heisingId;
            HeisingMeaning = heisingMeaning;
        }
    }
}