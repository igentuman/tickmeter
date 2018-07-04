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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tickMeter
{
    public partial class PacketStats : Form
    {
        List<Packet> PacketBuffer;

        public GUI gui;
        public bool tracking;

        public PacketStats()
        {
            InitializeComponent();
        }

        public void Start()
        {
            packet_id = 0;
            PacketBuffer = new List<Packet>();
            timer1.Enabled = true;
            if (!backgroundWorker1.IsBusy)
            backgroundWorker1.RunWorkerAsync();
            tracking = true;

        }

        private void PacketStats_Shown(object sender, EventArgs e)
        {
            Start();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (PacketCommunicator communicator = gui.selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                // Check the link layer.
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("This program works only on Ethernet networks!");
                    return;
                }
                using (BerkeleyPacketFilter filter = communicator.CreateFilter("udp"))
                {
                   // communicator.SetFilter(filter);
                }
                // Begin the capture
                communicator.ReceivePackets(0, PacketHandler);
            }
        }

        private void PacketHandler(Packet packet)
        {
            //UdpDatagram udp = packet.Ethernet.IpV4.Udp;
            
            PacketBuffer.Add(packet);
        }
        int packet_id;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PacketBuffer.Count < 1)
            {
                return;
            }
            Packet packet = PacketBuffer.First();
            

            IpV4Datagram ip = packet.Ethernet.IpV4;
            UdpDatagram udp = ip.Udp;
            TcpDatagram tcp = ip.Tcp;

            
            
            string from_ip = ip.Source.ToString();
            string to_ip = ip.Destination.ToString();

            string packet_size = ip.Length.ToString();

            string protocol = ip.Protocol.ToString();
            string from_port = "";
            string to_port = "";
            if (protocol == Protocol.UDP.ToString())
            {
                from_port = udp.SourcePort.ToString();
                to_port = udp.DestinationPort.ToString();
            } else
            {
                from_port = tcp.SourcePort.ToString();
                to_port = tcp.DestinationPort.ToString();
            }

            ListViewItem item = new ListViewItem(packet.Timestamp.ToString());
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
            timer1.Enabled = false;
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            tracking = false;
        }

        private void PacketStats_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tracking)
                Stop();
        }

        private void clear_Click(object sender, EventArgs e)
        {
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
    }
}
