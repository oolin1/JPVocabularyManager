using ScriptExecutor.Executors;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebScraper.Data;

namespace WebScraper.WebScrapers {
    public class JishoScraper {
        public JishoData ScrapeJishoData(string kanji) {
            PythonScriptExecutor scriptParser = new PythonScriptExecutor(Constants.JishoScriptPath);
            string args = HttpUtility.UrlEncode(kanji);

            return ParseScriptOutput(scriptParser.ExecuteScript(args));
        }

        private JishoData ParseScriptOutput(string scriptOutput) {
            List<string> rows = scriptOutput.Split("\n").Where(x => x != "").ToList();
            for (int i = 0; i < rows.Count(); i++) {
                rows[i] = new Regex("[\n\r]").Replace(HttpUtility.UrlDecode(rows[i]), "");
            }

            List<string> meanings = rows[0].Split(", ").ToList();
            List<string> kunReadings = rows[1].Split("、 ").ToList();
            List<string> onReadings = rows[2].Split("、 ").ToList();

            return new JishoData(meanings, kunReadings, onReadings);
        }
    }
}
