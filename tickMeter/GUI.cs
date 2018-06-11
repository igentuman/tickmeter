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

namespace NSHW
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
        int tmpT = 0;
        string lastPingedServer = "";
        int restarts = 0;
        int restartLimit = 1;
        int lastSelectedAdapterID = -1;
        private const int WM_ACTIVATE = 0x0006;
        private const int WA_ACTIVE = 1;
        private const int WA_CLICKACTIVE = 2;
        private const int WA_INACTIVE = 0;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
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

        public static string GetUserCountryByIp(string ip)
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

            return ipInfo.Country;
        }

        public GUI()
        {
            InitializeComponent();

            try
            {
                AdaptersList = LivePacketDevice.AllLocalMachine.Where(adapter => adapter.Description.Contains("Microsoft") == false).ToList();
            }
            catch(Exception)
            {
                MessageBox.Show("WinPCAP точно установлен?");
            }

            PcapDotNetAnalysis.OptIn = true;

            if (AdaptersList.Count == 0)
            {

                MessageBox.Show("Не вижу сетевых подключений");

                return;

            }

            for (int i = 0; i != AdaptersList.Count; ++i)
            {
                LivePacketDevice Adapter = AdaptersList[i];

                if (Adapter.Description != null)
                {
                    adapters_list.Items.Add(Adapter.Description);
                }
                else
                {
                    adapters_list.Items.Add("Unknown");

                }
            }

        }

        [PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_ACTIVE)
            {
                this.BackColor = SystemColors.Control;
                this.TransparencyKey = Color.PaleVioletRed;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Height = 310;
                
            }
            else if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_CLICKACTIVE)
            {
                this.BackColor = SystemColors.Control;
                this.TransparencyKey = Color.PaleVioletRed;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Height = 310;

            }
            else if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_INACTIVE)
            {
                this.BackColor = SystemColors.WindowFrame;
                this.TransparencyKey = SystemColors.WindowFrame;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Height = 140;

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
            if (portSRC > 6999 && portSRC < 7999)
            {
                server = ip.Source.ToString();
                ticks++;
            }    
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!trackingFlag || !IsGameRunning())
            {
                return;
            }
            tmpT = ticks;
            ticks = 0;

            //временная затычка
            if (tmpT > 61)
            {
                ticks = tmpT-61;
                tmpT = 61;
            } 
            
            label1.Text = tmpT.ToString();
            if(tmpT < 30)
            {
                label1.ForeColor = Color.Red;
            } else if(tmpT < 50)
            {
                label1.ForeColor = Color.DarkOrange;
            } else
            {
                label1.ForeColor = Color.ForestGreen;
            }
            lineID++;
            if(checkBox1.Checked)
            {
                logData += lineID.ToString() + "," + tmpT.ToString() + Environment.NewLine;
            }
            
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
                    MessageBox.Show("Мне нужны только Ethernet подключения!");

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
            if (!trackingFlag)
            {
                return;
            }

            
            
            if (server == "" || server == "0.0.0.0")
            {
                return;
            }
            

            label6.Text = server;
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

        private void GUI_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        public async Task startTracking()
        {
           
            if(!IsGameRunning())
            {
                //stopTracking();
                //MessageBox.Show("Пубжик не запущен!","STOP IT! Get some help", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //return;
            }
            lineID = 0;
            trackingFlag = true;
            timer1.Enabled = true;
            selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
            lastSelectedAdapterID = adapters_list.SelectedIndex;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            adapters_list.Enabled = false;
            checkBox1.Enabled = false;
            timer3.Enabled = true;
        }

        public void stopTracking()
        {
            adapters_list.Enabled = true;
            checkBox1.Enabled = true;
            timer3.Enabled = false;
            timer1.Enabled = false;
            trackingFlag = false;
            label7.Text = "";
            label6.Text = "000.000.000.000";
            label1.Text = "0";
            label2.Text = "0 ms";
            label1.ForeColor = Color.OrangeRed;
            label2.ForeColor = Color.OrangeRed;

            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            adapters_list.SelectedIndex = -1;
            if (checkBox1.Checked)
            {
                File.AppendAllText(@"logs\" + server + "_ticks.csv", logData);
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
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UConzx4k6IVXSs9PsY9Snkbg");
        }
    }
}
