using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace tickMeter.GameManagers
{
    class PubgStatsManager
    {
        int StartPort = 6999;
        int EndPort = 7999;
        string ProcessName = "tslGame";
        public const string GameCode = "PUBG";
        Timer GameInfoTimer;
        public bool GameRunningFlag = false;
        public ConnectionsManager ConnMngr;
        public List<int> openPorts = new List<int>();
        public List<int> openPorts2 = new List<int>();
        public int lastGamePing = 0;
        public TickMeterState meterState;
        bool NetworkActivityFlag;

        public bool IsGameRunning()
        {
            Process[] pname = Process.GetProcessesByName(ProcessName);
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

        public PubgStatsManager()
        {
            SetGameInfoTimer();
        }

        private async void GameInfoTimerTick(Object source, ElapsedEventArgs e)
        {
            await Task.Run(() => { FetchGameInfo(); });
        }

        public void FetchGameInfo()
        {
            openPorts.Clear();
            openPorts2.Clear();

            GameRunningFlag = IsGameRunning();
            if (!GameRunningFlag) return;
            if(meterState.ConnectionsManagerFlag)
            {
                int ProcessId = Process.GetProcessesByName(ProcessName).First().Id;
                List<UdpProcessRecord> gamePorts = ConnMngr.UdpActiveConnections;
                foreach (UdpProcessRecord gamePort in gamePorts)
                {
                    if (gamePort.ProcessId == ProcessId)
                    {
                        openPorts.Add((int)gamePort.LocalPort);
                    }
                }
            }
            

            if (!NetworkActivityFlag && meterState.Game == GameCode)
            {
                meterState.IsTracking = false;
            }
            NetworkActivityFlag = false;
        }

        public void ProcessPacket(Packet packet, GUI gui)
        {
            if (!GameRunningFlag || openPorts.Count < 2) return;
            
            //search within port range and destination (local) port we fetched from connections manager
            if (packet.Ethernet.IpV4.Udp.SourcePort > StartPort && packet.Ethernet.IpV4.Udp.SourcePort < EndPort)
            {
                if (meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.DestinationPort)) return;
                meterState.CurrentTimestamp = packet.Timestamp.ToString();
                meterState.Game = GameCode;
                meterState.Server.Ip = packet.Ethernet.IpV4.Source.ToString();
                meterState.DownloadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
                meterState.TickRate++;
                NetworkActivityFlag = true;
            }
            if (packet.Ethernet.IpV4.Udp.DestinationPort > StartPort && packet.Ethernet.IpV4.Udp.DestinationPort < EndPort)
            {
                if (meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.SourcePort)) return;
                NetworkActivityFlag = true;
                gui.meterState.UploadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
            }
        }
    }
}
