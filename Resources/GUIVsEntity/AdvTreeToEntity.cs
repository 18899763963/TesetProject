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

        int cid = 1;
        /// <summary>
        /// 将AdvTree节点数据转换为obj
        /// </summary>
        /// <param name="advTree">输入树结构</param>
        public StructEntity GetEntityByAdvTreeNode(AdvTree advTree)
        {
            if (advTree == null) return null; ;
            //重置cid
            int cid = 1;
            //重开辟空间       
            StructEntity structEntity = new StructEntity();
            foreach (Node node in advTree.DisplayRootNode.Nodes)
            {
                AdvTreeObj advTreeObj = new AdvTreeObj();
                string nodeType = advTreeObj.getNodeType(node);
                if (nodeType == "struct" && node.HasChildNodes)
                {
                    //2.通过节点数据得到StructItem对象
                    StructItem structItem = GetStructItemEntity(node, cid++);
                    structEntity.nodeList.Add(structItem);
                    TraversalTreeNode(structEntity.nodeList, node);
                }
            }
            return structEntity;
        }
        /// <summary>
        /// 遍历树节点
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="inNode"></param>
        public void TraversalTreeNode(List<object> nodeList, Node inNode)
        {
            AdvTreeObj advTreeObj = new AdvTreeObj();
            string nodeType = advTreeObj.getNodeType(inNode);
            switch (nodeType)
            {
                case "base":
                    {
                        //得到Parameter对象
                        Parameter parameter = GetParameterEntity(inNode, cid++);
                        (nodeList.LastOrDefault() as StructItem).parameterList.Add(parameter);
                        break;
                    }

                case "enum":
                    {
                        //得到Parameter对象
                        Parameter parameter = GetParameterEntity(inNode, cid++);
                        (nodeList.LastOrDefault() as StructItem).parameterList.Add(parameter);
                        break;
                    }
                case "struct":
                    {
                        //添加struct节点
                        //2.通过节点数据得到StructItem对象
                        if (inNode.Parent != ComData.advTree.DisplayRootNode)
                        {
                            StructItem structItem = GetStructItemEntity(inNode, cid++);
                            (nodeList.LastOrDefault() as StructItem).parameterList.Add(structItem);
                        }

                        //遍历struct子节点
                        if (inNode.HasChildNodes)
                        {
                            foreach (Node nodeChild in inNode.Nodes)
                            {
                                if (inNode.Parent == ComData.advTree.DisplayRootNode)
                                {
                                    TraversalTreeNode(nodeList, nodeChild);

                                }
                                else
                                {
                                    TraversalTreeNode((nodeList.LastOrDefault() as StructItem).parameterList, nodeChild);
                                }
                            }
                        }

                        break;
                    }
                default: break;
            }
        }
        /// <summary>
        /// 得到struct Obj
        /// </summary>
        /// <param name="node"></param>
        /// <param name="CID"></param>
        /// <returns></returns>
        public StructItem GetStructItemEntity(Node node, int CID)
        {
            StructItem structItem = new StructItem();
            AdvTreeObj advTreeObj = new AdvTreeObj();
            Dictionary<string, string> NodeDictinary = advTreeObj.GetSelectedColumnData(node);
            structItem.CID = string.Format("{0:D6}", CID);
            structItem.type = NodeDictinary.ContainsKey("type") ? NodeDictinary["type"] : "";
            structItem.name = NodeDictinary.ContainsKey("name") ? NodeDictinary["name"] : "";
            structItem.preinput = NodeDictinary.ContainsKey("preinput") ? NodeDictinary["preinput"] : "";
            structItem.note = NodeDictinary.ContainsKey("note") ? NodeDictinary["note"] : "";
            structItem.nodetype = NodeDictinary.ContainsKey("nodetype") ? NodeDictinary["nodetype"] : "";
            structItem.index = NodeDictinary.ContainsKey("index") ? NodeDictinary["index"] : "";
            return structItem;
        }
        /// <summary>
        /// 得到parameter Obj
        /// </summary>
        /// <param name="node"></param>
        /// <param name="CID"></param>
        /// <returns></returns>
        public Parameter GetParameterEntity(Node node, int CID)
        {
            Parameter parameter = new Parameter();
            AdvTreeObj advTreeObj = new AdvTreeObj();
            Dictionary<string, string> NodeDictinary = advTreeObj.GetSelectedColumnData(node);
            parameter.CID = string.Format("{0:D6}", CID);
            parameter.type = NodeDictinary.ContainsKey("type") ? NodeDictinary["type"] : "";
            parameter.name = NodeDictinary.ContainsKey("name") ? NodeDictinary["name"] : "";
            parameter.preinput = NodeDictinary.ContainsKey("preinput") ? NodeDictinary["preinput"] : "";
            parameter.range = NodeDictinary.ContainsKey("range") ? NodeDictinary["range"] : "";
            parameter.value = NodeDictinary.ContainsKey("value") ? NodeDictinary["value"] : "";
            parameter.length = NodeDictinary.ContainsKey("length") ? NodeDictinary["length"] : "";
            parameter.note = NodeDictinary.ContainsKey("note") ? NodeDictinary["note"] : "";
            parameter.nodetype = NodeDictinary.ContainsKey("nodetype") ? NodeDictinary["nodetype"] : "";
            parameter.index = NodeDictinary.ContainsKey("index") ? NodeDictinary["index"] : "";
            return parameter;
        }


    }
}
