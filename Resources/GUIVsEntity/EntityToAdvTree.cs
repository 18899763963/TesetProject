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
        public void FullDataToAdvTreeFromXMLObj(StructEntity inputEntity)
        {
            string structBody = "StructList";
            if (ComData.advTree == null && inputEntity == null && ComData.nodeElementStyle == null) return;
            ComData.advTree.BeginUpdate();
            Node NewTreeNode = null;
            NewTreeNode = CreateNode(structBody, ComData.nodeElementStyle["BlockStyle"], 0);
            NewTreeNode.Name = "StructList";
            FillDataToTreeByTraversvalObj(inputEntity, NewTreeNode);
            NewTreeNode.Expanded = true;
            ComData.advTree.DisplayRootNode = NewTreeNode;
            ComData.advTree.Nodes.Add(NewTreeNode);
            ComData.advTree.EndUpdate();
        }
        public void FillDataToTreeByTraversvalObj(StructEntity inputEntity, Node RreeNode)
        {
            foreach (StructItem structitemObj in inputEntity.nodeList)
            {
                //1.将structItemObj数据添加到节点中
                Node StructNode = GetNodeByObjOfStructitem(structitemObj, ComData.nodeElementStyle["BlockStyle"], 0);
                //2.将structItemObj. List<parameter> 添加到节点中
                foreach (object objItem in structitemObj.parameterList)
                {
                    GetNodeByTraversal(StructNode, objItem);

                }
                RreeNode.Nodes.Add(StructNode);

            }
        }
        private Node CreateNode(string nodeText, ElementStyle nodeItemStyle, int imageIndex)
        {
            Node node = new Node(nodeText, nodeItemStyle);
            //node.Name = nodeName;
            node.ImageIndex = imageIndex;
            return node;
        }
        private Node GetNodeByObjOfStructitem(StructItem sObj, ElementStyle elementStyle, int imageIndex)
        {
            Node newTreeNode = CreateNode(sObj.type, elementStyle, imageIndex);
            newTreeNode.Name = sObj.preinput;
            newTreeNode.Cells.Add(new Cell(sObj.preinput, elementStyle));
            newTreeNode.Cells.Add(new Cell(sObj.name, elementStyle));
            newTreeNode.Cells.Add(new Cell(sObj.index, elementStyle));
            newTreeNode.Cells.Add(new Cell("", elementStyle));
            newTreeNode.Cells.Add(new Cell("", elementStyle));
            newTreeNode.Cells.Add(new Cell("", elementStyle));           
            newTreeNode.Cells.Add(new Cell(sObj.nodetype, elementStyle));
            newTreeNode.Cells.Add(new Cell(sObj.CID, elementStyle));
            newTreeNode.Cells.Add(new Cell(sObj.note, elementStyle));
            //Dictionary<string, string> TagDict = new Dictionary<string, string>();
            //TagDict["preinput"] = sObj.preinput;
            //TagDict["name"] = sObj.name;
            //TagDict["CID"] = sObj.CID;
            //TagDict["nodetype"] = sObj.nodetype ?? "struct";
            //newTreeNode.Tag = TagDict;

            return newTreeNode;
        }
        private Node GetNodeByObjOfBase(Parameter pObj, ElementStyle elementStyle, int imageIndex)
        {
            Node newTreeNode = CreateNode(pObj.type, elementStyle, imageIndex);
            newTreeNode.Name = pObj.preinput;
            newTreeNode.Cells.Add(new Cell(pObj.preinput, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.name, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.index, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.length, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.range, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.value, elementStyle));       
            newTreeNode.Cells.Add(new Cell(pObj.nodetype, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.CID, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.note, elementStyle));
            Dictionary<string, string> TagDict = new Dictionary<string, string>();
            //TagDict["preinput"] = pObj.preinput;
            //TagDict["name"] = pObj.name;
            //TagDict["CID"] = pObj.CID;
            //TagDict["nodetype"] = pObj.nodetype ?? "base";
            //newTreeNode.Tag = TagDict;
            //2.注册entry变量
            if (pObj.preinput.Equals("entry"))
            {
                AdvTreeObj advTreeObj = new AdvTreeObj();
                advTreeObj.AddEntryVar(pObj.name, pObj.value);
            }
            return newTreeNode;

        }
        private Node GetNodeByObjOfEnum(Parameter pObj, ElementStyle elementStyle, int imageIndex)
        {
            Node newTreeNode = CreateNode(pObj.type, elementStyle, imageIndex);
            newTreeNode.Name = pObj.preinput;
            newTreeNode.Cells.Add(new Cell(pObj.preinput, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.name, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.index, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.length, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.range, elementStyle));
            if (pObj.nodetype=="enum")
            {
                ComBoxObj comBoxObj = new ComBoxObj();
                Cell cell = new Cell();
                Control control = comBoxObj.CreateEnbedCombox(cell,pObj.type);             
                cell.HostedControl = control;
                control = cell.HostedControl;
                newTreeNode.Cells.Add(cell);
            }
            else
            {
                newTreeNode.Cells.Add(new Cell(pObj.value, elementStyle));
            }
           
            newTreeNode.Cells.Add(new Cell(pObj.nodetype, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.CID, elementStyle));
            newTreeNode.Cells.Add(new Cell(pObj.note, elementStyle));
            //Dictionary<string, string> TagDict = new Dictionary<string, string>();
            //TagDict["preinput"] = pObj.preinput;
            //TagDict["name"] = pObj.name;
            //TagDict["CID"] = pObj.CID;
            //TagDict["nodetype"] = pObj.nodetype ?? "enum";
            //newTreeNode.Tag = TagDict;
            //2.注册entry变量
            if (pObj.preinput.Equals("entry"))
            {
                AdvTreeObj advTreeObj = new AdvTreeObj();
                advTreeObj.AddEntryVar(pObj.name,pObj.value);
            }
            return newTreeNode;
        }

        private void GetNodeByTraversal(Node pNode, object objItem)
        {// ComData.nodeElementStyle["ParameterStyle"], 2
            //        pNode.Nodes.Add();
            if (objItem is Parameter)
            {
                Parameter pObj = objItem as Parameter;
                switch (pObj.nodetype)
                {
                    case "base":
                        //Node ParameterNode = GetNodeByObjOfParameter(objItem, ComData.nodeElementStyle["ParameterStyle"], 2);
                        Node baseNode = GetNodeByObjOfBase(pObj, ComData.nodeElementStyle["ParameterStyle"], 2);
                        pNode.Nodes.Add(baseNode);
                        break;
                    case "enum":
                        Node enumNode = GetNodeByObjOfEnum(pObj, ComData.nodeElementStyle["ParameterStyle"], 1);
                        pNode.Nodes.Add(enumNode);
                        break;
                    default:
                        break;

                }
            }
            else if (objItem is StructItem)
            {
                StructItem sObj = objItem as StructItem;
                if (sObj.nodetype == "struct")
                {
                    Node StructNode = GetNodeByObjOfStructitem(sObj, ComData.nodeElementStyle["BlockStyle"], 0);
                    foreach (object childItem in sObj.parameterList)
                    {
                        GetNodeByTraversal(StructNode, childItem);
                    }
                    pNode.Nodes.Add(StructNode);
                }
            }
        }
    }
}
