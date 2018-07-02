using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace tickMeter
{
    public class TickMeterState
    {
        public bool IsTracking { get; set; } = false;

        public GameServer Server { get; set; }

        public string Game { get; set; } = "";

        public List<int> TicksHistory { get; set; }

        public int TickRate { get; set; }
        protected string timeStamp;
        public ConnectionsManager ConnMngr;

        public TickMeterState()
        {
            Reset();
        }

        public string CurrentTimestamp { get { return timeStamp; }
        set
            {
                if(value != timeStamp)
                {
                    OutputTickRate = TickRate;
                    TicksHistory.Add(OutputTickRate);
                    TickRateLog += timeStamp + ";" + OutputTickRate.ToString() + Environment.NewLine;
                    TickRate = 0;
                    timeStamp = value;
                }
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
            Server = new GameServer();
            Game = "";
            TicksHistory = new List<int>();
            UploadTraffic = 0;
            DownloadTraffic = 0;
            TickRate = 0;
            OutputTickRate = 0;
            TickRateLog = "";
        }

        

        public class GameServer
        {
            private string CurrentIP = "";
            private int PingLimit = 1000;
            public int Ping { get; set; } = 0;

            public string Country { get; set; } = "";
            private System.Timers.Timer PingTimer;

            public string Ip {
                get { return CurrentIP; }
                set
                {
                    string oldIP = CurrentIP;
                    CurrentIP = value;
                    if (oldIP != CurrentIP)
                    {
                        PingTimer = null;
                        SetPingTimer();
                        DetectCountry();
                    }
                }
            }

            private void SetPingTimer()
            {
                if (PingTimer == null)
                {
                    PingTimer = new System.Timers.Timer
                    {
                        Interval = 5000
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

            private async void DetectCountry()
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
                    Country = ipInfo.Country;
                });
            }

            public static IPEndPoint CreateIPEndPoint(string endPoint)
            {
                string[] ep = endPoint.Split(':');
                if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
                IPAddress ip;
                if (!IPAddress.TryParse(ep[0], out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
                int port;
                if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
                {
                    throw new FormatException("Invalid port");
                }
                return new IPEndPoint(ip, port);
            }

            private async void PingServer()
            {
                if (Ip == "") return;
                
                await Task.Run(() =>
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.Blocking = true;
                    sock.ReceiveTimeout = PingLimit;
                    var stopwatch = new Stopwatch();
                    EndPoint ep = CreateIPEndPoint(Ip + ":80");
                    stopwatch.Start();
                    try { sock.Connect(ep); } catch (Exception) { }
                    stopwatch.Stop();
                    sock.Close();
                    int curPing = int.Parse(stopwatch.Elapsed.ToString("fff"));
                    if(curPing < PingLimit) Ping = curPing;
                });
            }
        }
    }
}
