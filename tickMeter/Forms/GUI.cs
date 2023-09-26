using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;
using tickMeter.Classes;
using System.Threading;
using System.Net.Sockets;
using System.Linq;

namespace tickMeter.Forms
{
    public partial class GUI : Form
    {
        
        public PacketDevice selectedAdapter;
        public Thread PcapThread;

        public BackgroundWorker pcapWorker;
        public Boolean allowClose = false;
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
        public string targetKey = "";

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
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            try
            {
                InitializeComponent();
                App.Init();
                App.gui = this;

                for (int i = 0; i != App.GetAdapters().Count; ++i)
                {
                    LivePacketDevice Adapter = App.GetAdapters()[i];

                    if (Adapter.Description != null)
                    {
                        App.settingsForm.adapters_list.Items.Add(App.GetAdapterAddress(Adapter) + " " + Adapter.Description.Replace("Network adapter ","").Replace("'Microsoft' ",""));
                    }
                    else
                    {
                        App.settingsForm.adapters_list.Items.Add("Unknown");
                    }
                }

            }
            catch (Exception e)
            {
                DebugLogger.log(e);
                MessageBox.Show(e.Message);
            }

        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            DebugLogger.log(e);
            MessageBox.Show(e.Message);
        }

        public void InitMeterState()
        {
            Debug.Print("InitMeterState");
            if (App.meterState != null) App.meterState.KillTimers();
            App.meterState = new TickMeterState();
            App.meterState.ConnectionsManagerFlag = true;
        }

        protected void ShowAll()
        {
            ip_val.Visible = 
            ip_lbl.Visible = 
            ping_val.Visible = 
            ping_lbl.Visible = 
            countryLbl.Visible = 
            traffic_lbl.Visible = 
            traffic_val.Visible = 
            time_lbl.Visible = 
            time_val.Visible = 
            SettingsButton.Visible =
            gameProfilesButton.Visible =
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
                
                SettingsButton.Visible =
                gameProfilesButton.Visible =
                webStatsButton.Visible =
                packetStatsBtn.Visible = false;
                TopMost = true;
                if (App.settingsForm.settings_rtss_output.Checked)
                {
                    TopMost = false;
                    OnScreen = false;
                }
                if (!App.settingsForm.settings_chart_checkbox.Checked)
                {
                    Height = 160;
                }
                Width = 475;

                if(!App.settingsForm.settings_ip_checkbox.Checked)
                {
                    ip_val.Visible = false;
                    ip_lbl.Visible = false;
                }
                if (!App.settingsForm.settings_ping_checkbox.Checked)
                {
                    ping_val.Visible = false;
                    ping_lbl.Visible = false;
                    countryLbl.Visible = false;
                }
                if (!App.settingsForm.settings_traffic_checkbox.Checked)
                {
                    traffic_lbl.Visible = false;
                    traffic_val.Visible = false;
                }

                if (!App.settingsForm.settings_session_time_checkbox.Checked)
                {
                    time_lbl.Visible = false;
                    time_val.Visible = false;
                }
            }
            base.WndProc(ref m);
        }

      

        private void PacketHandler(Packet packet)
        {
            if (!App.meterState.IsTracking) return;
            GameProfileManager.CallBuitInProfiles(packet);
            GameProfileManager.CallCustomProfiles(packet);
            ActiveWindowTracker.AnalyzePacket(packet);
        }

        bool RTSS_Failed = false;
        
        private async void TicksLoop_Tick(object sender, EventArgs e)
        {
            AutoDetectMngr.GetActiveProcessName(true);
            if(!App.meterState.isBuiltInProfileActive && !App.meterState.isCustomProfileActive)
            {
                updateMetherStateFromActiveWindow();
            }
            if (App.settingsForm.settings_rtss_output.Checked)
            {
                await Task.Run(() => {
                    try { RivaTuner.BuildRivaOutput(); } catch (Exception ex) {
                        if(!RTSS_Failed)
                        {
                            DebugLogger.log(ex);
                            RTSS_Failed = true;
                        }
                    }
                });
            }

            //form overlay isn't visible, quit
            if (!OnScreen) return;

            //update tickrate
            Color TickRateColor = App.settingsForm.ColorGood.ForeColor;
            if (App.meterState.OutputTickRate < 30)
            {
                TickRateColor = App.settingsForm.ColorBad.ForeColor;
            }
            else if (App.meterState.OutputTickRate < 50)
            {
                TickRateColor = App.settingsForm.ColorMid.ForeColor;
            }
            
            await Task.Run(
                    () => {
                        tickrate_val.Invoke(new Action(() => {
                            tickrate_val.Text = App.meterState.OutputTickRate.ToString();
                            tickrate_val.ForeColor = TickRateColor;
                        }));
                        //update tickrate chart
                        if (App.settingsForm.settings_chart_checkbox.Checked)
                        {
                            graph.Invoke(new Action(() => graph.Image = UpdateGraph(App.meterState.TicksHistory)));
                        }
                        //update traffic
                        if (App.settingsForm.settings_traffic_checkbox.Checked)
                        {
                            float formatedUpload = (float)App.meterState.UploadTraffic / (1024 * 1024);
                            float formatedDownload = (float)App.meterState.DownloadTraffic / (1024 * 1024);
                            traffic_val.Invoke(new Action(() => traffic_val.Text = formatedUpload.ToString("N2") + " / " + formatedDownload.ToString("N2") + " mb"));
                        }
                        //update IP
                        if (App.settingsForm.settings_ip_checkbox.Checked)
                        {
                        ip_val.Invoke(new Action(() => ip_val.Text = App.meterState.Server.Ip));
                        }
                        //update PING
                        if (App.settingsForm.settings_ping_checkbox.Checked)
                        {
                        countryLbl.Invoke(new Action(() => countryLbl.Text = App.meterState.Server.Location));
                        ping_val.Invoke(new Action(() => ping_val.Text = App.meterState.Server.Ping.ToString() + " ms"));
                        }
                        //update time
                        if (App.settingsForm.settings_session_time_checkbox.Checked && App.meterState.Server.Ip != "")
                        {
                            TimeSpan result = DateTime.Now.Subtract(App.meterState.SessionStart);
                            string Duration = result.ToString("mm':'ss");
                            ip_val.Invoke(new Action(() => time_val.Text = Duration));
                        }
                    });
            if (!App.meterState.IsTracking)
            {
                StopTracking();
            }
        }

        private bool isValidToTrack(string key)
        {
            if(key != "" && !ActiveWindowTracker.connections.Keys.Contains(key))
            {
                ProcessNetworkStats connection = ActiveWindowTracker.connections[key];
                return
                    AutoDetectMngr.GetActiveProcessName() == connection.name
                    && ActiveWindowTracker.connections[key].TrackingDelta() > 3
                    && ActiveWindowTracker.connections[key].LastUpdateDelta() < 2
                    && ActiveWindowTracker.connections[key].downloaded > 0;

            }
            return false;
        }

        private void updateMetherStateFromActiveWindow()
        {
            int maxTicks = 0;
            string activeProcess = AutoDetectMngr.GetActiveProcessName();

            if(!isValidToTrack(targetKey))
            {
                foreach (string procKey in ActiveWindowTracker.connections.Keys)
                {
                    if (
                        ActiveWindowTracker.connections[procKey].ticksIn > maxTicks
                        && isValidToTrack(procKey)
                        )
                    {
                        maxTicks = ActiveWindowTracker.connections[procKey].ticksIn;
                        targetKey = procKey;
                    }
                }
            }
            
            
            if(targetKey != "")
            {
                ProcessNetworkStats procStats = ActiveWindowTracker.connections[targetKey];
                App.meterState.tickTimeBuffer = procStats.tickTimeBuffer;
                App.meterState.CurrentTimestamp = DateTime.Now;
                App.meterState.Game = procStats.name;
                App.meterState.Server.Ip = procStats.remoteIp.ToString();
                App.meterState.DownloadTraffic = procStats.downloaded;
                App.meterState.TickRate = procStats.getTicksIn();
                App.meterState.Server.PingPort = (int)procStats.remotePort;
                App.meterState.SessionStart = procStats.startTrack;
                App.meterState.IsTracking = true;
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

       
        public void StartTracking()
        {
            Debug.Print("StartTracking");
            
            if (App.meterState != null)
                StopTracking();
            InitMeterState();
            App.meterState.IsTracking = true;
            ticksLoop.Enabled = true;
            List<LivePacketDevice> devices = App.GetAdapters();
            int deviceId = App.settingsForm.adapters_list.SelectedIndex;
            if (devices.Count > deviceId && deviceId > 0)
            {
                selectedAdapter = App.GetAdapters()[deviceId];
            } else
            {
                return;
            }
            
            App.meterState.LocalIP = App.settingsForm.local_ip_textbox.Text;
            lastSelectedAdapterID = deviceId;
            try
            {
                if (PcapThread == null)
                {
                    PcapThread = new Thread(InitPcapWorker);
                    PcapThread.Start();
                    PcapThread.Join();
                    Debug.Print("Starting thread " + PcapThread.ManagedThreadId.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("PCAP Thread init error");
            }
            //InitPcapWorker();


        }
        private void PcapWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!App.meterState.IsTracking) return;
            if (App.meterState.TickRate == 0)
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
            if (!App.meterState.IsTracking) return;
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
            if (App.meterState == null) return;
            try
            {
                GameProfileManager.PubgMngr.firstPacket = true;
            } catch (TypeInitializationException e) {

            }
            
            Debug.Print("StopTracking");
            tickrate_val.ForeColor = App.settingsForm.ColorBad.ForeColor;
            ping_val.ForeColor = App.settingsForm.ColorMid.ForeColor;
            try { graph.Image = graph.InitialImage; } catch(Exception) {  }
            
            
            if (App.settingsForm.settings_log_checkbox.Checked)
            { 
                if(App.meterState.Server.Ip != "" && App.meterState.TickRateLog != "")
                {
                    if (!Directory.Exists("logs"))
                    {
                        Directory.CreateDirectory("logs");
                    }
                    try
                    {
                        File.AppendAllText(@"logs\" + App.meterState.Server.Ip + "_ticks.csv", "timestamp;tickrate" + Environment.NewLine + App.meterState.TickRateLog);
                    }
                    catch (Exception) { }
                }
            }

            if (App.settingsForm.settings_data_send.Checked && App.meterState.TicksHistory.Count > 900 && App.meterState.Server.Ip != "")
            {
               // WebStatsManager.uploadTickrate(); //no no no. not today
            }

            try { RivaTuner.PrintData(""); } catch (Exception exc) { MessageBox.Show(exc.Message); }
            if(App.meterState.Server.Ip != "")
            {
                if (!Directory.Exists("logs"))
                {
                    Directory.CreateDirectory("logs");
                }
                TimeSpan result = DateTime.Now.Subtract(App.meterState.SessionStart);
                string Duration = result.ToString("mm':'ss");
                string serverStat = DateTime.Now.ToLocalTime() + " - IP: " + App.meterState.Server.Ip + " (" + App.meterState.Server.Location + ") Ping: " + App.meterState.Server.AvgPing + "ms, avg Tickrate: "+ App.meterState.AvgTickrate+ ", Time: "+ Duration + Environment.NewLine;
                try
                {
                    File.AppendAllText(@"logs\"+App.meterState.Game+"_SERVERS-STATS.log", serverStat);
                }
                catch (Exception) { }
            }


            App.meterState.IsTracking = false;
        }

        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ServerLbl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ip_val.Text);
        }
    
        
        private void RetryTimer_Tick(object sender, EventArgs e)
        {
            if ((App.meterState == null || !App.meterState.IsTracking) && lastSelectedAdapterID != -1)
            {
                 App.settingsForm.adapters_list.SelectedIndex = lastSelectedAdapterID;
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

            App.settingsForm.ApplyFromConfig();

            
           // App.settingsForm.CheckNewVersion();

            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (ci.TwoLetterISOLanguageName != "ru")
            {
                App.settingsForm.SwitchToEnglish();
            }
            if(App.settingsForm.run_minimized.Checked)
            {
                Hide();
            }
            ETW.init();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            App.settingsForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            App.packetStatsForm.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            App.profilesForm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            App.tickrateStatisticsForm.Show();
        }

        private void GUI_Resize(object sender, EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !allowClose)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void icon_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            StopTracking();
            App.settingsForm.SaveToConfig();
            RivaTuner.KillRtss();
            allowClose = true;
            Close();
        }

        private void icon_menu_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
