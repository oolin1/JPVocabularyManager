using System.Collections.Generic;

namespace DatabaseHandler {
    public class Kanji {
        public string Text { get; set; }
        public int HeisingID { get; private set; }
        public List<string> Meanings { get; private set; }
        public string HeisingMeaning { get; private set; }
        public List<string> KunReadings { get; private set; }
        public List<string> OnReadings { get; private set; }

        public Kanji(string text, int heisingId, List<string> meanings, string heisingMeaning, List<string> kunReadings, List<string> onReadings) {
            Text = text;
            HeisingID = heisingId;
            Meanings = meanings;
            HeisingMeaning = heisingMeaning;
            KunReadings = kunReadings;
            OnReadings = onReadings;
        }
    }
}