using System.Windows.Forms;

namespace tickMeter
{
    partial class PacketStats
    {
        public class ListViewNF : ListView
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
            this.process = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.autoscroll = new System.Windows.Forms.CheckBox();
            this.start = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.filter = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.avgStats = new System.Windows.Forms.Timer(this.components);
            from_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            from_port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // from_ip
            // 
            from_ip.Text = "From IP";
            from_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            from_ip.Width = 125;
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
            this.protocol,
            this.process});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(0, 78);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MinimumSize = new System.Drawing.Size(781, 590);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(905, 590);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // time
            // 
            this.time.Text = "Time";
            this.time.Width = 100;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 55;
            // 
            // to_ip
            // 
            this.to_ip.Text = "To IP";
            this.to_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.to_ip.Width = 125;
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
            this.protocol.Width = 80;
            // 
            // process
            // 
            this.process.Text = "Process";
            this.process.Width = 120;
            // 
            // autoscroll
            // 
            this.autoscroll.AutoSize = true;
            this.autoscroll.Location = new System.Drawing.Point(361, 38);
            this.autoscroll.Name = "autoscroll";
            this.autoscroll.Size = new System.Drawing.Size(102, 17);
            this.autoscroll.TabIndex = 16;
            this.autoscroll.Text = "Автопрокрутка";
            this.autoscroll.UseVisualStyleBackColor = true;
            // 
            // start
            // 
            this.start.Image = global::tickMeter.Properties.Resources.movie;
            this.start.Location = new System.Drawing.Point(7, 12);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(66, 60);
            this.start.TabIndex = 17;
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // stop
            // 
            this.stop.Image = global::tickMeter.Properties.Resources.multimedia;
            this.stop.Location = new System.Drawing.Point(79, 13);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(64, 59);
            this.stop.TabIndex = 18;
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 672);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(910, 22);
            this.statusStrip1.TabIndex = 20;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Interval = 5;
            this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTick);
            // 
            // filter
            // 
            this.filter.Image = global::tickMeter.Properties.Resources.tools_icon;
            this.filter.Location = new System.Drawing.Point(219, 13);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(66, 60);
            this.filter.TabIndex = 21;
            this.filter.UseVisualStyleBackColor = false;
            this.filter.Click += new System.EventHandler(this.filter_Click);
            // 
            // clear
            // 
            this.clear.Image = global::tickMeter.Properties.Resources.reload;
            this.clear.Location = new System.Drawing.Point(149, 14);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(64, 60);
            this.clear.TabIndex = 19;
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.autoscroll);
            this.groupBox1.Location = new System.Drawing.Point(301, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 61);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stats";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(601, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 4;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "IN 0  |  OUT 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "IN 0  |  OUT 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kb/s";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Packets/s";
            // 
            // avgStats
            // 
            this.avgStats.Interval = 1000;
            this.avgStats.Tick += new System.EventHandler(this.avgStats_Tick);
            // 
            // PacketStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 694);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.filter);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.start);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(798, 733);
            this.Name = "PacketStats";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Live Packets View";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketStats_FormClosing);
            this.Shown += new System.EventHandler(this.PacketStats_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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

        
        private CheckBox autoscroll;
        private Button start;
        private Button stop;
        private Button clear;
        private StatusStrip statusStrip1;
        public ListView listView1;
        private Timer RefreshTimer;
        private Button filter;
        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Timer avgStats;
        private Label label5;
        private ColumnHeader process;
    }
}