using System;
using System.Collections.Generic;
using System.Linq;
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
            public string in_ip;
            public string out_ip;
            public int in_port;
            public int out_port;

            public ProcessNetworkData(string name, int pId, string in_ip, string out_ip, int in_port, int out_port)
            {
                this.pName = name;
                this.pId = pId;
                this.in_ip = in_ip;
                this.out_ip = out_ip;
                this.in_port = in_port;
                this.out_port = out_port;
            }

            public static string Hash(string name, int pId, string in_ip, string out_ip, int in_port, int out_port)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = new UTF8Encoding().GetBytes(name + pId.ToString() + in_ip + out_ip + in_port.ToString() + out_port.ToString());
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
                        || processes[hash].in_ip != daddr
                        || processes[hash].out_ip != saddr
                        || processes[hash].in_port != dport
                        || processes[hash].out_port != sport
                    )
                )
                {
                    processes.Remove(hash);
                }
                if (!processes.ContainsKey(hash))
                {
                    processes.Add(hash, new ProcessNetworkData(processName, processID, saddr, daddr, sport, dport));
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

        public static string resolveProcessname(string from_ip, string to_ip, string from_port, string to_port)
        {
            ProcessNetworkData data = null;
            try
            {
                data = processes.First(procData =>
                        (procData.Value.in_port.ToString() == to_port
                        && procData.Value.out_port.ToString() == from_port
                        && procData.Value.in_ip == to_ip
                        && procData.Value.out_ip == from_ip)
                        ||
                        (procData.Value.in_port.ToString() == from_port
                        && procData.Value.out_port.ToString() == to_port
                        && procData.Value.in_ip == from_ip
                        && procData.Value.out_ip == to_ip)
                    ).Value;
            } catch (InvalidOperationException exception)
            { }
            if(data != null )
            {
                return data.pName;
            }
            return @"n\a";
        }
    }
}
