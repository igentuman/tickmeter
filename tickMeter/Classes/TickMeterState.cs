using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using tickMeter.Classes;

namespace tickMeter
{
    public class TickMeterState
    {
        private int LastTicksCount = 0;
        private int _tickrate;
        private System.Timers.Timer MeterValidateTimer;
        private DateTime timeStamp;

        public bool IsTracking { get; set; } = false;
        public bool ConnectionsManagerFlag = false;

        public DateTime SessionStart { get; set; }
        public GameServer Server { get; set; }

        public string LocalIP { get; set; }
        public string Game { get; set; } = "";

        public bool isBuiltInProfileActive = false;
        public bool isCustomProfileActive = false;

        public int AvgTickrate;
        public List<int> TicksHistory { get; set; }
        public List<float> tickTimeBuffer = new List<float>();
        public List<float> pingBuffer = new List<float>();
        public List<float> tickrateGraph = new List<float>();
        
        public int TickRate {
            get { return _tickrate; }
            set {
                _tickrate = value;
                SetMeterTimer();
            }
        }

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
                pingBuffer.Add(30);
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
            public int AvgPing { get; set; } = 0;
            public string Location { get; set; } = "";
            private System.Timers.Timer PingTimer;
            public int LastSuccedPort = 0;
            public List<int> PortsToPing;
            public List<int> OfflinePorts;
            public int PingPortFails = 0;
            private int gamePort;

            public GameServer()
            {
                string ping_ports = App.settingsManager.GetOption("ping_ports");
                if(ping_ports != "")
                {
                    PortsToPing = ping_ports.Split(',').Select(int.Parse).ToList();
                } else
                {
                    PortsToPing = new List<int>() { 80 };
                }
                PortsToPing.Insert(0, GamePort);
                SetPingTimer();
            }

            public int Ping {
                get { return _ping; }
                set {
                    _ping = value;
                    App.meterState.pingBuffer.Add(_ping);
                    App.meterState.pingBuffer.RemoveAt(0);
                    AvgPing = (AvgPing + _ping) / 2;
                }
            }


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

            public int GamePort {
                get => gamePort;
                set {
                    if(value != gamePort)
                    {
                        PortsToPing.Insert(0, value);
                    }
                        gamePort = value;
                    }
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
                ICMPfails = 0;
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
                if (ICMPfails > 5) return 0;
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
                TcpClient tcpClient = new TcpClient();
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    Blocking = true,
                    ReceiveTimeout = PingLimit
                };
                if(PingPortFails > 3)
                {
                    PingPort = PortsToPing.Last();
                    PingPortFails = 0;
                    PortsToPing.RemoveAt(PortsToPing.Count - 1);
                    if(PortsToPing.Count < 1)
                    {
                        PortsToPing.Add(80);
                    }
                }
                EndPoint ep = CreateIPEndPoint(Ip,PingPort);
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                try { sock.Connect(ep); } catch (Exception) {  }
                stopwatch.Stop();
                sock.Close();
                return (int)stopwatch.ElapsedMilliseconds;
            }

            int ICMPfails = 0;
            private async void PingServer()
            {
                if (Ip == "") return;
                
                await Task.Run(() =>
                {
                    int pingTime = PingICMP();
                    
                    Debug.Print("icmp " + pingTime.ToString());
                    if (pingTime == 0 || pingTime == 1000) {
                        ICMPfails++;
                        pingTime = PingSocket();
                        Debug.Print("socket "+PingPort+ ": " + pingTime.ToString());
                        if (pingTime == 0 || pingTime > 2000) PingPortFails++;
                    }

                    if (pingTime < PingLimit) Ping = pingTime;
                });
            }
        }
    }
}
