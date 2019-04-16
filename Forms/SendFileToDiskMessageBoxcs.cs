using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Forms
{
    public partial class SendFileToDiskMessageBoxcs : Form
    {
   
        public SendFileToDiskMessageBoxcs( )
        {

            InitializeComponent();           
        }
        public string ConfigItemString = null;
        public string FileDiskAddressString = null;
        private void Confirmbutton1_Click(object sender, EventArgs e)
        {
            //得到输入字符串
             FileDiskAddressString = FileDiskAddressTextBox.Text.Trim();
             ConfigItemString = ConfigItemTextBox.Text.Trim();
             this.Close();
        }

        private void Cancelbutton2_Click(object sender, EventArgs e)
        {
                this.Close();       
        }
    }
}
