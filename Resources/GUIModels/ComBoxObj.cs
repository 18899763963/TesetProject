using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIModels
{
    class ComBoxObj
    {
        public Control CreateEnbedCombox(List<Enumration> enumrationList)
        {
            Control controlObj = null;
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (Enumration enumrationItem in enumrationList)
            {
                comboBox.Items.Add(enumrationItem.en);
            }
            comboBox.SelectedIndex = 0;
            comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            controlObj = comboBox;
            return controlObj;
        }
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Tag != null)
            {
            }

        }
    }
}
