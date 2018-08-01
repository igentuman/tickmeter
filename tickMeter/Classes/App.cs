using PcapDotNet.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using tickMeter.Forms;

namespace tickMeter.Classes
{
    public static class App
    {
        public static GUI gui;
        public static PacketFilterForm packetFilterForm;
        public static ProfileEditForm profileEditForm;
        public static SettingsForm settingsForm;
        public static TickrateStatistics tickrateStatisticsForm;
        public static PacketStats packetStatsForm;
        public static ProfilesForm profilesForm;
        public static TickMeterState meterState;
        public static SettingsManager settingsManager;
        public static ConnectionsManager connMngr;
        static IList<LivePacketDevice> AdaptersList;

        public static void Init()
        {
            tickrateStatisticsForm = new TickrateStatistics();
            packetFilterForm = new PacketFilterForm();
            settingsForm = new SettingsForm();
            packetStatsForm = new PacketStats();
            profilesForm = new ProfilesForm();
            profileEditForm = new ProfileEditForm();
            settingsManager = new SettingsManager();
            connMngr = new ConnectionsManager();
        }

        public static IList<LivePacketDevice> GetAdapters()
        {
            try
            {
                AdaptersList = LivePacketDevice.AllLocalMachine.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Install WinPCAP. Try to run as Admin");
                if (MessageBox.Show("Download WinPCAP?", "WinPCAP", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start("https://bitbucket.org/dvman8bit/tickmeter/downloads/WinPcap_4_1_3.exe");
                }
            }

            if (AdaptersList.Count == 0)
            {
                MessageBox.Show("No network connections found");
            }
            return AdaptersList;
        }

        public static string GetAdapterAddress(LivePacketDevice Adapter)
        {
            if (Adapter.Description != null)
            {
                DeviceAddress adapterAddress = Adapter.Addresses.LastOrDefault();
                string addr = "";
                if (adapterAddress != null)
                    addr = adapterAddress.ToString();

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

    }
}
