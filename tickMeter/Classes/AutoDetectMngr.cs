using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace tickMeter.Classes
{
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
        public static List<ProcessNetworkStats> activeProcesses = new List<ProcessNetworkStats>();

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

            string fromIp = ip.Source.ToString();
            string toIp = ip.Destination.ToString();

            uint packetSize = (uint)ip.TotalLength;

            string protocol = ip.Protocol.ToString();
            uint fromPort = 0;
            uint toPort = 0;
            string processName = @"n\a";
            if (protocol == IpV4Protocol.Udp.ToString())
            {
                fromPort = udp.SourcePort;
                toPort = udp.DestinationPort;
                try
                {
                    UdpProcessRecord record;
                    List<UdpProcessRecord> UdpConnections = App.connMngr.UdpActiveConnections;
                    if (UdpConnections.Count > 0)
                    {
                        record = UdpConnections.First(procReq => procReq.LocalPort== fromPort || procReq.LocalPort == toPort);

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
                fromPort = tcp.SourcePort;
                toPort = tcp.DestinationPort;
                try
                {
                    TcpProcessRecord record;
                    List<TcpProcessRecord> TcpConnections = App.connMngr.TcpActiveConnections;
                    if (TcpConnections.Count > 0)
                    {
                       record = TcpConnections.First(procReq => 
                       (procReq.LocalPort == fromPort && procReq.RemotePort == toPort)
                       || (procReq.LocalPort == fromPort && procReq.RemotePort == toPort)
                       );
                        
                        if (record != null)
                        {
                            processName = record.ProcessName;
                        }
                    }
                }
                catch (Exception) {processName = @"n\a"; }
            }

            if (processName == @"n\a")
            {
                processName = ETW.resolveProcessname(fromIp, toIp, fromPort, toPort);
            }


            int i = -1;
            if (activeProcesses.Count > 0)
                for (i = 0; i < activeProcesses.Count; i++)
                {
                    if (activeProcesses[i].name == processName)
                    {
                        break;
                    }
                }

            if (i == -1 && processName != @"n\a")
            {
                ProcessNetworkStats tmpProc = new ProcessNetworkStats()
                {
                    name = processName,
                    remoteIp = toIp != App.meterState.LocalIP ? toIp : fromIp,
                    remotePort = toIp != App.meterState.LocalIP ? toPort : fromPort,
                    localPort = toIp == App.meterState.LocalIP ? toPort : fromPort,
                    startTrack = DateTime.Now,
                    protocol = protocol,

                };
                activeProcesses.Add(tmpProc);
                i = activeProcesses.Count - 1;
            }
            if (i > -1 && activeProcesses.Count > i)
            {
                if (toIp == App.meterState.LocalIP)
                {
                    activeProcesses[i].ticksIn++;
                }
                else
                {
                    activeProcesses[i].ticksOut++;
                }
               // activeProcesses[i].lastUpdate = DateTime.Now;
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static string activeProcess = "";
        public static string GetActiveProcessName(Boolean refresh = false)
        {
            string newName = "";
            try
            {
                if (!refresh) return activeProcess;
                IntPtr hwnd = GetForegroundWindow();
                uint pid;
                GetWindowThreadProcessId(hwnd, out pid);
                Process p = Process.GetProcessById((int)pid);
                newName = p.ProcessName != null ? p.ProcessName : pid.ToString();
            } catch (Exception) { }
            if(activeProcess != newName)
            {
                App.gui.targetKey = "";
            }
            activeProcess = newName;
            return activeProcess;
        }

        public static bool IsEnabled()
        {
            return false;//temporary disable this
        }

        public static void InitFilter(ProcessNetworkStats proc)
        {
            profileFilter.DestIpFilter = App.meterState.LocalIP;
            profileFilter.DestPortFilter = proc.localPort.ToString();
            profileFilter.SourceIpFilter = proc.remoteIp;
            profileFilter.SourcePortFilter = proc.remotePort.ToString();
            profileFilter.ProcessFilter = proc.name;
            profileFilter.ProtocolFilter = proc.protocol;
            RequireProcess = proc.name;
        }

        public static bool ValidateProc(Packet packet, ProcessNetworkStats proc)
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
            List<ProcessNetworkStats> activeProcesses = GetActiveProcesses();
            string pName = GetActiveProcessName();
            if (activeProcesses.Count() == 0) return;
            if (App.meterState.Game != "" && activeProcesses.Where(process => App.meterState.Game == process.name).Count() == 0) return;
            profileFilter.ip = packet.Ethernet.IpV4;
            if (activeProcesses.Where(process => pName == process.name).Count() > 0)
            {
                ProcessNetworkStats activeProc = activeProcesses.First(process => pName == process.name);
                if (ValidateProc(packet, activeProc)) return;
            }

            foreach(ProcessNetworkStats proc in activeProcesses)
            {
                if (ValidateProc(packet, proc)) return;
            }

        }

        public static List<ProcessNetworkStats> GetActiveProcesses()
        {
            List<ProcessNetworkStats> tmpList = new List<ProcessNetworkStats>();
            foreach (var procNet in activeProcesses)
            {
                if (procNet.TrackingDelta() == 0) continue;
                if (procNet.ticksIn / procNet.TrackingDelta() > 2 && procNet.ticksOut / procNet.TrackingDelta() > 2 && procNet.TrackingDelta() > 3)
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
            List<ProcessNetworkStats> tmpList = activeProcesses.ToList();
            foreach (var procNet in tmpList)
            {

                if (procNet.TrackingDelta() == 0) continue;
                if (procNet.ticksIn / procNet.TrackingDelta() > 2 && procNet.ticksOut / procNet.TrackingDelta() > 2 && procNet.TrackingDelta() > 3)
                {
                    ListViewItem procItem = new ListViewItem(procNet.remoteIp);
                    procItem.SubItems.Add(procNet.remotePort.ToString());
                    procItem.SubItems.Add(procNet.localPort.ToString());
                    procItem.SubItems.Add((procNet.ticksIn / procNet.TrackingDelta()).ToString());
                    procItem.SubItems.Add((procNet.ticksOut / procNet.TrackingDelta()).ToString());
                    procItem.SubItems.Add(procNet.protocol.ToString());
                    procItem.SubItems.Add(procNet.name);
                    procItems.Add(procItem);
                }
                else
                {
                    if (procNet.TrackingDelta() > 3 && procNet.ticksIn / procNet.TrackingDelta() < 4 && procNet.ticksOut / procNet.TrackingDelta() < 4)
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
