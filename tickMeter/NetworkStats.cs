using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tickMeter
{
    class NetworkStats
    {
        //source https://gist.github.com/cheynewallace/5971686

        public static List<Port> GetNetStatPorts(string ProcessNameFilter = "", string ProtocolFilter = "")
        {
            var Ports = new List<Port>();

            try
            {
                using (Process p = new Process())
                {

                    ProcessStartInfo ps = new ProcessStartInfo
                    {
                        Arguments = "-a -n -o",
                        FileName = "netstat.exe",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
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
                            string process_name = tokens[1] == "UDP" ? LookupProcess(Convert.ToInt16(tokens[4])) : LookupProcess(Convert.ToInt16(tokens[5]));

                            if(ProcessNameFilter != "" && ProcessNameFilter != process_name)
                            {
                                continue;
                            }
                            if (ProtocolFilter != "" && ProtocolFilter != protocol)
                            {
                                continue;
                            }
                            Ports.Add(new Port
                            {
                                Protocol = protocol,
                                PortNumber = port_number,
                                ProcessName = process_name,
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

        public static string LookupProcess(int pid)
        {
            string procName;
            try { procName = Process.GetProcessById(pid).ProcessName; }
            catch (Exception) { procName = "-"; }
            return procName;
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
            public string PortNumber { get; set; }
            public string ProcessName { get; set; }
            public string Protocol { get; set; }
            public string RawData { get; set; }
        }
    }
}
