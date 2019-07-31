using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIModels
{

    public class ComBoxEnumChild
    {
        public string en { get; set; }
        public string cn { get; set; }
        public string value { get; set; }
        public override string ToString()
        {
            return this.value;
        }
    }
    public class ComBoxEnum
    {
        public string name { get; set; }
        public string type { get; set; }
        public int length { get; set; }
        public List<ComBoxEnumChild> comBoxEnumChild = null;
    }

    class ComBoxObj
    {


        public Control CreateEnbedCombox(List<ComBoxEnumChild> enumrationList)
        {
            Control controlObj = null;
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;         
            
            foreach (ComBoxEnumChild enumrationItem in enumrationList)
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
