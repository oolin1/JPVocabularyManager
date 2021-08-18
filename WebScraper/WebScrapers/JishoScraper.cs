using System.Collections.Generic;
using System.Linq;
using WebScraper.Data;

namespace WebScraper.WebScrapers {
    public class JishoScraper : AbstractScraper {
        public JishoScraper() {
            scriptPath = Constants.JishoScriptPath;
        }

        protected override object BuildResult(List<string> parsedRows) {
            if (parsedRows.Count == 0 || string.IsNullOrEmpty(parsedRows[0])) {
                return null;
            }

            List<string> meanings = BuildList(parsedRows[0], ", ");
            List<string> kunReadings = BuildList(parsedRows[1], "、"); 
            List<string> onReadings = BuildList(parsedRows[2], "、"); 

            return new JishoData(meanings, kunReadings, onReadings);
        }

        private List<string> BuildList(string parsedRow, string separator) {
            return string.IsNullOrEmpty(parsedRow) ? null : parsedRow.Split(separator).ToList();
        }
    }
}