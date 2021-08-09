using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutor.Executors {
    public class PythonScriptExecutor {
        private readonly string scriptPath;
        
        public PythonScriptExecutor(string scriptPath) {
            this.scriptPath = scriptPath;
        }

        public async Task<string> ExecuteScript(params string[] args) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Constants.PythonExePath;
            startInfo.Arguments = $"{scriptPath} {ArgumentBuilder(args)}";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            using (Process process = Process.Start(startInfo)) 
            using (StreamReader reader = process.StandardOutput) {
                return await reader.ReadToEndAsync();
            }
        }

        private string ArgumentBuilder(string[] args) {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string arg in args) {
                stringBuilder.Append($"{arg} ");
            }

            return stringBuilder.ToString();
        }
    }
}