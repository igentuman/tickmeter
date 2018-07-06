using System.Windows.Forms;

namespace tickMeter
{
    partial class PacketStats
    {
        class ListViewNF : ListView
        {
            public ListViewNF()
            {
                //Activate double buffering
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

                //Enable the OnNotifyMessage event so we get a chance to filter out 
                // Windows messages before they get to the form's WndProc
                this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            }

            protected override void OnNotifyMessage(Message m)
            {
                //Filter out the WM_ERASEBKGND message
                if (m.Msg != 0x14)
                {
                    base.OnNotifyMessage(m);
                }
            }
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader from_ip;
            System.Windows.Forms.ColumnHeader from_port;
            this.listView1 = new System.Windows.Forms.ListView();
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.to_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.to_port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.packet_size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.protocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.autoscroll = new System.Windows.Forms.CheckBox();
            this.start = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.protocol_filter = new System.Windows.Forms.ComboBox();
            this.packet_size_filter = new System.Windows.Forms.TextBox();
            this.to_port_filter = new System.Windows.Forms.TextBox();
            this.to_ip_filter = new System.Windows.Forms.TextBox();
            this.from_ip_filter = new System.Windows.Forms.TextBox();
            this.from_port_filter = new System.Windows.Forms.TextBox();
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            from_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            from_port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // from_ip
            // 
            from_ip.Text = "From IP";
            from_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            from_ip.Width = 130;
            // 
            // from_port
            // 
            from_port.Text = "From Port";
            from_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            from_port.Width = 90;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.AllowColumnReorder = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.time,
            this.ID,
            from_ip,
            from_port,
            this.to_ip,
            this.to_port,
            this.packet_size,
            this.protocol});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(0, 80);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MaximumSize = new System.Drawing.Size(781, 590);
            this.listView1.MinimumSize = new System.Drawing.Size(781, 590);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(781, 590);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // time
            // 
            this.time.Text = "Time";
            this.time.Width = 124;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // to_ip
            // 
            this.to_ip.Text = "To IP";
            this.to_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.to_ip.Width = 130;
            // 
            // to_port
            // 
            this.to_port.Text = "To Port";
            this.to_port.Width = 90;
            // 
            // packet_size
            // 
            this.packet_size.Text = "Packet Size";
            this.packet_size.Width = 80;
            // 
            // protocol
            // 
            this.protocol.Text = "Protocol";
            this.protocol.Width = 100;
            // 
            // autoscroll
            // 
            this.autoscroll.AutoSize = true;
            this.autoscroll.Location = new System.Drawing.Point(11, 55);
            this.autoscroll.Name = "autoscroll";
            this.autoscroll.Size = new System.Drawing.Size(102, 17);
            this.autoscroll.TabIndex = 16;
            this.autoscroll.Text = "Автопрокрутка";
            this.autoscroll.UseVisualStyleBackColor = true;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(12, 12);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(51, 23);
            this.start.TabIndex = 17;
            this.start.Text = "start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // stop
            // 
            this.stop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stop.Location = new System.Drawing.Point(69, 12);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(51, 23);
            this.stop.TabIndex = 18;
            this.stop.Text = "stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(126, 12);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(51, 23);
            this.clear.TabIndex = 19;
            this.clear.Text = "clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 672);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(782, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // protocol_filter
            // 
            this.protocol_filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocol_filter.FormattingEnabled = true;
            this.protocol_filter.Items.AddRange(new object[] {
            "",
            "udp",
            "tcp",
            "tcp and udp"});
            this.protocol_filter.Location = new System.Drawing.Point(682, 50);
            this.protocol_filter.Name = "protocol_filter";
            this.protocol_filter.Size = new System.Drawing.Size(98, 21);
            this.protocol_filter.TabIndex = 21;
            this.protocol_filter.SelectedIndexChanged += new System.EventHandler(this.protocol_filter_SelectedIndexChanged);
            // 
            // packet_size_filter
            // 
            this.packet_size_filter.Location = new System.Drawing.Point(601, 50);
            this.packet_size_filter.Name = "packet_size_filter";
            this.packet_size_filter.Size = new System.Drawing.Size(75, 20);
            this.packet_size_filter.TabIndex = 22;
            this.packet_size_filter.Leave += new System.EventHandler(this.Packet_size_filter_Leave);
            // 
            // to_port_filter
            // 
            this.to_port_filter.Location = new System.Drawing.Point(512, 50);
            this.to_port_filter.Name = "to_port_filter";
            this.to_port_filter.Size = new System.Drawing.Size(81, 20);
            this.to_port_filter.TabIndex = 23;
            this.to_port_filter.Leave += new System.EventHandler(this.To_port_filter_Leave);
            // 
            // to_ip_filter
            // 
            this.to_ip_filter.Location = new System.Drawing.Point(381, 50);
            this.to_ip_filter.Name = "to_ip_filter";
            this.to_ip_filter.Size = new System.Drawing.Size(125, 20);
            this.to_ip_filter.TabIndex = 24;
            this.to_ip_filter.Leave += new System.EventHandler(this.To_ip_filter_Leave);
            // 
            // from_ip_filter
            // 
            this.from_ip_filter.Location = new System.Drawing.Point(163, 51);
            this.from_ip_filter.Name = "from_ip_filter";
            this.from_ip_filter.Size = new System.Drawing.Size(125, 20);
            this.from_ip_filter.TabIndex = 25;
            this.from_ip_filter.Leave += new System.EventHandler(this.From_ip_filter_Leave);
            // 
            // from_port_filter
            // 
            this.from_port_filter.Location = new System.Drawing.Point(294, 51);
            this.from_port_filter.Name = "from_port_filter";
            this.from_port_filter.Size = new System.Drawing.Size(81, 20);
            this.from_port_filter.TabIndex = 26;
            this.from_port_filter.Leave += new System.EventHandler(this.From_port_filter_Leave);
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Interval = 1;
            this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTick);
            // 
            // PacketStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 694);
            this.Controls.Add(this.from_port_filter);
            this.Controls.Add(this.from_ip_filter);
            this.Controls.Add(this.to_ip_filter);
            this.Controls.Add(this.to_port_filter);
            this.Controls.Add(this.packet_size_filter);
            this.Controls.Add(this.protocol_filter);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.start);
            this.Controls.Add(this.autoscroll);
            this.Controls.Add(this.listView1);
            this.MaximumSize = new System.Drawing.Size(798, 733);
            this.MinimumSize = new System.Drawing.Size(798, 733);
            this.Name = "PacketStats";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Live Packets View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketStats_FormClosing);
            this.Shown += new System.EventHandler(this.PacketStats_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader to_ip;
        private System.Windows.Forms.ColumnHeader packet_size;
        private System.Windows.Forms.ColumnHeader protocol;
        private System.Windows.Forms.ColumnHeader to_port;

        private System.Windows.Forms.Timer timer1;
        private CheckBox autoscroll;
        private Button start;
        private Button stop;
        private Button clear;
        private StatusStrip statusStrip1;
        private TextBox packet_size_filter;
        private TextBox to_port_filter;
        private TextBox to_ip_filter;
        private TextBox from_ip_filter;
        private TextBox from_port_filter;
        public ComboBox protocol_filter;
        public ListView listView1;
        private Timer RefreshTimer;
    }
}