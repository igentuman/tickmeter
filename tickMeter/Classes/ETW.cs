using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;

namespace tickMeter.Classes
{
    public class ETW
    {
        public class ProcessNetworkData
        {
            public string pName;
            public int pId;
            public string toIp;
            public string fromIp;
            public uint toPort;
            public uint fromPort;

            public ProcessNetworkData(string name, int pId, string toIp, string fromIp, uint toPort, uint fromPort)
            {
                this.pName = name;
                this.pId = pId;
                this.toIp = toIp;
                this.fromIp = fromIp;
                this.toPort = toPort;
                this.fromPort = fromPort;
            }

            public static string Hash(string name, int pId, string toIp, string fromIp, int toPort, int fromPort)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = new UTF8Encoding().GetBytes(name + pId.ToString() + toIp + fromIp + toPort.ToString() + fromPort.ToString());
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
                }
            }

            internal static void processEventData(string processName, int processID, string saddr, string daddr, int sport, int dport)
            {
                string hash = ProcessNetworkData.Hash(processName, processID, saddr, daddr, sport, dport);
                if (
                    processes.ContainsKey(hash) && (
                        processes[hash].pName != processName
                        || processes[hash].toIp != daddr
                        || processes[hash].fromIp != saddr
                        || processes[hash].toPort != dport
                        || processes[hash].fromPort != sport
                    )
                )
                {
                    processes.Remove(hash);
                }
                if (!processes.ContainsKey(hash))
                {
                    processes.Add(hash, new ProcessNetworkData(processName, processID, saddr, daddr, (uint)sport, (uint)dport));
                }
            }
        }

        public static Dictionary<string, ProcessNetworkData> processes = new Dictionary<string, ProcessNetworkData>();
        public static async void init()
        {
            await Task.Run(() =>
            {
                using (var kernelSession = new TraceEventSession(KernelTraceEventParser.KernelSessionName))
                {
                    kernelSession.EnableKernelProvider(KernelTraceEventParser.Keywords.NetworkTCPIP);


                    kernelSession.Source.Kernel.TcpIpAccept += acceptTCPIP;
                    kernelSession.Source.Kernel.TcpIpPartACK += ackTCPIP;
                    kernelSession.Source.Kernel.TcpIpFullACK += ackTCPIP;
                    kernelSession.Source.Kernel.TcpIpDupACK += ackTCPIP;
                    kernelSession.Source.Kernel.TcpIpConnect += acceptTCPIP;
                    kernelSession.Source.Kernel.TcpIpReconnect += tcpIpTrace;
                    kernelSession.Source.Kernel.TcpIpConnectIPV6 += acceptTCPIPv6;
                    kernelSession.Source.Kernel.TcpIpSendIPV6 += sendTCPIPv6;
                    kernelSession.Source.Kernel.TcpIpRecvIPV6 += recvTCPIPv6;
                    kernelSession.Source.Kernel.TcpIpAcceptIPV6 += acceptTCPIPv6;
                    kernelSession.Source.Kernel.TcpIpSend += tcpIpSend;
                    kernelSession.Source.Kernel.TcpIpRecv += tcpIpTrace;
                    kernelSession.Source.Kernel.UdpIpSend += udpSendTrace;
                    kernelSession.Source.Kernel.UdpIpRecv += udpSendTrace;

                    kernelSession.Source.Process();
                }
            });
        }

        private static void ackTCPIP(TcpIpTraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }

        private static void recvTCPIPv6(TcpIpV6TraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }

        private static void sendTCPIPv6(TcpIpV6SendTraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }


        private static void acceptTCPIPv6(TcpIpV6ConnectTraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }


        private static void udpSendTrace(UdpIpTraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }


        private static void acceptTCPIP(TcpIpConnectTraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }

        private static void tcpIpTrace(TcpIpTraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }

        private static void tcpIpSend(TcpIpSendTraceData session)
        {
            ProcessNetworkData.processEventData(session.ProcessName, session.ProcessID, session.saddr.ToString(), session.daddr.ToString(), session.sport, session.dport);
        }

        public static string resolveProcessname(string fromIp, string toIp, uint fromPort, uint toPort)
        {
            try
            {
                foreach (ProcessNetworkData procData in processes.Values)
                {
                    if(procData == null) continue;
                    if (
                            (procData.toPort == toPort
                        && procData.fromPort == fromPort
                        && procData.toIp == toIp
                        && procData.fromIp == fromIp)
                        ||
                        (procData.toPort == fromPort
                        && procData.fromPort == toPort
                        && procData.toIp == fromIp
                        && procData.fromIp == toIp))
                    {
                        return procData.pName;
                    }
                }
            } catch { }
            return @"n\a";
        }
    }
}
