using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using tickMeter.Classes;

namespace tickMeter
{
    public class DbdStatsManager
    {
        int StartPort = 7000;
        int EndPort = 8000;
        string ProcessName = "DeadByDaylight-Win64-Shipping";
        string GameName = "DeadByDaylight-Win64-Shipping";
        public const string GameCode = "DEAD_BY_DAYLIGHT";
        Timer GameInfoTimer;
        public bool GameRunningFlag = false;
        public List<int> openPorts = new List<int>();
        public List<int> openPorts2 = new List<int>();
        public int lastGamePing = 0;
        bool NetworkActivityFlag;
        public int ignorePortFrom = 27000;
        public int ignorePortTo = 27030;
        public bool isEnabled = false;

        public bool IsGameRunning()
        {
            Process[] pname = Process.GetProcessesByName(GameName);
            return pname.Length != 0;
        }

        private void SetGameInfoTimer()
        {
            if (GameInfoTimer == null)
            {
                GameInfoTimer = new Timer
                {
                    Interval = 5000
                };
                GameInfoTimer.Elapsed += GameInfoTimerTick;
                GameInfoTimer.AutoReset = true;
                GameInfoTimer.Enabled = true;
            }
        }

        public DbdStatsManager()
        {
            SetGameInfoTimer();
        }

        private async void GameInfoTimerTick(Object source, ElapsedEventArgs e)
        {
            await Task.Run(() => { FetchGameInfo(); });
        }

        public void FetchGameInfo()
        {
            if (App.settingsManager.GetOption(GameCode) != "True") return;

            openPorts.Clear();
            openPorts2.Clear();

            GameRunningFlag = IsGameRunning();
            if (!GameRunningFlag) return;
            if(App.meterState.ConnectionsManagerFlag)
            {
                int[] ProcessIds = Process.GetProcessesByName(ProcessName).Select(e => e.Id).ToArray();
                List<UdpProcessRecord> gamePorts = App.connMngr.UdpActiveConnections;
                foreach (UdpProcessRecord gamePort in gamePorts)
                {
                    if (ProcessIds.Contains(gamePort.ProcessId))
                    {
                        openPorts.Add((int)gamePort.LocalPort);
                    }
                }
            }
            

            if (!NetworkActivityFlag && App.meterState.Game == GameCode)
            {
                App.meterState.IsTracking = false;
            }
            NetworkActivityFlag = false;
        }
        public bool firstPacket = true;
        public void ProcessPacket(Packet packet)
        {

            if (!GameRunningFlag || App.settingsManager.GetOption(GameCode) != "True") return;
            if (packet.Ethernet.IpV4.Protocol != PcapDotNet.Packets.IpV4.IpV4Protocol.Udp) return;
            if (packet.Ethernet.IpV4.Udp.SourcePort < ignorePortTo && packet.Ethernet.IpV4.Udp.SourcePort > ignorePortFrom) return;
            if (packet.Ethernet.IpV4.Udp.DestinationPort < ignorePortTo && packet.Ethernet.IpV4.Udp.DestinationPort > ignorePortFrom) return;
            //search within port range and destination (local) port we fetched from connections manager
            if (packet.Ethernet.IpV4.Udp.SourcePort > StartPort && packet.Ethernet.IpV4.Udp.SourcePort < EndPort && packet.Ethernet.IpV4.Destination.ToString() == App.meterState.LocalIP)
            {
                if (firstPacket)
                {
                    firstPacket = false;
                    Debug.Print(packet.Ethernet.IpV4.Udp.Payload.ToHexadecimalString());
                }
                if (App.meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.DestinationPort)) return;
                App.meterState.updateTicktimeBuffer(packet.Timestamp.Ticks);
                App.meterState.CurrentTimestamp = packet.Timestamp;
                App.meterState.Game = GameCode;
                App.meterState.Server.GamePort = packet.Ethernet.IpV4.Udp.SourcePort;
                App.meterState.Server.Ip = packet.Ethernet.IpV4.Source.ToString();
                App.meterState.DownloadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
                App.meterState.TickRate++;
                NetworkActivityFlag = true;
            }
            Debug.Print(App.meterState.TickRate.ToString());
            if (packet.Ethernet.IpV4.Udp.DestinationPort > StartPort && packet.Ethernet.IpV4.Udp.DestinationPort < EndPort && packet.Ethernet.IpV4.Source.ToString() == App.meterState.LocalIP)
            {
                if (App.meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.SourcePort)) return;
                NetworkActivityFlag = true;
                App.meterState.UploadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
            }
        }
    }
}
