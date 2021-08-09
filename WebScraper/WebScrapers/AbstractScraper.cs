using ScriptExecutor.Executors;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace WebScraper.WebScrapers {
    public abstract class AbstractScraper {
        protected string scriptPath;

        public async Task<object> ScrapeData(string kanji) {
            PythonScriptExecutor scriptExecutor = new PythonScriptExecutor(scriptPath);
            string[] args = BuildArguments(kanji);

            string scriptOutput = await scriptExecutor.ExecuteScript(args);
            return ParseScriptOutput(scriptOutput);
        }

        protected virtual string[] BuildArguments(string kanji) {
            string[] args = { HttpUtility.UrlEncode(kanji) };
            return args;
        }

        protected abstract object BuildResult(List<string> parsedRows);

        private object ParseScriptOutput(string scriptOutput) {
            List<string> rows = scriptOutput.Split("\n").Where(x => x != "").ToList();
            for (int i = 0; i < rows.Count(); i++) {
                rows[i] = new Regex("[\n\r]").Replace(HttpUtility.UrlDecode(rows[i]), "");
            }

            return BuildResult(rows);
        }
    }
}