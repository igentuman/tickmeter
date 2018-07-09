using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace tickMeter
{
    public static class RivaTuner
    {
        public static string rtss_exe = @"C:\Program Files (x86)\RivaTuner Statistics Server\RTSS.exe";
        static TickMeterState meterState;
        public static string LabelColor;
        public static string ColorBad;
        public static string ColorMid;
        public static string ColorGood;
        public static Process RtssInstance;

        [DllImport("kernel32")]
        private unsafe static extern void* LoadLibrary(string dllname);
        [DllImport("kernel32")]
        private unsafe static extern void FreeLibrary(void* handle);

        private sealed unsafe class LibraryUnloader
        {
            internal LibraryUnloader(void* handle)
            {
                this.handle = handle;
            }

            ~LibraryUnloader()
            {
                if (handle != null)
                    FreeLibrary(handle);
            }

            private void* handle;

        } // LibraryUnloader

        private static readonly LibraryUnloader unloader;

        static RivaTuner()
        {
            string path = "rivatuner.dll";
            unsafe
            {
                void* handle = LoadLibrary(path);

                if (handle == null)
                    throw new FileNotFoundException("Unable to find the native rivatuner library: " + path);

                unloader = new LibraryUnloader(handle);
            }
        }

        public static bool IsRivaRunning()
        {
            Process[] pname = Process.GetProcessesByName("RTSS");
            if (pname.Length == 0)
                return false;
            else
                return true;
        }

        public static bool VerifyRiva()
        {
            bool fExists = File.Exists(rtss_exe);
            bool dllEsists = File.Exists("rivatuner.dll");
            return dllEsists && fExists;
        }

        public static void RunRiva()
        {
            FileInfo f = new FileInfo(rtss_exe);
            if (VerifyRiva())
            {
                try
                {
                    RtssInstance = Process.Start(f.FullName);
                    Thread.Sleep(2000);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static void KillRtss()
        {
            if (RtssInstance == null) return;
            try
            {
                RtssInstance.Kill();
                Process[] proc = Process.GetProcessesByName("RTSSHooksLoader64");
                proc[0].Kill();
            }
            catch (Exception) { }
            
        }

        public static string TextFormat()
        {
            return "<C0=" + LabelColor + "><C1=" + ColorBad+ "><C2=" + ColorMid + "><C3=" + ColorGood + "><S0=47><S1=70>";
        }

        public static string FormatTickrate()
        {
            string tickRateStr = "<S><C0>Tickrate: ";
            if (meterState.OutputTickRate < 30)
            {
                tickRateStr += "<C1>" + meterState.OutputTickRate.ToString();
            }
            else if (meterState.OutputTickRate < 50)
            {
                tickRateStr += "<C2>" + meterState.OutputTickRate.ToString();
            }
            else
            {
                tickRateStr += "<C3>" + meterState.OutputTickRate.ToString();
            }
            string output = tickRateStr + Environment.NewLine;
            return output;
        }

        public static string FormatServer()
        {
            return "<S><C0>IP: <C>" + meterState.Server.Ip + Environment.NewLine;
        }

        public static string FormatTraffic()
        {
            float formatedUpload = (float)meterState.UploadTraffic / (1024 * 1024);
            float formatedDownload = (float)meterState.DownloadTraffic / (1024 * 1024);
            return "<S><C0>UP/DL: <C>" + formatedUpload.ToString("N2") + " / " + formatedDownload.ToString("N2") + "<S1> Mb" + Environment.NewLine;
        }

        public static string FormatTime()
        {
            TimeSpan result = TimeSpan.FromSeconds(meterState.SessionTime);
            string Duration = result.ToString("mm':'ss");
            return "<S><C0>Time: <C>" + Duration + Environment.NewLine;
        }

        public static string FormatPing()
        {
            string pingFont = "";
            if (meterState.Server.Ping < 100)
            {
                pingFont = "<C3>";
            }
            else if (meterState.Server.Ping < 150)
            {
                pingFont = "<C2>";
            }
            else
            {
                pingFont = "<C1>";
            }
            return "<S><C0>Ping: " + pingFont + meterState.Server.Ping.ToString() + "<S0>ms <S0><C>(" + meterState.Server.Location + ")" + Environment.NewLine;
        }

        public static void BuildRivaOutput(GUI gui)
        {
            string output = "";
            if(gui.meterState.TickRate == 0 && gui.meterState.Game == "")
            {
                PrintData(output, true);
                return;
            }
            meterState = gui.meterState;
            output += FormatTickrate();

            if (gui.settingsForm.settings_ip_checkbox.Checked)
            {
                output += FormatServer();
            }

            if (gui.settingsForm.settings_ping_checkbox.Checked)
            {
                output += FormatPing();
            }
            if (gui.settingsForm.settings_traffic_checkbox.Checked)
            {
                output += FormatTraffic();
            }
            if (gui.settingsForm.settings_session_time_checkbox.Checked)
            {
                output += FormatTime();
            }
            PrintData(output, true);
        }

        public static void PrintData(string text,bool RunRivaFlag = false)
        {
            if ((!IsRivaRunning() && !RunRivaFlag) || !VerifyRiva()) return;
           
            
            if (!IsRivaRunning() && RunRivaFlag)
            {
                RunRiva();
            }
            if (text != "")
            {
                text = TextFormat() + text;
            }
            print(text);
        }

        [DllImport("rivatuner", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool print(string text);
    }
}
