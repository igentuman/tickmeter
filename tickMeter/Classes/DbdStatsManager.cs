﻿using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace tickMeter
{
    public class DbdStatsManager
    {
        int StartPort = 49000;
        int EndPort = 65000;
        string ProcessName = "Steam";
        string GameName = "DeadByDaylight-Win64-Shipping";
        public const string GameCode = "DBD";
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
            openPorts.Clear();
            openPorts2.Clear();

            GameRunningFlag = IsGameRunning();
            if (!GameRunningFlag) return;
            if(meterState.ConnectionsManagerFlag)
            {
                int[] ProcessIds = Process.GetProcessesByName(ProcessName).Select(e => e.Id).ToArray();
                List<UdpProcessRecord> gamePorts = ConnMngr.UdpActiveConnections;
                foreach (UdpProcessRecord gamePort in gamePorts)
                {
                    if (ProcessIds.Contains(gamePort.ProcessId))
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
            if (!GameRunningFlag) return;
            if (packet.Ethernet.IpV4.Protocol != PcapDotNet.Packets.IpV4.IpV4Protocol.Udp) return;

            //search within port range and destination (local) port we fetched from connections manager
            if (packet.Ethernet.IpV4.Udp.SourcePort > StartPort && packet.Ethernet.IpV4.Udp.SourcePort < EndPort && packet.Ethernet.IpV4.Destination.ToString() == meterState.LocalIP)
            {
                if (meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.DestinationPort)) return;
                meterState.CurrentTimestamp = packet.Timestamp.ToString();
                meterState.Game = GameCode;
                meterState.Server.Ip = packet.Ethernet.IpV4.Source.ToString();
                meterState.DownloadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
                meterState.TickRate++;
                NetworkActivityFlag = true;
            }
            if (packet.Ethernet.IpV4.Udp.DestinationPort > StartPort && packet.Ethernet.IpV4.Udp.DestinationPort < EndPort && packet.Ethernet.IpV4.Source.ToString() == meterState.LocalIP)
            {
                if (meterState.ConnectionsManagerFlag && !openPorts.Contains(packet.Ethernet.IpV4.Udp.SourcePort)) return;
                NetworkActivityFlag = true;
                gui.meterState.UploadTraffic += packet.Ethernet.IpV4.Udp.TotalLength;
            }
        }
    }
}
