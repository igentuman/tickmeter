﻿namespace tickMeter
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
            this.adapters_label = new System.Windows.Forms.Label();
            this.adapters_list = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.RetryTimer = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.settings_chart_checkbox = new System.Windows.Forms.CheckBox();
            this.graph = new System.Windows.Forms.PictureBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.settings_ip_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_ping_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_traffic_checkbox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // adapters_label
            // 
            this.adapters_label.AutoSize = true;
            this.adapters_label.BackColor = System.Drawing.Color.Transparent;
            this.adapters_label.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.adapters_label.ForeColor = System.Drawing.Color.Black;
            this.adapters_label.Location = new System.Drawing.Point(11, 218);
            this.adapters_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.adapters_label.Name = "adapters_label";
            this.adapters_label.Size = new System.Drawing.Size(323, 23);
            this.adapters_label.TabIndex = 15;
            this.adapters_label.Text = "Выбери сетевое подключение :";
            // 
            // adapters_list
            // 
            this.adapters_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.adapters_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.adapters_list.FormattingEnabled = true;
            this.adapters_list.ItemHeight = 24;
            this.adapters_list.Location = new System.Drawing.Point(12, 245);
            this.adapters_list.Margin = new System.Windows.Forms.Padding(4);
            this.adapters_list.Name = "adapters_list";
            this.adapters_list.Size = new System.Drawing.Size(415, 32);
            this.adapters_list.TabIndex = 12;
            this.adapters_list.SelectedIndexChanged += new System.EventHandler(this.adapters_list_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Unispace", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(178, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 53);
            this.label1.TabIndex = 24;
            this.label1.Text = "0";
            this.label1.UseCompatibleTextRendering = true;
            this.label1.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(178, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 39);
            this.label2.TabIndex = 25;
            this.label2.Text = "0 ms";
            this.label2.UseCompatibleTextRendering = true;
            this.label2.UseMnemonic = false;
            // 
            // timer3
            // 
            this.timer3.Interval = 5000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.OrangeRed;
            this.label6.Location = new System.Drawing.Point(178, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(264, 39);
            this.label6.TabIndex = 28;
            this.label6.Text = "000.000.000.000";
            this.label6.UseCompatibleTextRendering = true;
            this.label6.UseMnemonic = false;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.OrangeRed;
            this.label7.Location = new System.Drawing.Point(248, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 28);
            this.label7.TabIndex = 29;
            this.label7.UseCompatibleTextRendering = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(6, 57);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(232, 23);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "Логгировать тикрейт в CSV";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // RetryTimer
            // 
            this.RetryTimer.Enabled = true;
            this.RetryTimer.Interval = 5000;
            this.RetryTimer.Tick += new System.EventHandler(this.RetryTimer_Tick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(9, 190);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(272, 18);
            this.label8.TabIndex = 31;
            this.label8.Text = "Youtube: Беларуский Айтишник";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // settings_chart_checkbox
            // 
            this.settings_chart_checkbox.AutoSize = true;
            this.settings_chart_checkbox.Enabled = false;
            this.settings_chart_checkbox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_chart_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_chart_checkbox.Location = new System.Drawing.Point(6, 178);
            this.settings_chart_checkbox.Name = "settings_chart_checkbox";
            this.settings_chart_checkbox.Size = new System.Drawing.Size(154, 23);
            this.settings_chart_checkbox.TabIndex = 33;
            this.settings_chart_checkbox.Text = "График тикрейта";
            this.settings_chart_checkbox.UseVisualStyleBackColor = true;
            // 
            // graph
            // 
            this.graph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.graph.Enabled = false;
            this.graph.Image = global::tickMeter.Properties.Resources.grid;
            this.graph.InitialImage = global::tickMeter.Properties.Resources.grid;
            this.graph.Location = new System.Drawing.Point(15, 399);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(418, 177);
            this.graph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.graph.TabIndex = 34;
            this.graph.TabStop = false;
            this.graph.Visible = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox3.ForeColor = System.Drawing.Color.Black;
            this.checkBox3.Location = new System.Drawing.Point(6, 28);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(163, 23);
            this.checkBox3.TabIndex = 35;
            this.checkBox3.Text = "Вывод через RTSS";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.settings_traffic_checkbox);
            this.groupBox1.Controls.Add(this.settings_ping_checkbox);
            this.groupBox1.Controls.Add(this.settings_ip_checkbox);
            this.groupBox1.Controls.Add(this.settings_chart_checkbox);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(477, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 265);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки";
            // 
            // settings_ip_checkbox
            // 
            this.settings_ip_checkbox.AutoSize = true;
            this.settings_ip_checkbox.Checked = true;
            this.settings_ip_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_ip_checkbox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_ip_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_ip_checkbox.Location = new System.Drawing.Point(6, 91);
            this.settings_ip_checkbox.Name = "settings_ip_checkbox";
            this.settings_ip_checkbox.Size = new System.Drawing.Size(139, 23);
            this.settings_ip_checkbox.TabIndex = 36;
            this.settings_ip_checkbox.Text = "Отображать IP";
            this.settings_ip_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_ping_checkbox
            // 
            this.settings_ping_checkbox.AutoSize = true;
            this.settings_ping_checkbox.Checked = true;
            this.settings_ping_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_ping_checkbox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_ping_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_ping_checkbox.Location = new System.Drawing.Point(6, 120);
            this.settings_ping_checkbox.Name = "settings_ping_checkbox";
            this.settings_ping_checkbox.Size = new System.Drawing.Size(223, 23);
            this.settings_ping_checkbox.TabIndex = 37;
            this.settings_ping_checkbox.Text = "Отображать Ping и страну";
            this.settings_ping_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_traffic_checkbox
            // 
            this.settings_traffic_checkbox.AutoSize = true;
            this.settings_traffic_checkbox.Checked = true;
            this.settings_traffic_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_traffic_checkbox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settings_traffic_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_traffic_checkbox.Location = new System.Drawing.Point(6, 149);
            this.settings_traffic_checkbox.Name = "settings_traffic_checkbox";
            this.settings_traffic_checkbox.Size = new System.Drawing.Size(178, 23);
            this.settings_traffic_checkbox.TabIndex = 39;
            this.settings_traffic_checkbox.Text = "Отображать трафик";
            this.settings_traffic_checkbox.UseVisualStyleBackColor = true;
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.OrangeRed;
            this.label10.Location = new System.Drawing.Point(178, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 39);
            this.label10.TabIndex = 37;
            this.label10.Text = "0 / 0 Mb";
            this.label10.UseCompatibleTextRendering = true;
            this.label10.UseMnemonic = false;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(739, 282);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.adapters_label);
            this.Controls.Add(this.adapters_list);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "tickMeter";
            this.TransparencyKey = System.Drawing.SystemColors.Info;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GUI_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label adapters_label;
        private System.Windows.Forms.ComboBox adapters_list;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Timer RetryTimer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox settings_chart_checkbox;
        private System.Windows.Forms.PictureBox graph;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox settings_traffic_checkbox;
        private System.Windows.Forms.CheckBox settings_ping_checkbox;
        private System.Windows.Forms.CheckBox settings_ip_checkbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

