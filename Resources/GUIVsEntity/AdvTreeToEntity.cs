using DevComponents.AdvTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIVsEntity
{
    class AdvTreeToEntity
    {

        public void GetEntityByAdvTreeNode(AdvTree advTree)
        {
            if (advTree == null && ComRunDatas.StructOfSourceFileEntity == null) return;
            int CID = 1;
            //1.重新得到ComRunDatas.StructEntity对象
            StructOfSourceFileDataOperation structDataOperation = new StructOfSourceFileDataOperation();
            structDataOperation.CreateConfigFileInfo();
            foreach (Node RootNode in advTree.Nodes)
            {
                foreach (Node node in RootNode.Nodes)
                {
                    if (node.HasChildNodes)
                    {
                        //2.通过节点数据得到StructItem对象
                        StructItem structItem = GetStructItemEntity(node, CID++);
                        foreach (Node childNode in node.Nodes)
                        { //3.通过节点数据得到Parameter对象
                            Parameter parameter = GetParameterEntity(childNode, CID++);
                            structItem.parameterList.Add(parameter);
                        }
                        //4.添加数据到Parameter对象
                        ComRunDatas.StructOfSourceFileEntity.structItemList.Add(structItem);
                    }
                }
            }
        }

        public StructItem GetStructItemEntity(Node node, int CID)
        {
            StructItem structItem = new StructItem();
            Dictionary<string, string> NodeDictinary = GetNodeEntireData(node);
            structItem.CID = string.Format("{0:D6}", CID);
            structItem.type = NodeDictinary.ContainsKey("type") ? NodeDictinary["type"] : "";
            structItem.name = NodeDictinary.ContainsKey("name") ? NodeDictinary["name"] : "";
            structItem.preinput = NodeDictinary.ContainsKey("preinput") ? NodeDictinary["preinput"] : "";
            structItem.note = NodeDictinary.ContainsKey("note") ? NodeDictinary["note"] : "";
            structItem.parameterList = new List<Parameter>();
            return structItem;
        }
        public Parameter GetParameterEntity(Node node, int CID)
        {
            Parameter parameter = new Parameter();
            Dictionary<string, string> NodeDictinary = GetNodeEntireData(node);
            parameter.CID = string.Format("{0:D6}", CID);
            parameter.type = NodeDictinary.ContainsKey("type") ? NodeDictinary["type"] : "";
            parameter.name = NodeDictinary.ContainsKey("name") ? NodeDictinary["name"] : "";
            parameter.preinput = NodeDictinary.ContainsKey("preinput") ? NodeDictinary["preinput"] : "";
            parameter.range = NodeDictinary.ContainsKey("range") ? NodeDictinary["range"] : "";
            parameter.value = NodeDictinary.ContainsKey("value") ? NodeDictinary["value"] : "";
            parameter.length = NodeDictinary.ContainsKey("length") ? NodeDictinary["length"] : "";
            parameter.note = NodeDictinary.ContainsKey("note") ? NodeDictinary["note"] : "";
            return parameter;
        }
        private Dictionary<string, string> GetNodeEntireData(Node node)
        {
            Dictionary<string, string> NodeDictinary = new Dictionary<string, string>();
            foreach (Cell cellItem in node.Cells)
            {
                string stringText = cellItem.Text;
                string HeaderName = cellItem.ColumnHeader == null ? "" : cellItem.ColumnHeader.Name;
                if (!HeaderName.Equals("value"))
                {
                    NodeDictinary[HeaderName] = stringText;
                }
                else
                {
                    if (cellItem.HostedControl != null)
                    {
                        ComboBox comboBox = cellItem.HostedControl as ComboBox;
                        stringText = comboBox.Text.ToString();
                        // stringText = comboBox.SelectedItem.ToString();

                    }
                    NodeDictinary[HeaderName] = stringText;
                }
            }
            if (node.Tag != null)
            {
                Dictionary<string, string> TagData = (Dictionary<string, string>)node.Tag;
                foreach (string key in TagData.Keys)
                {
                    if (!NodeDictinary.ContainsKey(key))
                    {
                        NodeDictinary[key] = TagData[key];
                    }
                }
            }
            return NodeDictinary;
        }

    }
}
