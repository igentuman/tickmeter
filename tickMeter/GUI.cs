using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PcapDotNet.Analysis;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Base;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using System.Resources;

namespace tickMeter
{
    

    public partial class GUI : Form
    {
        

        private  IList<LivePacketDevice> AdaptersList;
        private PacketDevice selectedAdapter;
        public bool trackingFlag = false;
        public string server = "0.0.0.0";
        string udpscr = "";
        string udpdes = "";
        int ticks = 0;
        int lineID = 0;
        string logData = "";
        int tickRate = 0;
        string lastPingedServer = "";
        int restarts = 0;
        int restartLimit = 1;
        int lastSelectedAdapterID = -1;
        int uploadTraf;
        int downloadTraf;
        Bitmap chartBckg;
        int chartLeftPadding = 25;
        int chartXStep = 4;
        int appInitHeigh;
        int appInitWidth;
        Graphics g;
        Pen pen;
        List<int> ticksHistory;

        private const int WM_ACTIVATE = 0x0006;
        private const int WA_ACTIVE = 1;
        private const int WA_CLICKACTIVE = 2;
        private const int WA_INACTIVE = 0;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 SWP_SIZE = 0x0003;
        private const UInt32 SWP_MOVE = 0x0004;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        private const UInt32 NOTOPMOST_FLAGS = SWP_MOVE | SWP_SIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public class IpInfo
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

        public string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }
            country = ipInfo.Country;
            return ipInfo.Country;
        }

        string country = "";
        int ping = 0;

        public GUI()
        {
            InitializeComponent();

            try
            {
                AdaptersList = LivePacketDevice.AllLocalMachine.ToList();
            }
            catch(Exception)
            {
                MessageBox.Show("Install WinPCAP. Try to run as admin");
            }

            PcapDotNetAnalysis.OptIn = true;

            if (AdaptersList.Count == 0)
            {

                MessageBox.Show("No network connections found");

                return;

            }

            for (int i = 0; i != AdaptersList.Count; ++i)
            {
                LivePacketDevice Adapter = AdaptersList[i];

                if (Adapter.Description != null)
                {
                    string addr = Adapter.Addresses.First().ToString();
                    var match = Regex.Match(addr, "(\\d)+\\.(\\d)+\\.(\\d)+\\.(\\d)+");
  
                     adapters_list.Items.Add(match.Value + " " + Adapter.Description.Replace("Network adapter ","").Replace("'Microsoft' ",""));
                }
                else
                {
                    adapters_list.Items.Add("Unknown");

                }
            }


            ticksHistory = new List<int>();
            pen = new Pen(Color.DarkRed);
            
        }
        protected void showAll()
        {
            label6.Visible = true;
            label5.Visible = true;
            label2.Visible = true;
            label4.Visible = true;
            label7.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
        }

        [PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_ACTIVE)
            {
                this.BackColor = SystemColors.Control;
                this.TransparencyKey = Color.PaleVioletRed;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Height = appInitHeigh;
                this.Width = appInitWidth;
                showAll();


            }
            else if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_CLICKACTIVE)
            {
                this.BackColor = SystemColors.Control;
                this.TransparencyKey = Color.PaleVioletRed;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Height = appInitHeigh;
                this.Width = appInitWidth;
                showAll();

            }
            else if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_INACTIVE)
            {
                this.BackColor = SystemColors.WindowFrame;
                this.TransparencyKey = SystemColors.WindowFrame;
                this.FormBorderStyle = FormBorderStyle.None;
                if( ! settings_chart_checkbox.Checked)
                {
                    this.Height = 160;
                }
                this.Width = 475;

                if( ! settings_ip_checkbox.Checked)
                {
                    label6.Visible = false;
                    label5.Visible = false;
                }
                if (!settings_ping_checkbox.Checked)
                {
                    label2.Visible = false;
                    label4.Visible = false;
                    label7.Visible = false;
                }
                if (!settings_traffic_checkbox.Checked)
                {
                    label9.Visible = false;
                    label10.Visible = false;
                }
            }
            base.WndProc(ref m);
        }

      

        private void PacketHandler(Packet packet)
        {
            if(! trackingFlag)
            {
                return;
            }
            this.udpscr = "";
            this.udpdes = ""; 

            IpV4Datagram ip = packet.Ethernet.IpV4;
            UdpDatagram udp = ip.Udp;
            
            udpscr = udp.SourcePort.ToString();
            udpdes = udp.DestinationPort.ToString();
            int portSRC = int.Parse(udpscr);
            int portDES = int.Parse(udpdes);
            if (portSRC > 6999 && portSRC < 7999)
            {
                server = ip.Source.ToString();
                downloadTraf += udp.TotalLength;
                ticks++;
            }
            if (portDES > 6999 && portDES < 7999)
            {
                uploadTraf += udp.TotalLength;
            }
        }


        private async void timer1_Tick(object sender, EventArgs e)
        {

            
            if (!trackingFlag || !IsGameRunning())
            {
                return;
            }
            tickRate = ticks;
            ticks = 0;

            //временная затычка
            if (tickRate > 61)
            {
                ticks = tickRate - 61;
                tickRate = 61;
            }
            ticksHistory.Add(tickRate);
            if(ticksHistory.Count>(graph.Image.Width-chartLeftPadding)/chartXStep)
            {
                ticksHistory.RemoveAt(0);
                ticksHistory = ticksHistory.ToList<int>();
                
            }
            label1.Text = tickRate.ToString();
            if(tickRate < 30)
            {
                label1.ForeColor = Color.Red;
            } else if(tickRate < 50)
            {
                label1.ForeColor = Color.DarkOrange;
            } else
            {
                label1.ForeColor = Color.ForestGreen;
            }
            lineID++;
            if(settings_log_checkobx.Checked)
            {
                logData += lineID.ToString() + "," + tickRate.ToString() + Environment.NewLine;
            }


            if(settings_chart_checkbox.Checked)
            {
                await Task.Run(
                        () => {
                            graph.Invoke(new Action(() => graph.Image = UpdateGraph(ticksHistory.ToList<int>())));
                        });
            }
            if (settings_traffic_checkbox.Checked)
            {
                float formatedUpload = (float)uploadTraf / (1024 * 1024);
                float formatedDownload = (float)downloadTraf / (1024 * 1024);
                await Task.Run(
                       () => {
                           label10.Invoke(new Action(() => label10.Text = formatedUpload.ToString("N2")+" / "+ formatedDownload.ToString("N2")+" mb"));
                       });
            }

            if (settings_ip_checkbox.Checked)
            {
                await Task.Run(
                       () => {
                       label6.Invoke(new Action(() => label6.Text = server));
                       });
            }
            if(settings_rtss_output.Checked)
            {
                await Task.Run(
                   () => {
                       if (!RivaTuner.IsRivaRunning())
                       {
                           RivaTuner.RunRiva();
                       }
                       RivaTuner.print(buildRivaOutput());
                   });
               
            }
        }

        public Bitmap UpdateGraph(List<int> ticks)
        {
            chartBckg = new Bitmap(graph.InitialImage);
            Graphics g = Graphics.FromImage(chartBckg);
            int w = graph.Image.Width;
            int h = graph.Image.Height;
            float scale =  (float)h / 61; //2.8
            for (int i = 1; i < ticks.Count; i++)
            {
                g.DrawLine(new Pen(Color.Red, 1), new Point(chartLeftPadding + (i - 1) * chartXStep, h - (int)((float)ticks[i - 1]*scale)), new Point(chartLeftPadding + i * chartXStep, h - (int)((float)ticks[i]*scale)));
            }
            return chartBckg;
        }

        public WebRequest request;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!trackingFlag)
            {
                return;
            }
            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("Ethernet connections only!");

                    return;
                }

                using (BerkeleyPacketFilter filter = communicator.CreateFilter("udp"))
                {
                    communicator.SetFilter(filter);
                }
                communicator.ReceivePackets(0, PacketHandler);

            }

        }


        private async Task PingServer()
        {
            
            if (!trackingFlag || !settings_ping_checkbox.Checked)
            {
                return;
            }

            
            
            if (server == "" || server == "0.0.0.0")
            {
                return;
            }
            

            
            if (lastPingedServer != server)
            {
                await Task.Run(
                   () => {
                       label7.Invoke(new Action(() => label7.Text = GetUserCountryByIp(server)));
                   });
                //label7.Text = "(" + GetUserCountryByIp(server) + ")";
            }
            await Task.Run(
               () => {
                   
                   request = WebRequest.Create("http://" + server);
                   Stopwatch sw = Stopwatch.StartNew();
                   try
                   {
                       using (WebResponse response = request.GetResponse())
                       {

                       }

                   }
                   catch (Exception) { sw.Stop(); }
                   sw.Stop();

                   int pingInt = int.Parse(sw.Elapsed.ToString("fff"));
                   if (pingInt > 200)
                   {
                       label2.Invoke(new Action(() => label2.ForeColor = Color.Red));
                       //label2.ForeColor = Color.Red;
                   }
                   else if (pingInt > 100 && pingInt < 200)
                   {
                       label2.Invoke(new Action(() => label2.ForeColor = Color.DarkOrange));
                       //label2.ForeColor = Color.DarkOrange;
                   }
                   else
                   {
                       label2.Invoke(new Action(() => label2.ForeColor = Color.ForestGreen));
                       //label2.ForeColor = Color.ForestGreen;
                   }
                   ping = pingInt;
                   label2.Invoke(new Action(() => label2.Text = pingInt.ToString() + " ms"));

                   //label2.Text = pingInt.ToString() + " ms";

               });
            
        }

        
        private async void timer3_Tick(object sender, EventArgs e)
        {
            if (!trackingFlag || !IsGameRunning())
            {
                return;
            }
           
            await PingServer();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!trackingFlag)
            {
                return;
            }
            if(ticks == 0)
            {
                restarts++;
                if (restarts > restartLimit)
                {
                    stopTracking();
                    return;
                }
            } else
            {
                restarts = 0;
            }
            
            backgroundWorker1.RunWorkerAsync();
        }

        public void switchToEnglish()
        {

            settings_lbl.Text = Resources.en.ResourceManager.GetString(settings_lbl.Name);
            settings_rtss_output.Text = Resources.en.ResourceManager.GetString(settings_rtss_output.Name);
            settings_log_checkobx.Text = Resources.en.ResourceManager.GetString(settings_log_checkobx.Name);
            settings_ip_checkbox.Text = Resources.en.ResourceManager.GetString(settings_ip_checkbox.Name);
            settings_ping_checkbox.Text = Resources.en.ResourceManager.GetString(settings_ping_checkbox.Name);
            settings_traffic_checkbox.Text = Resources.en.ResourceManager.GetString(settings_traffic_checkbox.Name);
            settings_chart_checkbox.Text = Resources.en.ResourceManager.GetString(settings_chart_checkbox.Name);
            network_connection_lbl.Text = Resources.en.ResourceManager.GetString(network_connection_lbl.Name);

        }

        public async Task startTracking()
        {
           
            lineID = 0;
            uploadTraf = 0;
            downloadTraf = 0;
            trackingFlag = true;
            timer1.Enabled = true;
            selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
            lastSelectedAdapterID = adapters_list.SelectedIndex;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            adapters_list.Enabled = false;
            settings_log_checkobx.Enabled = false;
            timer3.Enabled = true;
        }

        public void stopTracking()
        {
            adapters_list.Enabled = true;
            settings_log_checkobx.Enabled = true;
            timer3.Enabled = false;
            timer1.Enabled = false;
            trackingFlag = false;
            label7.Text = "";
            label6.Text = "000.000.000.000";
            label1.Text = "0";
            label2.Text = "0 ms";
            label1.ForeColor = Color.OrangeRed;
            label2.ForeColor = Color.OrangeRed;
            graph.Image = graph.InitialImage;
            ticksHistory.Clear();
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            adapters_list.SelectedIndex = -1;
            if (settings_log_checkobx.Checked && logData.Length > 1)
            {
                if( ! Directory.Exists("logs"))
                {
                    Directory.CreateDirectory("logs");
                }
                File.AppendAllText(@"logs\" + server + "_ticks.csv", logData);
            }
            if (RivaTuner.IsRivaRunning())
            {
                RivaTuner.print("");
            }
            

        }

        private async void adapters_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(adapters_list.SelectedIndex >= 0)
            {
               await startTracking();
            }
        }

        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopTracking();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label6.Text);
        }
    
        private bool IsGameRunning()
        {
            Process[] pname = Process.GetProcessesByName("tslGame");
            return pname.Length != 0;
        }

        private async void RetryTimer_Tick(object sender, EventArgs e)
        {
            
            if (IsGameRunning())
            {
                if (!trackingFlag && lastSelectedAdapterID != -1)
                {
                    adapters_list.SelectedIndex = lastSelectedAdapterID;
                    await startTracking();
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UConzx4k6IVXSs9PsY9Snkbg");
        }

        private string getTextFormat()
        {
            return "<C0=ff8300><C1=5CD689><C2=32b503><C3=CC0000><C4=FFD500><C5=999999><C6=666666><C7=4DA6FF><C8=b70707><S0=50><S1=70>";
        }

        public string FormatTickrate()
        {
            string tickRateStr = "<S><C>Tickrate: ";
            if(tickRate < 30)
            {
                tickRateStr += "<C3>" + tickRate.ToString();
            } else if(tickRate < 50)
            {
                tickRateStr += "<C0>" + tickRate.ToString();
            } else
            {
                tickRateStr += "<C2>" + tickRate.ToString();
            }
            string output =  getTextFormat() + tickRateStr+Environment.NewLine;
            return output;
        }

        public string FormatServer()
        {
            return "<C><S>IP: " + server+Environment.NewLine;
        }

        public string FormatTraffic()
        {
            float formatedUpload = (float)uploadTraf / (1024 * 1024);
            float formatedDownload = (float)downloadTraf / (1024 * 1024);
            return "<C><S>UP/DL: " + formatedUpload.ToString("N2")+" / " + formatedDownload.ToString("N2") + "<S1> Mb" + Environment.NewLine;
        }

        public string FormatPing()
        {
            string pingFont = "";
            if(ping < 100)
            {
                pingFont = "<C2>";
            } else if(ping < 150)
            {
                pingFont = "<C0>";
            } else
            {
                pingFont = "<C3>";
            }
            return "<C>Ping: " + pingFont + ping.ToString() + "<S0>ms <S1><C>(" + country + ")"+Environment.NewLine;
        }

        public string buildRivaOutput()
        {
            string output = getTextFormat();
            output += FormatTickrate();

            if (settings_ip_checkbox.Checked)
            {
                output += FormatServer();
            }
            
            if(settings_ping_checkbox.Checked)
            {
                output += FormatPing();

            }
            if(settings_traffic_checkbox.Checked)
            {
                output += FormatTraffic();
            }
            return output;
        }

        private void settings_rtss_output_CheckedChanged(object sender, EventArgs e)
        {
            if (RivaTuner.IsRivaRunning())
            {
                RivaTuner.print("");
            } else
            {
                RivaTuner.RunRiva();
            }
            if (settings_rtss_output.Checked)
            {
                SetWindowPos(this.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            } else
            {
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            }
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            appInitHeigh = this.Height;
            appInitWidth = this.Width;
            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (ci.TwoLetterISOLanguageName == "en")
            {
                switchToEnglish();
            }
        }

        private void network_connection_lbl_Click(object sender, EventArgs e)
        {
            switchToEnglish();
        }
    }
}
