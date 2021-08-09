using System.Collections.Generic;
using System.Web;
using WebScraper.Data;

namespace WebScraper.WebScrapers {
    public class KoohiiScraper : AbstractScraper {
        public KoohiiScraper() {
            scriptPath = Constants.KoohiiScriptPath;
        }

        protected override string[] BuildArguments(string kanji) {
            string[] args = { HttpUtility.UrlEncode(kanji), Constants.KoohiiUserName, Constants.KoohiiPassword };
            return args;
        }

        protected override object BuildResult(List<string> parsedRows) {
            string heisingMeaning = parsedRows[0];
            int heisingId = int.Parse(parsedRows[1]);

            return new KoohiiData(heisingId, heisingMeaning);
        }
    }
}