namespace tickMeter.Forms
{
    partial class ProfileEditForm
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.process_filter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.from_port_filter = new System.Windows.Forms.TextBox();
            this.from_ip_filter = new System.Windows.Forms.TextBox();
            this.to_ip_filter = new System.Windows.Forms.TextBox();
            this.to_port_filter = new System.Windows.Forms.TextBox();
            this.packet_size_filter = new System.Windows.Forms.TextBox();
            this.protocol_filter = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.require_process = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.profile_name = new System.Windows.Forms.TextBox();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel1.Location = new System.Drawing.Point(124, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(138, 16);
            this.linkLabel1.TabIndex = 58;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Помощь по фильтру";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "Process Name";
            // 
            // process_filter
            // 
            this.process_filter.Location = new System.Drawing.Point(10, 320);
            this.process_filter.Name = "process_filter";
            this.process_filter.Size = new System.Drawing.Size(252, 20);
            this.process_filter.TabIndex = 56;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 416);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 55;
            this.label6.Text = "Protocol";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Packet Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "To Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "To IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "From Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "From IP";
            // 
            // from_port_filter
            // 
            this.from_port_filter.Location = new System.Drawing.Point(10, 95);
            this.from_port_filter.Name = "from_port_filter";
            this.from_port_filter.Size = new System.Drawing.Size(252, 20);
            this.from_port_filter.TabIndex = 49;
            // 
            // from_ip_filter
            // 
            this.from_ip_filter.Location = new System.Drawing.Point(10, 43);
            this.from_ip_filter.Name = "from_ip_filter";
            this.from_ip_filter.Size = new System.Drawing.Size(252, 20);
            this.from_ip_filter.TabIndex = 48;
            // 
            // to_ip_filter
            // 
            this.to_ip_filter.Location = new System.Drawing.Point(10, 154);
            this.to_ip_filter.Name = "to_ip_filter";
            this.to_ip_filter.Size = new System.Drawing.Size(252, 20);
            this.to_ip_filter.TabIndex = 47;
            // 
            // to_port_filter
            // 
            this.to_port_filter.Location = new System.Drawing.Point(10, 215);
            this.to_port_filter.Name = "to_port_filter";
            this.to_port_filter.Size = new System.Drawing.Size(252, 20);
            this.to_port_filter.TabIndex = 46;
            // 
            // packet_size_filter
            // 
            this.packet_size_filter.Location = new System.Drawing.Point(10, 269);
            this.packet_size_filter.Name = "packet_size_filter";
            this.packet_size_filter.Size = new System.Drawing.Size(252, 20);
            this.packet_size_filter.TabIndex = 45;
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
            this.protocol_filter.Location = new System.Drawing.Point(118, 408);
            this.protocol_filter.Name = "protocol_filter";
            this.protocol_filter.Size = new System.Drawing.Size(144, 21);
            this.protocol_filter.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 357);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 13);
            this.label8.TabIndex = 60;
            this.label8.Text = "Require running Process Name";
            // 
            // require_process
            // 
            this.require_process.Location = new System.Drawing.Point(10, 373);
            this.require_process.Name = "require_process";
            this.require_process.Size = new System.Drawing.Size(252, 20);
            this.require_process.TabIndex = 59;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 454);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 62;
            this.label9.Text = "Profile Name";
            // 
            // profile_name
            // 
            this.profile_name.Location = new System.Drawing.Point(10, 470);
            this.profile_name.Name = "profile_name";
            this.profile_name.Size = new System.Drawing.Size(252, 20);
            this.profile_name.TabIndex = 61;
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Image = global::tickMeter.Properties.Resources.check;
            this.ApplyBtn.Location = new System.Drawing.Point(9, 496);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(75, 68);
            this.ApplyBtn.TabIndex = 63;
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // ProfileEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 571);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.profile_name);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.require_process);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.process_filter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.from_port_filter);
            this.Controls.Add(this.from_ip_filter);
            this.Controls.Add(this.to_ip_filter);
            this.Controls.Add(this.to_port_filter);
            this.Controls.Add(this.packet_size_filter);
            this.Controls.Add(this.protocol_filter);
            this.Name = "ProfileEditForm";
            this.Text = "ProfileEditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileEditForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox protocol_filter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button ApplyBtn;
        public System.Windows.Forms.TextBox process_filter;
        public System.Windows.Forms.TextBox from_port_filter;
        public System.Windows.Forms.TextBox from_ip_filter;
        public System.Windows.Forms.TextBox to_ip_filter;
        public System.Windows.Forms.TextBox to_port_filter;
        public System.Windows.Forms.TextBox packet_size_filter;
        public System.Windows.Forms.TextBox require_process;
        public System.Windows.Forms.TextBox profile_name;
    }
}