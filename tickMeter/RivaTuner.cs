using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace tickMeter
{
    public static class RivaTuner
    {
        static string rtss_exe = @"C:\Program Files (x86)\RivaTuner Statistics Server\RTSS.exe";
        static TickMeterState meterState;

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
            if (!VerifyRiva()) return;
            string path = "rivatuner.dll";

            unsafe
            {
                void* handle = LoadLibrary(path);

                if (handle == null)
                    throw new DllNotFoundException("Unable to find the native rivatuner library: " + path);

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
                    Process.Start(f.FullName);
                    Thread.Sleep(2000);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static string TextFormat()
        {
            return "<C0=ff8300><C1=2361C4><C2=32b503><C3=CC0000><C4=FFD500><C5=999999><C6=666666><C7=4DA6FF><C8=b70707><S0=50><S1=70>";
        }

        public static string FormatTickrate()
        {
            string tickRateStr = "<S><C1>Tickrate: ";
            if (meterState.OutputTickRate < 30)
            {
                tickRateStr += "<C3>" + meterState.OutputTickRate.ToString();
            }
            else if (meterState.OutputTickRate < 50)
            {
                tickRateStr += "<C0>" + meterState.OutputTickRate.ToString();
            }
            else
            {
                tickRateStr += "<C2>" + meterState.OutputTickRate.ToString();
            }
            string output = tickRateStr + Environment.NewLine;
            return output;
        }

        public static string FormatServer()
        {
            return "<S><C1>IP: <C>" + meterState.Server.Ip + Environment.NewLine;
        }

        public static string FormatTraffic()
        {
            float formatedUpload = (float)meterState.UploadTraffic / (1024 * 1024);
            float formatedDownload = (float)meterState.DownloadTraffic / (1024 * 1024);
            return "<S><C1>UP/DL: <C>" + formatedUpload.ToString("N2") + " / " + formatedDownload.ToString("N2") + "<S1> Mb" + Environment.NewLine;
        }

        public static string FormatPing()
        {
            string pingFont = "";
            if (meterState.Server.Ping < 100)
            {
                pingFont = "<C2>";
            }
            else if (meterState.Server.Ping < 150)
            {
                pingFont = "<C0>";
            }
            else
            {
                pingFont = "<C3>";
            }
            return "<S><C1>Ping: " + pingFont + meterState.Server.Ping.ToString() + "<S0>ms <S1><C>(" + meterState.Server.Country + ")" + Environment.NewLine;
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

            if (gui.settings_ip_checkbox.Checked)
            {
                output += FormatServer();
            }

            if (gui.settings_ping_checkbox.Checked)
            {
                output += FormatPing();
            }
            if (gui.settings_traffic_checkbox.Checked)
            {
                output += FormatTraffic();
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
