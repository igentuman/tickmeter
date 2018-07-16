using Newtonsoft.Json;
using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using tickMeter.Classes;

namespace tickMeter
{
    public class TickMeterState
    {
        public bool IsTracking { get; set; } = false;
        public DateTime SessionStart { get; set; }
        public GameServer Server { get; set; }
        public string LocalIP { get; set; }
        public string Game { get; set; } = "";
        protected int LastTicksCount = 0;
        protected int _tickrate;
        public List<int> TicksHistory { get; set; }
        private System.Timers.Timer MeterValidateTimer;
        public List<float> tickTimeBuffer = new List<float>();
        public int TickRate { get { return _tickrate; } set { _tickrate = value; SetMeterTimer(); } }
        public int AvgTickrate;

        public void updateTicktimeBuffer(long packetTicks)
        {
            if (tickTimeBuffer.Count > 512)
            {
                tickTimeBuffer.RemoveAt(0);
            }
            if (CurrentTimestamp != null)
            {
                float tickTime = (float)(packetTicks - CurrentTimestamp.Ticks) / 10000;
                if (OutputTickRate > 0)
                {
                    tickTime -= 500 / OutputTickRate;
                    if (tickTime < 0)
                    {
                        tickTime = 0;
                    }
                }
               tickTimeBuffer.Add(tickTime);
            }
        }
        public List<float> tickrateGraph = new List<float>();
        
        protected DateTime timeStamp;
        
        public void SetMeterTimer()
        {
            if(MeterValidateTimer == null || !MeterValidateTimer.Enabled)
            {
                MeterValidateTimer = new System.Timers.Timer();
                MeterValidateTimer.Elapsed += MeterValidateTimerTick;
                MeterValidateTimer.Interval = 2000;
                MeterValidateTimer.AutoReset = true;
                MeterValidateTimer.Enabled = true;
            }
        }

        public TickMeterState()
        {
            for(int i = 0; i < 513; i++)
            {
                tickTimeBuffer.Add(0);
                tickrateGraph.Add(0);
            }
            
            Reset();
            Server = new GameServer();
            SetMeterTimer();
        }
        private void MeterValidateTimerTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            if(LastTicksCount == TicksHistory.Count)
            {
                KillTimers();
            } else
            {
                Server.SetPingTimer();
            }
            LastTicksCount = TicksHistory.Count;
        }
        public DateTime CurrentTimestamp { get { return timeStamp; }
        set
            {
                if (!IsTracking) return;
                if(value.ToString() != timeStamp.ToString())
                {
                    OutputTickRate = TickRate;
                    AvgTickrate = (AvgTickrate+OutputTickRate)/2;
                    TicksHistory.Add(OutputTickRate);
                    if(tickrateGraph.Count > 512)
                    {
                        tickrateGraph.RemoveAt(0);
                        tickrateGraph.RemoveAt(0);
                    }
                    tickrateGraph.Add(OutputTickRate);
                    tickrateGraph.Add(OutputTickRate);
                    TickRateLog += timeStamp.ToString() + ";" + OutputTickRate.ToString() + Environment.NewLine;
                    TickRate = 0;
                    
                }
                timeStamp = value;
            }
        }
        public int OutputTickRate { get; set; }
        public int UploadTraffic { get; set; } = 0;

        public int DownloadTraffic { get; set; } = 0;

        public string TickRateLog { get; set; } = "";
        public bool ConnectionsManagerFlag = false;


        public void Reset()
        {
            IsTracking = false;
            
            Game = "";
            TicksHistory = new List<int>();
            UploadTraffic = 0;
            DownloadTraffic = 0;
            TickRate = 0;
            OutputTickRate = 0;
            TickRateLog = "";           
        }

        public void KillTimers()
        {
            if(MeterValidateTimer != null)
            {
                MeterValidateTimer.Enabled = false;
                MeterValidateTimer.Close();
                MeterValidateTimer.Dispose();
                Debug.Print("killed meter timer");
            }
            
            Server.KillTimer();
        }


        public class GameServer
        {
            private string CurrentIP = "";
            public int PingPort = 80;
            private int PingLimit = 1000;
            private int _ping = 0;
            public int Ping {
                get { return _ping; }
                set {
                    _ping = value;
                    AvgPing = (AvgPing + _ping) / 2;
                }
            }
            public int AvgPing { get; set; } = 0;
            public string Location { get; set; } = "";
            private System.Timers.Timer PingTimer;

            public string Ip {
                get { return CurrentIP; }
                set
                {
                    string oldIP = CurrentIP;
   
                    CurrentIP = value;
                    if (oldIP != CurrentIP && CurrentIP != "")
                    {
                        App.meterState.SessionStart = DateTime.Now;
                        if(PingTimer == null)
                        SetPingTimer();
                        DetectLocation();
                    }
                }
            }

            public GameServer()
            {
                SetPingTimer();
            }


            public void KillTimer()
            {
               
                if(PingTimer != null)
                {
                    PingTimer.Enabled = false;
                    PingTimer.Close();
                    PingTimer.Dispose();
                    Debug.Print("killed server timer");
                }
            }

            public void SetPingTimer()
            {
                int PingInterval = 2000;
                if(App.settingsManager.GetOption("ping_interval") != null && App.settingsManager.GetOption("ping_interval") != "")
                {
                    PingInterval = int.Parse(App.settingsManager.GetOption("ping_interval"));
                }
                if (PingTimer == null || !PingTimer.Enabled)
                {
                    PingTimer = new System.Timers.Timer
                    {
                        Interval = PingInterval
                    };
                    PingTimer.Elapsed += PingServerTimer;
                    PingTimer.AutoReset = true;
                    PingTimer.Enabled = true;
                }
            }

            private void PingServerTimer(Object source, System.Timers.ElapsedEventArgs e)
            {
                PingServer();
            }

            private class IpInfo
            {

                [JsonProperty("ip")]
                public string Ip { get; set; }

                [JsonProperty("hostname")]
                public string Hostname { get; set; }

                [JsonProperty("city")]
                public string City { get; set; }

                [JsonProperty("region")]
                public string Region { get; set; }

                [JsonProperty("country")]
                public string Country { get; set; }

                [JsonProperty("loc")]
                public string Loc { get; set; }

                [JsonProperty("org")]
                public string Org { get; set; }

                [JsonProperty("postal")]
                public string Postal { get; set; }
            }

            private async void DetectLocation()
            {
                if (Ip == "") return;

                await Task.Run(() =>
                {
                    IpInfo ipInfo = new IpInfo();
                    try
                    {
                        string info = new WebClient().DownloadString("http://ipinfo.io/" + Ip);
                        ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                        RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                        ipInfo.Country = myRI1.EnglishName;
                    }
                    catch (Exception)
                    {
                        ipInfo.Country = "";
                    }
                    Location = ipInfo.Country;
                    if (ipInfo.City != "")
                    {
                        Location += ", " + ipInfo.City;
                    }
                });
            }

            public static IPEndPoint CreateIPEndPoint(string endPoint,int Port)
            {
                IPAddress ip;
                if (!IPAddress.TryParse(endPoint, out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }

                return new IPEndPoint(ip, Port);
            }

            public int PingICMP()
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply pingReply;
                try
                {
                    pingReply = ping.Send(Ip, 1000);
                } catch(Exception) { return 0; }
                return (int)pingReply.RoundtripTime;
            }

            public int PingSocket()
            {
                Socket sock = new Socket(System.Net.Sockets.AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    Blocking = true,
                    ReceiveTimeout = PingLimit
                };
                EndPoint ep = CreateIPEndPoint(Ip,PingPort);
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                try { sock.Connect(ep); } catch (Exception) { return 0; }
                stopwatch.Stop();
                sock.Close();
                return (int)stopwatch.ElapsedMilliseconds;
            }

            private async void PingServer()
            {
                if (Ip == "") return;
                
                await Task.Run(() =>
                {
                    int pingTime = PingICMP();
                    
                    Debug.Print("icmp " + pingTime.ToString());
                    if (pingTime == 0 || pingTime == 1000) {
                        pingTime = PingSocket();
                        Debug.Print("socket " + pingTime.ToString());

                    }

                    if (pingTime < PingLimit) Ping = pingTime;
                });
            }
        }
    }
}
