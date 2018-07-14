using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyHttp;
using EasyHttp.Http;
using JsonFx.Json;

namespace tickMeter.Classes
{
    public static class WebStatsManager
    {
        public static void uploadTickrate()
        {
            
            var http = new HttpClient();
            var tickrate = new Tickrate();
            http.Post("https://it-man.website/tickmeter/stats/upload", new { Genus = "1,2,3,4,5,6,1,2,3,4,5,6" }, HttpContentTypes.ApplicationJson);
            MessageBox.Show(http.Response.RawText);
        }

        private class Tickrate : Object
        {
            public Tickrate()
            {
                
            }
            [JsonName("tickrate")]
            public string tickrate = "sdfsdfsdf";
        }
    }
}
