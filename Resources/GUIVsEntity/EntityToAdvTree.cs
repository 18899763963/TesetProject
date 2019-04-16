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
        //private void SettingDisplayDictionary()
        //{
        //    structblockDic = new Dictionary<string, string>();
        //    parameterDic = new Dictionary<string, string>();
        //    structblockDic["CID"] = "Null";
        //    structblockDic["type"] = "Null";
        //    structblockDic["name"] = "Null";
        //    structblockDic["preinput"] = "Null";
        //    structblockDic["note"] = "Null";
        //    parameterDic["CID"] = "Null";
        //    parameterDic["type"] = "Null";
        //    parameterDic["vartype"] = "Null";
        //    parameterDic["preinput"] = "Null";
        //    parameterDic["preinputarea"] = "Null";
        //    parameterDic["name"] = "Null";
        //    parameterDic["range"] = "Null";
        //    parameterDic["value"] = "Null";
        //    parameterDic["length"] = "Null";
        //    parameterDic["note"] = "Null";
        //}
        public void FullDataToAdvTreeFromXMLObj()
        {
            if (ComRunDatas.advTree == null && ComRunDatas.StructEntity == null && ComRunDatas.NodeElementStyle == null) return;
            ComRunDatas.advTree.BeginUpdate();
            Node NewTreeNode = null;
            NewTreeNode = CreateNode(ComRunDatas.StructBody, ComRunDatas.NodeElementStyle["BlockStyle"], 0);
            FillDataToTreeByTraversvalObj(ComRunDatas.StructEntity, NewTreeNode);
            NewTreeNode.Expanded = true;
            ComRunDatas.advTree.Nodes.Add(NewTreeNode);
            ComRunDatas.advTree.EndUpdate();
        }
        public void FillDataToTreeByTraversvalObj(StructDatas StructEntity, Node RreeNode)
        {
            foreach (structitem structitemObj in StructEntity.structitemlist)
            {
                //1.将structItemObj数据添加到节点中
                Node StructitemNode = GetNodeByObjOfStructitem(structitemObj, ComRunDatas.NodeElementStyle["BlockStyle"], 0);
                //2.将structItemObj. List<parameter> 添加到节点中
                foreach (parameter parameterObj in structitemObj.parameterlist)
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
        private Node GetNodeByObjOfStructitem(structitem structItemObj, ElementStyle elementStyle, int imageIndex)
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


        private Node GetNodeByObjOfParameter(parameter paremeterObj, ElementStyle elementStyle, int imageIndex)
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


        //public void AdvTreeDisplayDataViaObj()
        //{

        //    //清空页状态记录
        //    //清空变更记录
        //    //UpdateRecords.Clear();
        //    //清空preinput的变量名

        //    //初始化显示列字典
        //    //SettingDisplayDictionary();
        //    //得到节点元素
        //    //xmlFileClass = new TransformFileClass();
        //    //string PathName = fileOperation.GetDirectionNameString(xmlpathSource);
        //    //string FileName = fileOperation.GetFileNameString(xmlpathSource);
        //    //string FileFullName = PathName + FileName;
        //    //xElementStartPublic = xmlFileClass.GetRootElement(FileFullName);
        //    //得到指定节点下子节点名称
        //ListBlockName = xmlFileClass.GetStartElementChildName(xElementStartPublic, "structitems");
        //CreatePageAndPageProperty();
        //得到第一页的内容
        //string TabPageNameBlock = GetTabPageName(0);
        // SetTabPageProperty(0, "cn", true);
        //得到反序列化的对象
        //StructDatas structfileObj = ObjectToXml.xmlDeSerializeToStructObj(PathName, FileName);
        //填充第一页的内容 用structfileObj 下级 TabPageNameBlock
        // FullDataToAdvTreeFromXMLObj(CurrentAdvTree, structfileObj, TabPageNameBlock, 0);
        //}
        //private AdvTree GetAdvTree(int index)
        //{
        //    //得到指定的advTree
        //    if (tabControl2.TabPages.Count >= 1 && tabControl2.TabPages.Count > index)
        //    {
        //        var currentControls = tabControl2.TabPages[index].Controls;
        //        foreach (Control con in currentControls)
        //        {
        //            if (con is AdvTree)
        //            {
        //                return (AdvTree)con;
        //            }
        //        }
        //    }
        //    return null;
        //}
        //    private void TraversalAdvTreeToObj(Node node)
        //    {
        //        if (node == null) return;
        //        int CID = 1;
        //        if (node.HasChildNodes)
        //        {
        //            foreach (Node StructNode in node.Nodes)
        //            {
        //                //得到structitem数据 eg <structitem CID="0001" type="_otn_user_b_type_info" name="OTN_USER_B_TYPE_INFO" preinput="preinputValue" note="NoteValue">
        //                string typeOfStruct = GetNodeCellColumnName(StructNode, "type");
        //                string nameOfStruct = GetNodeCellColumnName(StructNode, "name");
        //                string noteOfStruct = GetNodeCellColumnName(StructNode, "note");
        //                string preinputOfStruct = GetTagDataByName(StructNode, "preinput");
        //                ObjectToXml.addValueOfStructItem(CID.ToString().PadLeft(4, '0'), typeOfStruct, nameOfStruct, preinputOfStruct, noteOfStruct);
        //                CID++;
        //                if (StructNode.HasChildNodes)
        //                {

        //                    foreach (Node ParameterNode in StructNode.Nodes)
        //                    {
        //                        //1.得到name,value,vartype和preinput,preinputarea
        //                        string typeOfParameter = GetNodeCellColumnName(ParameterNode, "type");
        //                        string vartypeOfParameter = GetNodeCellColumnName(ParameterNode, "vartype");
        //                        string nameOfParameter = GetNodeCellColumnName(ParameterNode, "name");
        //                        string rangeOfParameter = GetNodeCellColumnName(ParameterNode, "range");
        //                        string valueOfParameter = GetNodeCellColumnName(ParameterNode, "value");
        //                        string lengthOfParameter = GetNodeCellColumnName(ParameterNode, "length");
        //                        string noteOfParameter = GetNodeCellColumnName(ParameterNode, "note");
        //                        string preinputOfParameter = GetTagDataByName(ParameterNode, "preinput"); ;
        //                        string preinputareaOfParameter = GetTagDataByName(ParameterNode, "preinputarea");
        //                        ObjectToXml.addValueOfParameterItem(CID.ToString().PadLeft(4, '0'),
        //                            typeOfParameter, vartypeOfParameter, preinputOfParameter,
        //                            preinputareaOfParameter, nameOfParameter, rangeOfParameter,
        //                            valueOfParameter, lengthOfParameter, noteOfParameter);
        //                        CID++;

        //                    }
        //                }
        //            }
        //        }
        //    }
    }
}
