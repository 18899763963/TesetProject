using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Forms
{
    public partial class TipMessageBoxcs : Form
    {
        public TipMessageBoxcs()
        {
            InitializeComponent();
            TextBoxSetting();
        }
        public void TextBoxSetting()
        {
            textBox1.Text = "一.本软件主要功能：" + "\r\n";
            textBox1.AppendText("1.主程序将XML文件中\"ConfigInfo\"下的内容转换为指定格式二进制文档。" + "\r\n");
            textBox1.AppendText("2.通过Telnet将二进制文件信息传送到主机或单盘。" + "\r\n");
            textBox1.AppendText("\r\n");
            textBox1.AppendText("二.软件主要实现方法和步骤：" + "\r\n");
            textBox1.AppendText("1.将导入的XML和base/enum文件合并，生成新的XML文件。" + "\r\n");
            textBox1.AppendText("2.导入并显示新的XML文件到主窗体。" + "\r\n");
            textBox1.AppendText("3.在主窗体上编辑信息。" + "\r\n");
            textBox1.AppendText("4.保存编辑后的信息，并将内容转换为二进制文件。" + "\r\n");
            textBox1.AppendText("5.二进制文件的相关信息通过Telnet传送到主机或单盘。" + "\r\n");
            textBox1.AppendText("\r\n");
            textBox1.AppendText("三.生成文件规则：" + "\r\n");
            textBox1.AppendText("1.存储文件时，请填写完整的提示信息。" + "\r\n");
            textBox1.AppendText("2.新生成的XML文件和二进制Bin文件的存放路径与导入的XML路径相同。" + "\r\n");
            textBox1.AppendText("3.生成二进制Bin文件过程是将” ConfigInfo”节点下所有“Parameter”节点找出，并将其中的“default”属性的值转换为相应的二进制内容，除了以下两种情况会跳过转换" + "\r\n");
            textBox1.AppendText("4.一是生成二进制Bin文件时，如果path的上一个节点为中有属性Data=\"Line_Number\"，Default=\"0000\",则跳过Path中的内容。" + "\r\n");
            textBox1.AppendText("5.二是生成二进制Bin文件时，如果path的上一个节点为中有属性Data=\"Mask_Config_Item_Number\"，Default=\"00000000\",则跳过Path中的内容。" + "\r\n");

        }
    }
}
