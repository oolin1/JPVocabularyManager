using System.Collections.Generic;
using WebScraper.Data;

namespace WebScraper.WebScrapers {
    public class JitenonScraper : AbstractScraper {
        public JitenonScraper() {
            scriptPath = Constants.JitenonScriptPath;
        }

        protected override object BuildResult(List<string> parsedRows) {
            return new JitenonData(parsedRows);
        }
    }
}