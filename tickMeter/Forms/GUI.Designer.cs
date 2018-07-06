namespace tickMeter
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
            this.label3 = new System.Windows.Forms.Label();
            this.ticksLoop = new System.Windows.Forms.Timer(this.components);
            this.pcapWorker = new System.ComponentModel.BackgroundWorker();
            this.tickRateLbl = new System.Windows.Forms.Label();
            this.pingLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.serverLbl = new System.Windows.Forms.Label();
            this.countryLbl = new System.Windows.Forms.Label();
            this.retryLoop = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.trafficLbl = new System.Windows.Forms.Label();
            this.SettingsButton = new System.Windows.Forms.PictureBox();
            this.graph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.OrangeRed;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 39);
            this.label3.TabIndex = 18;
            this.label3.Text = "Tickrate:";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // ticksLoop
            // 
            this.ticksLoop.Interval = 1000;
            this.ticksLoop.Tick += new System.EventHandler(this.TicksLoop_Tick);
            // 
            // pcapWorker
            // 
            this.pcapWorker.WorkerSupportsCancellation = true;
            this.pcapWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PcapWorker_DoWork);
            this.pcapWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.PcapWorker_RunWorkerCompleted);
            // 
            // tickRateLbl
            // 
            this.tickRateLbl.AutoSize = true;
            this.tickRateLbl.BackColor = System.Drawing.Color.Transparent;
            this.tickRateLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tickRateLbl.Font = new System.Drawing.Font("Unispace", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tickRateLbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.tickRateLbl.Location = new System.Drawing.Point(178, 6);
            this.tickRateLbl.Name = "tickRateLbl";
            this.tickRateLbl.Size = new System.Drawing.Size(36, 53);
            this.tickRateLbl.TabIndex = 24;
            this.tickRateLbl.Text = "0";
            this.tickRateLbl.UseCompatibleTextRendering = true;
            this.tickRateLbl.UseMnemonic = false;
            // 
            // pingLbl
            // 
            this.pingLbl.AutoSize = true;
            this.pingLbl.BackColor = System.Drawing.Color.Transparent;
            this.pingLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pingLbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingLbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.pingLbl.Location = new System.Drawing.Point(178, 94);
            this.pingLbl.Name = "pingLbl";
            this.pingLbl.Size = new System.Drawing.Size(77, 39);
            this.pingLbl.TabIndex = 25;
            this.pingLbl.Text = "0 ms";
            this.pingLbl.UseCompatibleTextRendering = true;
            this.pingLbl.UseMnemonic = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.OrangeRed;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 39);
            this.label4.TabIndex = 26;
            this.label4.Text = "Ping :";
            this.label4.UseCompatibleTextRendering = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.OrangeRed;
            this.label5.Location = new System.Drawing.Point(12, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 39);
            this.label5.TabIndex = 27;
            this.label5.Text = "IP:";
            this.label5.UseCompatibleTextRendering = true;
            // 
            // serverLbl
            // 
            this.serverLbl.AutoSize = true;
            this.serverLbl.BackColor = System.Drawing.Color.Transparent;
            this.serverLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.serverLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serverLbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverLbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.serverLbl.Location = new System.Drawing.Point(178, 53);
            this.serverLbl.Name = "serverLbl";
            this.serverLbl.Size = new System.Drawing.Size(264, 39);
            this.serverLbl.TabIndex = 28;
            this.serverLbl.Text = "000.000.000.000";
            this.serverLbl.UseCompatibleTextRendering = true;
            this.serverLbl.UseMnemonic = false;
            this.serverLbl.Click += new System.EventHandler(this.ServerLbl_Click);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.OrangeRed;
            this.label9.Location = new System.Drawing.Point(12, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 39);
            this.label9.TabIndex = 38;
            this.label9.Text = "UP/DL :";
            this.label9.UseCompatibleTextRendering = true;
            // 
            // trafficLbl
            // 
            this.trafficLbl.AutoSize = true;
            this.trafficLbl.BackColor = System.Drawing.Color.Transparent;
            this.trafficLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.trafficLbl.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trafficLbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.trafficLbl.Location = new System.Drawing.Point(178, 133);
            this.trafficLbl.Name = "trafficLbl";
            this.trafficLbl.Size = new System.Drawing.Size(145, 39);
            this.trafficLbl.TabIndex = 37;
            this.trafficLbl.Text = "0 / 0 Mb";
            this.trafficLbl.UseCompatibleTextRendering = true;
            this.trafficLbl.UseMnemonic = false;
            // 
            // SettingsButton
            // 
            this.SettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingsButton.Image = global::tickMeter.Properties.Resources.gear;
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
            this.graph.Location = new System.Drawing.Point(11, 175);
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
            this.ClientSize = new System.Drawing.Size(434, 356);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.trafficLbl);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.countryLbl);
            this.Controls.Add(this.serverLbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pingLbl);
            this.Controls.Add(this.tickRateLbl);
            this.Controls.Add(this.label3);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "tickMeter 1.5";
            this.TransparencyKey = System.Drawing.SystemColors.Info;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GUI_FormClosed);
            this.Load += new System.EventHandler(this.GUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SettingsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer ticksLoop;
        private System.ComponentModel.BackgroundWorker pcapWorker;
        private System.Windows.Forms.Label tickRateLbl;
        private System.Windows.Forms.Label pingLbl;
        private System.Windows.Forms.Label serverLbl;
        private System.Windows.Forms.Label countryLbl;
        private System.Windows.Forms.Timer retryLoop;
        private System.Windows.Forms.PictureBox graph;
        private System.Windows.Forms.Label trafficLbl;
        private System.Windows.Forms.PictureBox SettingsButton;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label9;
    }
}

