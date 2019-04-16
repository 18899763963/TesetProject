using SmallManagerSpace.Resources.GUIModels;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace SmallManagerSpace
{
    public partial class PropertySettingBox : Form
    {
        public PropertySettingBox()
        {
            InitializeComponent();
            PropertyGridObj propertyGridObj = new PropertyGridObj();
            propertyGridObj.InitPropertyGrid(this.Panel);
        }
        #region 函数区
        private void Confirmbutton1_Click(object sender, EventArgs e)
        {

        }
        private void Cancelbutton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
