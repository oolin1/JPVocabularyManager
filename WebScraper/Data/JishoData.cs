using System.Collections.Generic;

namespace WebScraper.Data {
    public class JishoData {
        public List<string> Meanings { get; private set; }
        public List<string> KunReadings { get; private set; }
        public List<string> OnReadings { get; private set; }

        public JishoData(List<string> meanings, List<string> kunReadings, List<string> onReadings) {
            Meanings = meanings;
            KunReadings = kunReadings;
            OnReadings = onReadings;
        }
    }
}