using System;
using System.IO;
using System.Threading.Tasks;

namespace tickMeter.Classes
{
    public class DebugLogger
    {
        private DebugLogger() {
        }

        public static DebugLogger Instance { get; private set; } = new DebugLogger();
        public static void log(String message)
        {
            using (StreamWriter writer = File.AppendText("debug.log")) {
                writer.WriteLineAsync(DateTime.Now.ToLongTimeString() + ": " + message);
            }
        }
        public static async void log(String[] messages)
        {
            await Task.Run(() =>
            {
                foreach (String message in messages)
                {
                    log(message);
                }
            });
        }

        public static async void log(Exception ex)
        {
            await Task.Run(() =>
            {
                log(ex.Message);
                log(ex.StackTrace);
            });
        }
    }
}
