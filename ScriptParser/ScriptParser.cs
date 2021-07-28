using System.Diagnostics;
using System.IO;

namespace ScriptParsers {
    public class ScriptParser {
        private readonly string scriptPath;

        public ScriptParser(string scriptPath) {
            this.scriptPath = scriptPath;
        }

        public string RunScript(string args) {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = Constants.PythonExePath;
            start.Arguments = $"{scriptPath} {args}";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            using (Process process = Process.Start(start)) 
            using (StreamReader reader = process.StandardOutput) {
                return reader.ReadToEnd();
            }
            
        }
    }
}