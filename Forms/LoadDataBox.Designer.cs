namespace MasterDetailSample
{
    partial class LoadDataBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancelbutton2
            // 
            this.Cancelbutton2.Location = new System.Drawing.Point(51, 165);
            this.Cancelbutton2.Name = "Cancelbutton2";
            this.Cancelbutton2.Size = new System.Drawing.Size(72, 24);
            this.Cancelbutton2.TabIndex = 5;
            this.Cancelbutton2.Text = "取消";
            this.Cancelbutton2.UseVisualStyleBackColor = true;
            this.Cancelbutton2.Click += new System.EventHandler(this.Cancelbutton2_Click);
            // 
            // Confirmbutton1
            // 
            this.Confirmbutton1.Location = new System.Drawing.Point(238, 165);
            this.Confirmbutton1.Name = "Confirmbutton1";
            this.Confirmbutton1.Size = new System.Drawing.Size(75, 23);
            this.Confirmbutton1.TabIndex = 4;
            this.Confirmbutton1.Text = "确定";
            this.Confirmbutton1.UseVisualStyleBackColor = true;
            this.Confirmbutton1.Click += new System.EventHandler(this.Confirmbutton1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 138);
            this.panel1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(338, 138);
            this.textBox1.TabIndex = 0;
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 201);
            this.Controls.Add(this.Cancelbutton2);
            this.Controls.Add(this.Confirmbutton1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导入数据";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancelbutton2;
        private System.Windows.Forms.Button Confirmbutton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
    }
}