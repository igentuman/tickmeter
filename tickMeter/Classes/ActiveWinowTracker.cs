using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tickMeter.Classes
{

    public static class ActiveWindowTracker
    {
       
        public static Dictionary<string, ProcessNetworkStats> connections = new Dictionary<string, ProcessNetworkStats>();

        public static void trackTick(string name, string localIp, uint localPort, string remoteIp, uint remotePort, int tickIn, int tickOut, uint traffic, DateTime tickTime)
        {
            string hash = Hash(name, remoteIp, remotePort);
            if (!connections.ContainsKey(hash)) { 
                connections.Add(hash, new ProcessNetworkStats());
                connections[hash].name = name;
                connections[hash].localIp = localIp;
                connections[hash].remoteIp = remoteIp;
                connections[hash].localPort = localPort;
                connections[hash].remotePort = remotePort;
                connections[hash].downloaded = 0;
                connections[hash].sent = 0;
                connections[hash].ticksIn = 0;
                connections[hash].ticksOut = 0;
                connections[hash].startTrack = tickTime;
            }
            connections[hash].ticksIn +=tickIn;
            connections[hash].ticksOut+=tickOut;
            if (tickIn > 0)
            {
                connections[hash].updateTicktimeBuffer(tickTime.Ticks);
                connections[hash].lastUpdate = tickTime;
                connections[hash].downloaded += (int)traffic;
            }
            if(tickOut > 0)
            {
                connections[hash].sent += (int)traffic;
            }
        }

        public static string Hash(string game, string from_ip, uint from_port)
        {
            return game+from_ip+from_port.ToString();
        }

        public static void AnalyzePacket(Packet packet)
        {
            if(App.meterState.isBuiltInProfileActive ||  App.meterState.isCustomProfileActive) { return; }
            if (!IsEnabled()) return;
            IpV4Datagram ip;
            try
            {
                ip = packet.Ethernet.IpV4;
            }
            catch (Exception) { return; }

            UdpDatagram udp = ip.Udp;
            TcpDatagram tcp = ip.Tcp;

            if (udp == null && tcp == null) return;

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
                        record = UdpConnections.Find(procReq => procReq.LocalPort == fromPort || procReq.LocalPort == toPort);

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
                       record = TcpConnections.Find(procReq => 
                       (procReq.LocalPort == fromPort && procReq.RemotePort == toPort)
                       || (procReq.LocalPort == fromPort && procReq.RemotePort == toPort)
                       );
                        if (record != null)
                        {
                            processName = record.ProcessName;
                        }
                    }
                }
                catch (InvalidOperationException) {processName = @"n\a"; }
            }

            if (processName == @"n\a")
            {
                processName = ETW.resolveProcessname(fromIp, toIp, fromPort, toPort);
            }
            string activeProcess = AutoDetectMngr.GetActiveProcessName();
            if(activeProcess != processName) { return; }
            uint remotePort = 0;
            uint localPort = 0;
            if (App.meterState.LocalIP == toIp.ToString())
            {
                switch(protocol.ToLower())
                {
                    case "udp":
                        remotePort = udp.SourcePort;
                        localPort = udp.DestinationPort;
                        break;
                    case "tcp":
                        remotePort = tcp.SourcePort;
                        localPort = tcp.DestinationPort;
                        break;
                }
                trackTick(processName, App.meterState.LocalIP, localPort, ip.Source.ToString(), remotePort, 1, 0, packetSize, packet.Timestamp);
            } else
            {
                switch (protocol.ToLower())
                {
                    case "udp":
                        remotePort = udp.DestinationPort;
                        localPort = udp.SourcePort;
                        break;
                    case "tcp":
                        remotePort = tcp.DestinationPort;
                        localPort = tcp.SourcePort;
                        break;
                }
                trackTick(processName, App.meterState.LocalIP, localPort, ip.Source.ToString(), remotePort, 0, 1, packetSize, packet.Timestamp);
            }
        }

        public static bool IsEnabled()
        {
            return App.settingsManager.GetOption("autodetect") == "True";
        }
    }
}
