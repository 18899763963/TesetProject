namespace SmallManagerSpace.Forms
{
    partial class SendFileToDiskMessageBoxcs
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
            this.Cancelbutton2 = new System.Windows.Forms.Button();
            this.Confirmbutton1 = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.Panel();
            this.ConfigItemTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FileDiskAddressTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancelbutton2
            // 
            this.Cancelbutton2.Location = new System.Drawing.Point(49, 228);
            this.Cancelbutton2.Name = "Cancelbutton2";
            this.Cancelbutton2.Size = new System.Drawing.Size(72, 24);
            this.Cancelbutton2.TabIndex = 11;
            this.Cancelbutton2.Text = "取消";
            this.Cancelbutton2.UseVisualStyleBackColor = true;
            this.Cancelbutton2.Click += new System.EventHandler(this.Cancelbutton2_Click);
            // 
            // Confirmbutton1
            // 
            this.Confirmbutton1.Location = new System.Drawing.Point(212, 229);
            this.Confirmbutton1.Name = "Confirmbutton1";
            this.Confirmbutton1.Size = new System.Drawing.Size(75, 23);
            this.Confirmbutton1.TabIndex = 10;
            this.Confirmbutton1.Text = "确定";
            this.Confirmbutton1.UseVisualStyleBackColor = true;
            this.Confirmbutton1.Click += new System.EventHandler(this.Confirmbutton1_Click);
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.ConfigItemTextBox);
            this.Panel.Controls.Add(this.label2);
            this.Panel.Controls.Add(this.FileDiskAddressTextBox);
            this.Panel.Controls.Add(this.label1);
            this.Panel.Location = new System.Drawing.Point(12, 12);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(309, 170);
            this.Panel.TabIndex = 9;
            // 
            // ConfigItemTextBox
            // 
            this.ConfigItemTextBox.Location = new System.Drawing.Point(82, 97);
            this.ConfigItemTextBox.Name = "ConfigItemTextBox";
            this.ConfigItemTextBox.Size = new System.Drawing.Size(205, 21);
            this.ConfigItemTextBox.TabIndex = 4;
            this.ConfigItemTextBox.Text = "fhapp_debug_conf";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "配置项目：";
            // 
            // FileDiskAddressTextBox
            // 
            this.FileDiskAddressTextBox.Location = new System.Drawing.Point(82, 44);
            this.FileDiskAddressTextBox.Name = "FileDiskAddressTextBox";
            this.FileDiskAddressTextBox.Size = new System.Drawing.Size(205, 21);
            this.FileDiskAddressTextBox.TabIndex = 2;
            this.FileDiskAddressTextBox.Text = "/tffs/conf_data.bin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件地址：";
            // 
            // SendFileToDiskMessageBoxcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 270);
            this.Controls.Add(this.Cancelbutton2);
            this.Controls.Add(this.Confirmbutton1);
            this.Controls.Add(this.Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendFileToDiskMessageBoxcs";
            this.Text = "发送文件到单盘的配置";
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancelbutton2;
        private System.Windows.Forms.Button Confirmbutton1;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.TextBox ConfigItemTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FileDiskAddressTextBox;
        private System.Windows.Forms.Label label1;
    }
}