namespace SmallManagerSpace
{
    partial class PropertySettingBox
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
            this.SuspendLayout();
            // 
            // Cancelbutton2
            // 
            this.Cancelbutton2.Location = new System.Drawing.Point(35, 258);
            this.Cancelbutton2.Name = "Cancelbutton2";
            this.Cancelbutton2.Size = new System.Drawing.Size(72, 24);
            this.Cancelbutton2.TabIndex = 8;
            this.Cancelbutton2.Text = "取消";
            this.Cancelbutton2.UseVisualStyleBackColor = true;
            this.Cancelbutton2.Click += new System.EventHandler(this.Cancelbutton2_Click);
            // 
            // Confirmbutton1
            // 
            this.Confirmbutton1.Location = new System.Drawing.Point(260, 259);
            this.Confirmbutton1.Name = "Confirmbutton1";
            this.Confirmbutton1.Size = new System.Drawing.Size(75, 23);
            this.Confirmbutton1.TabIndex = 7;
            this.Confirmbutton1.Text = "确定";
            this.Confirmbutton1.UseVisualStyleBackColor = true;
            this.Confirmbutton1.Click += new System.EventHandler(this.Confirmbutton1_Click);
            // 
            // Panel
            // 
            this.Panel.Location = new System.Drawing.Point(-1, 0);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(382, 252);
            this.Panel.TabIndex = 6;
            // 
            // PropertySettingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 294);
            this.Controls.Add(this.Cancelbutton2);
            this.Controls.Add(this.Confirmbutton1);
            this.Controls.Add(this.Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertySettingBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置属性";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancelbutton2;
        private System.Windows.Forms.Button Confirmbutton1;
        private System.Windows.Forms.Panel Panel;
    }
}