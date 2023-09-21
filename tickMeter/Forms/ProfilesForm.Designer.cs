namespace tickMeter.Forms
{
    partial class ProfilesForm
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
            this.built_in_profiles = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.custom_profiles = new System.Windows.Forms.CheckedListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // built_in_profiles
            // 
            this.built_in_profiles.CheckOnClick = true;
            this.built_in_profiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.built_in_profiles.FormattingEnabled = true;
            this.built_in_profiles.Items.AddRange(new object[] {
            "PUBG",
            "Dead By Daylight"});
            this.built_in_profiles.Location = new System.Drawing.Point(12, 36);
            this.built_in_profiles.Name = "built_in_profiles";
            this.built_in_profiles.Size = new System.Drawing.Size(236, 130);
            this.built_in_profiles.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Встроенные профили";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Настроенные профили";
            // 
            // custom_profiles
            // 
            this.custom_profiles.CheckOnClick = true;
            this.custom_profiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.custom_profiles.FormattingEnabled = true;
            this.custom_profiles.Location = new System.Drawing.Point(12, 193);
            this.custom_profiles.Name = "custom_profiles";
            this.custom_profiles.Size = new System.Drawing.Size(236, 172);
            this.custom_profiles.TabIndex = 2;
            this.custom_profiles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.custom_profiles_ItemCheck);
            this.custom_profiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.custom_profiles_KeyDown);
            this.custom_profiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.custom_profiles_MouseDoubleClick);
            // 
            // button2
            // 
            this.button2.Image = global::tickMeter.Properties.Resources.add;
            this.button2.Location = new System.Drawing.Point(173, 377);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 68);
            this.button2.TabIndex = 46;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Image = global::tickMeter.Properties.Resources.check;
            this.ApplyBtn.Location = new System.Drawing.Point(12, 378);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(75, 68);
            this.ApplyBtn.TabIndex = 45;
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // button1
            // 
            this.button1.Image = global::tickMeter.Properties.Resources._switch;
            this.button1.Location = new System.Drawing.Point(93, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 68);
            this.button1.TabIndex = 47;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(232, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 48;
            this.label3.Text = "?";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ProfilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 457);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.custom_profiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.built_in_profiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ProfilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Профили отслеживания";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.profileForm_FormClosing);
            this.Shown += new System.EventHandler(this.profileForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox built_in_profiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox custom_profiles;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
    }
}