using ScriptExecutor.Executors;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebScraper.Data;

namespace WebScraper.WebScrapers {
    public class KoohiiScraper { 
        public KoohiiData ScrapeData(string kanji) {
            PythonScriptExecutor scriptExecutor = new PythonScriptExecutor(Constants.KoohiiScriptPath);
            string[] args = { HttpUtility.UrlEncode(kanji), Constants.KoohiiUserName, Constants.KoohiiPassword };

            return ParseScriptOutput(scriptExecutor.ExecuteScript(args));
        }

        private KoohiiData ParseScriptOutput(string scriptOutput) {
            List<string> rows = scriptOutput.Split("\n").Where(x => x != "").ToList();
            for (int i = 0; i < rows.Count(); i++) {
                rows[i] = new Regex("[\r]").Replace(rows[i], "");
            }

            string heisingMeaning = rows[0];
            int heisingId = int.Parse(rows[1]);
            
            return new KoohiiData(heisingId, heisingMeaning);
        }
    }
}