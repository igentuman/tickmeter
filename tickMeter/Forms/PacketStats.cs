using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using tickMeter.Classes;
using tickMeter.Forms;

namespace tickMeter
{
    public partial class PacketStats : Form
    {
        List<Packet> PacketBuffer;
        public int inPackets = 0;
        public int outPackets = 0;
        public int inTraffic = 0;
        public int outTraffic = 0;

        public ConnectionsManager connMngr;
        public GUI gui;
        public bool tracking;
        Thread PcapThread;
        public PacketFilterForm filterForm;
        public BackgroundWorker pcapWorker;
        public TickMeterState meterState;

        PacketFilter packetFilter;

        public PacketStats()
        {
            InitializeComponent();
            packetFilter = new PacketFilter();
            filterForm = new PacketFilterForm();
            filterForm.packetFilter = packetFilter;
        }
        public void InitWorker()
        {
            pcapWorker = new BackgroundWorker();
            pcapWorker.DoWork += PcapWorkerDoWork;
            pcapWorker.RunWorkerCompleted += PcapWorkerCompleted;
            pcapWorker.RunWorkerAsync();
        }

        

        public void Start()
        {
            
            PacketBuffer = new List<Packet>();
            connMngr = new ConnectionsManager(500)
            {
                meterState = meterState
                
            };
            try
            {
                if (PcapThread != null)
                {
                    PcapThread.Abort();
                }
                
                PcapThread = new Thread(InitWorker);
                PcapThread.Start();
            } catch (Exception)
            {
                MessageBox.Show("PCAP Thread init error");
            }
            
            meterState.LocalIP = gui.GetAdapterAddress((LivePacketDevice)gui.selectedAdapter);
            RefreshTimer.Enabled = true;
            avgStats.Enabled = true;
            tracking = true;

        }

        private void PacketStats_Shown(object sender, EventArgs e)
        {
            Start();
        }

        private void PcapWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                pcapWorker.RunWorkerAsync();

            } catch(Exception) { }

        }

        private void PcapWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            using (PacketCommunicator communicator = gui.selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("This program works only on Ethernet networks!");
                    return;
                }
               
                communicator.ReceivePackets(0, PacketHandler);
            }
        }

        

        private void PacketHandler(Packet packet)
        {
            IpV4Datagram ip;
            try
            {
                ip = packet.Ethernet.IpV4;
            }
            catch (Exception) { return; }
            packetFilter.ip = ip;
            if (!packetFilter.Validate()) return;
            PacketBuffer.Add(packet);

            if (ip.Source.ToString() == meterState.LocalIP)
            {
                outPackets++;
                outTraffic += ip.TotalLength;
            }
            if (ip.Destination.ToString() == meterState.LocalIP)
            {
                inPackets++;
                inTraffic += ip.TotalLength;
            }
        }

        int packet_id;
        private async void RefreshTick(object sender, EventArgs e)
        {
            
            if (PacketBuffer.Count < 1)
            {
                return;
            }
            List<Packet> tmpPackets = PacketBuffer.ToList();
            PacketBuffer.Clear();
            
            ListViewItem[] items = new ListViewItem[tmpPackets.Count];
            int iKey = 0;
            foreach (Packet packet in tmpPackets) {
                IpV4Datagram ip;
                try
                {
                    ip = packet.Ethernet.IpV4;
                }
                catch (Exception) { return; }

                UdpDatagram udp = ip.Udp;
                TcpDatagram tcp = ip.Tcp;



                string from_ip = ip.Source.ToString();
                string to_ip = ip.Destination.ToString();

                string packet_size = ip.TotalLength.ToString();

                string protocol = ip.Protocol.ToString();
                string from_port = "";
                string to_port = "";
                string processName = @"n\a";
                if (protocol == IpV4Protocol.Udp.ToString())
                {
                    from_port = udp.SourcePort.ToString();
                    to_port = udp.DestinationPort.ToString();
                    try
                    {
                        UdpProcessRecord record;
                        if (from_ip == meterState.LocalIP)
                        {
                            record = connMngr.UdpActiveConnections.Where(procReq => procReq.LocalPort.ToString() == from_port).First();
                        }
                        else
                        {
                            record = connMngr.UdpActiveConnections.Where(procReq => procReq.LocalPort.ToString() == to_port).First();
                        }

                        if (record != null)
                        {
                            
                            processName = record.ProcessName;
                        }
                    } catch(Exception) { processName = @"n\a"; }
                    
                }
                else
                {
                    try
                    {
                        TcpProcessRecord record;
                        if (from_ip == meterState.LocalIP)
                        {
                            record = connMngr.TcpActiveConnections.Where(procReq => procReq.LocalPort.ToString() == from_port && procReq.RemotePort.ToString() == to_port).First();
                        } else
                        {
                            record = connMngr.TcpActiveConnections.Where(procReq => procReq.LocalPort.ToString() == to_port && procReq.RemotePort.ToString() == from_port).First();
                        }
                        if (record != null)
                        {
                            processName = record.ProcessName;
                        }
                    } catch (Exception) { processName = @"n\a"; }
                from_port = tcp.SourcePort.ToString();
                    to_port = tcp.DestinationPort.ToString();
                }
                if (!packetFilter.ValidateProcess(processName)) continue;
                ListViewItem item = new ListViewItem(packet.Timestamp.ToString("HH:mm:ss.fff"));
                packet_id++;
                string id = packet_id.ToString();
                item.SubItems.Add(id);
                item.SubItems.Add(from_ip);
                item.SubItems.Add(from_port);
                item.SubItems.Add(to_ip);
                item.SubItems.Add(to_port);
                item.SubItems.Add(packet_size);
                item.SubItems.Add(protocol);
                item.SubItems.Add(processName);
                
                items[iKey] = item;
                iKey++;


            }
            int realItems = items.Where(id => id != null).Count();
           
            if (realItems > 0)
            {
                items =  items.Where(id => id != null).ToArray();
               
            } else
            {
                return;
            }
            
            await Task.Run(() =>
            {
            listView1.Invoke(new Action(() => {
                listView1.BeginUpdate();
                ListView.ListViewItemCollection lvic = new ListView.ListViewItemCollection(listView1);
                try
                {
                    lvic.AddRange(items);
                } catch(Exception)
                {
                    
                }
                
                if (autoscroll.Checked)
                {
                    listView1.EnsureVisible(listView1.Items.Count - 1);
                }
                listView1.EndUpdate();
            }));
            });
            


        }

        
        public void Stop()
        {

            try
            {
                
                if (PcapThread != null)
                {
                    PcapThread.Abort();
                    PcapThread = null;
                }
            }
            catch (Exception) { }
            tracking = false;
            RefreshTimer.Enabled = false;
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            packet_id = 0;
            PacketBuffer.Clear();
            listView1.Items.Clear();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            if (tracking)
                Stop();
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (!tracking)
            Start();
        }

        

        private void PacketStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            if (tracking)
                Stop();
        }

        private void filter_Click(object sender, EventArgs e)
        {
            filterForm.Show();
        }

        private async void avgStats_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
            label3.Invoke( new Action(() =>{
                    label3.Text = "IN " + inPackets.ToString() + " | OUT " + outPackets.ToString();
            }));
                label4.Invoke(new Action(() => {
                    label4.Text = "DL " + (inTraffic / 1024).ToString() + " | UP " + (outTraffic/ 1024).ToString();
                }));
                label5.Invoke(new Action(() => {
                    label5.Text = "Local IP: " + meterState.LocalIP;
                }));
                inPackets = outPackets = inTraffic = outTraffic = 0;
            });
        }
    }
}
