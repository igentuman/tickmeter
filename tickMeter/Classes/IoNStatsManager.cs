﻿using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace tickMeter.Classes
{
    public class IoNStatsManager
    {
        int StartPort = 10000;
        int EndPort = 20000;
        string ProcessName = "IONBranch";
        public const string GameCode = "ISLANDS_OF_NYNE";
        Timer GameInfoTimer;
        public bool GameRunningFlag = false;
        public List<int> openPorts = new List<int>();
        public List<int> openPorts2 = new List<int>();
        public int lastGamePing = 0;
        bool NetworkActivityFlag;
        public bool isEnabled = false;

        public IoNStatsManager()
        {
            SetGameInfoTimer();
        }

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
            if (App.meterState.ConnectionsManagerFlag)
            {
                int ProcessId = Process.GetProcessesByName(ProcessName).First().Id;
                List<UdpProcessRecord> gamePorts = App.connMngr.UdpActiveConnections;
                foreach (UdpProcessRecord gamePort in gamePorts)
                {
                    if (gamePort.ProcessId == ProcessId)
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

        public void ProcessPacket(Packet packet)
        {
            if (!GameRunningFlag || App.settingsManager.GetOption(GameCode) != "True") return;
            if (packet.Ethernet.IpV4.Protocol != PcapDotNet.Packets.IpV4.IpV4Protocol.Udp) return;

            //search within port range and destination (local) port we fetched from connections manager
            if (packet.Ethernet.IpV4.Udp.SourcePort > StartPort && packet.Ethernet.IpV4.Udp.SourcePort < EndPort && packet.Ethernet.IpV4.Destination.ToString() == App.meterState.LocalIP)
            {
                if (App.meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.DestinationPort)) return;
                App.meterState.updateTicktimeBuffer(packet.Timestamp.Ticks);
                App.meterState.CurrentTimestamp = packet.Timestamp;
                App.meterState.Game = GameCode;
                App.meterState.Server.Ip = packet.Ethernet.IpV4.Source.ToString();
                App.meterState.DownloadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
                App.meterState.TickRate++;
                NetworkActivityFlag = true;
            }
            if (packet.Ethernet.IpV4.Udp.DestinationPort > StartPort && packet.Ethernet.IpV4.Udp.DestinationPort < EndPort && packet.Ethernet.IpV4.Source.ToString() == App.meterState.LocalIP)
            {
                if (App.meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.SourcePort)) return;
                NetworkActivityFlag = true;
                App.meterState.UploadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
            }
        }
    }
}
