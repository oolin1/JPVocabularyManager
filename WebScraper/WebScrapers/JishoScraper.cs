using System.Collections.Generic;
using System.Linq;
using WebScraper.Data;

namespace WebScraper.WebScrapers {
    public class JishoScraper : AbstractScraper {
        public JishoScraper() {
            scriptPath = Constants.JishoScriptPath;
        }

        protected override object BuildResult(List<string> parsedRows) {
            List<string> meanings = parsedRows[0].Split(", ").ToList();
            List<string> kunReadings = parsedRows[1].Split("、 ").ToList();
            List<string> onReadings = parsedRows[2].Split("、 ").ToList();

            return new JishoData(meanings, kunReadings, onReadings);
        }
    }
}