using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tickMeter.Classes
{

    public class ProcessNetInstance
    {
        public string processName;
        public int processID;
        public DateTime startTrack;
        public int ticksIn;
        public int ticksOut;
        public DateTime lastUpdate;
        public int remotePort;
        public int localPort;
        public string remoteIP;
        public string protocol;

        public int TrackingDelta()
        {
            return (int)(TimeSpan.FromTicks(DateTime.Now.Ticks - startTrack.Ticks).TotalSeconds);
        }

        public int LastUpdateDelta()
        {
            return (int)(TimeSpan.FromTicks(DateTime.Now.Ticks - lastUpdate.Ticks).TotalSeconds);
        }
    }

    public static class AutoDetectMngr
    {
        public static List<ProcessNetInstance> activeProcesses = new List<ProcessNetInstance>();

        public static void ProcessRecord(string to_ip, string from_ip, string processName, int to_port, int from_port, string protocol)
        {
            int ii = -1;
            if (activeProcesses.Count > 0)
                for (ii = 0; ii < activeProcesses.Count; ii++)
                {
                    if (activeProcesses[ii].processName == processName)
                    {
                        break;
                    }
                }

            if (ii == -1 && processName != @"n\a")
            {
                ProcessNetInstance tmpProc = new ProcessNetInstance()
                {
                    processName = processName,
                    remoteIP = to_ip != App.meterState.LocalIP ? to_ip : from_ip,
                    remotePort = to_ip != App.meterState.LocalIP ? to_port : from_port,
                    localPort = to_ip == App.meterState.LocalIP ? to_port : from_port,
                    startTrack = DateTime.Now,
                    protocol = protocol,

                };
                activeProcesses.Add(tmpProc);
                ii = activeProcesses.Count - 1;
            }
            if (ii > -1 && activeProcesses.Count > ii)
            {
                if (to_ip == App.meterState.LocalIP)
                {
                    activeProcesses[ii].ticksIn++;
                }
                else
                {
                    activeProcesses[ii].ticksOut++;
                }
                activeProcesses[ii].lastUpdate = DateTime.Now;
            }
        }


        public static List<ListViewItem> getActiveProccesses(List<ListViewItem> procItems)
        {
            procItems.Clear();
            int indx = 0;
            List<ProcessNetInstance> tmpList = activeProcesses.ToList();
            foreach (var procNet in tmpList)
            {

                if (procNet.TrackingDelta() == 0) continue;
                if (procNet.ticksIn / procNet.TrackingDelta() > 10 && procNet.ticksOut / procNet.TrackingDelta() > 10 && procNet.TrackingDelta() > 5)
                {
                    ListViewItem procItem = new ListViewItem(procNet.remoteIP);
                    procItem.SubItems.Add(procNet.remotePort.ToString());
                    procItem.SubItems.Add(procNet.localPort.ToString());
                    procItem.SubItems.Add((procNet.ticksIn / procNet.TrackingDelta()).ToString());
                    procItem.SubItems.Add((procNet.ticksOut / procNet.TrackingDelta()).ToString());
                    procItem.SubItems.Add(procNet.protocol.ToString());
                    procItem.SubItems.Add(procNet.processName);
                    procItems.Add(procItem);
                }
                else
                {
                    if (procNet.TrackingDelta() > 5 && procNet.ticksIn / procNet.TrackingDelta() < 10 && procNet.ticksOut / procNet.TrackingDelta() < 10)
                    {
                        activeProcesses.RemoveAt(indx);
                    }
                }
                if(procNet.LastUpdateDelta() > 5)
                {
                    activeProcesses.RemoveAt(indx);
                }
                indx++;
            }
            return procItems;
        }

    
    }
}
