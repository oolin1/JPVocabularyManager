using ScriptExecutor.Executors;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebScraper.Data;

namespace WebScraper.WebScrapers {
    public class JitenonScraper {
        public JitenonData ScrapeData(string kanji) {
            PythonScriptExecutor scriptExecutor = new PythonScriptExecutor(Constants.JitenonScriptPath);
            string arg = HttpUtility.UrlEncode(kanji);

            return ParseScriptOutput(scriptExecutor.ExecuteScript(arg));
        }

        private JitenonData ParseScriptOutput(string scriptOutput) {
            List<string> rows = scriptOutput.Split("\n").Where(x => x != "").ToList();
            for (int i = 0; i < rows.Count(); i++) {
                rows[i] = HttpUtility.UrlDecode(new Regex("[\r]").Replace(rows[i], ""));
            }

            return new JitenonData(rows);
        }
    }
}
