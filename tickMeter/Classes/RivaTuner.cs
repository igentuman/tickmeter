﻿using RTSS;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace tickMeter.Classes
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
        static OSD osd;
        public static string RivaOutput;
        public static uint chartOffset = 0;

        public static string DrawChart(float[] graphData)
        {
            uint chartSize;
            int max = 60;
            if(graphData.Max() > 61)
            {
                max = 90;
            }
            if (graphData.Max() > 91)
            {
                max = 120;
            }
            unsafe
            {
                fixed (float* lpBuffer = graphData)
                {
                    chartSize = osd.EmbedGraph(chartOffset, lpBuffer: lpBuffer, dwBufferPos: 0, 512, dwWidth: -24, dwHeight: -3, dwMargin: 1, fltMin: 0, fltMax: max, dwFlags: 0);
                }
                string chartEntry = "<C1><S2>" + max + "<OBJ=" + chartOffset.ToString("X8") + "><S1>" + App.meterState.OutputTickRate.ToString();
                chartOffset += chartSize;
                return chartEntry;
            }

        }

        public static void Print(string text)
        {
            osd.Update(text);
        }

        static RivaTuner()
        {
            if (!IsRivaRunning())
            {
                RunRiva();
            } else
            {
                osd = new OSD("TickMeter");
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
           return File.Exists(rtss_exe);
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
            return "<C0=" + LabelColor + "><C1=" + ColorBad+ "><C2=" + ColorMid + "><C3=" + ColorGood + "><S0=47><S1=65><S2=55><A0=-15><A1=55>";
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
            return "<S><C0>UP/DL: <C>" + formatedUpload.ToString("N2") + " / " + formatedDownload.ToString("N2") + "<S0> Mb" + Environment.NewLine;
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

        public static void BuildRivaOutput()
        {
            string output = "";
            if(App.meterState.TickRate == 0 && App.meterState.Game == "")
            {
                PrintData(output, true);
                return;
            }
            chartOffset = 0;
            meterState = App.meterState;
            output += FormatTickrate();

            if (App.settingsForm.settings_ip_checkbox.Checked)
            {
                output += FormatServer();
            }

            if (App.settingsForm.settings_ping_checkbox.Checked)
            {
                output += FormatPing();
            }
            if (App.settingsForm.settings_traffic_checkbox.Checked)
            {
                output += FormatTraffic();
            }
            if (App.settingsForm.settings_session_time_checkbox.Checked)
            {
                output += FormatTime();
            }
            if (App.settingsForm.settings_chart_checkbox.Checked)
            {
                output += DrawChart(App.meterState.tickrateGraph.ToArray());
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
            Print(text);
        }
    }
}
