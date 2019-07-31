using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using MasterDetailSample;
using SmallManagerSpace.Resources.GUIModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIVsEntity
{
    class AdvTreeObj
    {

        public string SelectedCellOriginValue;
        //class NodeType
        //{
        //    public string CID;
        //    public string NodeLength;
        //    public NodeType(string _CID, string _NodeLength)
        //    {
        //        CID = _CID;
        //        NodeLength = _NodeLength;
        //    }
        //}
        //class UpdateRecord : NodeType
        //{
        //    public string NodeValue;
        //    public UpdateRecord(string _CID, string _NodeLength, string _NodeValue) : base(_CID, _NodeLength)
        //    {
        //        NodeValue = _NodeValue;
        //    }
        //}
        public void InitAdvTreeDatas()
        {
            ElementStyleSetting();
            AdvTreeSetting();
        }
        public void ElementStyleSetting()
        {
            ComRunDatas.nodeElementStyle.Clear();
            //  Define node style
            ElementStyle BlockStyle = new ElementStyle();
            BlockStyle.TextColor = Color.SeaGreen;
            BlockStyle.Font = new Font("宋体", 10.5f);
            BlockStyle.Name = "BlockStyle";
            ComRunDatas.nodeElementStyle.Add("BlockStyle", BlockStyle);
            ElementStyle PathStyle = new ElementStyle();
            PathStyle.TextColor = Color.Navy;
            PathStyle.Font = new Font("宋体", 10.5f);
            PathStyle.Name = "PathStyle";
            ComRunDatas.nodeElementStyle.Add("PathStyle", PathStyle);
            ElementStyle ParameterStyle = new ElementStyle();
            ParameterStyle.TextColor = Color.Navy;
            ParameterStyle.Font = new Font("宋体", 10.5f);
            ParameterStyle.Name = "ParameterStyle";
            ComRunDatas.nodeElementStyle.Add("ParameterStyle", ParameterStyle);
            ElementStyle SpanStyle = new ElementStyle();
            ParameterStyle.TextColor = Color.Navy;
            ParameterStyle.Font = new Font("宋体", 10.5f);
            ParameterStyle.Name = "SpanStyle";
            ComRunDatas.nodeElementStyle.Add("SpanStyle", ParameterStyle);
            ElementStyle ValueStyle = new ElementStyle();
            ParameterStyle.TextColor = Color.Navy;
            ParameterStyle.Font = new Font("宋体", 10.5f);
            ParameterStyle.Name = "ValueStyle";
            ComRunDatas.nodeElementStyle.Add("ValueStyle", ParameterStyle);
        }

        /// <summary>
        /// 设置该页的名称，树的列，图片，字体等
        /// </summary>
        private void AdvTreeSetting()
        {
            ComRunDatas.advTree.Nodes.Clear();
            ComRunDatas.advTree.View = eView.Tree;
            ComRunDatas.advTree.ImageList = new ImageList();
            ComRunDatas.advTree.ImageList.Images.Add("BlockImage", SmallManagerSpace.Raws.Resource.BlockIco);
            ComRunDatas.advTree.ImageList.Images.Add("PathImage", SmallManagerSpace.Raws.Resource.PathIco);
            ComRunDatas.advTree.ImageList.Images.Add("ParameterImage", SmallManagerSpace.Raws.Resource.ParameterIco);
            ComRunDatas.advTree.AllowExternalDrop = false;
            ComRunDatas.advTree.EnableDataPositionChange = false;
            ComRunDatas.advTree.DragDropEnabled = false;
            //列设置
            DevComponents.AdvTree.ColumnHeader columnHeader1 = new DevComponents.AdvTree.ColumnHeader("类型名称");
            columnHeader1.Name = "type";
            columnHeader1.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader2 = new DevComponents.AdvTree.ColumnHeader("变量名称");
            columnHeader2.Name = "name";
            columnHeader2.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader3 = new DevComponents.AdvTree.ColumnHeader("变量长度");
            columnHeader3.Name = "length";
            columnHeader3.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader4 = new DevComponents.AdvTree.ColumnHeader("变量范围");
            columnHeader4.Name = "range";
            columnHeader4.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader5 = new DevComponents.AdvTree.ColumnHeader("变量值");
            columnHeader5.Name = "value";
            columnHeader5.Editable = true;
            DevComponents.AdvTree.ColumnHeader columnHeader6 = new DevComponents.AdvTree.ColumnHeader("变量注释");
            columnHeader6.Name = "note";
            columnHeader6.Editable = false;

            columnHeader1.Width.Relative = 20;
            columnHeader2.Width.Relative = 10;
            columnHeader3.Width.Relative = 10;
            columnHeader4.Width.Relative = 10;
            columnHeader5.Width.Relative = 10;
            columnHeader6.StretchToFill = true;
            ComRunDatas.advTree.Columns.Add(columnHeader1);
            ComRunDatas.advTree.Columns.Add(columnHeader2);
            ComRunDatas.advTree.Columns.Add(columnHeader3);
            ComRunDatas.advTree.Columns.Add(columnHeader4);
            ComRunDatas.advTree.Columns.Add(columnHeader5);
            ComRunDatas.advTree.Columns.Add(columnHeader6);
            ComRunDatas.advTree.CellEdit = true;
            ComRunDatas.advTree.BeforeCellEdit += new CellEditEventHandler(this.AdvTreeBeforeCellEdit);
            ComRunDatas.advTree.AfterCellEditComplete += new CellEditEventHandler(this.AdvTreeAfterCellEditComplete);

            ComRunDatas.advTree.GridColumnLineResizeEnabled = true;
            ComRunDatas.advTree.AlternateRowColor = Color.AntiqueWhite;
            ComRunDatas.advTree.Dock = DockStyle.Fill;


            TabItem tim = ComRunDatas.tabControl1.CreateTab("配置项");
            tim.AttachedControl.Controls.Add(ComRunDatas.advTree);
        }


        private void AdvTreeBeforeCellEdit(object sender, EventArgs e)
        {
            //1.得到选中节点的数据
            AdvTree CurrentAdvTree = (AdvTree)sender;
            Node selectedNode = CurrentAdvTree.SelectedNode;
            SelectedCellOriginValue = selectedNode.SelectedCell.Text;

            if (selectedNode.Tag != null)
            {
                Dictionary<string, string> SelectedTagData = (Dictionary<string, string>)selectedNode.Tag;
                Dictionary<string, string> SelectedNodeData = GetSelectedNodeData(selectedNode);
                string type_space_name = SelectedNodeData["type"] + " " + SelectedNodeData["name"];
                //2.判断节点类型是否为 *var[] 或者*var[]类型
                //if (IsMatchedPointerVar(type_space_name))
                //{
                //    //3.打开数据导入窗口
                //    LoadDataBox loadDataBox = new LoadDataBox(SelectedCellOriginValue);
                //    loadDataBox.ShowDialog();
                //    selectedNode.SelectedCell.Text = loadDataBox.InputTextData;
                //    //PropertySettingBox propertySettingBox = new PropertySettingBox();
                //    //propertySettingBox.ShowDialog();
                //}
            }
        }

        private void AdvTreeAfterCellEditComplete(object sender, EventArgs e)
        {
            //1.得到选中节点的数据
            AdvTree CurrentAdvTree = (AdvTree)sender;
            Node selectedNode = CurrentAdvTree.SelectedNode;
            if (selectedNode.Tag != null)
            {
                //2.得到选中节点的文本
                Dictionary<string, string> SelectedTagData = (Dictionary<string, string>)selectedNode.Tag;
                Dictionary<string, string> SelectedNodeData = GetSelectedNodeData(selectedNode);
                //3.判断选中节点的数据是否含有preinput=entry 变量
                if (IsMatchedEntryVar(SelectedNodeData["name"]))
                {
                    //4. 根据变量的范围统一设定为节点的父节点内     
                    int CountOfProcess = GetStateOfProcessNode(selectedNode, SelectedNodeData["name"], SelectedNodeData["value"]);
                    //5.判断是否为PublicPreinput
                    if ((SelectedNodeData["name"]).Equals(ComRunDatas.publicPreinputName) && CountOfProcess != 0)
                    {
                        Dictionary<string, List<Node>> ListNode = GetMatchNodeOnAncestorTree(selectedNode, SelectedTagData["name"]);
                        if (CountOfProcess > 0)
                        {
                            //增加节点
                            AddNodeOnAncestorTree(ListNode, CountOfProcess);
                        }
                        else if (CountOfProcess < 0)
                        {
                            //删除节点
                            SubNodeOnAncestorTree(ListNode, CountOfProcess);
                        }
                    }
                    else
                    {
                        Dictionary<string, List<Node>> ListNode = GetMatchNodeOnParentTree(selectedNode, SelectedTagData["name"]);
                        if (CountOfProcess > 0)
                        {
                            //增加节点
                            AddNodeOnParentTree(ListNode, CountOfProcess);
                        }
                        else if (CountOfProcess < 0)
                        {
                            //删除节点
                            SubNodeOnParentTree(ListNode, CountOfProcess);
                        }
                    }
                }
            }
        }

        private void SubNodeOnAncestorTree(Dictionary<string, List<Node>> ListNode, int Count)
        {
            //1.复制节点最后一个内容
            foreach (string KeyName in ListNode.Keys)
            {
                List<Node> CurrentListNode = ListNode[KeyName];
                Node OriginNode = CurrentListNode.FirstOrDefault<Node>();
                int BaseCount = CurrentListNode.Count;
                for (int index = Count; index < 0; index++)
                {
                    Node node = CurrentListNode[BaseCount + index];
                    OriginNode.Parent.Nodes.Remove(node);
                }
            }
        }

        private void AddNodeOnAncestorTree(Dictionary<string, List<Node>> ListNode, int Count)
        {
            //1.复制节点最后一个内容
            foreach (string KeyName in ListNode.Keys)
            {
                List<Node> CurrentListNode = ListNode[KeyName];
                int BaseCount = CurrentListNode.Count;
                for (int index = 0; index < Count; index++)
                {
                    //1.复制节点最后一个内容
                    Node OriginNode = CurrentListNode.LastOrDefault<Node>();
                    //2.修改复制的节点内容 isRealChildNode ,NodeCount,ReviseName=preinput,
                    Node CloneNode = GetModifiedMultiNode(OriginNode, BaseCount + index);
                    //3.插入节点
                    OriginNode.Parent.Nodes.Insert(OriginNode.Index + index + 1, CloneNode);
                }
            }
        }
        private void SubNodeOnParentTree(Dictionary<string, List<Node>> ListNode, int Count)
        {
            //1.复制节点最后一个内容
            foreach (string KeyName in ListNode.Keys)
            {
                List<Node> CurrentListNode = ListNode[KeyName];
                Node OriginNode = CurrentListNode.FirstOrDefault<Node>();
                int BaseCount = CurrentListNode.Count;
                for (int index = Count; index < 0; index++)
                {
                    Node node = CurrentListNode[BaseCount + index];
                    OriginNode.Parent.Nodes.Remove(node);
                }
            }
        }

        private void AddNodeOnParentTree(Dictionary<string, List<Node>> ListNode, int Count)
        {
            foreach (string KeyName in ListNode.Keys)
            {
                List<Node> CurrentListNode = ListNode[KeyName];
                int BaseCount = CurrentListNode.Count;
                for (int index = 0; index < Count; index++)
                {
                    //1.复制节点最后一个内容
                    Node OriginNode = CurrentListNode.LastOrDefault<Node>();
                    //2.修改复制的节点内容 isRealChildNode ,NodeCount,ReviseName=preinput,
                    Node CloneNode = GetModifiedSingleNode(OriginNode, BaseCount + index);
                    //3.插入节点
                    OriginNode.Parent.Nodes.Insert(OriginNode.Index + index + 1, CloneNode);
                }
            }
        }


        private Node GetModifiedMultiNode(Node OriginNode, int index)
        {
            Node CloneNode = OriginNode.DeepCopy();
            if (OriginNode.HasChildNodes)
            {
                EntityToAdvTree entityToAdvTreeOBJ = new EntityToAdvTree();
                DevComponents.AdvTree.ColumnHeader columnHeader = null;
                string CaptureName = null;
                int indexOfNodes = 0;
                foreach (Node ChildNode in OriginNode.Nodes)
                {
                    Node CloneChildNode = CloneNode.Nodes[indexOfNodes++];
                    int indexOfCells = 0;
                    foreach (Cell cellItem in ChildNode.Cells)
                    {
                        columnHeader = cellItem.ColumnHeader;
                        //1.匹配Enum，并调价嵌入下拉列表控件                
                        if (CaptureName == null)
                        {
                            if (entityToAdvTreeOBJ.IsMatchedEnumName(cellItem.Text))
                            {
                                CaptureName = cellItem.Text;
                            }
                        }
                        else if (CaptureName != null)
                        {
                            if (columnHeader.Name.Equals("value"))
                            {
                                ComBoxObj comBoxObj = new ComBoxObj();
                                List<Enumration> enumrationList = entityToAdvTreeOBJ.GetEnumrationList(CaptureName);
                                Control control = comBoxObj.CreateEnbedCombox(enumrationList);
                                CloneChildNode.Cells[indexOfCells].HostedControl = control;
                                CaptureName = null;
                            }
                        }
                        indexOfCells++;
                    }
                }
            }
            else if (!OriginNode.HasChildNodes)
            {
                int indexOfCells = 1;
                //1.修改[index]
                Dictionary<string, string> SelectedNodeData = GetSelectedNodeData(OriginNode);
                if (SelectedNodeData["name"].Contains("[") && SelectedNodeData["name"].Contains("]"))
                {
                    string subString = SelectedNodeData["name"].Substring(0, SelectedNodeData["name"].IndexOf('['));
                    CloneNode.Cells[indexOfCells].Text = subString + "[" + index + "]";
                }
            }
            return CloneNode;
        }

        private Node GetModifiedSingleNode(Node OriginNode, int index)
        {
            if (OriginNode.HasChildNodes) return null;
            Node CloneNode = OriginNode.DeepCopy();

            EntityToAdvTree entityToAdvTreeOBJ = new EntityToAdvTree();
            DevComponents.AdvTree.ColumnHeader columnHeader = null;
            string CaptureName = null;
            int indexOfCells = 0;
            foreach (Cell cellItem in OriginNode.Cells)
            {
                columnHeader = cellItem.ColumnHeader;

                //1.修改[index]
                if (columnHeader.Name.Equals("name"))
                {
                    if (cellItem.Text.Contains("[") && cellItem.Text.Contains("]"))
                    {
                        string subString = cellItem.Text.Substring(0, cellItem.Text.IndexOf('['));
                        CloneNode.Cells[indexOfCells].Text = subString + "[" + index + "]";
                    }
                }
                //2.匹配Enum，并调价嵌入下拉列表控件 
                if (CaptureName == null)
                {
                    if (entityToAdvTreeOBJ.IsMatchedEnumName(cellItem.Text))
                    {
                        CaptureName = cellItem.Text;
                    }
                }
                else if (CaptureName != null)
                {
                    if (columnHeader.Name.Equals("value"))
                    {
                        ComBoxObj comBoxObj = new ComBoxObj();
                        List<Enumration> enumrationList = entityToAdvTreeOBJ.GetEnumrationList(CaptureName);
                        Control control = comBoxObj.CreateEnbedCombox(enumrationList);
                        CloneNode.Cells[indexOfCells].HostedControl = control;
                        CaptureName = null;
                    }
                }
                indexOfCells++;
            }
            return CloneNode;
        }
        private Dictionary<string, List<Node>> GetMatchNodeOnParentTree(Node SelectedNode, string MatchName)
        {
            Dictionary<string, List<Node>> ListNodeDic = new Dictionary<string, List<Node>>();
            ////1.判断目前节点中有多少个Preinput依赖节点
            Node ParentNode = SelectedNode.Parent;
            TraverslOnParentTree(ParentNode, MatchName, ListNodeDic);
            return ListNodeDic;
        }
        private Dictionary<string, List<Node>> GetMatchNodeOnAncestorTree(Node SelectedNode, string MatchName)
        {
            Dictionary<string, List<Node>> ListNodeDic = new Dictionary<string, List<Node>>();
            //1.判断目前节点中有多少个Preinput依赖节点
            Node AncestorNode = SelectedNode.Parent.Parent;
            TraverslOnAncestorTree(AncestorNode, MatchName, ListNodeDic);
            return ListNodeDic;
        }
        private void TraverslOnAncestorTree(Node ParentNode, string PreinputMatchName, Dictionary<string, List<Node>> ListNodeDic)
        {
            if (ParentNode == null) return;
            foreach (Node ChildNode in ParentNode.Nodes)
            {
                if (ChildNode.Tag != null)
                {
                    Dictionary<string, string> TagDict = (Dictionary<string, string>)ChildNode.Tag;
                    if (TagDict["preinput"] == PreinputMatchName)
                    {
                        if (ListNodeDic.ContainsKey(TagDict["name"]) == false)
                        {
                            ListNodeDic[TagDict["name"]] = new List<Node>();
                        }
                        ListNodeDic[TagDict["name"]].Add(ChildNode);
                    }
                }
                if (ChildNode.HasChildNodes)
                {
                    TraverslOnParentTree(ChildNode, PreinputMatchName, ListNodeDic);
                }
            }
        }

        private void TraverslOnParentTree(Node ParentNode, string PreinputMatchName, Dictionary<string, List<Node>> ListNodeDic)
        {
            if (ParentNode == null) return;
            foreach (Node ChildNode in ParentNode.Nodes)
            {
                if (ChildNode.Tag != null)
                {
                    Dictionary<string, string> TagDict = (Dictionary<string, string>)ChildNode.Tag;
                    if (TagDict["preinput"] == PreinputMatchName)
                    {
                        string subString = TagDict["name"].Substring(0, TagDict["name"].IndexOf('['));
                        if (ListNodeDic.ContainsKey(subString) == false)
                        {
                            ListNodeDic[subString] = new List<Node>();
                        }
                        ListNodeDic[subString].Add(ChildNode);
                    }
                }
                if (ChildNode.HasChildNodes)
                {
                    TraverslOnParentTree(ChildNode, PreinputMatchName, ListNodeDic);
                }
            }
        }
        private void UpdateRegisterPreinputValue(string name, int value)
        {
            //1.注册entry变量的新值
            ComRunDatas.registerPreinput[name] = value;

        }
        /// <summary>
        /// 判断是否需要变更树的节点
        /// </summary>
        /// <param name="selectedNode">选择的节点</param>
        /// <param name="PreinputName">匹配变量名称</param>
        /// <param name="NowSelectedValue">选择的节点值</param>
        /// <returns>等于0:不需要更新树的节点</returns>
        /// <returns>小于0:需要删除树的节点</returns>
        /// <returns>大于0:需要添加树的节点</returns>
        private int GetStateOfProcessNode(Node selectedNode,string PreinputName, string NowSelectedValue)
        {
            int stateProcess = 0;
            //值相同：返回0
            if (NowSelectedValue.Equals(SelectedCellOriginValue)) return stateProcess;
            else
            {
                int NowValueInt = 1;
                int BeforeValueInt = 1;
                if (int.TryParse(NowSelectedValue, out NowValueInt) && int.TryParse(SelectedCellOriginValue, out BeforeValueInt))
                {
                    //输入的值大于0：正常操作
                    if (NowValueInt > 0 && BeforeValueInt > 0)
                    {
                        stateProcess = NowValueInt - BeforeValueInt;
                        UpdateRegisterPreinputValue(PreinputName,NowValueInt);
                        return stateProcess;
                    }
                    else
                    {//输入的值小于0：则恢复之前的值
                        selectedNode.SelectedCell.Text = SelectedCellOriginValue;
                        return stateProcess;
                    }
                }
                else
                {//如果修改的值不正确：则恢复之前的值
                    selectedNode.SelectedCell.Text = SelectedCellOriginValue;
                    return stateProcess;
                }
            }

        }
        private Dictionary<string, string> GetSelectedNodeData(Node node)
        {
            Dictionary<string, string> TempKeyValuePairs = new Dictionary<string, string>();
            foreach (Cell cellItem in node.Cells)
            {
                string stringText = cellItem.Text;
                string HeaderName = cellItem.ColumnHeader == null ? "" : cellItem.ColumnHeader.Name;
                TempKeyValuePairs[HeaderName] = stringText;
            }
            return TempKeyValuePairs;
        }
        private bool IsMatchedPointerVar(string inputName)
        {
            if (!inputName.Contains("AAL_SINT8") && inputName.Contains("*"))
            {
                return true;
            }
            return false;
        }
        private bool IsMatchedEntryVar(string inputName)
        {
            if (ComRunDatas.registerPreinput.ContainsKey(inputName))
            {
                return true;
            }
            return false;

        }
        ////private void GetSpecElement(string xmlpathSource, string StartElementName, string StartElementChildName)
        ////{
        ////    //1.合并文件XML文件，并生成新的XML
        ////    xmlFileClass = new TransformFileClass();
        ////    //2.得到文件指定节点元素
        ////    xElementStartPublic = xmlFileClass.GetStartElement(StartElementName, xmlpathSource);
        ////}
        //private void ExpandNodeChild(Node nodeFather)
        //{
        //    if (nodeFather.Expanded != true)
        //    {
        //        nodeFather.Expanded = true;
        //    }
        //    if (nodeFather.HasChildNodes == true)
        //    {
        //        foreach (Node node in nodeFather.Nodes)
        //        {
        //            ExpandNodeChild(node);
        //        }
        //    }
        //}

        //private string GetNodeCellColumnName(Node node, string ColumnName)
        //{
        //    string ReturnText = null;
        //    foreach (Cell cell in node.Cells)
        //    {
        //        string CellColumnName = cell.ColumnHeader.Name;
        //        if (CellColumnName == ColumnName)
        //        {
        //            ReturnText = cell.Text;
        //            return ReturnText;
        //        }
        //    }
        //    return ReturnText;
        //}
        //private string GetTagDataByName(Node node, string name)
        //{
        //    if (node == null) return "";
        //    Dictionary<string, string> TagDict = (Dictionary<string, string>)node.Tag;
        //    if (TagDict != null && TagDict.ContainsKey(name))
        //    {
        //        return TagDict[name];
        //    }
        //    return "";

        //}



        //private Dictionary<string, string> GetNodeValueWithName(Dictionary<string, string> InputDic, Node node, string matchPreinput, int ListNodeCount)
        //{
        //    if (node == null || ListNodeCount == 0) { return null; }
        //    //在枚举过程中，字典不能修改
        //    Dictionary<string, string> ReturnDic = new Dictionary<string, string>(InputDic);
        //    //1.填充数据到字典
        //    foreach (Cell cellItem in node.Cells)
        //    {
        //        string cellName = cellItem.ColumnHeader == null ? "" : cellItem.ColumnHeader.Name;
        //        string cellText = cellItem.Text;
        //        ReturnDic[cellName] = cellText;
        //    }
        //    //2.更新vartype="array[i]"中的数据
        //    if (ReturnDic["vartype"].Contains("array"))
        //    {
        //        ReturnDic["vartype"] = "array" + "[" + ListNodeCount + "]";
        //    }
        //    //3.更新preinput="*"中的数据
        //    if (ReturnDic.ContainsKey("preinput"))
        //    {
        //        ReturnDic["preinput"] = matchPreinput;
        //    }
        //    return ReturnDic;
        //}
        //private Dictionary<string, string> GetElementValueWithName(Dictionary<string, string> inputLocalDic, XElement xElement)
        //{
        //    //在枚举过程中，字典不能修改
        //    Dictionary<string, string> ReturnDic = new Dictionary<string, string>();
        //    foreach (string item in inputLocalDic.Keys)
        //    {
        //        //重新得到字典的值      
        //        ReturnDic[item] = xElement.Attribute(item) == null ? "Null" : xElement.Attribute(item).Value;
        //    }
        //    //如果是入口变量，则存储该变量的value值
        //    if (ReturnDic.ContainsKey("preinput"))
        //    {
        //        if (ReturnDic["preinput"] == "entry")
        //        {
        //            int Result = 0;
        //            int.TryParse(ReturnDic["value"], out Result);
        //            registerPreinput[ReturnDic["name"]] = Result;
        //        }
        //    }
        //    return ReturnDic;
        //}
        //// newTreeNode = CreateNode(ReturnDic["CID"], ReturnDic["name"], NodeElementStyle["BlockStyle"], 0);
        //private Node GetStructitemNodeByDictionay(Dictionary<string, string> inputLocalDic, int pageIndex)
        //{
        //    Node newTreeNode = new Node();
        //    int step = 1;
        //    if (step == 1)
        //    {
        //        string tempString1 = inputLocalDic.ContainsKey("CID") == true ? inputLocalDic["CID"] : " ";
        //        string tempString2 = inputLocalDic.ContainsKey("type") == true ? inputLocalDic["type"] : " ";
        //        newTreeNode = CreateNode(tempString2, NodeElementStyle["BlockStyle"], 0);
        //        step++;
        //    }
        //    if (step == 2)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("vartype") == true ? inputLocalDic["vartype"] : " ";
        //        newTreeNode.Cells.Add(new Cell(tempString, NodeElementStyle["BlockStyle"]));
        //        step++;
        //    }
        //    if (step == 3)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("name") == true ? inputLocalDic["name"] : " ";
        //        newTreeNode.Cells.Add(new Cell(tempString, NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 4)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("length") == true ? inputLocalDic["length"] : " ";
        //        newTreeNode.Cells.Add(new Cell(tempString, NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 5)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("range") == true ? inputLocalDic["range"] : " ";
        //        newTreeNode.Cells.Add(new Cell(tempString, NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 6)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("value") == true ? inputLocalDic["value"] : " ";
        //        newTreeNode.Cells.Add(new Cell(tempString, NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 7)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("nate") == true ? inputLocalDic["note"] : " ";
        //        newTreeNode.Cells.Add(new Cell(tempString, NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 8 && inputLocalDic.ContainsKey("preinput") && inputLocalDic.ContainsKey("preinputarea") && inputLocalDic.ContainsKey("name"))
        //    {
        //        Dictionary<string, string> TagDict = new Dictionary<string, string>();
        //        TagDict["preinput"] = inputLocalDic["preinput"];
        //        TagDict["preinputarea"] = inputLocalDic["preinputarea"];
        //        TagDict["name"] = inputLocalDic["name"];
        //        newTreeNode.Tag = TagDict;
        //        step++;
        //    }
        //    return newTreeNode;
        //}

        //private Node GetParameterNodeByDictionay(Dictionary<string, string> inputLocalDic, int pageIndex)
        //{
        //    Node newTreeNode = new Node();
        //    int step = 1;
        //    if (step == 1 && inputLocalDic.ContainsKey("CID") && inputLocalDic.ContainsKey("type"))
        //    {
        //        newTreeNode = CreateNode(inputLocalDic["type"], NodeElementStyle["ParameterStyle"], 2);
        //        step++;
        //    }
        //    if (step == 2 && inputLocalDic.ContainsKey("vartype"))
        //    {
        //        newTreeNode.Cells.Add(new Cell(inputLocalDic["vartype"], NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 3 && inputLocalDic.ContainsKey("name"))
        //    {
        //        newTreeNode.Cells.Add(new Cell(inputLocalDic["name"], NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 4 && inputLocalDic.ContainsKey("length"))
        //    {
        //        newTreeNode.Cells.Add(new Cell(inputLocalDic["length"], NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 5 && inputLocalDic.ContainsKey("range"))
        //    {
        //        newTreeNode.Cells.Add(new Cell(inputLocalDic["range"], NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 6 && inputLocalDic.ContainsKey("value"))
        //    {

        //        Cell cell = new Cell(inputLocalDic["value"], NodeElementStyle["ParameterStyle"]);
        //        cell.EditorType = eCellEditorType.Default;
        //        newTreeNode.Cells.Add(cell);
        //        step++;
        //    }
        //    if (step == 7 && inputLocalDic.ContainsKey("note"))
        //    {
        //        newTreeNode.Cells.Add(new Cell(inputLocalDic["note"], NodeElementStyle["ParameterStyle"]));
        //        step++;
        //    }
        //    if (step == 8 && inputLocalDic.ContainsKey("preinput") && inputLocalDic.ContainsKey("preinputarea") && inputLocalDic.ContainsKey("name"))
        //    {

        //        Dictionary<string, string> TagDict = new Dictionary<string, string>();
        //        TagDict["preinput"] = inputLocalDic["preinput"];
        //        TagDict["preinputarea"] = inputLocalDic["preinputarea"];
        //        TagDict["name"] = inputLocalDic["name"];
        //        newTreeNode.Tag = TagDict;
        //        step++;
        //    }
        //    return newTreeNode;

        //}


        //private Node GetNodeByDictionay(Dictionary<string, string> inputLocalDic, ElementStyle elementStyle, int imageIndex, int pageIndex)
        //{
        //    Node newTreeNode = new Node();
        //    int step = 1;
        //    if (step == 1)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("type") == true ? inputLocalDic["type"] : "";
        //        newTreeNode = CreateNode(tempString, elementStyle, imageIndex);
        //        step++;
        //    }
        //    if (step == 2)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("vartype") == true ? inputLocalDic["vartype"] : "";
        //        newTreeNode.Cells.Add(new Cell(tempString, elementStyle));
        //        step++;
        //    }
        //    if (step == 3)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("name") == true ? inputLocalDic["name"] : "";
        //        newTreeNode.Cells.Add(new Cell(tempString, elementStyle));
        //        step++;

        //    }
        //    if (step == 4)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("length") == true ? inputLocalDic["length"] : "";
        //        newTreeNode.Cells.Add(new Cell(tempString, elementStyle));
        //        step++;
        //    }
        //    if (step == 5)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("range") == true ? inputLocalDic["range"] : "";
        //        newTreeNode.Cells.Add(new Cell(tempString, elementStyle));
        //        step++;
        //    }
        //    if (step == 6)
        //    {
        //        string tempString = inputLocalDic.ContainsKey("value") == true ? inputLocalDic["value"] : "";
        //        newTreeNode.Cells.Add(new Cell(tempString, elementStyle));
        //        step++;
        //    }
        //    if (step == 7 && inputLocalDic.ContainsKey("note"))
        //    {

        //        string tempString = inputLocalDic.ContainsKey("note") == true ? inputLocalDic["note"] : "";
        //        newTreeNode.Cells.Add(new Cell(tempString, elementStyle));
        //        step++;
        //    }
        //    if (step == 8)
        //    {
        //        Dictionary<string, string> TagDict = new Dictionary<string, string>();
        //        TagDict["preinput"] = inputLocalDic.ContainsKey("preinput") == true ? inputLocalDic["preinput"] : "";
        //        TagDict["preinputarea"] = inputLocalDic.ContainsKey("preinputarea") == true ? inputLocalDic["preinputarea"] : "";
        //        TagDict["name"] = inputLocalDic.ContainsKey("name") == true ? inputLocalDic["name"] : "";
        //        TagDict["CID"] = inputLocalDic.ContainsKey("CID") == true ? inputLocalDic["CID"] : "";
        //        newTreeNode.Tag = TagDict;
        //        step++;
        //    }
        //    return newTreeNode;
        //}
        //private void FillDataToTreeByTraversvalXML(XElement inputElement, Node treeNode, int pageIndex)
        //{
        //    foreach (XElement xElementChild in inputElement.Elements())
        //    {
        //        //创建一个新的treenode，将xmlnode中信息存到treenode中。
        //        Node newTreeNode = null;
        //        if (xElementChild.Name == "configfile" || xElementChild.Name == "parameters" || xElementChild.Name == "structitems")
        //        {
        //            FillDataToTreeByTraversvalXML(xElementChild, treeNode, pageIndex);
        //            return;
        //        }
        //        if (xElementChild.Name == "structitem")
        //        {
        //            //根据preinput参数设置
        //            //1.取出字典指定的数据
        //            Dictionary<string, string> ReturnDic = GetElementValueWithName(structblockDic, xElementChild);
        //            newTreeNode = GetNodeByDictionay(ReturnDic, NodeElementStyle["BlockStyle"], 0, pageIndex);
        //            treeNode.Nodes.Add(newTreeNode);
        //        }
        //        else if (xElementChild.Name == "parameter")
        //        {
        //            //1.取出字典指定的数据
        //            Dictionary<string, string> ReturnDic = GetElementValueWithName(parameterDic, xElementChild);
        //            //2.根据字典的数据生成节点
        //            newTreeNode = GetNodeByDictionay(ReturnDic, NodeElementStyle["ParameterStyle"], 2, pageIndex);
        //            treeNode.Nodes.Add(newTreeNode);
        //        }
        //        if (xElementChild.Elements().Count() != 0)
        //        {
        //            FillDataToTreeByTraversvalXML(xElementChild, newTreeNode, pageIndex);
        //        }
        //    }
        //}
        //private void CheckDictionaryItemLen()
        //{
        //    foreach (var item in UpdateRecords)
        //    {
        //        //int NodeLength = Convert.ToInt32(item.Value.NodeLength);
        //        int NodeLength = item.Value.NodeLength.ToString().Length;
        //        item.Value.NodeValue = item.Value.NodeValue.PadLeft(NodeLength, '0');
        //    }

        //}
        ////private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    ComboBox comboBox = (ComboBox)sender;
        ////    if (comboBox.Tag != null)
        ////    {
        ////        NodeType nodeType = (NodeType)comboBox.Tag;
        ////        UpdateRecords[nodeType.CID] = new UpdateRecord(nodeType.CID, nodeType.NodeLength, ((ComboxItem)comboBox.SelectedItem).Values);
        ////    }
        ////}


        //private Dictionary<string, List<Node>> GetMatchNodeOnAllTree(AdvTree advtree, string MatchName)
        //{
        //    Dictionary<string, List<Node>> ListNodeDic = new Dictionary<string, List<Node>>();
        //    //1.判断目前树中有多少个Preinput依赖节点
        //    foreach (Node nodeItem in advtree.Nodes)
        //    {
        //        TraverslOnAllTree(nodeItem, MatchName, ListNodeDic);
        //    }
        //    return ListNodeDic;
        //}





        //private void AddMinusNodeOnChildTree(Dictionary<string, List<Node>> ListNode, string matchName, int preinputNumber)
        //{
        //    //3.比较Preinput依赖节点数目，根据结果来做出删除和增加操作
        //    foreach (List<Node> listnode in ListNode.Values)
        //    {
        //        //1.增加节点
        //        int listcount = listnode.Count();
        //        if (preinputNumber > listcount)
        //        {
        //            for (int count = 0; count < (preinputNumber - listcount); count++)
        //            {
        //                //1.复制节点最后一个内容
        //                Node OriginNode = listnode.LastOrDefault<Node>();
        //                Node LastNode = OriginNode.DeepCopy();
        //                //2.修改复制的节点内容 isRealChildNode ,NodeCount,ReviseName=preinput,
        //                GetRevisedNode(LastNode, matchName, listcount + count);
        //                //3.插入节点
        //                OriginNode.Parent.Nodes.Insert(OriginNode.Index + count + 1, LastNode);
        //            }
        //        }
        //        //2.删除节点
        //        else if (preinputNumber < listcount)
        //        {
        //            //1.复制节点最后一个内容
        //            Node OriginNode = listnode.FirstOrDefault<Node>();
        //            for (int count = 0; count < (listcount - preinputNumber); count++)
        //            {
        //                Node node = listnode[listcount - 1 - count];
        //                OriginNode.Parent.Nodes.Remove(node);
        //            }
        //        }
        //    }
        //}


        //private void TraverslOnAllTree(Node ParentNode, string PreinputMatchName, Dictionary<string, List<Node>> ListNodeDic)
        //{
        //    if (ParentNode == null) return;
        //    foreach (Node ChildNode in ParentNode.Nodes)
        //    {
        //        if (ChildNode.Tag != null)
        //        {
        //            Dictionary<string, string> TagDict = (Dictionary<string, string>)ChildNode.Tag;
        //            if (TagDict["preinput"] == PreinputMatchName)
        //            {
        //                if (ListNodeDic.ContainsKey(TagDict["name"]) == false)
        //                {
        //                    ListNodeDic[TagDict["name"]] = new List<Node>();
        //                }
        //                ListNodeDic[TagDict["name"]].Add(ChildNode);
        //            }
        //        }
        //        if (ChildNode.HasChildNodes)
        //        {
        //            TraverslOnAllTree(ChildNode, PreinputMatchName, ListNodeDic);
        //        }
        //    }
        //}

        //// private void TraverslAdvTree(Node ParentNode, string PreinputMatchName, Dictionary<string, List<Node>> ListNodeDic)
        //// {
        ////     if (ParentNode == null) return;
        ////     foreach (Node ChildNode in ParentNode.Nodes)
        ////     {
        ////         if (ChildNode.Tag != null)
        ////         {
        ////             if (ChildNode.Tag.ToString() == PreinputMatchName)
        ////             {
        ////                 foreach (Cell cellItem in ChildNode.Cells)
        ////                 {
        ////                     if (cellItem.ColumnHeader.Name.Equals("name"))
        ////                     {
        ////                         if (ListNodeDic.ContainsKey(cellItem.Text) == false)
        ////                         {
        ////                             ListNodeDic[cellItem.Text] = new List<Node>();
        ////                         }
        ////                         ListNodeDic[cellItem.Text].Add(ChildNode);
        ////                     }
        ////                 }
        ////             }
        ////         }
        ////         if (ChildNode.Nodes.Count > 0)
        ////         {
        ////             TraverslAdvTree(ChildNode, PreinputMatchName, ListNodeDic);
        ////         }
        ////     }
        //// }

        ////private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        ////{
        ////    NumericUpDown numericUpDown = (NumericUpDown)sender;
        ////    if (numericUpDown.Tag != null)
        ////    {
        ////        AdvTree CurrentAdvTree = numericUpDown.Parent as AdvTree;
        ////        int SetValue = Convert.ToInt32(numericUpDown.Value);
        ////        GetAdvTreeSelectedRowData<int>(CurrentAdvTree, SetValue);
        ////        NodeType nodeType = (NodeType)numericUpDown.Tag;
        ////        string FormatString = "X" + nodeType.NodeLength;
        ////        string HexString = SetValue.ToString(FormatString);
        ////        UpdateRecords[nodeType.CID] = new UpdateRecord(nodeType.CID, nodeType.NodeLength, HexString);
        ////    }
        ////}
        //private void DomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        //{
        //    DomainUpDown domainUpDown = (DomainUpDown)sender;
        //    if (domainUpDown.Tag != null)
        //    {
        //        //1.得到设置值和Tag值
        //        NodeType nodeType = (NodeType)domainUpDown.Tag;
        //        string selectItemString = domainUpDown.Text.ToString();
        //        int selectItemLength = selectItemString.Length;
        //        uint specNodeLength = Convert.ToUInt32(nodeType.NodeLength);
        //        //2.检查设置值是否长度正确
        //        if (specNodeLength >= selectItemLength)
        //        {
        //            //3.正确，则将数据放入变更记录，同时显示到DomainUpDown中
        //            byte[] needBytes = System.Text.Encoding.ASCII.GetBytes(selectItemString);
        //            StringBuilder stringBuilder = new StringBuilder();
        //            for (int index = 0; index < specNodeLength; index++)
        //            {
        //                if (index < needBytes.Count())
        //                {
        //                    stringBuilder.Append(needBytes[index].ToString("X2"));
        //                }
        //                else
        //                {
        //                    stringBuilder.Append("00");
        //                }
        //            }
        //            UpdateRecords[nodeType.CID] = new UpdateRecord(nodeType.CID, nodeType.NodeLength, stringBuilder.ToString());
        //        }
        //        else
        //        {
        //            //4.不正确，则提示超出长度，显示空值                       
        //            domainUpDown.Text = selectItemString.Substring(0, (int)specNodeLength);
        //        }
        //    }
        //}



        //private void FullDataToAdvTreeFromXMLNode(AdvTree advTree, XElement xElement, string BlockName, int pageIndex)
        //{
        //    if (xElement != null)
        //    {
        //        advTree.BeginUpdate();
        //        Node newTreeNode = null;
        //        newTreeNode = CreateNode(BlockName, NodeElementStyle["BlockStyle"], 0);
        //        FillDataToTreeByTraversvalXML(xElement, newTreeNode, pageIndex);
        //        newTreeNode.Expanded = true;
        //        advTree.Nodes.Add(newTreeNode);
        //        advTree.EndUpdate();
        //    }
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



        //private void TraversalAdvTreeToDict(Node node, Dictionary<string, CSoureFormat> CSourceDic)
        //{
        //    if (node == null) return;
        //    //1.node第一次为configfile
        //    if (node.HasChildNodes)
        //    {
        //        //2.StructNode为复合节点
        //        foreach (Node StructNode in node.Nodes)
        //        {
        //            //3.得到父节点名称，作为字典的key,同时添加一个新的字典值
        //            string StructVarName = GetNodeCellColumnName(StructNode, "name");
        //            if (StructVarName != null)
        //            {   //4.如果是第一次建立,初始化变量
        //                if (CSourceDic.ContainsKey(StructVarName) == false)
        //                {
        //                    CSoureFormat cSoureFormat = new CSoureFormat();
        //                    cSoureFormat.count = 0;
        //                    cSoureFormat.StructData = new List<string>();
        //                    CSourceDic[StructVarName] = cSoureFormat;
        //                }
        //                //5.如果还有子节点
        //                if (StructNode.HasChildNodes)
        //                {
        //                    //6.次数加一
        //                    CSourceDic[StructVarName].count++;
        //                    CSourceDic[StructVarName].StructData.Add(CommStr.BraceBracketOpen);
        //                    foreach (Node ParameterNode in StructNode.Nodes)
        //                    {

        //                        #region 1.将同一个结构体中数组[]成员 放到{}中作为嵌套部分 2.将*pointer类型添加""
        //                        //1.得到name,value,vartype和preinput,preinputarea
        //                        string nameValue = GetNodeCellColumnName(ParameterNode, "name");
        //                        string valueValue = GetNodeCellColumnName(ParameterNode, "value");
        //                        string typeValue = GetNodeCellColumnName(ParameterNode, "type");
        //                        string vartypeValue = GetNodeCellColumnName(ParameterNode, "vartype");
        //                        string preinputvalue = string.Empty;
        //                        string preinputarea = string.Empty;
        //                        Dictionary<string, string> TagDict = (Dictionary<string, string>)ParameterNode.Tag;
        //                        if (TagDict != null && TagDict.ContainsKey("name"))
        //                        {
        //                            if (TagDict["name"].Equals(nameValue))
        //                            {
        //                                preinputvalue = TagDict.ContainsKey("preinput") ? TagDict["preinput"] : "";
        //                                preinputarea = TagDict.ContainsKey("preinputarea") ? TagDict["preinputarea"] : "";
        //                            }
        //                        }
        //                        //2.判断当前变量preintput是否为注册的输入类型entry变量,或者输出类型变量
        //                        // 如果是entry 则将数据value存入字典中
        //                        if (registerPreinput.ContainsKey(nameValue) && preinputvalue.Equals("entry"))
        //                        {
        //                            int Result = 1;
        //                            if (int.TryParse(valueValue, out Result))
        //                            {
        //                                registerPreinput[nameValue] = Result;
        //                            }
        //                        }// 如果是输出型变量数组，则将数据放入{子集中
        //                        if (registerPreinput.ContainsKey(preinputvalue) && vartypeValue.Equals("array[0]"))
        //                        {
        //                            CSourceDic[StructVarName].StructData.Add(CommStr.BraceBracketOpen);
        //                        }
        //                        //如果是字符串指针，添加双""
        //                        if (typeValue.Equals("AAL_UINT8") && vartypeValue.Equals("pointer *"))
        //                        {
        //                            CSourceDic[StructVarName].StructData.Add(CommStr.Quotation + valueValue + CommStr.Quotation);
        //                        }
        //                        else
        //                        {
        //                            CSourceDic[StructVarName].StructData.Add(valueValue);
        //                        }
        //                        // 如果是输出型变量数组，则将数据放入}子集中
        //                        if (registerPreinput.ContainsKey(preinputvalue) && vartypeValue.Equals("array" + "[" + (registerPreinput[preinputvalue] - 1) + "]"))
        //                        {
        //                            CSourceDic[StructVarName].StructData.Add(CommStr.BraceBracketClose);
        //                        }
        //                        #endregion
        //                    }
        //                    CSourceDic[StructVarName].StructData.Add(CommStr.BraceBracketClose);
        //                }
        //                //7.如果没有子节点
        //                else if (StructNode.HasChildNodes == false)
        //                {
        //                    CSourceDic[StructVarName].count++;
        //                    CSourceDic[StructVarName].StructData.Add(CommStr.BraceBracketOpen);
        //                    string ChildString = GetNodeCellColumnName(StructNode, "value");
        //                    CSourceDic[StructVarName].StructData.Add(ChildString);
        //                    CSourceDic[StructVarName].StructData.Add(CommStr.BraceBracketClose);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void AdvTree_DisplayDataViaObj(string xmlpathSource)
        //{
        //    ElementStyleSetting();
        //    //清空页状态记录
        //    //清空变更记录
        //    UpdateRecords.Clear();
        //    //清空preinput的变量名
        //    registerPreinput.Clear();
        //    //初始化显示列字典
        //    SettingDisplayDictionary();
        //    //得到节点元素
        //    xmlFileClass = new TransformFileClass();
        //    string PathName = fileOperation.GetDirectionNameString(xmlpathSource);
        //    string FileName = fileOperation.GetFileNameString(xmlpathSource);
        //    string FileFullName = PathName + FileName;
        //    xElementStartPublic = xmlFileClass.GetRootElement(FileFullName);
        //    //得到指定节点下子节点名称
        //    ListBlockName = xmlFileClass.GetStartElementChildName(xElementStartPublic, "structitems");
        //    CreatePageAndPageProperty();
        //    //得到第一页的内容
        //    string TabPageNameBlock = GetTabPageName(0);
        //    AdvTree CurrentAdvTree = GetAdvTree(0);
        //    AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, 0);
        //    SetTabPageProperty(0, "cn", true);
        //    //得到反序列化的对象
        //    StructDatas structfileObj = ObjectToXml.xmlDeSerializeToStructObj(PathName, FileName);
        //    //填充第一页的内容 用structfileObj 下级 TabPageNameBlock
        //    FullDataToAdvTreeFromXMLObj(CurrentAdvTree, structfileObj, TabPageNameBlock, 0);
        //}
        //void ExpandToolStripButton2_Click(object sender, EventArgs e)
        //{
        //    int PageSelectedIndex = tabControl2.SelectedIndex;
        //    AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
        //    if (CurrentAdvTree != null)
        //    {
        //        foreach (Node node in CurrentAdvTree.Nodes)
        //        {
        //            ExpandNodeChild(node);
        //        }
        //    }
        //}
    }
}
