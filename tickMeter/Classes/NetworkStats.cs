using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tickMeter.Classes
{
    //initial source https://gist.github.com/cheynewallace/5971686
    public class NetworkStats
    {
        private System.Timers.Timer MngrTimer;
        public Process[] ProcessInfoList;
        public List<Port> ActivePorts = new List<Port>();

        public NetworkStats()
        {
            SetConnectionsManagerTimer();
        }
        

        private void SetConnectionsManagerTimer()
        {
            if (MngrTimer == null)
            {
                MngrTimer = new System.Timers.Timer
                {
                    Interval = 5000
                };
                MngrTimer.Elapsed += MngrTimerTick;
                MngrTimer.AutoReset = true;
                MngrTimer.Enabled = true;
            }
        }

        private async void MngrTimerTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            await Task.Run(() =>
            {
                ActivePorts = GetNetStatPorts();
                ProcessInfoList = Process.GetProcesses();
                Process procc;
                for (var i = 0; i < ActivePorts.Count; i++)
                {
                    procc = ProcessInfoList.Where(process => ActivePorts[i].ProcessId == process.Id).First();
                    if (procc != null)
                    {
                        ActivePorts[i].ProcessName = procc.ProcessName;
                    }
                }
            });

        }

        public static List<Port> GetNetStatPorts(string ProtocolFilter = "")
        {
            var Ports = new List<Port>();

            try
            {
                using (Process p = new Process())
                {

                    ProcessStartInfo ps = new ProcessStartInfo
                    {
                        Arguments = "-ano",
                        FileName = "netstat.exe",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    p.StartInfo = ps;
                    p.Start();

                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;

                    string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
                    string exitStatus = p.ExitCode.ToString();

                    if (exitStatus != "0")
                    {
                        // Command Errored. Handle Here If Need Be
                        return new List<Port>();
                    }

                    //Get The Rows
                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        //Split it baby
                        string[] tokens = Regex.Split(row, "\\s+");
                        if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP")))
                        {
                            string localAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");
                            string protocol = localAddress.Contains("1.1.1.1") ? String.Format("{0}v6", tokens[1]) : String.Format("{0}v4", tokens[1]);
                            string port_number = localAddress.Split(':')[1];

                            if (ProtocolFilter != "" && ProtocolFilter != protocol)
                            {
                                continue;
                            }
                            Ports.Add(new Port
                            {
                                Protocol = protocol,
                                PortNumber = port_number,
                                ProcessId = tokens[1] == "UDP" ? Convert.ToInt16(tokens[4]) : Convert.ToInt16(tokens[5]),
                                RawData = row
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<Port>();
            }
            return Ports;
        }

        public class Port
        {
            public string Name
            {
                get
                {
                    return string.Format("{0} ({1} port {2})", ProcessName, Protocol, PortNumber);
                }
                set { }
            }
            public int ProcessId { get; set; }
            public string PortNumber { get; set; }
            public string ProcessName { get; set; }
            public string Protocol { get; set; }
            public string RawData { get; set; }
        }
    }
}
