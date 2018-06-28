using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tickMeter
{
    public class ConnectionsManager
    {
        //SOURCES
        // https://code.msdn.microsoft.com/windowsdesktop/C-Sample-to-list-all-the-4817b58f
        // https://www.codeproject.com/Articles/4298/Getting-active-TCP-UDP-connections-on-a-box

        // The version of IP used by the TCP/UDP endpoint. AF_INET is used for IPv4.
        private const int AF_INET = 2;
        public const string dllFile = "iphlpapi.dll";

        public List<TcpProcessRecord> TcpActiveConnections = new List<TcpProcessRecord>();

        public List<UdpProcessRecord> UdpActiveConnections = new List<UdpProcessRecord>();

        public Process[] ProcessInfoList;

        private System.Timers.Timer MngrTimer;
        public TickMeterState meterState;

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
            if (!meterState.ConnectionsManagerFlag) return;
            await Task.Run(() =>
            {
                ProcessInfoList = Process.GetProcesses();
                TcpActiveConnections = GetAllTcpConnections();
                UdpActiveConnections = GetAllUdpConnections();
                Process[] proccArray;
                for (var i = 0; i < TcpActiveConnections.Count; i++)
                {
                    proccArray = ProcessInfoList.Where(process => TcpActiveConnections[i].ProcessId == process.Id).ToArray();
                    if(proccArray.Length > 0)
                    {
                        TcpActiveConnections[i].ProcessName = proccArray.First().ProcessName;
                    }
                }

                for (var i = 0; i < UdpActiveConnections.Count; i++)
                {
                    proccArray = ProcessInfoList.Where(process => UdpActiveConnections[i].ProcessId == process.Id).ToArray();
                    if (proccArray.Length > 0)
                    {
                        UdpActiveConnections[i].ProcessName = proccArray.First().ProcessName;
                    }
                }
            });
        }

        public ConnectionsManager()
        {
            SetConnectionsManagerTimer();
        }

        [DllImport(dllFile, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize,
            bool bOrder, int ulAf, TcpTableClass tableClass, uint reserved = 0);


        [DllImport(dllFile, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize,
            bool bOrder, int ulAf, UdpTableClass tableClass, uint reserved = 0);
        

        public List<TcpProcessRecord> GetAllTcpConnections()
        {
            int bufferSize = 0;
            List<TcpProcessRecord> tcpTableRecords = new List<TcpProcessRecord>();
            
            uint result = GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, AF_INET,
                TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

            IntPtr tcpTableRecordsPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                result = GetExtendedTcpTable(tcpTableRecordsPtr, ref bufferSize, true,
                    AF_INET, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

                if (result != 0)
                    return new List<TcpProcessRecord>();

                MIB_TCPTABLE_OWNER_PID tcpRecordsTable = (MIB_TCPTABLE_OWNER_PID)
                                        Marshal.PtrToStructure(tcpTableRecordsPtr,
                                        typeof(MIB_TCPTABLE_OWNER_PID));
                IntPtr tableRowPtr = (IntPtr)((long)tcpTableRecordsPtr +
                                        Marshal.SizeOf(tcpRecordsTable.dwNumEntries));

            for (int row = 0; row < tcpRecordsTable.dwNumEntries; row++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.
                        PtrToStructure(tableRowPtr, typeof(MIB_TCPROW_OWNER_PID));

                tcpTableRecords.Add(new TcpProcessRecord(
                                            new IPAddress(tcpRow.localAddr),
                                            new IPAddress(tcpRow.remoteAddr),
                                            BitConverter.ToUInt16(new byte[2] {
                                            tcpRow.localPort[1],
                                            tcpRow.localPort[0] }, 0),
                                            BitConverter.ToUInt16(new byte[2] {
                                            tcpRow.remotePort[1],
                                            tcpRow.remotePort[0] }, 0),
                                            tcpRow.owningPid, tcpRow.state));
                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(tcpRow));
                }
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                MessageBox.Show(outOfMemoryException.Message, "Out Of Memory",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTableRecordsPtr);
            }
            return tcpTableRecords != null ? tcpTableRecords.Distinct()
                .ToList<TcpProcessRecord>() : new List<TcpProcessRecord>();
        }

        public List<UdpProcessRecord> GetAllUdpConnections()
        {
            int bufferSize = 0;
            List<UdpProcessRecord> udpTableRecords = new List<UdpProcessRecord>();

            uint result = GetExtendedUdpTable(IntPtr.Zero, ref bufferSize, true,
                AF_INET, UdpTableClass.UDP_TABLE_OWNER_PID);

            IntPtr udpTableRecordPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                result = GetExtendedUdpTable(udpTableRecordPtr, ref bufferSize, true,
                    AF_INET, UdpTableClass.UDP_TABLE_OWNER_PID);

                if (result != 0)
                    return new List<UdpProcessRecord>();
                MIB_UDPTABLE_OWNER_PID udpRecordsTable = (MIB_UDPTABLE_OWNER_PID)
                    Marshal.PtrToStructure(udpTableRecordPtr, typeof(MIB_UDPTABLE_OWNER_PID));

                IntPtr tableRowPtr = (IntPtr)((long)udpTableRecordPtr +
                    Marshal.SizeOf(udpRecordsTable.dwNumEntries));

                for (int i = 0; i < udpRecordsTable.dwNumEntries; i++)
                {

                    MIB_UDPROW_OWNER_PID udpRow = (MIB_UDPROW_OWNER_PID)
                        Marshal.PtrToStructure(tableRowPtr, typeof(MIB_UDPROW_OWNER_PID));
                    udpTableRecords.Add(new UdpProcessRecord(new IPAddress(udpRow.localAddr),
                        BitConverter.ToUInt16(new byte[2] { udpRow.localPort[1],
                            udpRow.localPort[0] }, 0), udpRow.owningPid));
                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(udpRow));
                }
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                MessageBox.Show(outOfMemoryException.Message, "Out Of Memory",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Marshal.FreeHGlobal(udpTableRecordPtr);
            }
            return udpTableRecords != null ? udpTableRecords.Distinct()
                .ToList<UdpProcessRecord>() : new List<UdpProcessRecord>();
        }
    }

        public enum Protocol
        {
            TCP,
            UDP
        }

        public enum TcpTableClass
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
        }

        public enum UdpTableClass
        {
            UDP_TABLE_BASIC,
            UDP_TABLE_OWNER_PID,
            UDP_TABLE_OWNER_MODULE
        }

        public enum MibTcpState
        {
            CLOSED = 1,
            LISTENING = 2,
            SYN_SENT = 3,
            SYN_RCVD = 4,
            ESTABLISHED = 5,
            FIN_WAIT1 = 6,
            FIN_WAIT2 = 7,
            CLOSE_WAIT = 8,
            CLOSING = 9,
            LAST_ACK = 10,
            TIME_WAIT = 11,
            DELETE_TCB = 12,
            NONE = 0
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public MibTcpState state;
            public uint localAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] localPort;
            public uint remoteAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] remotePort;
            public int owningPid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPTABLE_OWNER_PID
        {
            public uint dwNumEntries;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
            public MIB_TCPROW_OWNER_PID[] table;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TcpProcessRecord
        {
            [DisplayName("Local Address")]
            public IPAddress LocalAddress { get; set; }
            [DisplayName("Local Port")]
            public ushort LocalPort { get; set; }
            [DisplayName("Remote Address")]
            public IPAddress RemoteAddress { get; set; }
            [DisplayName("Remote Port")]
            public ushort RemotePort { get; set; }
            [DisplayName("State")]
            public MibTcpState State { get; set; }
            [DisplayName("Process ID")]
            public int ProcessId { get; set; }
            [DisplayName("Process Name")]
            public string ProcessName { get; set; }

            public TcpProcessRecord(IPAddress localIp, IPAddress remoteIp, ushort localPort,
                ushort remotePort, int pId, MibTcpState state)
            {
                LocalAddress = localIp;
                RemoteAddress = remoteIp;
                LocalPort = localPort;
                RemotePort = remotePort;
                State = state;
                ProcessId = pId;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_UDPROW_OWNER_PID
        {
            public uint localAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] localPort;
            public int owningPid;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_UDPTABLE_OWNER_PID
        {
            public uint dwNumEntries;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
            public MIB_UDPROW_OWNER_PID[] table;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class UdpProcessRecord
        {
            [DisplayName("Local Address")]
            public IPAddress LocalAddress { get; set; }
            [DisplayName("Local Port")]
            public uint LocalPort { get; set; }
            [DisplayName("Process ID")]
            public int ProcessId { get; set; }
            [DisplayName("Process Name")]
            public string ProcessName { get; set; }

            public UdpProcessRecord(IPAddress localAddress, uint localPort, int pId)
            {
                LocalAddress = localAddress;
                LocalPort = localPort;
                ProcessId = pId;
            }
        }
    }