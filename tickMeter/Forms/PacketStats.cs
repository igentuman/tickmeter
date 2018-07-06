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

namespace tickMeter
{
    public partial class PacketStats : Form
    {
        List<Packet> PacketBuffer;
        public GUI gui;
        public bool tracking;
        Thread PcapThread;
        public System.Timers.Timer RefreshInfoTimer;

        public BackgroundWorker pcapWorker;
        
        PacketFilter packetFilter;

        public PacketStats()
        {
            InitializeComponent();
            packetFilter = new PacketFilter();
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
            RefreshTimer.Enabled = true;

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
            using (PacketCommunicator communicator = gui.selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 2000))
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
        }

        int packet_id;
        private void RefreshTick(object sender, EventArgs e)
        {
            if (PacketBuffer.Count < 1)
            {
                return;
            }
            Packet packet = PacketBuffer.First();
            IpV4Datagram ip;
            try
            {
                ip = packet.Ethernet.IpV4;
            } catch(Exception) { return; }
            
            UdpDatagram udp = ip.Udp;
            TcpDatagram tcp = ip.Tcp;

            
            
            string from_ip = ip.Source.ToString();
            string to_ip = ip.Destination.ToString();

            string packet_size = ip.TotalLength.ToString();

            string protocol = ip.Protocol.ToString();
            string from_port = "";
            string to_port = "";
            if (protocol == IpV4Protocol.Udp.ToString())
            {
                from_port = udp.SourcePort.ToString();
                to_port = udp.DestinationPort.ToString();
            } else
            {
                from_port = tcp.SourcePort.ToString();
                to_port = tcp.DestinationPort.ToString();
            }

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
            listView1.Items.Add(item);
            if (autoscroll.Checked)
            {
                listView1.EnsureVisible(listView1.Items.Count-1);
            }
                PacketBuffer.Remove(packet);
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

        private void protocol_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            packetFilter.ProtocolFilter = protocol_filter.SelectedItem.ToString();
            Restart();
        }

        private void PacketStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            if (tracking)
                Stop();
        }

        private void From_ip_filter_Leave(object sender, EventArgs e)
        {
            PacketBuffer.Clear();
            packetFilter.SourceIpFilter = from_ip_filter.Text;
        }

        private void From_port_filter_Leave(object sender, EventArgs e)
        {
            PacketBuffer.Clear();
            packetFilter.SourcePortFilter = from_port_filter.Text;
        }

        private void To_ip_filter_Leave(object sender, EventArgs e)
        {
            PacketBuffer.Clear();
            packetFilter.DestIpFilter = to_ip_filter.Text;
        }

        private void Packet_size_filter_Leave(object sender, EventArgs e)
        {
            PacketBuffer.Clear();
            packetFilter.PacketSizeFilter = packet_size_filter.Text;
        }

        private void To_port_filter_Leave(object sender, EventArgs e)
        {
            PacketBuffer.Clear();
            packetFilter.DestPortFilter = to_port_filter.Text;
        }
    }
}
