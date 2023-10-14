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
        private int avgStableTickrate;
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

                    if(avgStableTickrate == 0)
                    {
                        avgStableTickrate = _ticksIn;
                    }

                    float ratio = ((float)avgStableTickrate / (float)ticksIn);

                    if (ratio < 1.5 && ratio > 0.5)
                    {
                        avgStableTickrate += (ticksIn+avgStableTickrate);
                        avgStableTickrate /= 3;
                    }

                    if(TrackingDelta() > 5)
                    {
                        avgStableTickrate = (int) Math.Round(avgStableTickrate / 5.0) * 5;
                    }
                    int dropped = avgStableTickrate - ticksIn;
                    if(dropped < 0) { dropped = 0; }
                    loss += dropped;
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
        private uint pcktId = 0;
        public int loss = 0;
        public int totalTicksCnt = 0;

        public uint id { 
            set { 
                if(value != 0)
                {
                    if(pcktId == value)
                    {
                        loss++;
                        pcktId = value;
                    }
                }
            }
            get
            {
                return pcktId;
            }
        }

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
            totalTicksCnt++;
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
