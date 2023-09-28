using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace tickMeter.Classes
{
    public class ProcessNetworkStats
    {
        public string localIp;
        public string remoteIp;
        public uint localPort;
        public uint remotePort;
        public string name;
        public int pid;
        public int ticksIn;
        private int _ticksIn;
        private int _ticksOut;

        public int ticksOut;
        public int downloaded;
        public int sent;

        public int getTicksIn()
        {
            return _ticksIn;
        }

        public int getTicksOut()
        {
            return _ticksOut;
        }
        private DateTime _lastUpate;

        public DateTime lastUpdate { 
            set {
                if(_lastUpate != null && Trim(value).Second != Trim(_lastUpate).Second && value>_lastUpate)
                {
                    _ticksIn = ticksIn;
                    _ticksOut = ticksOut;

                    ticksIn = 0;
                    ticksOut = 0;
                }
                _lastUpate = value;
            }
            get { return _lastUpate; } }
        public DateTime startTrack;
        public string protocol;
        public static DateTime Trim(DateTime date)
        {
            return new DateTime(date.Ticks - (date.Ticks % TimeSpan.TicksPerSecond), date.Kind);
        }

        public List<float> tickTimeBuffer = new List<float>();

        public int TrackingDelta()
        {
            return (int)Math.Abs(DateTime.Now.Subtract(startTrack).TotalSeconds);
        }

        public int LastUpdateDelta()
        {
            return (int)Math.Abs(DateTime.Now.Subtract(lastUpdate).TotalSeconds);
        }

        public void updateTicktimeBuffer(long packetTicks)
        {
            if (tickTimeBuffer.Count > 511)
            {
                tickTimeBuffer.RemoveAt(0);
            }
            if (lastUpdate != null)
            {
                float tickTime = Math.Abs(Math.Min((float)(packetTicks - lastUpdate.Ticks) / 10000, 100));

                tickTimeBuffer.Add(tickTime);
            }
        }
    }
}
