using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterDetailSample
{
    public partial class LoadDataBox : Form
    {
        public LoadDataBox(string InputOriginDataStr = null)
        {
            InitializeComponent();
            if (InputOriginDataStr != null)
            {
                if (InputOriginDataStr.Contains("{") || InputOriginDataStr.Contains("}"))
                {
                    string currentString = InputOriginDataStr.Replace('{', ' ');
                    currentString = currentString.Replace('}', ' ');
                    textBox1.Text = currentString.Trim();
                }
                else
                {
                    textBox1.Text = InputOriginDataStr.Trim();
                }
                InputOriginData = InputOriginDataStr;
            }
        }
        #region 属性区
        public string InputOriginData = null;
        public string InputTextData = null;
        public int InputSplitCount = 0;
        #endregion
        #region 函数区
        private void Confirmbutton1_Click(object sender, EventArgs e)
        {
            string InputData = string.Empty;
            if (!textBox1.Text.Contains("{") && !textBox1.Text.Contains("}"))
            {
                InputData = textBox1.Text.Trim();

                bool Result = Regex.IsMatch(InputData, @"^[0-9a-fA-FxX,]+$");
                if (!Result)
                {
                    System.Windows.Forms.MessageBox.Show("请输入一组十进制或十六进制数据.");
                    InputTextData = InputOriginData;
                    InputSplitCount = InputOriginData.Split(',').Count();
                    return;
                }
                InputTextData = "{" + InputData + "}";
                InputSplitCount = InputData.Split(',').Count();
            }
            this.Close();
            return;

        }

        private void Cancelbutton2_Click(object sender, EventArgs e)
        {
            InputTextData = InputOriginData;
            InputSplitCount = InputOriginData.Split(',').Count();
            this.Close();
        }
        #endregion
    }
}
