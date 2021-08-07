using System.Diagnostics;
using System.IO;

namespace ScriptExecutor.Executors {
    public class PythonScriptExecutor : IScriptExecutor {
        private readonly string scriptPath;
        
        public PythonScriptExecutor(string scriptPath) {
            this.scriptPath = scriptPath;
        }

        public string ExecuteScript(string args) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Constants.PythonExePath;
            startInfo.Arguments = $"{scriptPath} {args}";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            using (Process process = Process.Start(startInfo)) 
            using (StreamReader reader = process.StandardOutput) {
                return reader.ReadToEnd();
            }
        }
    }
}