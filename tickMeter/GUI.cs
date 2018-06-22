using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using PcapDotNet.Analysis;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text.RegularExpressions;
using tickMeter.GameManagers;

namespace tickMeter
{
    public partial class GUI : Form
    {
        private  IList<LivePacketDevice> AdaptersList;
        private PacketDevice selectedAdapter;
        public ConnectionsManager NetworkConnectionsMngr;

        public TickMeterState meterState;
        public string udpscr = "";
        public string udpdes = "";
        
        int restarts = 0;
        int restartLimit = 1;
        int lastSelectedAdapterID = -1;
        
        Bitmap chartBckg;
        int chartLeftPadding = 25;
        int chartXStep = 4;
        int appInitHeigh;
        int appInitWidth;
        bool OnScreen;
        PubgStatsManager PubgMngr;

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

        /// <summary>
        /// Form initialization
        /// </summary>
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
                    if(match.Value == "")
                    {
                        addr = Adapter.Addresses[1].ToString();
                        match = Regex.Match(addr, "(\\d)+\\.(\\d)+\\.(\\d)+\\.(\\d)+");
                    }
                    adapters_list.Items.Add(match.Value + " " + Adapter.Description.Replace("Network adapter ","").Replace("'Microsoft' ",""));
                }
                else
                {
                    adapters_list.Items.Add("Unknown");
                }
            }
            NetworkConnectionsMngr = new ConnectionsManager();
            meterState = new TickMeterState
            {
                ConnMngr = NetworkConnectionsMngr,
            };
            PubgMngr = new PubgStatsManager
            {
                ConnMngr = NetworkConnectionsMngr,
                meterState = meterState
            };
        }

        protected void ShowAll()
        {
            serverLbl.Visible = true;
            label5.Visible = true;
            pingLbl.Visible = true;
            label4.Visible = true;
            countryLbl.Visible = true;
            label9.Visible = true;
            trafficLbl.Visible = true;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_ACTIVE)
            {
                OnScreen = true;
                BackColor = SystemColors.Control;
                TransparencyKey = Color.PaleVioletRed;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
                Height = appInitHeigh;
                Width = appInitWidth;
                ShowAll();
            }
            else if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_CLICKACTIVE)
            {
                OnScreen = true;
                BackColor = SystemColors.Control;
                TransparencyKey = Color.PaleVioletRed;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
                Height = appInitHeigh;
                Width = appInitWidth;
                ShowAll();
            }
            else if (m.Msg == WM_ACTIVATE & m.WParam == (IntPtr)WA_INACTIVE)
            {
                OnScreen = true;
                BackColor = SystemColors.WindowFrame;
                TransparencyKey = SystemColors.WindowFrame;
                FormBorderStyle = FormBorderStyle.None;
                
                if(settings_rtss_output.Checked)
                {
                    OnScreen = false;
                }
                if ( ! settings_chart_checkbox.Checked)
                {
                    Height = 160;
                }
                Width = 475;

                if( ! settings_ip_checkbox.Checked)
                {
                    serverLbl.Visible = false;
                    label5.Visible = false;
                }
                if (!settings_ping_checkbox.Checked)
                {
                    pingLbl.Visible = false;
                    label4.Visible = false;
                    countryLbl.Visible = false;
                }
                if (!settings_traffic_checkbox.Checked)
                {
                    label9.Visible = false;
                    trafficLbl.Visible = false;
                }
            }
            base.WndProc(ref m);
        }

      

        private async void PacketHandler(Packet packet)
        {
            if (!meterState.IsTracking) return;
            //using async game managers to track network stats
            switch (meterState.Game)
            {
                case PubgStatsManager.GameCode:
                    await Task.Run(() => { PubgMngr.ProcessPacket(packet, this); });
                    break;
                default://run all managers to detect game
                    await Task.Run(() => { PubgMngr.ProcessPacket(packet, this); });
                    break;
            }
        }


        private async void TicksLoop_Tick(object sender, EventArgs e)
        {
            

            if (settings_rtss_output.Checked)
            {
                await Task.Run(() => { RivaTuner.BuildRivaOutput(this); });
            }

            //form overlay isn't visible, quit
            if (!OnScreen) return;

            //update tickrate
            Color TickRateColor = Color.ForestGreen;
            if (meterState.OutputTickRate < 30)
            {
                TickRateColor = Color.Red;
            }
            else if (meterState.OutputTickRate < 50)
            {
                TickRateColor = Color.DarkOrange;
            }

            await Task.Run(
                    () => {
                        tickRateLbl.Invoke(new Action(() => {
                            tickRateLbl.Text = meterState.OutputTickRate.ToString();
                            tickRateLbl.ForeColor = TickRateColor;
                        }));
                        //update tickrate chart
                        if (settings_chart_checkbox.Checked)
                        {
                            graph.Invoke(new Action(() => graph.Image = UpdateGraph(meterState.TicksHistory)));
                        }
                        //update traffic
                        if (settings_traffic_checkbox.Checked)
                        {
                            float formatedUpload = (float)meterState.UploadTraffic / (1024 * 1024);
                            float formatedDownload = (float)meterState.DownloadTraffic / (1024 * 1024);
                            trafficLbl.Invoke(new Action(() => trafficLbl.Text = formatedUpload.ToString("N2") + " / " + formatedDownload.ToString("N2") + " mb"));
                        }
                        //update IP
                        if (settings_ip_checkbox.Checked)
                        {
                        serverLbl.Invoke(new Action(() => serverLbl.Text = meterState.Server.Ip));
                        }
                        //update PING
                        if (settings_ping_checkbox.Checked)
                        {
                        countryLbl.Invoke(new Action(() => countryLbl.Text = meterState.Server.Country));
                        pingLbl.Invoke(new Action(() => pingLbl.Text = meterState.Server.Ping.ToString() + " ms"));
                        }
                    });
            if (!meterState.IsTracking)
            {
                StopTracking();
            }

        }

        public Bitmap UpdateGraph(List<int> ticks)
        {
            chartBckg = new Bitmap(graph.InitialImage);
            if (ticks.Count < 2) return chartBckg;
            Graphics g = Graphics.FromImage(chartBckg);
            int w = graph.Image.Width;
            int h = graph.Image.Height;
            float scale =  (float)h / 61; //2.8
            int GraphMaxTicks = (w - chartLeftPadding) / chartXStep;
            Pen pen = new Pen(Color.Red, 1);
            int stepX = 0;
            for (int i = ticks.Count-2; i >= 0 && ticks.Count - i - 1 < GraphMaxTicks; i--)
            {
                stepX++;
                g.DrawLine(pen, new Point(chartLeftPadding + (stepX - 1) * chartXStep, h - (int)((float)ticks[i + 1]*scale)), new Point(chartLeftPadding + stepX * chartXStep, h - (int)((float)ticks[i]*scale)));
            }
            return chartBckg;
        }


        private void PcapWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!meterState.IsTracking) return;
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

        private void PcapWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!meterState.IsTracking) return;
            if(meterState.TickRate == 0)
            {
                restarts++;
                if (restarts > restartLimit)
                {
                    StopTracking();
                    return;
                }
            } else
            {
                restarts = 0;
            }
            pcapWorker.RunWorkerAsync();
        }

        public void SwitchToEnglish()
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

        public void StartTracking()
        {
            meterState.Reset();
            meterState.IsTracking = true;
            ticksLoop.Enabled = true;
            selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
            lastSelectedAdapterID = adapters_list.SelectedIndex;
            if (!pcapWorker.IsBusy)
            {
                pcapWorker.RunWorkerAsync();
            }
            adapters_list.Enabled = false;
            settings_log_checkobx.Enabled = false;
        }

        public void StopTracking()
        {
            ticksLoop.Enabled = false;
            meterState.IsTracking = false;

            tickRateLbl.ForeColor = Color.OrangeRed;
            pingLbl.ForeColor = Color.OrangeRed;
            graph.Image = graph.InitialImage;
            if (pcapWorker.IsBusy)
            {
                pcapWorker.CancelAsync();
            }
            adapters_list.SelectedIndex = -1;
            if (settings_log_checkobx.Checked)
            {
                if(meterState.Server.Ip != "" && meterState.TickRateLog != "")
                {
                    if (!Directory.Exists("logs"))
                    {
                        Directory.CreateDirectory("logs");
                    }
                    try
                    {
                        File.AppendAllText(@"logs\" + meterState.Server.Ip + "_ticks.csv", "timestamp;tickrate" + Environment.NewLine + meterState.TickRateLog);
                    }
                    catch (IOException) { }
                }
            }

            RivaTuner.PrintData("");
            meterState.Reset();
        }

        private void Adapters_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adapters_list.SelectedIndex >= 0)
            {
               StartTracking();
            }
        }

        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopTracking();
        }

        private void ServerLbl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(serverLbl.Text);
        }
    
        
        private void RetryTimer_Tick(object sender, EventArgs e)
        {
            if (!meterState.IsTracking && lastSelectedAdapterID != -1)
            {
                adapters_list.SelectedIndex = lastSelectedAdapterID;
                StartTracking();
            }
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UConzx4k6IVXSs9PsY9Snkbg");
        }

        private async void Settings_rtss_output_CheckedChanged(object sender, EventArgs e)
        {

            await Task.Run(() => { RivaTuner.PrintData(""); });
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
            appInitHeigh = Height;
            appInitWidth = Width;
            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (ci.TwoLetterISOLanguageName == "en")
            {
                SwitchToEnglish();
            }
        }
    }
}
