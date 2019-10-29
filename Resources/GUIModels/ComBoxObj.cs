using DevComponents.AdvTree;
using SmallManagerSpace.Resources.GUIVsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SmallManagerSpace.Resources;

namespace SmallManagerSpace.Resources.GUIModels
{
    class MyComboBox : System.Windows.Forms.ComboBox
    {
        //禁用ComboBox中鼠标滑轮
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x020A)
            { }
            else
            {
                base.WndProc(ref m);
            }
        }
        //protected override void CreateHandle()
        //{
        //    if (!IsHandleCreated)
        //    {
        //        try
        //        {
        //            base.CreateHandle();
        //        }
        //        catch { }
        //        finally
        //        {
        //            if (!IsHandleCreated)
        //            {
        //                base.RecreateHandle();
        //            }
        //        }
        //    }
        //}
    }

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
        //MyComboBox comboBox = new MyComboBox();
        public Control CreateEnbedCombox(Cell HostedCell, string typeName,string defaultEnum)
        {
            Control controlObj = null;
            MyComboBox comboBox = new MyComboBox();
            simpleType selectItem = null;
            if (ComData.enumEntity.simpleTypes.Where(x => x.name == typeName).Count() != 0)
            {
                selectItem = ComData.enumEntity.simpleTypes.Where(x => x.name == typeName).First();
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                int SelectedIndex = 0;
                int TempIndex = 0;
                foreach (EnumValue eItem in selectItem.EnumValues)
                {
                    if (eItem.en == defaultEnum)
                    {
                        SelectedIndex = TempIndex;
                    }
                    TempIndex++;
                    comboBox.Items.Add(eItem.en);
                    comboBox.DisplayMember = eItem.en;
                    comboBox.ValueMember = eItem.value;
                }
                comboBox.SelectedIndex = SelectedIndex;     
                HostedCell.Text = selectItem.EnumValues[SelectedIndex].en;
                comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
                controlObj = comboBox;
             
            }
            return controlObj;

        }
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyComboBox comboBox = (MyComboBox)sender;
            AdvTree advTree = comboBox.Parent as AdvTree;
            if (advTree != null)
            {
                Node selectedNode = advTree.SelectedNode;
                AdvTreeObj advTreeObj = new AdvTreeObj();
                Cell selectedCell = advTreeObj.GetSelectedNodeCell(selectedNode, "value");
                //数据被修改，则设置modified为Y
                if (!selectedCell.Text.Equals(comboBox.Text))
                {
                    advTreeObj.SetSelectedNodeCellData(selectedNode, "modified", "Y");
                }
                selectedCell.Text = comboBox.Text;

            }            
        }
    }
}
