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

        public string BeforeValue;
        bool isFisrtEntry = true;
        public Dictionary<string, string> BeforeSelectedColumnData = new Dictionary<string, string>();
        Dictionary<string, int> ColumnName = new Dictionary<string, int>() {
            { "type", 0 }, { "preinput", 1 }, { "name", 2 },
            { "index",3 }, {"length",4 }, {"range",5 }, {"value",6 },
            { "nodetype",7 }, {"CID",8 }, {"note" ,9 } };
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
            ParameterStyle.TextColor = Color.FromArgb(50, 50, 50);
            ParameterStyle.Font = new Font("宋体", 10.5f);
            ParameterStyle.Name = "ValueStyle";
            ComData.nodeElementStyle.Add("ValueStyle", ParameterStyle);
        }

        /// <summary>
        /// 设置Advtree的颜色主题
        /// </summary>
        /// <param name="BackColor"></param>
        /// <param name="ColumnsBackColor"></param>
        /// <param name="AlternateRowColor"></param>
        public void AdvTreeSkinSet(Color BackColor, Color ColumnsBackColor, Color AlternateRowColor)
        {
            ComData.advTree.BackColor = BackColor;
            ComData.advTree.ColumnsBackgroundStyle = new ElementStyle();
            ComData.advTree.ColumnsBackgroundStyle.BackColor = ColumnsBackColor;
            ComData.advTree.AlternateRowColor = AlternateRowColor;
        }


        /// <summary>
        /// 设置该页的名称，树的列，图片，字体等
        /// </summary>
        private void AdvTreeSetting()
        {
            ComData.advTree.Nodes.Clear();
            ComData.advTree.View = eView.Tree;
            if(ComData.skinIndex==0)//默认主题
            {
                AdvTreeSkinSet(Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), Color.AntiqueWhite);
            }
            else if(ComData.skinIndex == 1)//浅色主题
            {
                AdvTreeSkinSet(Color.FromArgb(252, 252, 252), Color.FromArgb(225, 236, 233), Color.WhiteSmoke);
            }
            ComData.advTree.ImageList = new ImageList();
            ComData.advTree.ImageList.Images.Add("BlockImage", SmallManagerSpace.Raws.Resource.BlockIco);
            ComData.advTree.ImageList.Images.Add("PathImage", SmallManagerSpace.Raws.Resource.PathIco);
            ComData.advTree.ImageList.Images.Add("ParameterImage", SmallManagerSpace.Raws.Resource.ParameterIco);
            ComData.advTree.AllowExternalDrop = false;
            ComData.advTree.EnableDataPositionChange = false;
            ComData.advTree.DragDropEnabled = false;
            //列设置
            DevComponents.AdvTree.ColumnHeader columnHeader1 = new DevComponents.AdvTree.ColumnHeader("类型");
            columnHeader1.Name = "type";
            columnHeader1.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader2 = new DevComponents.AdvTree.ColumnHeader("预输入");
            columnHeader2.Name = "preinput";
            columnHeader2.Editable = false;
            columnHeader2.Visible = true;
            DevComponents.AdvTree.ColumnHeader columnHeader3 = new DevComponents.AdvTree.ColumnHeader("名称");
            columnHeader3.Name = "name";
            columnHeader3.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader4 = new DevComponents.AdvTree.ColumnHeader("下标");
            columnHeader4.Name = "index";
            columnHeader4.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader5 = new DevComponents.AdvTree.ColumnHeader("长度");
            columnHeader5.Name = "length";
            columnHeader5.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader6 = new DevComponents.AdvTree.ColumnHeader("范围");
            columnHeader6.Name = "range";
            columnHeader6.Editable = false;
            DevComponents.AdvTree.ColumnHeader columnHeader7 = new DevComponents.AdvTree.ColumnHeader("值");
            columnHeader7.Name = "value";
            columnHeader7.Editable = true;
            DevComponents.AdvTree.ColumnHeader columnHeader8 = new DevComponents.AdvTree.ColumnHeader("元素类型");
            columnHeader8.Name = "nodetype";
            columnHeader8.Editable = false;
            columnHeader8.Visible = false;
            DevComponents.AdvTree.ColumnHeader columnHeader9 = new DevComponents.AdvTree.ColumnHeader("识别码");
            columnHeader9.Name = "CID";
            columnHeader9.Editable = false;
            columnHeader9.Visible = false;
            DevComponents.AdvTree.ColumnHeader columnHeader10 = new DevComponents.AdvTree.ColumnHeader("注释");
            columnHeader10.Name = "note";
            columnHeader10.Editable = false;

            columnHeader1.Width.Relative = 15;
            columnHeader2.Width.Relative = 8;
            columnHeader3.Width.Relative = 13;
            columnHeader4.Width.Relative = 5;
            columnHeader5.Width.Relative = 5;
            columnHeader6.Width.Relative = 8;
            columnHeader7.Width.Relative = 14;
            columnHeader8.Width.Relative = 8;
            columnHeader9.Width.Relative = 8;
            columnHeader10.StretchToFill = true;

            ComData.advTree.Columns.Add(columnHeader1);
            ComData.advTree.Columns.Add(columnHeader2);
            ComData.advTree.Columns.Add(columnHeader3);
            ComData.advTree.Columns.Add(columnHeader4);
            ComData.advTree.Columns.Add(columnHeader5);
            ComData.advTree.Columns.Add(columnHeader6);
            ComData.advTree.Columns.Add(columnHeader7);
            ComData.advTree.Columns.Add(columnHeader8);
            ComData.advTree.Columns.Add(columnHeader9);
            ComData.advTree.Columns.Add(columnHeader10);
            ComData.advTree.CellEdit = true;
            ComData.advTree.BeforeCellEdit += new CellEditEventHandler(this.AdvTreeBeforeCellEdit);
            ComData.advTree.AfterCellEditComplete += new CellEditEventHandler(this.AdvTreeAfterCellEditComplete);

            ComData.advTree.GridColumnLineResizeEnabled = true;
            //ComData.advTree.AlternateRowColor = Color.AntiqueWhite;
            ComData.advTree.Dock = DockStyle.Fill;
            TabItem tim = ComData.tabControl1.CreateTab("配置项");
            tim.AttachedControl.Controls.Add(ComData.advTree);
        }
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
            BeforeValue = selectedNode.SelectedCell.Text;
            // BeforeSelectedColumnData = GetSelectedColumnData(selectedNode);
            //string type_space_name = BeforeSelectedColumnData["type"] + " " + BeforeSelectedColumnData["name"];
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
            //如果是entry节点           
            if (SelectedColumnData["preinput"] == "entry")
            {
                //判断选中节点的数据是否含有preinput=entry 变量
                //处理方法：1.含preinput=entry 变量
                //          2.从父节点开始找到 同级的兄妹结点  name和preinput值相同的节点
                //          3.在同父亲节点中添加或删除新节点

                //1.含preinput=entry 变量
                string preinputName = SelectedColumnData["name"];
                //得到节点变动数值  current,diff  
                Dictionary<string, int> nameNum = GetAddNodeCount(selectedNode, SelectedColumnData["name"], SelectedColumnData["value"]);

                if (nameNum.Count > 0)
                {
                    if (nameNum["diff"] > 0)
                    {
                        //增加节点
                        Node RootNode = CurrentAdvTree.DisplayRootNode;
                        AddStructNode(RootNode, selectedNode, preinputName, nameNum["current"], nameNum["diff"]);
                        AddBaseEnumNode(RootNode, selectedNode, preinputName, nameNum["current"], nameNum["diff"]);
                    }
                    else if (nameNum["diff"] < 0)
                    {
                        //删除节点
                        Node RootNode = CurrentAdvTree.DisplayRootNode;
                        DeletetNode(RootNode, selectedNode, preinputName, nameNum["current"], nameNum["diff"]);

                    }
                }


            }
        }

        private void SubNodeOnSpecial(Dictionary<string, List<Node>> ListNode, int Count)
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

        private void AddNodeOnSpecial(Dictionary<string, List<Node>> ListNode, string MatchName, int Count)
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
                    Node CloneNode = new Node();
                    // GetModifiedMultiNode(OriginNode, CloneNode, MatchName, BaseCount + index);
                    //3.插入节点
                    OriginNode.Parent.Nodes.Insert(OriginNode.Index + index + 1, CloneNode);
                }
            }
        }
        //private void SubNodeOnGeneral(Dictionary<string, List<Node>> ListNode, int Count)
        //{
        //    //1.复制节点最后一个内容
        //    foreach (string KeyName in ListNode.Keys)
        //    {
        //        List<Node> CurrentListNode = ListNode[KeyName];
        //        Node OriginNode = CurrentListNode.FirstOrDefault<Node>();
        //        int BaseCount = CurrentListNode.Count;
        //        for (int index = Count; index < 0; index++)
        //        {
        //            Node node = CurrentListNode[BaseCount + index];
        //            OriginNode.Parent.Nodes.Remove(node);
        //        }
        //    }
        //}
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
        //private void AddNodeOnGeneral(Dictionary<string, List<Node>> ListNode, int Count)
        //{
        //    foreach (string KeyName in ListNode.Keys)
        //    {
        //        List<Node> CurrentListNode = ListNode[KeyName];
        //        int BaseCount = CurrentListNode.Count;
        //        for (int index = 0; index < Count; index++)
        //        {
        //            //1.复制节点最后一个内容
        //            Node OriginNode = CurrentListNode.LastOrDefault<Node>();
        //            //2.修改复制的节点内容 isRealChildNode ,NodeCount,ReviseName=preinput,
        //            Node CloneNode = GetModifiedSingleNode(OriginNode, BaseCount + index);
        //            //3.插入节点
        //            OriginNode.Parent.Nodes.Insert(OriginNode.Index + index + 1, CloneNode);
        //        }
        //    }
        //}

        //private void GetModifiedMultiNode(Node OriginNode, Node CloneNode, string MatchName, int index)
        //{
        //    CloneNode = OriginNode.DeepCopy();
        //    Dictionary<string, string> SelectedNodeData = GetSelectedColumnData(OriginNode);
        //    //负责节点的数据
        //    //如果是struct,enum,base中数组[]
        //    if (SelectedNodeData["index"] != "" && SelectedNodeData["name"] == MatchName)
        //    {
        //        //1.修改index值
        //        CloneNode.Cells[2].Text = index.ToString();

        //    }
        //    //如果是struct节点
        //    if (OriginNode.HasChildNodes && SelectedNodeData["nodetype"] == "struct")
        //    {
        //        //进入下级节点处修改                    
        //        for (int i = 0; i < OriginNode.Nodes.Count; i++)
        //        {
        //            GetModifiedMultiNode(OriginNode.Nodes[i], CloneNode.Nodes[i], MatchName, index);
        //        }
        //    }
        //    //如果是emum节点
        //    else if (SelectedNodeData["nodetype"] == "enum")
        //    {
        //        //嵌入Combox控件
        //        ComBoxObj comBoxObj = new ComBoxObj();
        //        Control control = comBoxObj.CreateEnbedCombox(CloneNode.Cells[5], SelectedNodeData["name"]);
        //        if (control != null) CloneNode.Cells[5].HostedControl = control;
        //    }
        //    //如果是base节点
        //    else if (SelectedNodeData["nodetype"] == "base")
        //    {
        //        //不做事情
        //    }
        //}
        /// <summary>
        /// 得到节点类型
        /// </summary>
        /// <param name="node"></param>
        /// <returns>enum,base,struct</returns>
        public  string getNodeType(Node node)
        {
            Dictionary<string, string> SelectedNodeData = GetSelectedColumnData(node);
            return SelectedNodeData["nodetype"];
        }
        private void setNodeIndexForArray(Node node, int index)
        {
            int indexCell = ColumnName["index"];
            //1.修改index值
            node.Cells[indexCell].Text = index.ToString();
        }
        private void setNodeControlForEnum(Node node, string enumName)
        {
            //嵌入Combox控件
            ComBoxObj comBoxObj = new ComBoxObj();
            int indexCell = ColumnName["value"];
            Control control = comBoxObj.CreateEnbedCombox(node.Cells[indexCell], enumName);
            if (control != null) node.Cells[indexCell].HostedControl = control;
        }

        /// <summary>
        /// 修改节点列表的index 和添加嵌入控件combox
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="setIndex">下标</param>
        private void GetTraversalModifiedStructNode(Node node, int setIndex)
        {

            Dictionary<string, string> SelectedNodeData = GetSelectedColumnData(node);
            switch (SelectedNodeData["nodetype"])
            {
                case "struct":

                    if (isFisrtEntry == true && SelectedNodeData["index"] != "")
                    {
                        //1.修改index值
                        setNodeIndexForArray(node, setIndex);
                        isFisrtEntry = false;
                    }
                    if (node.HasChildNodes)
                    {
                        foreach (Node iNode in node.Nodes)
                        {
                            GetTraversalModifiedStructNode(iNode, setIndex);
                        }
                    }

                    //进入下级节点处修改  
                    break;
                case "enum":
                    setNodeControlForEnum(node, SelectedNodeData["type"]);
                    break;
                case "base":
                    //不做事情
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 修改节点列表的index 和添加嵌入控件combox
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="setIndex">下标</param>
        private void GetTraversalModifiedBaseEnumNode(Node node, int setIndex)
        {
            Dictionary<string, string> SelectedNodeData = GetSelectedColumnData(node);
            //如果是struct,enum,base中数组[]
            if (SelectedNodeData["index"] != "")
            {
                //1.修改index值
                setNodeIndexForArray(node, setIndex);
            }
            switch (SelectedNodeData["nodetype"])
            {
                case "struct":
                    if (SelectedNodeData["index"] != "")
                    {
                        //1.修改index值
                        setNodeIndexForArray(node, setIndex);
                    }
                    if (node.HasChildNodes)
                    {
                        foreach (Node iNode in node.Nodes)
                        {
                            GetTraversalModifiedStructNode(iNode, setIndex);
                        }
                    }

                    //进入下级节点处修改  
                    break;
                case "enum":
                    setNodeControlForEnum(node, SelectedNodeData["type"]);
                    break;
                case "base":
                    //不做事情
                    break;
                default:
                    break;
            }

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
                        CaptureName = null;
                    }
                }
                indexOfCells++;
            }
            return CloneNode;
        }

        /// <summary>
        /// 遍历删除Struct节点
        /// </summary>
        /// <param name="RootN">根节点</param>
        /// <param name="SelectNode">选择节点</param>
        /// <param name="preinputName">预输入名</param>
        /// <param name="require">当前数量</param>
        /// <param name="diff">需要数量（正数）</param>
        private void DeletetNode(Node RootN, Node SelectNode, string preinputName, int require, int diff)
        {
            //********************删除顶级节点************************************//
            //查找同名节点
            Node[] nodes = RootN.Nodes.Find(preinputName, true);
            string columnName = "name";
            //分组同名节点
            Dictionary<string, List<Node>> SameNameNode = NodeGroupByCellHeaderName(nodes, columnName);
            //过滤节点 
            Dictionary<string, List<Node>> StructNodeGroup = SelectNodeGruopByType(SameNameNode, "struct");
            //删除顶级节点
            foreach (string vName in StructNodeGroup.Keys)
            {
                //得到当前同名原属节点数量
                int curCount = StructNodeGroup[vName].Count;
                if ((require - diff) == curCount)
                {
                    //找到第一个节点
                    Node FirstNode = StructNodeGroup[vName].First();
                    if (FirstNode.Parent == ComData.advTree.DisplayRootNode)
                    {
                        //删除节点
                        for (int i = 0; i < curCount - require; i++)
                        {
                            Node node = StructNodeGroup[vName][curCount - i - 1];
                            FirstNode.Parent.Nodes.Remove(node);
                        }
                    }
                }
            }

            //********************删除同级和子级节点************************************//
            //查找同名节点
            Node[] LowNode = SelectNode.Parent.Nodes.Find(preinputName, true);
            //分组同名节点
            string LowColumnName = "name";
            Dictionary<string, List<Node>> LowSameNameNode = NodeGroupByCellHeaderName(LowNode, LowColumnName);
            //遍历删除同级和子级节点
            foreach (string vName in LowSameNameNode.Keys)
            {
                //得到当前同名原属节点数量
                int curCount = LowSameNameNode[vName].Count;
                if ((require - diff) == curCount)
                {
                    //找到第一个节点
                    Node FirstNode = LowSameNameNode[vName].First();
                    //删除节点
                    for (int i = 0; i < curCount - require; i++)
                    {
                        Node node = LowSameNameNode[vName][curCount - i - 1];
                        FirstNode.Parent.Nodes.Remove(node);
                    }

                }
            }
        }

        /// <summary>
        /// 遍历添加Struct节点
        /// </summary>
        /// <param name="RootN">根节点</param>
        /// <param name="SelectNode">选择节点</param>
        /// <param name="preinputName">预输入名</param>
        /// <param name="require">当前数量</param>
        /// <param name="diff">需要数量（正数）</param>
        private void AddStructNode(Node RootN, Node SelectNode, string preinputName, int require, int diff)
        {
            //查找同名节点
            Node[] nodes = RootN.Nodes.Find(preinputName, true);
            string columnName = "name";
            //分组同名节点
            Dictionary<string, List<Node>> SameNameNode = NodeGroupByCellHeaderName(nodes, columnName);
            //过滤节点 
            Dictionary<string, List<Node>> StructNodeGroup = SelectNodeGruopByType(SameNameNode, "struct");
            //分组同父节点
            Dictionary<string, List<Node>> StructSameParentNodeGroup = null;
            if (SelectNode.Parent != ComData.advTree.DisplayRootNode)
            {
                Dictionary<string, string> NodeDictinary = GetSelectedColumnData(SelectNode.Parent);
                string key = NodeDictinary["type"] + "_" + NodeDictinary["preinput"] + "_" + NodeDictinary["name"] + "_" + NodeDictinary["index"];
                StructSameParentNodeGroup = NodeGroupBySameParent(StructNodeGroup, key);
            }
            else if (SelectNode.Parent == ComData.advTree.DisplayRootNode)
            {
                string key = "TopLevel";
                StructSameParentNodeGroup = NodeGroupBySameParent(StructNodeGroup, key);
            }

            //添加节点
            foreach (string vName in StructSameParentNodeGroup.Keys)
            {

                //得到当前同名原属节点数量
                int curCount = StructSameParentNodeGroup[vName].Count;
                if ((require - diff) == curCount)
                {

                    //找到最后一个节点
                    Node LastNode = StructSameParentNodeGroup[vName].LastOrDefault();
                    //复制节点
                    List<Node> CloneNodeL = new List<Node>();
                    for (int i = 0; i < diff; i++)
                    {
                        Node CloneNode = LastNode.DeepCopy();
                        CloneNodeL.Add(CloneNode);
                    }
                    //修改复制的节点
                    int times = 0;
                    foreach (Node iNode in CloneNodeL)
                    {
                        //设置第一次进入
                        isFisrtEntry = true;
                        GetTraversalModifiedStructNode(iNode, curCount + times);
                        times++;
                    }

                    //添加复制的节点
                    for (int i = 0; i < CloneNodeL.Count; i++)
                    {
                        LastNode.Parent.Nodes.Insert(LastNode.Index + i + 1, CloneNodeL.ElementAt(i));
                    }
                }
            }
        }
        /// <summary>
        /// 添加base,enum节点
        /// </summary>
        /// <param name="RootN"></param>
        /// <param name="selectedNode"></param>
        /// <param name="preinputName"></param>
        /// <param name="require"></param>
        /// <param name="diff"></param>
        private void AddBaseEnumNode(Node RootN, Node selectedNode, string preinputName, int require, int diff)
        {
            TraverslAddBaseEnumNode(RootN, selectedNode, preinputName, require, diff);
        }
        /// <summary>
        /// 遍历添加base,enum节点
        /// </summary>
        /// <param name="RootN"></param>
        /// <param name="selectedNode"></param>
        /// <param name="preinputName"></param>
        /// <param name="require"></param>
        /// <param name="diff"></param>
        private void TraverslAddBaseEnumNode(Node RootN, Node selectedNode, string preinputName, int require, int diff)
        {
            //添加同级子级节点
            //寻找，分类
            Node[] nodes = selectedNode.Parent.Nodes.Find(preinputName, false);
            //分组同名节点
            string columnName = "name";
            Dictionary<string, List<Node>> SameNameNode = NodeGroupByCellHeaderName(nodes, columnName);
            //选择节点类型 
            string[] selectArr = { "base", "enum" };
            Dictionary<string, List<Node>> BaseEnumNodeGroup = SelectNodeGruopByType(SameNameNode, selectArr);
            ////分组同父节点
            //Dictionary<string, List<Node>> StructSameParentNodeGroup = null;
            //if (selectedNode.Parent != ComData.advTree.DisplayRootNode)
            //{
            //    Dictionary<string, string> NodeDictinary = GetSelectedColumnData(selectedNode.Parent);
            //    string key = NodeDictinary["type"] + "_" + NodeDictinary["preinput"] + "_" + NodeDictinary["name"] + "_" + NodeDictinary["index"];
            //    StructSameParentNodeGroup = NodeGroupBySameParent(BaseEnumNodeGroup, key);
            //}
            //else if (selectedNode.Parent == ComData.advTree.DisplayRootNode)
            //{
            //    string key = "TopLevel";
            //    StructSameParentNodeGroup = NodeGroupBySameParent(BaseEnumNodeGroup, key);
            //}
            //添加节点
            foreach (string vName in BaseEnumNodeGroup.Keys)
            {
                //得到当前同名原属节点数量
                int curCount = BaseEnumNodeGroup[vName].Count;
                if ((require - diff) == curCount)
                {
                    //找到最后一个节点
                    Node LastNode = BaseEnumNodeGroup[vName].LastOrDefault();
                    //复制节点
                    List<Node> CloneNodeL = new List<Node>();
                    for (int i = 0; i < diff; i++)
                    {
                        Node CloneNode = LastNode.DeepCopy();
                        CloneNodeL.Add(CloneNode);
                    }
                    //修改复制的节点
                    int times = 0;
                    foreach (Node iNode in CloneNodeL)
                    {
                        GetTraversalModifiedBaseEnumNode(iNode, curCount + times);
                        times++;
                    }
                    //添加复制的节点
                    for (int i = 0; i < CloneNodeL.Count; i++)
                    {
                        LastNode.Parent.Nodes.Insert(LastNode.Index + i + 1, CloneNodeL.ElementAt(i));
                    }
                }
            }
            //进入子点
            if (selectedNode.HasChildNodes)
            {
                foreach (Node selectedNodeChild in selectedNode.Nodes)
                {
                    TraverslAddBaseEnumNode(RootN, selectedNodeChild, preinputName, require, diff);
                }
            }

        }

        /// <summary>
        /// 过滤指定类型的节点
        /// </summary>
        /// <param name="nodes">原节点列表</param>
        /// <param name="nodeType">nodtetype:struct,enum,base</param>
        /// <returns></returns>
        private Dictionary<string, List<Node>> SelectNodeGruopByType(Dictionary<string, List<Node>> nodes, string nodeType)
        {
            Dictionary<string, List<Node>> nodeClone = new Dictionary<string, List<Node>>();

            foreach (string key in nodes.Keys)
            {
                foreach (Node node in nodes[key])
                {

                    Cell cell = node.Cells.GetByColumnName("nodetype");
                    //如果不是要过滤的节点，则添加
                    if (cell.Text == nodeType)
                    {
                        if (!nodeClone.ContainsKey(key))
                        {
                            List<Node> newNode = new List<Node>();
                            nodeClone[key] = newNode;
                        }
                        nodeClone[key].Add(node);
                    }

                }

            }
            return nodeClone;
        }
        /// <summary>
        /// 过滤指定类型的节点
        /// </summary>
        /// <param name="nodes">原节点列表</param>
        /// <param name="nodeTypeArr">nodtetype:struct,enum,base</param>
        /// <returns></returns>
        private Dictionary<string, List<Node>> SelectNodeGruopByType(Dictionary<string, List<Node>> nodes, string[] nodeTypeArr)
        {
            Dictionary<string, List<Node>> nodeClone = new Dictionary<string, List<Node>>();
            foreach (string str in nodeTypeArr)
            {
                foreach (string key in nodes.Keys)
                {
                    foreach (Node node in nodes[key])
                    {

                        Cell cell = node.Cells.GetByColumnName("nodetype");
                        //如果不是要过滤的节点，则添加
                        if (cell.Text == str)
                        {
                            if (!nodeClone.ContainsKey(key))
                            {
                                List<Node> newNode = new List<Node>();
                                nodeClone[key] = newNode;
                            }
                            nodeClone[key].Add(node);
                        }

                    }

                }
            }

            return nodeClone;
        }
        /// <summary>
        /// 根据同父分组节点（name,preinput,name,index）都相同则为同父亲
        /// </summary>
        /// <param name="nodes">原节点列表</param>
        /// <param name="selectParentNodeKey">选择节点父节点key</param>
        /// <returns></returns>
        private Dictionary<string, List<Node>> NodeGroupBySameParent(Dictionary<string, List<Node>> nodes, string selectParentNodeKey)
        {
            Dictionary<string, List<Node>> sameNameNode = new Dictionary<string, List<Node>>();
            foreach (List<Node> nodeL in nodes.Values)
            {
                foreach (Node node in nodeL)
                {
                    //若不是顶级父节点，得到父节点数据     
                    if (node.Parent != ComData.advTree.DisplayRootNode)
                    {
                        Dictionary<string, string> NodeDictinary = GetSelectedColumnData(node.Parent);
                        string key = NodeDictinary["type"] + "_" + NodeDictinary["preinput"] + "_" + NodeDictinary["name"] + "_" + NodeDictinary["index"];
                        //如果触发节点和收变动节点是在同一个{}中
                        if (selectParentNodeKey == key)
                        {
                            //如果不存在该键名，创建该列表
                            if (!sameNameNode.ContainsKey(key))
                            {

                                List<Node> nodeItem = new List<Node>();
                                sameNameNode[key] = nodeItem;
                            }

                            sameNameNode[key].Add(node);
                        }

                    }
                    if (node.Parent == ComData.advTree.DisplayRootNode)
                    //如果是顶级节点,得到自身数据
                    {
                        Dictionary<string, string> NodeDictinary = GetSelectedColumnData(node);
                        string key = NodeDictinary["type"] + "_" + NodeDictinary["preinput"] + "_" + NodeDictinary["name"];
                        //如果不存在该键名，创建该列表
                        if (!sameNameNode.ContainsKey(key))
                        {

                            List<Node> nodeItem = new List<Node>();
                            sameNameNode[key] = nodeItem;
                        }

                        sameNameNode[key].Add(node);
                    }

                }
            }
            return sameNameNode;
        }

        /// <summary>
        /// 根据列名称分组节点
        /// </summary>
        /// <param name="nodes">原节点列表</param>
        /// <param name="name">列名</param>
        /// <returns></returns>
        private Dictionary<string, List<Node>> NodeGroupByCellHeaderName(Node[] nodes, string name)
        {
            Dictionary<string, List<Node>> sameNameNode = new Dictionary<string, List<Node>>();
            foreach (Node node in nodes)
            {
                Cell cell = node.Cells.GetByColumnName(name);
                //如果不存在该键名，创建该列表
                if (!sameNameNode.ContainsKey(cell.Text))
                {

                    List<Node> nodeL = new List<Node>();
                    sameNameNode[cell.Text] = nodeL;
                }

                sameNameNode[cell.Text].Add(node);
            }
            return sameNameNode;
        }

        private void TraverslOnSpecial(Node ParentNode, string EntryName, Dictionary<string, List<Node>> ListNodeDic)
        {
            if (ParentNode == null) return;
            foreach (Node ChildNode in ParentNode.Nodes)
            {
                Dictionary<string, string> TagDict = GetSelectedColumnData(ChildNode);
                if (TagDict != null)
                {
                    //如果preinput="xx",并且nodetype=struct
                    if (TagDict["preinput"] == EntryName && TagDict["nodetype"] == "struct")
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
                    TraverslOnSpecial(ChildNode, EntryName, ListNodeDic);
                }
            }
        }

        //private void TraverslOnParentTree(Node ParentNode, string EntryName, Dictionary<string, List<Node>> ListNodeDic)
        //{
        //    if (ParentNode == null) return;
        //    foreach (Node ChildNode in ParentNode.Nodes)
        //    {
        //        if (ChildNode.Tag != null)
        //        {
        //            //Dictionary<string, string> TagDict = (Dictionary<string, string>)ChildNode.Tag;
        //           // Dictionary<string, string> TagDict = GetSelectedColumnData(ChildNode);
        //            if (TagDict["preinput"] == EntryName)
        //            {
        //                string subString = "";
        //                if (TagDict["index"] != "")
        //                {
        //                    if (ListNodeDic.ContainsKey(TagDict["name"]) == false)
        //                    {
        //                        ListNodeDic[subString] = new List<Node>();
        //                    }
        //                    ListNodeDic[subString].Add(ChildNode);
        //                }
        //            }
        //        }
        //        if (ChildNode.HasChildNodes)
        //        {
        //            TraverslOnParentTree(ChildNode, EntryName, ListNodeDic);
        //        }
        //    }
        //}

        /// <summary>
        /// 更新entry变量的新值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void UpdateEntryVarValue(string name, int value)
        {
            //1.更新entry变量的新值
            ComData.EntryVar[name] = value;

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

                ComData.EntryVar[name] = valueInt;
            }
        }
        /// <summary>
        /// 判断是否需要变更树的节点
        /// </summary>
        /// <param name="selectedNode">选择的节点</param>
        /// <param name="PreinputName">匹配变量名称</param>
        /// <param name="CurrentValue">选择的节点值</param>
        /// <returns>等于0:不需要更新树的节点</returns>
        /// <returns>小于0:需要删除树的节点</returns>
        /// <returns>大于0:需要添加树的节点</returns>
        private Dictionary<string, int> GetAddNodeCount(Node selectedNode, string PreinputName, string CurrentValue)
        {
            Dictionary<string, int> Cnt = new Dictionary<string, int>() { };
            int CurrentValueInt = 1;
            int BeforeValueInt = 1;
            //如果可以解析为整数
            if (int.TryParse(CurrentValue, out CurrentValueInt) && int.TryParse(BeforeValue, out BeforeValueInt))
            {
                //输入的值大于0：正常操作
                if (CurrentValueInt > 0 && BeforeValueInt > 0)
                {

                    UpdateEntryVarValue(PreinputName, CurrentValueInt);
                    Cnt["current"] = CurrentValueInt;
                    Cnt["diff"] = CurrentValueInt - BeforeValueInt;
                    return Cnt;
                }
                //值相同：返回0
                else if (CurrentValueInt == BeforeValueInt && CurrentValueInt > 0)
                {
                    Cnt["current"] = CurrentValueInt;
                    Cnt["diff"] = CurrentValueInt - BeforeValueInt;
                    return Cnt;
                }
                else
                {//输入的值小于0：则恢复之前的值
                    selectedNode.SelectedCell.Text = BeforeValue;
                    return Cnt;
                }
            }
            else
            {//如果修改的值不正确：则恢复之前的值
                selectedNode.SelectedCell.Text = BeforeValue;
                return Cnt;
            }
        }

        /// <summary>
        /// 得到选择节点的列名称和数据
        /// </summary>
        /// <param name="node"></param>
        /// <returns>"D(columnName,value)"</returns>
        public Dictionary<string, string> GetSelectedColumnData(Node node)
        {
            Dictionary<string, string> TempKeyValuePairs = new Dictionary<string, string>();

            string[] columnN = ColumnName.Keys.ToArray();
            foreach (int i in ColumnName.Values)
            {
                TempKeyValuePairs[columnN[i]] = node.Cells[i].Text;
            }
            return TempKeyValuePairs;
            //foreach (Cell cellItem in node.Cells)
            //{

            //    //如果不是克隆节点（克隆节点没有columnHeader)

            //    if (cellItem.ColumnHeader != null)
            //    {
            //        if (cellItem.Text == null) TempKeyValuePairs[cellItem.ColumnHeader.Name] = "";
            //        TempKeyValuePairs[cellItem.ColumnHeader.Name] = cellItem.Text;
            //    }
            //    ColumnName.
            //如果是克隆节点
            //else
            //{
            //    Dictionary< string ,int> lumnName = ColumnName.ElementAt(index);
            //    if (cellItem.Text == null) TempKeyValuePairs[cellItem.ColumnHeader.Name] = "";
            //    TempKeyValuePairs[ColumnName[index]] = cellItem.Text;
            //    //}
            //    index++;
            //}


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
            if (ComData.EntryVar.ContainsKey(inputName))
            {
                return true;
            }
            return false;

        }

    }
}
