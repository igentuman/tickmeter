using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tickMeter.Classes;

namespace tickMeter.Forms
{

    public partial class PacketFilterForm : Form
    {
        public PacketFilter packetFilter;
        public ProfileEditForm profileForm;

        public PacketFilterForm()
        {
            InitializeComponent();
            profileForm = new ProfileEditForm();
            

        }

        private void ApplyFilter()
        {
            try
            {
                packetFilter.PacketSizeFilter   = packet_size_filter.Text;
                packetFilter.DestIpFilter       = to_ip_filter.Text;
                packetFilter.SourcePortFilter   = from_port_filter.Text;
                packetFilter.SourceIpFilter     = from_ip_filter.Text;
                packetFilter.DestPortFilter     = to_port_filter.Text;
                packetFilter.ProcessFilter      = process_filter.Text;
                packetFilter.ProtocolFilter     = protocol_filter.SelectedItem.ToString();
                

            } catch(Exception) { }
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            ApplyFilter();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            packet_size_filter.Text = "";
            to_ip_filter.Text = "";
            from_port_filter.Text = "";
            from_ip_filter.Text = "";
            protocol_filter.SelectedIndex = 0;
            to_port_filter.Text = "";
            ApplyFilter();
        }

        private void PacketFilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://bitbucket.org/dvman8bit/tickmeter/wiki/Filter");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            profileForm.Show();
            profileForm.packet_size_filter.Text = packet_size_filter.Text;

            profileForm.to_ip_filter.Text = to_ip_filter.Text;
            profileForm.from_port_filter.Text = from_port_filter.Text;
            profileForm.from_ip_filter.Text = from_ip_filter.Text;
            profileForm.to_port_filter.Text = to_port_filter.Text;
            profileForm.process_filter.Text = process_filter.Text;
            profileForm.protocol_filter.SelectedIndex = protocol_filter.SelectedIndex;
            profileForm.profile_name.Text = process_filter.Text.ToUpper();
        }
    }
}
