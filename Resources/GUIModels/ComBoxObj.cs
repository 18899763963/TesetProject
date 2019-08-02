using System;
using System.Collections.Generic;
using System.Linq;
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
        public Control CreateEnbedCombox(string typeName)
        {
            Control controlObj = null;
            ComboBox comboBox = new ComboBox();
            simpleType selectItem = ComData.enumEntity.simpleTypes.Where(x => x.name == typeName).First();

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (EnumValue eItem in selectItem.EnumValues)
            {
                comboBox.Items.Add(eItem.en);
                comboBox.DisplayMember = eItem.en;
                comboBox.ValueMember = eItem.value;
                //comboBox.DataSource = selectItem.EnumValues;
                //comboBox.DisplayMember = "en";
                //comboBox.ValueMember = "value";

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
