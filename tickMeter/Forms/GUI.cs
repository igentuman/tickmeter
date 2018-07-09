using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using tickMeter.Classes;
using System.Threading;

namespace tickMeter
{
    public partial class GUI : Form
    {
        private  IList<LivePacketDevice> AdaptersList;
        public PacketDevice selectedAdapter;
        public ConnectionsManager NetworkConnectionsMngr;
        public NetworkStats networkStats;
        public Thread PcapThread;
        public SettingsForm settingsForm;
        public PacketStats packetStats;
        public TickMeterState meterState;
        public string udpscr = "";
        public string udpdes = "";
        public BackgroundWorker pcapWorker;
        int restarts = 0;
        int restartLimit = 1;
        int lastSelectedAdapterID = -1;
        public string threadID = ""; 
        Bitmap chartBckg;
        int chartLeftPadding = 25;
        int chartXStep = 4;
        int appInitHeigh;
        int appInitWidth;
        bool OnScreen;
        public PubgStatsManager PubgMngr;
        public DbdStatsManager DbdMngr;

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
            settingsForm = new SettingsForm();
            packetStats = new PacketStats();
            packetStats.gui = this;
            settingsForm.gui = this;
            try
            {
                AdaptersList = LivePacketDevice.AllLocalMachine.ToList();
            }
            catch(Exception)
            {
                MessageBox.Show("Install WinPCAP. Try to run as Admin");
                if (MessageBox.Show("Download WinPCAP?", "WinPCAP", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start("https://bitbucket.org/dvman8bit/tickmeter/downloads/WinPcap_4_1_3.exe");
                    Close();
                }
            }

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
                    settingsForm.adapters_list.Items.Add(GetAdapterAddress(Adapter) + " " + Adapter.Description.Replace("Network adapter ","").Replace("'Microsoft' ",""));
                }
                else
                {
                    settingsForm.adapters_list.Items.Add("Unknown");
                }
            }
            
        }

        public void InitManagers()
        {
            Debug.Print("InitManagers");
            meterState = new TickMeterState();
            PubgMngr = new PubgStatsManager
            {
                meterState = meterState
            };
            DbdMngr = new DbdStatsManager
            {
                meterState = meterState
            };
            try
            {
                if (!settingsForm.settings_netstats_checkbox.Checked)
                {
                    NetworkConnectionsMngr = new ConnectionsManager();
                    meterState.ConnMngr = NetworkConnectionsMngr;
                    meterState.ConnectionsManagerFlag = true;

                    NetworkConnectionsMngr.meterState = meterState;
                    PubgMngr.ConnMngr = NetworkConnectionsMngr;
                    DbdMngr.ConnMngr = NetworkConnectionsMngr;
                }

            }
            catch (Exception)
            {
                meterState.ConnectionsManagerFlag =
                settingsForm.settings_netstats_checkbox.Checked = true;
            }
        }

        protected void ShowAll()
        {
            serverLbl.Visible = 
            label5.Visible = 
            pingLbl.Visible = 
            label4.Visible = 
            countryLbl.Visible = 
            label9.Visible = 
            trafficLbl.Visible = 
            SettingsButton.Visible =
            packetStatsBtn.Visible = true;
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
                SettingsButton.Visible = false;
                packetStatsBtn.Visible = false;
                if (settingsForm.settings_rtss_output.Checked)
                {
                    OnScreen = false;
                }
                if ( !settingsForm.settings_chart_checkbox.Checked)
                {
                    Height = 160;
                }
                Width = 475;

                if( !settingsForm.settings_ip_checkbox.Checked)
                {
                    serverLbl.Visible = false;
                    label5.Visible = false;
                }
                if (!settingsForm.settings_ping_checkbox.Checked)
                {
                    pingLbl.Visible = false;
                    label4.Visible = false;
                    countryLbl.Visible = false;
                }
                if (!settingsForm.settings_traffic_checkbox.Checked)
                {
                    label9.Visible = false;
                    trafficLbl.Visible = false;
                }
            }
            base.WndProc(ref m);
        }

      

        private void PacketHandler(Packet packet)
        {

            if (!meterState.IsTracking) return;
            DbdMngr.ProcessPacket(packet, this);
            PubgMngr.ProcessPacket(packet, this);
        }


        private async void TicksLoop_Tick(object sender, EventArgs e)
        {
            if (settingsForm.settings_rtss_output.Checked)
            {
                await Task.Run(() => {
                    try { RivaTuner.BuildRivaOutput(this); } catch (Exception exc) { MessageBox.Show(exc.Message); }
                });
            }

            //form overlay isn't visible, quit
            if (!OnScreen) return;

            //update tickrate
            Color TickRateColor = settingsForm.ColorGood.ForeColor;
            if (meterState.OutputTickRate < 30)
            {
                TickRateColor = settingsForm.ColorBad.ForeColor;
            }
            else if (meterState.OutputTickRate < 50)
            {
                TickRateColor = settingsForm.ColorMid.ForeColor;
            }

            await Task.Run(
                    () => {
                        tickRateLbl.Invoke(new Action(() => {
                            tickRateLbl.Text = meterState.OutputTickRate.ToString();
                            tickRateLbl.ForeColor = TickRateColor;
                        }));
                        //update tickrate chart
                        if (settingsForm.settings_chart_checkbox.Checked)
                        {
                            graph.Invoke(new Action(() => graph.Image = UpdateGraph(meterState.TicksHistory)));
                        }
                        //update traffic
                        if (settingsForm.settings_traffic_checkbox.Checked)
                        {
                            float formatedUpload = (float)meterState.UploadTraffic / (1024 * 1024);
                            float formatedDownload = (float)meterState.DownloadTraffic / (1024 * 1024);
                            trafficLbl.Invoke(new Action(() => trafficLbl.Text = formatedUpload.ToString("N2") + " / " + formatedDownload.ToString("N2") + " mb"));
                        }
                        //update IP
                        if (settingsForm.settings_ip_checkbox.Checked)
                        {
                        serverLbl.Invoke(new Action(() => serverLbl.Text = meterState.Server.Ip));
                        }
                        //update PING
                        if (settingsForm.settings_ping_checkbox.Checked)
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

        public string GetAdapterAddress(LivePacketDevice Adapter)
        {
            if (Adapter.Description != null)
            {
                string addr = Adapter.Addresses.First().ToString();
                var match = Regex.Match(addr, "(\\d)+\\.(\\d)+\\.(\\d)+\\.(\\d)+");
                if (match.Value == "")
                {
                    addr = Adapter.Addresses[1].ToString();
                    match = Regex.Match(addr, "(\\d)+\\.(\\d)+\\.(\\d)+\\.(\\d)+");
                }
                return match.Value;
            }
            return "";
        }

        public void StartTracking()
        {
            Debug.Print("StartTracking");
            InitManagers();
            meterState.IsTracking = true;
            ticksLoop.Enabled = true;
            selectedAdapter = AdaptersList[settingsForm.adapters_list.SelectedIndex];
            meterState.LocalIP = GetAdapterAddress(AdaptersList[settingsForm.adapters_list.SelectedIndex]);
            lastSelectedAdapterID = settingsForm.adapters_list.SelectedIndex;
            try
            {
                if (PcapThread == null)
                {
                    
                    PcapThread = new Thread(InitPcapWorker);
                    PcapThread.Start();
                    Debug.Print("Starting thread " + PcapThread.ManagedThreadId.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("PCAP Thread init error");
            }
            
        }
        private void PcapWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!meterState.IsTracking) return;
            if (meterState.TickRate == 0)
            {
                restarts++;
                if (restarts > restartLimit)
                {
                    StopTracking();
                    return;
                }
            }
            else
            {
                restarts = 0;
            }

            try
            {
                pcapWorker.RunWorkerAsync();

            }
            catch (Exception) { }

        }

        private void PcapWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (!meterState.IsTracking) return;
            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 500))
            {
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("This program works only on Ethernet networks!");
                    return;
                }

                communicator.ReceivePackets(0, PacketHandler);
            }
        }
        public void InitPcapWorker()
        {
            pcapWorker = new BackgroundWorker();
            pcapWorker.DoWork += PcapWorkerDoWork;
            pcapWorker.RunWorkerCompleted += PcapWorkerCompleted;
            pcapWorker.RunWorkerAsync();
        }

        public void StopTracking()
        {
            ticksLoop.Enabled = false;
            if (meterState == null) return;

            tickRateLbl.ForeColor = settingsForm.ColorBad.ForeColor;
            pingLbl.ForeColor = settingsForm.ColorMid.ForeColor;
            graph.Image = graph.InitialImage;
            
            
            if (settingsForm.settings_log_checkbox.Checked)
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
            try { RivaTuner.PrintData(""); } catch (Exception exc) { MessageBox.Show(exc.Message); }
            
            meterState.Reset();
        }

        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopTracking();
            settingsForm.SaveToConfig();
            RivaTuner.KillRtss();
        }

        private void ServerLbl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(serverLbl.Text);
        }
    
        
        private void RetryTimer_Tick(object sender, EventArgs e)
        {
            if ((meterState == null || !meterState.IsTracking) && lastSelectedAdapterID != -1)
            {
                settingsForm.adapters_list.SelectedIndex = lastSelectedAdapterID;
                StartTracking();
            }
        }

        public void UpdateStyle(bool rtssFlag)
        {
            if (rtssFlag)
            {
                SetWindowPos(this.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            }
            else
            {
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            }
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            appInitHeigh = Height;
            appInitWidth = Width;
            

            settingsForm.ApplyFromConfig();
            settingsForm.CheckNewVersion();
            

            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (ci.TwoLetterISOLanguageName == "en")
            {
                settingsForm.SwitchToEnglish();
            }

        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            settingsForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            packetStats.meterState = meterState;
            packetStats.Show();

        }


    }
}
