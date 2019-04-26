using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using SmallManagerSpace.Resources.GUIModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIVsEntity
{
    class EntityToAdvTree
    {
        public void FullDataToAdvTreeFromXMLObj()
        {
            if (ComRunDatas.advTree == null && ComRunDatas.StructOfSourceFileEntity == null && ComRunDatas.NodeElementStyle == null) return;
            ComRunDatas.advTree.BeginUpdate();
            Node NewTreeNode = null;
            NewTreeNode = CreateNode(ComRunDatas.StructBody, ComRunDatas.NodeElementStyle["BlockStyle"], 0);
            FillDataToTreeByTraversvalObj(ComRunDatas.StructOfSourceFileEntity, NewTreeNode);
            NewTreeNode.Expanded = true;
            ComRunDatas.advTree.Nodes.Add(NewTreeNode);
            ComRunDatas.advTree.EndUpdate();
        }
        public void FillDataToTreeByTraversvalObj(StructOfSourceFileDatas StructEntity, Node RreeNode)
        {
            foreach (StructItem structitemObj in StructEntity.structItemList)
            {
                //1.将structItemObj数据添加到节点中
                Node StructitemNode = GetNodeByObjOfStructitem(structitemObj, ComRunDatas.NodeElementStyle["BlockStyle"], 0);
                //2.将structItemObj. List<parameter> 添加到节点中
                foreach (Parameter parameterObj in structitemObj.parameterList)
                {
                    Node ParameterNode = GetNodeByObjOfParameter(parameterObj, ComRunDatas.NodeElementStyle["ParameterStyle"], 2);
                    StructitemNode.Nodes.Add(ParameterNode);
                }
                RreeNode.Nodes.Add(StructitemNode);

            }
        }
        private Node CreateNode(string nodeText, ElementStyle nodeItemStyle, int imageIndex)
        {
            Node node = new Node(nodeText, nodeItemStyle);
            //   node.Name = nodeName;
            node.ImageIndex = imageIndex;
            return node;
        }
        private Node GetNodeByObjOfStructitem(StructItem structItemObj, ElementStyle elementStyle, int imageIndex)
        {
            Node newTreeNode = new Node();
            newTreeNode = CreateNode(structItemObj.type, elementStyle, imageIndex);
            newTreeNode.Cells.Add(new Cell(structItemObj.name, elementStyle));
            newTreeNode.Cells.Add(new Cell("", elementStyle));
            newTreeNode.Cells.Add(new Cell("", elementStyle));
            newTreeNode.Cells.Add(new Cell("", elementStyle));
            newTreeNode.Cells.Add(new Cell(structItemObj.note, elementStyle));
            Dictionary<string, string> TagDict = new Dictionary<string, string>();
            TagDict["preinput"] = structItemObj.preinput;
            TagDict["name"] = structItemObj.name;
            TagDict["CID"] = structItemObj.CID;
            newTreeNode.Tag = TagDict;
            return newTreeNode;
        }


        private Node GetNodeByObjOfParameter(Parameter paremeterObj, ElementStyle elementStyle, int imageIndex)
        {
            //1.添加数据到Node
            Node newTreeNode = new Node();
            newTreeNode = CreateNode(paremeterObj.type, elementStyle, imageIndex);
            newTreeNode.Cells.Add(new Cell(paremeterObj.name, elementStyle));
            newTreeNode.Cells.Add(new Cell(paremeterObj.length, elementStyle));
            newTreeNode.Cells.Add(new Cell(paremeterObj.range, elementStyle));
            if (IsMatchedEnumName(paremeterObj.type))
            {
                ComBoxObj comBoxObj = new ComBoxObj();
                List<Enumration> enumrationList = GetEnumrationList(paremeterObj.type);
                Control control = comBoxObj.CreateEnbedCombox(enumrationList);
                Cell cell = new Cell();
                cell.HostedControl = control;
                control = cell.HostedControl;
                newTreeNode.Cells.Add(cell);
            }
            else
            {
                newTreeNode.Cells.Add(new Cell(paremeterObj.value, elementStyle));
            }
            newTreeNode.Cells.Add(new Cell(paremeterObj.note, elementStyle));
            Dictionary<string, string> TagDict = new Dictionary<string, string>();
            TagDict["preinput"] = paremeterObj.preinput;
            TagDict["name"] = paremeterObj.name;
            TagDict["CID"] = paremeterObj.CID;
            newTreeNode.Tag = TagDict;
            //2.注册entry变量
            if (paremeterObj.preinput.Equals("entry"))
            {
                int Result = 1;
                if (int.TryParse(paremeterObj.value, out Result))
                {
                    ComRunDatas.RegisterPreinput[paremeterObj.name] = Result;
                }
            }
            return newTreeNode;
        }
        public bool IsMatchedEnumName(string inputName)
        {
            if (ComRunDatas.CommonEnumDictonary == null) return false;
            if (ComRunDatas.CommonEnumDictonary.ContainsKey(inputName))
            {
                return true;
            }
            return false;
        }
        public List<Enumration> GetEnumrationList(string inputName)
        {
            List<Enumration> enumrationList = ComRunDatas.CommonEnumDictonary[inputName].EnumrationList;
            return enumrationList;
        }
    }
}
