using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIModels
{
    class ComBoxObj
    {
        DataTable GetDataTable(List<Enumration> enumrationList)
        {
            DataTable dt = new DataTable();
            foreach (Enumration enumrationItem in enumrationList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = enumrationItem.value;
                dr[1] = enumrationItem.en;
                dr[2] = enumrationItem.cn;
                dt.Rows.Add(dr);
            }
            return dt;

        }
        public Control CreateEnbedCombox(List<Enumration> enumrationList)
        {
            Control controlObj = null;
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;         
            
            foreach (Enumration enumrationItem in enumrationList)
            {
                comboBox.Items.Add(enumrationItem);
                comboBox.DisplayMember = "en";
                comboBox.ValueMember = "value";
              
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
