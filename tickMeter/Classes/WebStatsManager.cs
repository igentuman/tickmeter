using System;
using System.Diagnostics;
using System.Threading;
using EasyHttp.Http;

namespace tickMeter.Classes
{
    public static class WebStatsManager
    {
        public static void uploadTickrate()
        {
            Object data = new {
                tickrate = App.meterState.TicksHistory.ToArray(),
                ip = App.meterState.Server.Ip,
                location = App.meterState.Server.Location,
                game = App.meterState.Game,
                ping = App.meterState.Server.AvgPing
            };
            var http = new HttpClient();
            http.Post("https://it-man.website/tickmeter/stats/upload", data, HttpContentTypes.ApplicationJson);
            Debug.Print(http.Response.RawText);
            Thread.Sleep(100);
        }


    }
}
