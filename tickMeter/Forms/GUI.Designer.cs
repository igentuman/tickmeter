namespace tickMeter.Forms
{
    partial class GUI
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.tickrate_lbl = new System.Windows.Forms.Label();
            this.ticksLoop = new System.Windows.Forms.Timer(this.components);
            this.tickrate_val = new System.Windows.Forms.Label();
            this.ping_val = new System.Windows.Forms.Label();
            this.ping_lbl = new System.Windows.Forms.Label();
            this.ip_lbl = new System.Windows.Forms.Label();
            this.ip_val = new System.Windows.Forms.Label();
            this.countryLbl = new System.Windows.Forms.Label();
            this.retryLoop = new System.Windows.Forms.Timer(this.components);
            this.traffic_lbl = new System.Windows.Forms.Label();
            this.traffic_val = new System.Windows.Forms.Label();
            this.time_lbl = new System.Windows.Forms.Label();
            this.time_val = new System.Windows.Forms.Label();
            this.webStatsButton = new System.Windows.Forms.PictureBox();
            this.gameProfilesButton = new System.Windows.Forms.PictureBox();
            this.packetStatsBtn = new System.Windows.Forms.PictureBox();
            this.SettingsButton = new System.Windows.Forms.PictureBox();
            this.graph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.webStatsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameProfilesButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packetStatsBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.SuspendLayout();
            // 
            // tickrate_lbl
            // 
            this.tickrate_lbl.AutoSize = true;
            this.tickrate_lbl.BackColor = System.Drawing.Color.Transparent;
            this.tickrate_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tickrate_lbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tickrate_lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.tickrate_lbl.Location = new System.Drawing.Point(12, 9);
            this.tickrate_lbl.Name = "tickrate_lbl";
            this.tickrate_lbl.Size = new System.Drawing.Size(162, 39);
            this.tickrate_lbl.TabIndex = 18;
            this.tickrate_lbl.Text = "Tickrate:";
            this.tickrate_lbl.UseCompatibleTextRendering = true;
            // 
            // ticksLoop
            // 
            this.ticksLoop.Interval = 17;
            this.ticksLoop.Tick += new System.EventHandler(this.TicksLoop_Tick);
            // 
            // tickrate_val
            // 
            this.tickrate_val.AutoSize = true;
            this.tickrate_val.BackColor = System.Drawing.Color.Transparent;
            this.tickrate_val.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tickrate_val.Font = new System.Drawing.Font("Unispace", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tickrate_val.ForeColor = System.Drawing.Color.OrangeRed;
            this.tickrate_val.Location = new System.Drawing.Point(178, 6);
            this.tickrate_val.Name = "tickrate_val";
            this.tickrate_val.Size = new System.Drawing.Size(36, 53);
            this.tickrate_val.TabIndex = 24;
            this.tickrate_val.Text = "0";
            this.tickrate_val.UseCompatibleTextRendering = true;
            this.tickrate_val.UseMnemonic = false;
            // 
            // ping_val
            // 
            this.ping_val.AutoSize = true;
            this.ping_val.BackColor = System.Drawing.Color.Transparent;
            this.ping_val.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ping_val.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ping_val.ForeColor = System.Drawing.Color.OrangeRed;
            this.ping_val.Location = new System.Drawing.Point(178, 92);
            this.ping_val.Name = "ping_val";
            this.ping_val.Size = new System.Drawing.Size(77, 39);
            this.ping_val.TabIndex = 25;
            this.ping_val.Text = "0 ms";
            this.ping_val.UseCompatibleTextRendering = true;
            this.ping_val.UseMnemonic = false;
            // 
            // ping_lbl
            // 
            this.ping_lbl.AutoSize = true;
            this.ping_lbl.BackColor = System.Drawing.Color.Transparent;
            this.ping_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ping_lbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ping_lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.ping_lbl.Location = new System.Drawing.Point(12, 92);
            this.ping_lbl.Name = "ping_lbl";
            this.ping_lbl.Size = new System.Drawing.Size(111, 39);
            this.ping_lbl.TabIndex = 26;
            this.ping_lbl.Text = "Ping :";
            this.ping_lbl.UseCompatibleTextRendering = true;
            // 
            // ip_lbl
            // 
            this.ip_lbl.AutoSize = true;
            this.ip_lbl.BackColor = System.Drawing.Color.Transparent;
            this.ip_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ip_lbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ip_lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.ip_lbl.Location = new System.Drawing.Point(12, 53);
            this.ip_lbl.Name = "ip_lbl";
            this.ip_lbl.Size = new System.Drawing.Size(60, 39);
            this.ip_lbl.TabIndex = 27;
            this.ip_lbl.Text = "IP:";
            this.ip_lbl.UseCompatibleTextRendering = true;
            // 
            // ip_val
            // 
            this.ip_val.AutoSize = true;
            this.ip_val.BackColor = System.Drawing.Color.Transparent;
            this.ip_val.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ip_val.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ip_val.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ip_val.ForeColor = System.Drawing.Color.OrangeRed;
            this.ip_val.Location = new System.Drawing.Point(178, 53);
            this.ip_val.Name = "ip_val";
            this.ip_val.Size = new System.Drawing.Size(264, 39);
            this.ip_val.TabIndex = 28;
            this.ip_val.Text = "000.000.000.000";
            this.ip_val.UseCompatibleTextRendering = true;
            this.ip_val.UseMnemonic = false;
            this.ip_val.Click += new System.EventHandler(this.ServerLbl_Click);
            // 
            // countryLbl
            // 
            this.countryLbl.AutoSize = true;
            this.countryLbl.BackColor = System.Drawing.Color.Transparent;
            this.countryLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.countryLbl.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countryLbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.countryLbl.Location = new System.Drawing.Point(248, 20);
            this.countryLbl.Name = "countryLbl";
            this.countryLbl.Size = new System.Drawing.Size(0, 28);
            this.countryLbl.TabIndex = 29;
            this.countryLbl.UseCompatibleTextRendering = true;
            // 
            // retryLoop
            // 
            this.retryLoop.Enabled = true;
            this.retryLoop.Interval = 5000;
            this.retryLoop.Tick += new System.EventHandler(this.RetryTimer_Tick);
            // 
            // traffic_lbl
            // 
            this.traffic_lbl.AutoSize = true;
            this.traffic_lbl.BackColor = System.Drawing.Color.Transparent;
            this.traffic_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.traffic_lbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traffic_lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.traffic_lbl.Location = new System.Drawing.Point(11, 131);
            this.traffic_lbl.Name = "traffic_lbl";
            this.traffic_lbl.Size = new System.Drawing.Size(128, 39);
            this.traffic_lbl.TabIndex = 38;
            this.traffic_lbl.Text = "UP/DL :";
            this.traffic_lbl.UseCompatibleTextRendering = true;
            // 
            // traffic_val
            // 
            this.traffic_val.AutoSize = true;
            this.traffic_val.BackColor = System.Drawing.Color.Transparent;
            this.traffic_val.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.traffic_val.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traffic_val.ForeColor = System.Drawing.Color.OrangeRed;
            this.traffic_val.Location = new System.Drawing.Point(177, 131);
            this.traffic_val.Name = "traffic_val";
            this.traffic_val.Size = new System.Drawing.Size(145, 39);
            this.traffic_val.TabIndex = 37;
            this.traffic_val.Text = "0 / 0 Mb";
            this.traffic_val.UseCompatibleTextRendering = true;
            this.traffic_val.UseMnemonic = false;
            // 
            // time_lbl
            // 
            this.time_lbl.AutoSize = true;
            this.time_lbl.BackColor = System.Drawing.Color.Transparent;
            this.time_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.time_lbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_lbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.time_lbl.Location = new System.Drawing.Point(12, 172);
            this.time_lbl.Name = "time_lbl";
            this.time_lbl.Size = new System.Drawing.Size(111, 39);
            this.time_lbl.TabIndex = 42;
            this.time_lbl.Text = "Time :";
            this.time_lbl.UseCompatibleTextRendering = true;
            // 
            // time_val
            // 
            this.time_val.AutoSize = true;
            this.time_val.BackColor = System.Drawing.Color.Transparent;
            this.time_val.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.time_val.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_val.ForeColor = System.Drawing.Color.OrangeRed;
            this.time_val.Location = new System.Drawing.Point(178, 172);
            this.time_val.Name = "time_val";
            this.time_val.Size = new System.Drawing.Size(94, 39);
            this.time_val.TabIndex = 41;
            this.time_val.Text = "00:00";
            this.time_val.UseCompatibleTextRendering = true;
            this.time_val.UseMnemonic = false;
            // 
            // webStatsButton
            // 
            this.webStatsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.webStatsButton.Image = global::tickMeter.Properties.Resources.bar;
            this.webStatsButton.Location = new System.Drawing.Point(217, 2);
            this.webStatsButton.Name = "webStatsButton";
            this.webStatsButton.Size = new System.Drawing.Size(48, 48);
            this.webStatsButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.webStatsButton.TabIndex = 44;
            this.webStatsButton.TabStop = false;
            this.webStatsButton.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // gameProfilesButton
            // 
            this.gameProfilesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gameProfilesButton.Image = global::tickMeter.Properties.Resources.Board_Games_red;
            this.gameProfilesButton.Location = new System.Drawing.Point(271, 2);
            this.gameProfilesButton.Name = "gameProfilesButton";
            this.gameProfilesButton.Size = new System.Drawing.Size(48, 48);
            this.gameProfilesButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gameProfilesButton.TabIndex = 43;
            this.gameProfilesButton.TabStop = false;
            this.gameProfilesButton.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // packetStatsBtn
            // 
            this.packetStatsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.packetStatsBtn.Image = global::tickMeter.Properties.Resources._switch;
            this.packetStatsBtn.Location = new System.Drawing.Point(325, 2);
            this.packetStatsBtn.Name = "packetStatsBtn";
            this.packetStatsBtn.Size = new System.Drawing.Size(48, 48);
            this.packetStatsBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.packetStatsBtn.TabIndex = 40;
            this.packetStatsBtn.TabStop = false;
            this.packetStatsBtn.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingsButton.Image = global::tickMeter.Properties.Resources.settings;
            this.SettingsButton.Location = new System.Drawing.Point(379, 2);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(48, 48);
            this.SettingsButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SettingsButton.TabIndex = 39;
            this.SettingsButton.TabStop = false;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // graph
            // 
            this.graph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.graph.Enabled = false;
            this.graph.Image = global::tickMeter.Properties.Resources.grid;
            this.graph.InitialImage = global::tickMeter.Properties.Resources.grid;
            this.graph.Location = new System.Drawing.Point(11, 214);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(416, 175);
            this.graph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.graph.TabIndex = 34;
            this.graph.TabStop = false;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(434, 395);
            this.Controls.Add(this.webStatsButton);
            this.Controls.Add(this.gameProfilesButton);
            this.Controls.Add(this.time_lbl);
            this.Controls.Add(this.time_val);
            this.Controls.Add(this.packetStatsBtn);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.traffic_lbl);
            this.Controls.Add(this.traffic_val);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.countryLbl);
            this.Controls.Add(this.ip_val);
            this.Controls.Add(this.ip_lbl);
            this.Controls.Add(this.ping_lbl);
            this.Controls.Add(this.ping_val);
            this.Controls.Add(this.tickrate_val);
            this.Controls.Add(this.tickrate_lbl);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "tickMeter 1.8";
            this.TransparencyKey = System.Drawing.SystemColors.Info;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GUI_FormClosed);
            this.Load += new System.EventHandler(this.GUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webStatsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameProfilesButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packetStatsBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer ticksLoop;
        private System.Windows.Forms.Label tickrate_val;
        private System.Windows.Forms.Label ping_val;
        private System.Windows.Forms.Label ip_val;
        private System.Windows.Forms.Label countryLbl;
        private System.Windows.Forms.Timer retryLoop;
        private System.Windows.Forms.PictureBox graph;
        private System.Windows.Forms.Label traffic_val;
        private System.Windows.Forms.PictureBox SettingsButton;
        public System.Windows.Forms.Label tickrate_lbl;
        public System.Windows.Forms.Label ping_lbl;
        public System.Windows.Forms.Label ip_lbl;
        public System.Windows.Forms.Label traffic_lbl;
        private System.Windows.Forms.PictureBox packetStatsBtn;
        public System.Windows.Forms.Label time_lbl;
        private System.Windows.Forms.Label time_val;
        private System.Windows.Forms.PictureBox gameProfilesButton;
        private System.Windows.Forms.PictureBox webStatsButton;
    }
}

