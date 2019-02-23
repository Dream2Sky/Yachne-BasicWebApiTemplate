using System;
using System.Diagnostics;

namespace Yachne.Terminal
{
    public class TerminalProvider : ITerminalProvider
    {
        public string ExecuteCommand(string command, params string[] args)
        {
            var processInfo = new ProcessStartInfo(command, string.Join(',', args))
            {
                RedirectStandardOutput = true
            };

            var currentProc = Process.Start(processInfo);
            if (currentProc == null)
            {
                return null;
            }
            var result = string.Empty;
            using (var output = currentProc.StandardOutput)
            {
                while (!output.EndOfStream)
                {
                    result += output.ReadLine();
                }

                if (!currentProc.HasExited)
                {
                    currentProc.Kill();
                }
            }

            return result;
        }
    }
}
