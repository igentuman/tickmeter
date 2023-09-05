using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tickMeter.Classes
{

    public class ProcessNetInstance
    {
        public string processName;
        public int processID;
        public DateTime startTrack;
        public int ticksIn;
        public int ticksOut;
        public DateTime lastUpdate;
        public int remotePort;
        public int localPort;
        public string remoteIP;
        public string protocol;

        public int TrackingDelta()
        {
            return (int)(TimeSpan.FromTicks(DateTime.Now.Ticks - startTrack.Ticks).TotalSeconds);
        }

        public int LastUpdateDelta()
        {
            return (int)(TimeSpan.FromTicks(DateTime.Now.Ticks - lastUpdate.Ticks).TotalSeconds);
        }
    }

    public static class AutoDetectMngr
    {

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        internal enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }

        private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);


        public static PacketFilter profileFilter = new PacketFilter();
        public static string GameName;
        public static string RequireProcess;
        public static List<ProcessNetInstance> activeProcesses = new List<ProcessNetInstance>();

        public static void AnalyzePacket(Packet packet)
        {
            if (!IsEnabled()) return;
            IpV4Datagram ip;
            try
            {
                ip = packet.Ethernet.IpV4;
            }
            catch (Exception) { return; }

            UdpDatagram udp = ip.Udp;
            TcpDatagram tcp = ip.Tcp;

            string from_ip = ip.Source.ToString();
            string to_ip = ip.Destination.ToString();

            string packet_size = ip.TotalLength.ToString();

            string protocol = ip.Protocol.ToString();
            string from_port = "";
            string to_port = "";
            string processName = @"n\a";
            if (protocol == IpV4Protocol.Udp.ToString())
            {
                from_port = udp.SourcePort.ToString();
                to_port = udp.DestinationPort.ToString();
                try
                {
                    UdpProcessRecord record;
                    List<UdpProcessRecord> UdpConnections = App.connMngr.UdpActiveConnections;
                    if (UdpConnections.Count > 0)
                    {
                        if (from_ip == App.meterState.LocalIP)
                        {
                            record = UdpConnections.Where(procReq => procReq.LocalPort.ToString() == from_port).First();
                        }
                        else
                        {
                            record = UdpConnections.Where(procReq => procReq.LocalPort.ToString() == to_port).First();
                        }

                        if (record != null)
                        {
                            processName = record.ProcessName != null ? record.ProcessName : record.ProcessId.ToString();
                            
                        }
                    }
                }
                catch (Exception) { processName = @"n\a"; }

            }
            else
            {
                try
                {
                    TcpProcessRecord record;
                    List<TcpProcessRecord> TcpConnections = App.connMngr.TcpActiveConnections;
                    if (TcpConnections.Count > 0)
                    {
                        if (from_ip == App.meterState.LocalIP)
                        {
                            record = TcpConnections.Where(procReq => procReq.LocalPort.ToString() == from_port && procReq.RemotePort.ToString() == to_port).First();
                        }
                        else
                        {
                            record = TcpConnections.Where(procReq => procReq.LocalPort.ToString() == to_port && procReq.RemotePort.ToString() == from_port).First();
                        }
                        if (record != null)
                        {
                            processName = record.ProcessName;
                        }
                    }

                }
                catch (Exception) {processName = @"n\a"; }
                from_port = tcp.SourcePort.ToString();
                to_port = tcp.DestinationPort.ToString();
            }

            int i = -1;
            if (activeProcesses.Count > 0)
                for (i = 0; i < activeProcesses.Count; i++)
                {
                    if (activeProcesses[i].processName == processName)
                    {
                        break;
                    }
                }

            if (i == -1 && processName != @"n\a")
            {
                ProcessNetInstance tmpProc = new ProcessNetInstance()
                {
                    processName = processName,
                    remoteIP = to_ip != App.meterState.LocalIP ? to_ip : from_ip,
                    remotePort = to_ip != App.meterState.LocalIP ? int.Parse(to_port) : int.Parse(from_port),
                    localPort = to_ip == App.meterState.LocalIP ? int.Parse(to_port) : int.Parse(from_port),
                    startTrack = DateTime.Now,
                    protocol = protocol,

                };
                activeProcesses.Add(tmpProc);
                i = activeProcesses.Count - 1;
            }
            if (i > -1 && activeProcesses.Count > i)
            {
                if (to_ip == App.meterState.LocalIP)
                {
                    activeProcesses[i].ticksIn++;
                }
                else
                {
                    activeProcesses[i].ticksOut++;
                }
                activeProcesses[i].lastUpdate = DateTime.Now;
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static string activeProcess = "";
        public static string GetActiveProcessName(Boolean refresh = false)
        {
            if(!refresh) return activeProcess;
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            activeProcess = p.ProcessName != null ? p.ProcessName : pid.ToString();
            return activeProcess;
        }

        public static bool IsEnabled()
        {
            return App.settingsManager.GetOption("autodetect") == "True";
        }

        public static void InitFilter(ProcessNetInstance proc)
        {
            profileFilter.DestIpFilter = App.meterState.LocalIP;
            profileFilter.DestPortFilter = proc.localPort.ToString();
            profileFilter.SourceIpFilter = proc.remoteIP;
            profileFilter.SourcePortFilter = proc.remotePort.ToString();
            profileFilter.ProcessFilter = proc.processName;
            profileFilter.ProtocolFilter = proc.protocol;
            RequireProcess = proc.processName;
        }

        public static bool ValidateProc(Packet packet, ProcessNetInstance proc)
        {
            InitFilter(proc);
            if (profileFilter.Validate() && packet.Ethernet.IpV4.Destination.ToString() == App.meterState.LocalIP)
            {
                App.meterState.updateTicktimeBuffer(packet.Timestamp.Ticks);
                App.meterState.CurrentTimestamp = packet.Timestamp;
                App.meterState.Game = profileFilter.ProcessFilter;
                App.meterState.Server.Ip = packet.Ethernet.IpV4.Source.ToString();
                App.meterState.DownloadTraffic += packet.Ethernet.IpV4.TotalLength;
                App.meterState.TickRate++;
                App.meterState.Server.PingPort = packet.Ethernet.IpV4.Udp.SourcePort;
                App.meterState.IsTracking = true;
                return true;
            }

            // validate packet sending
            if (profileFilter.ValidateForOutputPacket())
            {
                if (packet.Ethernet.IpV4.Source.ToString() == App.meterState.LocalIP)
                {
                    App.meterState.UploadTraffic += packet.Ethernet.IpV4.TotalLength;
                    return true;
                }
            }
            return false;
        }

        public static void ProcessPacket(Packet packet)
        {
            if (!IsEnabled()) return;
            string pName = GetActiveProcessName();
            if (GetActiveProcesses().Count() == 0) return;
            if (App.meterState.Game != "" && GetActiveProcesses().Where(process => App.meterState.Game == process.processName).Count() == 0) return;
            profileFilter.ip = packet.Ethernet.IpV4;
            if (GetActiveProcesses().Where(process => pName == process.processName).Count() > 0)
            {
                ProcessNetInstance activeProc = GetActiveProcesses().Where(process => pName == process.processName).FirstOrDefault();
                if (ValidateProc(packet, activeProc)) return;
            }

            foreach(ProcessNetInstance proc in GetActiveProcesses())
            {
                if (ValidateProc(packet, proc)) return;
            }

        }

        public static List<ProcessNetInstance> GetActiveProcesses()
        {
            List<ProcessNetInstance> tmpList = new List<ProcessNetInstance>();
            foreach (var procNet in activeProcesses)
            {
                if (procNet.TrackingDelta() == 0) continue;
                if (procNet.ticksIn / procNet.TrackingDelta() > 10 && procNet.ticksOut / procNet.TrackingDelta() > 10 && procNet.TrackingDelta() > 3)
                {
                    tmpList.Add(procNet);
                }
            }
            return tmpList;
        }

        public static List<ListViewItem> GetActiveProccessesList(List<ListViewItem> procItems)
        {
            procItems.Clear();
            int indx = 0;
            List<ProcessNetInstance> tmpList = activeProcesses.ToList();
            foreach (var procNet in tmpList)
            {

                if (procNet.TrackingDelta() == 0) continue;
                if (procNet.ticksIn / procNet.TrackingDelta() > 10 && procNet.ticksOut / procNet.TrackingDelta() > 10 && procNet.TrackingDelta() > 3)
                {
                    ListViewItem procItem = new ListViewItem(procNet.remoteIP);
                    procItem.SubItems.Add(procNet.remotePort.ToString());
                    procItem.SubItems.Add(procNet.localPort.ToString());
                    procItem.SubItems.Add((procNet.ticksIn / procNet.TrackingDelta()).ToString());
                    procItem.SubItems.Add((procNet.ticksOut / procNet.TrackingDelta()).ToString());
                    procItem.SubItems.Add(procNet.protocol.ToString());
                    procItem.SubItems.Add(procNet.processName);
                    procItems.Add(procItem);
                }
                else
                {
                    if (procNet.TrackingDelta() > 3 && procNet.ticksIn / procNet.TrackingDelta() < 10 && procNet.ticksOut / procNet.TrackingDelta() < 10)
                    {
                        activeProcesses.RemoveAt(indx);
                    }
                }
                if(procNet.LastUpdateDelta() > 3 && activeProcesses.Count-1 >= indx)
                {
                    activeProcesses.RemoveAt(indx);
                }
                indx++;
            }
            return procItems;
        }

    
    }
}
