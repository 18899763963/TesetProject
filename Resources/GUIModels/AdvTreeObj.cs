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

        public void InitAdvTreeDatas()
        {
            ElementStyleSetting();
            AdvTreeSetting();
        }
        /// <summary>
        /// 初始化节点和元素风格
        /// </summary>
        public void ElementStyleSetting()
        {
            ComData.nodeElementStyle.Clear();
            //  Define node style
            ElementStyle BlockStyle = new ElementStyle();
            BlockStyle.TextColor = Color.SeaGreen;
            BlockStyle.Font = new Font("宋体", 10.5f);
            BlockStyle.Name = "BlockStyle";
            ComData.nodeElementStyle.Add("BlockStyle", BlockStyle);
            ElementStyle PathStyle = new ElementStyle();
            PathStyle.TextColor = Color.Navy;
            PathStyle.Font = new Font("宋体", 10.5f);
            PathStyle.Name = "PathStyle";
            ComData.nodeElementStyle.Add("PathStyle", PathStyle);
            ElementStyle ParameterStyle = new ElementStyle();
            ParameterStyle.TextColor = Color.Navy;
            ParameterStyle.Font = new Font("宋体", 10.5f);
            ParameterStyle.Name = "ParameterStyle";
            ComData.nodeElementStyle.Add("ParameterStyle", ParameterStyle);
            ElementStyle SpanStyle = new ElementStyle();
            ParameterStyle.TextColor = Color.Navy;
            ParameterStyle.Font = new Font("宋体", 10.5f);
            ParameterStyle.Name = "SpanStyle";
            ComData.nodeElementStyle.Add("SpanStyle", ParameterStyle);
            ElementStyle ValueStyle = new ElementStyle();
            ParameterStyle.TextColor = Color.Navy;
            ParameterStyle.Font = new Font("宋体", 10.5f);
            ParameterStyle.Name = "ValueStyle";
            ComData.nodeElementStyle.Add("ValueStyle", ParameterStyle);
        }

        /// <summary>
        /// 设置该页的名称，树的列，图片，字体等
        /// </summary>
        private void AdvTreeSetting()
        {
            ComData.advTree.Nodes.Clear();
            ComData.advTree.View = eView.Tree;
            ComData.advTree.ImageList = new ImageList();
            ComData.advTree.ImageList.Images.Add("BlockImage", SmallManagerSpace.Raws.Resource.BlockIco);
            ComData.advTree.ImageList.Images.Add("PathImage", SmallManagerSpace.Raws.Resource.PathIco);
            ComData.advTree.ImageList.Images.Add("ParameterImage", SmallManagerSpace.Raws.Resource.ParameterIco);
            ComData.advTree.AllowExternalDrop = false;
            ComData.advTree.EnableDataPositionChange = false;
            ComData.advTree.DragDropEnabled = false;
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
            ComData.advTree.Columns.Add(columnHeader1);
            ComData.advTree.Columns.Add(columnHeader2);
            ComData.advTree.Columns.Add(columnHeader3);
            ComData.advTree.Columns.Add(columnHeader4);
            ComData.advTree.Columns.Add(columnHeader5);
            ComData.advTree.Columns.Add(columnHeader6);
            ComData.advTree.CellEdit = true;
            ComData.advTree.BeforeCellEdit += new CellEditEventHandler(this.AdvTreeBeforeCellEdit);
            ComData.advTree.AfterCellEditComplete += new CellEditEventHandler(this.AdvTreeAfterCellEditComplete);

            ComData.advTree.GridColumnLineResizeEnabled = true;
            ComData.advTree.AlternateRowColor = Color.AntiqueWhite;
            ComData.advTree.Dock = DockStyle.Fill;


            TabItem tim = ComData.tabControl1.CreateTab("配置项");
            tim.AttachedControl.Controls.Add(ComData.advTree);
        }
        Dictionary<string, string> BeforeSelectedColumnData = new Dictionary<string, string>();
        /// <summary>
        /// 保存选中节点的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvTreeBeforeCellEdit(object sender, EventArgs e)
        {
            //1.得到选中节点的数据
            AdvTree CurrentAdvTree = (AdvTree)sender;
            Node selectedNode = CurrentAdvTree.SelectedNode;
            SelectedCellOriginValue = selectedNode.SelectedCell.Text;
            BeforeSelectedColumnData = GetSelectedColumnData(selectedNode);
            string type_space_name = BeforeSelectedColumnData["type"] + " " + BeforeSelectedColumnData["name"];
        }

        /// <summary>
        /// 处理Value列变更后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvTreeAfterCellEditComplete(object sender, EventArgs e)
        {
            //得到选中节点的数据
            AdvTree CurrentAdvTree = (AdvTree)sender;
            Node selectedNode = CurrentAdvTree.SelectedNode;
            Dictionary<string, string> SelectedColumnData = GetSelectedColumnData(selectedNode);
            //判断ColumnHeader[value]是否有改变,如果值有变，则进入内部处理
            if (!SelectedColumnData["value"].Equals(BeforeSelectedColumnData["value"]))
            {
                //判断选中节点的数据是否含有preinput=entry 变量
                if (IsEntryVar(SelectedColumnData["name"]))
                {
                    //根据变量的范围统一设定为节点的父节点内     
                    int CountOfProcess = GetStateOfProcessNode(selectedNode, SelectedColumnData["name"], SelectedColumnData["value"]);
                    //判断是否为PublicPreinput
                    if ((SelectedColumnData["name"]).Equals(ComData.publicPreinputName) && CountOfProcess != 0)
                    {
                        Dictionary<string, List<Node>> ListNode = GetMatchNodeOnAncestorTree(selectedNode, SelectedColumnData["name"]);
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
                        Dictionary<string, List<Node>> ListNode = GetMatchNodeOnParentTree(selectedNode, SelectedColumnData["name"]);
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
        /// <summary>
        /// 得到选中节点列名的元胞
        /// </summary>
        /// <param name="node"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public Cell GetSelectedNodeCell(Node node, string ColumnName)
        {
            Cell cell = null;
            foreach (Cell cellItem in node.Cells)
            {
                if (cellItem.ColumnHeader.Name.Equals(ColumnName))
                {
                    return cell = cellItem;
                }
            }
            return cell;
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
                        //1.匹配Enum，并添加嵌入下拉列表控件                
                        if (CaptureName == null)
                        {
                            // if (entityToAdvTreeOBJ.IsMatchedEnumName(cellItem.Text))
                            {
                                CaptureName = cellItem.Text;
                            }
                        }
                        else if (CaptureName != null)
                        {
                            if (columnHeader.Name.Equals("value"))
                            {
                                ComBoxObj comBoxObj = new ComBoxObj();
                                //List<ComBoxEnumChild> enumrationList = entityToAdvTreeOBJ.GetEnumrationList(CaptureName);
                                // Control control = comBoxObj.CreateEnbedCombox(enumrationList);
                                //CloneChildNode.Cells[indexOfCells].HostedControl = control;
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
                Dictionary<string, string> SelectedNodeData = GetSelectedColumnData(OriginNode);
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
                    // if (entityToAdvTreeOBJ.IsMatchedEnumName(cellItem.Text))
                    {
                        CaptureName = cellItem.Text;
                    }
                }
                else if (CaptureName != null)
                {
                    if (columnHeader.Name.Equals("value"))
                    {
                        ComBoxObj comBoxObj = new ComBoxObj();
                        // List<ComBoxEnumChild> enumrationList = entityToAdvTreeOBJ.GetEnumrationList(CaptureName);
                        // Control control = comBoxObj.CreateEnbedCombox(enumrationList);
                        // CloneNode.Cells[indexOfCells].HostedControl = control;
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

        /// <summary>
        /// 更新entry变量的新值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void UpdateEntryVarValue(string name, int value)
        {
            //1.更新entry变量的新值
            ComData.entryVar[name] = value;

        }
        /// <summary>
        /// 注册entry变量的新值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddEntryVar(string name, string value)
        {
            int valueInt = 1;
            if (int.TryParse(value, out valueInt))
            {

                ComData.entryVar[name] = valueInt;
            }
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
        private int GetStateOfProcessNode(Node selectedNode, string PreinputName, string NowSelectedValue)
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
                        UpdateEntryVarValue(PreinputName, NowValueInt);
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
        /// <summary>
        /// 得到选择节点的列名称和数据
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetSelectedColumnData(Node node)
        {
            Dictionary<string, string> SelectedTagData = (Dictionary<string, string>)node.Tag;
            Dictionary<string, string> TempKeyValuePairs = new Dictionary<string, string>();
            foreach (Cell cellItem in node.Cells)
            {
                string stringText = cellItem.Text;
                string HeaderName = cellItem.ColumnHeader == null ? "" : cellItem.ColumnHeader.Name;
                TempKeyValuePairs[HeaderName] = stringText;
            }

            TempKeyValuePairs = TempKeyValuePairs.Union(SelectedTagData).ToDictionary(k => k.Key, v => v.Value);
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
        private bool IsEntryVar(string inputName)
        {
            if (ComData.entryVar.ContainsKey(inputName))
            {
                return true;
            }
            return false;

        }
     
    }
}
