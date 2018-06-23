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
            this.network_connection_lbl = new System.Windows.Forms.Label();
            this.adapters_list = new System.Windows.Forms.ComboBox();
            this.ticksLoop = new System.Windows.Forms.Timer(this.components);
            this.pcapWorker = new System.ComponentModel.BackgroundWorker();
            this.tickRateLbl = new System.Windows.Forms.Label();
            this.pingLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.serverLbl = new System.Windows.Forms.Label();
            this.countryLbl = new System.Windows.Forms.Label();
            this.settings_log_checkobx = new System.Windows.Forms.CheckBox();
            this.retryLoop = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.settings_chart_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_rtss_output = new System.Windows.Forms.CheckBox();
            this.settings_lbl = new System.Windows.Forms.GroupBox();
            this.possible_risks_lbl = new System.Windows.Forms.Label();
            this.settings_netstats_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_traffic_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_ping_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_ip_checkbox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.trafficLbl = new System.Windows.Forms.Label();
            this.graph = new System.Windows.Forms.PictureBox();
            this.settings_lbl.SuspendLayout();
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
            // network_connection_lbl
            // 
            this.network_connection_lbl.AutoSize = true;
            this.network_connection_lbl.BackColor = System.Drawing.Color.Transparent;
            this.network_connection_lbl.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.network_connection_lbl.ForeColor = System.Drawing.Color.Black;
            this.network_connection_lbl.Location = new System.Drawing.Point(5, 207);
            this.network_connection_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.network_connection_lbl.Name = "network_connection_lbl";
            this.network_connection_lbl.Size = new System.Drawing.Size(238, 23);
            this.network_connection_lbl.TabIndex = 15;
            this.network_connection_lbl.Text = "Сетевое подключение:";
            // 
            // adapters_list
            // 
            this.adapters_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.adapters_list.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.adapters_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.adapters_list.FormattingEnabled = true;
            this.adapters_list.ItemHeight = 15;
            this.adapters_list.Location = new System.Drawing.Point(6, 234);
            this.adapters_list.Margin = new System.Windows.Forms.Padding(4);
            this.adapters_list.Name = "adapters_list";
            this.adapters_list.Size = new System.Drawing.Size(241, 23);
            this.adapters_list.TabIndex = 12;
            this.adapters_list.SelectedIndexChanged += new System.EventHandler(this.Adapters_list_SelectedIndexChanged);
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
            // settings_log_checkobx
            // 
            this.settings_log_checkobx.AutoSize = true;
            this.settings_log_checkobx.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_log_checkobx.ForeColor = System.Drawing.Color.Black;
            this.settings_log_checkobx.Location = new System.Drawing.Point(6, 50);
            this.settings_log_checkobx.Name = "settings_log_checkobx";
            this.settings_log_checkobx.Size = new System.Drawing.Size(190, 20);
            this.settings_log_checkobx.TabIndex = 30;
            this.settings_log_checkobx.Text = "Логгировать тикрейт в CSV";
            this.settings_log_checkobx.UseVisualStyleBackColor = true;
            // 
            // retryLoop
            // 
            this.retryLoop.Enabled = true;
            this.retryLoop.Interval = 5000;
            this.retryLoop.Tick += new System.EventHandler(this.RetryTimer_Tick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(9, 301);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(230, 23);
            this.label8.TabIndex = 31;
            this.label8.Text = "Беларуский Айтишник";
            this.label8.Click += new System.EventHandler(this.Label8_Click);
            // 
            // settings_chart_checkbox
            // 
            this.settings_chart_checkbox.AutoSize = true;
            this.settings_chart_checkbox.Checked = true;
            this.settings_chart_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_chart_checkbox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_chart_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_chart_checkbox.Location = new System.Drawing.Point(6, 134);
            this.settings_chart_checkbox.Name = "settings_chart_checkbox";
            this.settings_chart_checkbox.Size = new System.Drawing.Size(128, 20);
            this.settings_chart_checkbox.TabIndex = 33;
            this.settings_chart_checkbox.Text = "График тикрейта";
            this.settings_chart_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_rtss_output
            // 
            this.settings_rtss_output.AutoSize = true;
            this.settings_rtss_output.Checked = true;
            this.settings_rtss_output.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_rtss_output.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_rtss_output.ForeColor = System.Drawing.Color.Black;
            this.settings_rtss_output.Location = new System.Drawing.Point(6, 28);
            this.settings_rtss_output.Name = "settings_rtss_output";
            this.settings_rtss_output.Size = new System.Drawing.Size(138, 20);
            this.settings_rtss_output.TabIndex = 35;
            this.settings_rtss_output.Text = "Вывод через RTSS";
            this.settings_rtss_output.UseVisualStyleBackColor = true;
            this.settings_rtss_output.CheckedChanged += new System.EventHandler(this.Settings_rtss_output_CheckedChanged);
            // 
            // settings_lbl
            // 
            this.settings_lbl.Controls.Add(this.possible_risks_lbl);
            this.settings_lbl.Controls.Add(this.settings_netstats_checkbox);
            this.settings_lbl.Controls.Add(this.label8);
            this.settings_lbl.Controls.Add(this.settings_traffic_checkbox);
            this.settings_lbl.Controls.Add(this.settings_ping_checkbox);
            this.settings_lbl.Controls.Add(this.settings_ip_checkbox);
            this.settings_lbl.Controls.Add(this.settings_chart_checkbox);
            this.settings_lbl.Controls.Add(this.settings_rtss_output);
            this.settings_lbl.Controls.Add(this.settings_log_checkobx);
            this.settings_lbl.Controls.Add(this.network_connection_lbl);
            this.settings_lbl.Controls.Add(this.adapters_list);
            this.settings_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_lbl.Location = new System.Drawing.Point(477, 12);
            this.settings_lbl.Name = "settings_lbl";
            this.settings_lbl.Size = new System.Drawing.Size(256, 334);
            this.settings_lbl.TabIndex = 36;
            this.settings_lbl.TabStop = false;
            this.settings_lbl.Text = "Настройки";
            // 
            // possible_risks_lbl
            // 
            this.possible_risks_lbl.AutoSize = true;
            this.possible_risks_lbl.BackColor = System.Drawing.Color.Transparent;
            this.possible_risks_lbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.possible_risks_lbl.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.possible_risks_lbl.ForeColor = System.Drawing.Color.DarkRed;
            this.possible_risks_lbl.Location = new System.Drawing.Point(9, 270);
            this.possible_risks_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.possible_risks_lbl.Name = "possible_risks_lbl";
            this.possible_risks_lbl.Size = new System.Drawing.Size(158, 18);
            this.possible_risks_lbl.TabIndex = 41;
            this.possible_risks_lbl.Text = "Возможные риски";
            this.possible_risks_lbl.Click += new System.EventHandler(this.Possible_risks_lbl_Click);
            // 
            // settings_netstats_checkbox
            // 
            this.settings_netstats_checkbox.AutoSize = true;
            this.settings_netstats_checkbox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_netstats_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_netstats_checkbox.Location = new System.Drawing.Point(6, 155);
            this.settings_netstats_checkbox.Name = "settings_netstats_checkbox";
            this.settings_netstats_checkbox.Size = new System.Drawing.Size(231, 36);
            this.settings_netstats_checkbox.TabIndex = 40;
            this.settings_netstats_checkbox.Text = "Не проверять сетевую активность\r\n(если тикметр вылетает)\r\n";
            this.settings_netstats_checkbox.UseVisualStyleBackColor = true;
            this.settings_netstats_checkbox.CheckedChanged += new System.EventHandler(this.settings_netstats_checkbox_CheckedChanged);
            // 
            // settings_traffic_checkbox
            // 
            this.settings_traffic_checkbox.AutoSize = true;
            this.settings_traffic_checkbox.Checked = true;
            this.settings_traffic_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_traffic_checkbox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_traffic_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_traffic_checkbox.Location = new System.Drawing.Point(6, 113);
            this.settings_traffic_checkbox.Name = "settings_traffic_checkbox";
            this.settings_traffic_checkbox.Size = new System.Drawing.Size(147, 20);
            this.settings_traffic_checkbox.TabIndex = 39;
            this.settings_traffic_checkbox.Text = "Отображать трафик";
            this.settings_traffic_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_ping_checkbox
            // 
            this.settings_ping_checkbox.AutoSize = true;
            this.settings_ping_checkbox.Checked = true;
            this.settings_ping_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_ping_checkbox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_ping_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_ping_checkbox.Location = new System.Drawing.Point(6, 92);
            this.settings_ping_checkbox.Name = "settings_ping_checkbox";
            this.settings_ping_checkbox.Size = new System.Drawing.Size(182, 20);
            this.settings_ping_checkbox.TabIndex = 37;
            this.settings_ping_checkbox.Text = "Отображать Ping и страну";
            this.settings_ping_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_ip_checkbox
            // 
            this.settings_ip_checkbox.AutoSize = true;
            this.settings_ip_checkbox.Checked = true;
            this.settings_ip_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_ip_checkbox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_ip_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_ip_checkbox.Location = new System.Drawing.Point(6, 71);
            this.settings_ip_checkbox.Name = "settings_ip_checkbox";
            this.settings_ip_checkbox.Size = new System.Drawing.Size(115, 20);
            this.settings_ip_checkbox.TabIndex = 36;
            this.settings_ip_checkbox.Text = "Отображать IP";
            this.settings_ip_checkbox.UseVisualStyleBackColor = true;
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
            this.ClientSize = new System.Drawing.Size(739, 356);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.trafficLbl);
            this.Controls.Add(this.settings_lbl);
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
            this.settings_lbl.ResumeLayout(false);
            this.settings_lbl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label network_connection_lbl;
        private System.Windows.Forms.ComboBox adapters_list;
        private System.Windows.Forms.Timer ticksLoop;
        private System.ComponentModel.BackgroundWorker pcapWorker;
        private System.Windows.Forms.Label tickRateLbl;
        private System.Windows.Forms.Label pingLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label serverLbl;
        private System.Windows.Forms.Label countryLbl;
        private System.Windows.Forms.CheckBox settings_log_checkobx;
        private System.Windows.Forms.Timer retryLoop;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox settings_chart_checkbox;
        private System.Windows.Forms.PictureBox graph;
        private System.Windows.Forms.CheckBox settings_rtss_output;
        private System.Windows.Forms.GroupBox settings_lbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label trafficLbl;
        public System.Windows.Forms.CheckBox settings_traffic_checkbox;
        public System.Windows.Forms.CheckBox settings_ping_checkbox;
        public System.Windows.Forms.CheckBox settings_ip_checkbox;
        private System.Windows.Forms.Label possible_risks_lbl;
        private System.Windows.Forms.CheckBox settings_netstats_checkbox;
    }
}

