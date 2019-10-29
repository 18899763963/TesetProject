using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;


namespace SmallManagerSpace.Resources
{
    [XmlRootAttribute("structdatas", IsNullable = false)]
    public class StructEntity
    {
        [XmlElement("structitem", Type = typeof(StructItem))]
        public List<object> nodeList = new List<object>();
    }

    [XmlRootAttribute("structitem", IsNullable = false)]
    public class StructItem
    {
        [XmlAttribute("CID")]
        public string CID { get; set; }

        [XmlAttribute("type")]
        public string type { get; set; }

        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("index")]
        public string index { get; set; }

        [XmlAttribute("preinput")]
        public string preinput { get; set; }

        [XmlAttribute("note")]
        public string note { get; set; }

        [XmlAttribute("nodetype")]
        public string nodetype { get; set; }

        //[XmlAttribute("modified")]
        //public string modified { get; set; }

        [XmlElement("parameter", Type = typeof(Parameter))]
        [XmlElement("structitem", Type = typeof(StructItem))]
        public List<object> parameterList = new List<object>();
    }
    public class Parameter
    {
        [XmlAttribute("CID")]
        public string CID { get; set; }
        [XmlAttribute("type")]
        public string type { get; set; }
        [XmlAttribute("preinput")]
        public string preinput { get; set; }
        [XmlAttribute("name")]
        public string name { get; set; }
        [XmlAttribute("index")]
        public string index { get; set; }
        [XmlAttribute("range")]
        public string range { get; set; }
        [XmlAttribute("value")]
        public string value { get; set; }
        [XmlAttribute("length")]
        public string length { get; set; }
        [XmlAttribute("note")]
        public string note { get; set; }
        [XmlAttribute("nodetype")]
        public string nodetype { get; set; }
        [XmlAttribute("modified")]
        public string modified { get; set; }        
    }

    /// <summary>
    /// 通过Name,Index判断两个项的排序
    /// </summary>
    public class StructSortByNameIndex : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            StructItem ox = x as StructItem;
            StructItem oy = y as StructItem;
            if(ox!=null && oy!=null)return (ox.name+ox.index).CompareTo(oy.name+oy.index);
            return 0;
        }
    }
    /// <summary>
    /// 通过Name,Index判断两个项目是否相等
    /// </summary>
    public class StructEqualityByNameIndex : IEqualityComparer<object>
    {
        public bool Equals(object x, object y)
        {
            StructItem ox = x as StructItem;
            StructItem oy = y as StructItem;
            return ox.name == oy.name && ox.index == oy.index;
        }
        public int GetHashCode(object obj)
        {
            StructItem objx = obj as StructItem;
            return (objx == null) ? 0 : objx.name.ToString().GetHashCode();
        }
    }
    /// <summary>
    /// 通过Name判断两个项目是否相等
    /// </summary>
    public class StructItemByName : IEqualityComparer<object>
    {
        public bool Equals(object x, object y)
        {
            StructItem ox = x as StructItem;
            StructItem oy = y as StructItem;
            return ox.name == oy.name;
        }
        public int GetHashCode(object obj)
        {
            StructItem objx = obj as StructItem;
            return (objx == null) ? 0 : objx.name.ToString().GetHashCode();
        }
    }
    /// <summary>
    /// 通过Name判断两个项目是否相等
    /// </summary>
    public class ParameterItemByName : IEqualityComparer<object>
    {
        public bool Equals(object x, object y)
        {

            if(x is Parameter && y is Parameter)
            {
                Parameter ox = x as Parameter;
                Parameter oy = y as Parameter;
                return ox.name == oy.name;

            }else if(x is StructItem && y is StructItem)
            {
                StructItem ox = x as StructItem;
                StructItem oy = y as StructItem;
                return ox.name == oy.name;
            }else if (x is StructItem && y is Parameter)
            {
                StructItem ox = x as StructItem;
                Parameter oy = y as Parameter;
                return ox.name == oy.name;
            }
            else if (x is Parameter && y is StructItem)
            {
                Parameter ox = x as Parameter;
                StructItem oy = y as StructItem;
                return ox.name == oy.name;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode(object obj)
        {

            if(obj is Parameter)
            {
                Parameter objx = obj as Parameter;
                return (objx == null) ? 0 : objx.name.ToString().GetHashCode();
            }else if(obj is StructItem)
            {
                StructItem objx = obj as StructItem;
                return (objx == null) ? 0 : objx.name.ToString().GetHashCode();
            }
            else
            {
                return 0;
            }

        }
    }

    public class StructFunction
    {
        string lastName = null;
        /// <summary>
        ///  创建对象
        /// </summary>
        public void CreateStructEntity()
        {
            ComData.structEntity = new StructEntity();
        }

        /// <summary>
        /// 得到StructEntity对象中的所有preinput="entry"的变量的value值
        /// </summary>
        /// <param name="sEntity">对象名</param>
        /// <returns></returns>
        public Dictionary<string, int> GetEntryVarValue(StructEntity sEntity)
        {
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            foreach (object ob in sEntity.nodeList)
            {
                GetEntryVarChildValue(keyValues, ob);
            }
            return keyValues;
        }

        /// <summary>
        /// 得到StructEntity对象中子节点的所有preinput="entry"的变量的value值
        /// </summary>
        /// <param name="keyValues">存储值的变量</param>
        /// <param name="ob">对象名</param>
        public void GetEntryVarChildValue(Dictionary<string, int> keyValues, object ob)
        {

            if (ob is Parameter)
            {
                Parameter pA = ob as Parameter;
                if (pA.preinput == "entry")
                {
                    int result = 0;
                    lastName = null;
                    if (int.TryParse(pA.value, out result))
                    {
                        if (keyValues.ContainsKey(pA.name))
                        {
                            if (keyValues[pA.name] < result)
                            {
                                keyValues[pA.name] = result;
                            }
                        }
                        else
                        {
                            keyValues[pA.name] = result;
                        }

                    }
                }
                else if (pA.preinput == "invariant")
                {
                    if (keyValues.ContainsKey(pA.name) && lastName == pA.name) keyValues[pA.name] = ++keyValues[pA.name];
                    else
                    {
                        keyValues[pA.name] = 1;
                        lastName = pA.name;
                    }
                }
            }
            else if (ob is StructItem)
            {
                StructItem sI = ob as StructItem;
                foreach (object obChild in sI.parameterList)
                {
                    GetEntryVarChildValue(keyValues, obChild);
                }
            }

        }

        /// <summary>
        /// 得到类型
        /// </summary>
        /// <param name="typeString"></param>
        /// <returns>"base","enum","struct","undefine"</returns>
        public Dictionary<string, object> GetRealNodeType(string typeString)
        {
            Dictionary<string, object> RealNode = new Dictionary<string, object>();
            //匹配基本类型的长度
            if ((ComData.baseDictonary != null) && (ComData.baseDictonary.ContainsKey(typeString)))
            {
                RealNode["base"] = ComData.baseDictonary[typeString];
                return RealNode;
            }
            //匹配enum类型的长度
            else if ((ComData.enumEntity != null) && (ComData.enumEntity.simpleTypes.Where(x => x.name == typeString).Count() != 0))
            {
                RealNode["enum"] = ComData.enumEntity.simpleTypes.Where(x => x.name == typeString).First();
                return RealNode;
            }
            //匹配struct类型的长度
            else if ((ComData.structEntity != null) && (ComData.structEntity.nodeList.Where(x => (x as StructItem).type == typeString).Count() != 0))
            {
                RealNode["struct"] = ComData.structEntity.nodeList.Where(x => (x as StructItem).type == typeString).First();
                return RealNode;
            }
            return RealNode;
        }
        /// <summary>
        /// 创建customStruct对象
        /// </summary>
        /// <param name="defineEntitys"></param>
        public StructEntity CreateCustomStruct(List<DefineEntity> defineEntitys)
        {

            StructEntity structEntity = new StructEntity();

            foreach (DefineEntity defineEntity in defineEntitys)
            {

                //string RegexStr4 = @"(?<varName>[\S]+)[\s]*[\[]+(?<varNum>[\S]*)[\]]+";
                //Match matc = Regex.Match(defineEntity.name, RegexStr4);
                string varName = defineEntity.name;
                string varNum = defineEntity.ArrayNum;
                if (varNum == "")
                {
                    StructItem ob = (ComData.structEntity.nodeList.Where(x => (x as StructItem).type == defineEntity.type).First()) as StructItem;
                    //修改structEntity的index
                    ob.index = "";
                    structEntity.nodeList.Add(new StructItem() { CID = ob.CID, type = ob.type, name = varName, index = ob.index, preinput = ob.preinput, note = ob.note, nodetype = ob.nodetype});
                    TraversalAddItem((structEntity.nodeList.LastOrDefault() as StructItem).parameterList, ob.parameterList);
                }
                else
                {
                    StructItem ob = (ComData.structEntity.nodeList.Where(x => (x as StructItem).type == defineEntity.type).First()) as StructItem;
                    //修改structEntity的index
                    ob.index = "0";
                    if (!ComData.EntryVar.ContainsKey(varNum)) { ComData.EntryVar.Add(varNum, 0); }
                    structEntity.nodeList.Add(new StructItem() { CID = ob.CID, type = ob.type, name = varName, index = ob.index, preinput = varNum, note = ob.note, nodetype = ob.nodetype});
                    TraversalAddItem((structEntity.nodeList.LastOrDefault() as StructItem).parameterList, ob.parameterList);

                }
            }
            return structEntity;
        }
        /// <summary>
        /// 填充customStruct对象的数据
        /// </summary>
        /// <param name="destList"></param>
        /// <param name="sourceList"></param>
        public void TraversalAddItem(List<object> destList, List<object> sourceList)
        {
            foreach (object obj in sourceList)
            {
                if (obj is StructItem)
                {
                    StructItem sobj = obj as StructItem;
                    destList.Add(new StructItem() { CID = sobj.CID, type = sobj.type, name = sobj.name, index = sobj.index, preinput = sobj.preinput, note = sobj.note, nodetype = sobj.nodetype });
                    TraversalAddItem((destList.LastOrDefault() as StructItem).parameterList, sobj.parameterList);
                }
                else if (obj is Parameter)
                {
                    Parameter sobj = obj as Parameter;
                    Dictionary<string, object> keyValue = GetRealNodeType(sobj.type);
                    if (keyValue.Count != 0)
                    {
                        switch (keyValue.Keys.First())
                        {
                            case "base":
                                destList.Add(new Parameter() { CID = sobj.CID, type = sobj.type, name = sobj.name, index = sobj.index, preinput = sobj.preinput, note = sobj.note, range = sobj.range, value = sobj.value, length = sobj.length, nodetype = "base" , modified = sobj.modified });
                                break;
                            case "enum":
                                //默认匹配第一项
                                if (ComData.enumEntity.simpleTypes.Where(x => x.name == sobj.type).Count() != 0)
                                {
                                    simpleType selectItem = ComData.enumEntity.simpleTypes.Where(x => x.name == sobj.type).First();
                                    string defaultEnum = selectItem.EnumValues[0].en;
                                    destList.Add(new Parameter() { CID = sobj.CID, type = sobj.type, name = sobj.name, index = sobj.index, preinput = sobj.preinput, note = sobj.note, range = sobj.range, value = defaultEnum, length = sobj.length, nodetype = "enum", modified = "N" });
                                }
                                break;
                            case "struct":
                                StructItem i = keyValue[keyValue.Keys.First()] as StructItem;
                                destList.Add(new StructItem() { CID = i.CID, type = i.type, name = sobj.name, index = sobj.index, preinput = sobj.preinput, note = i.note, nodetype = "struct"});
                                TraversalAddItem((destList.LastOrDefault() as StructItem).parameterList, i.parameterList);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 将xml文件化为Enum对象数据序列
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="enumItemsFileName"></param>
        /// <returns></returns>
        public EnumEntity XmlDeSerializeToEnumObj(string Path, string enumItemsFileName)
        {
            return EntitySerialize.DESerializerOnFile<EnumEntity>(Path + enumItemsFileName);
        }

        /// <summary>
        ///  将xml文件化为Struct对象数据序列
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="StructItemsFileName"></param>
        /// <returns></returns>
        public StructEntity XmlDeSerializeToStructObj(string Path, string StructItemsFileName)
        {
            return EntitySerialize.DESerializerOnFile<StructEntity>(Path + StructItemsFileName);
        }
        /// <summary>
        ///  修改StructItem中数据
        /// </summary>
        public void UpdateValueOfStructItem(StructItem structitemlast, string key, string value)
        {
            // StructItem structitemlast = ComRunDatas.structEntity.nodeList.LastOrDefault();

            if (structitemlast != null)
            {
                //找到最后一个structitem项目
                if (key.Equals("name"))
                {//由于实际上是一个引用值，即指针，因此修改的值会回传到源
                    structitemlast.name = value;
                }
                if (key.Equals("type"))
                {
                    //由于实际上是一个引用值，即指针，因此修改的值会回传到源
                    structitemlast.type = value;
                }
            }
        }
        /// <summary>
        ///  修改ParameterItem中数据
        /// </summary>
        public void UpdateValueOfParameterItem(StructItem structitemlast, string key, string value)
        {
            //1.找到最后一个structitem项目
            //StructItem structitemlast = ComRunDatas.structEntity.nodeList.LastOrDefault();
            //2.遍历structitemlast中项目
            List<string> PreinputRecord = new List<string>();
            if (key.Equals("preinput"))
            {
                if (structitemlast != null)
                {
                    //3.记录parameter的preinput非空值
                    foreach (Parameter parameterItem in structitemlast.parameterList)
                    {
                        if (!parameterItem.preinput.Equals(""))
                        {
                            PreinputRecord.Add(parameterItem.preinput);
                        }
                    }
                    //4.选择parameter中符合name项，修改该项的preinput值
                    foreach (Parameter parameterItem in structitemlast.parameterList)
                    {
                        if (PreinputRecord.Contains(parameterItem.name))
                        {
                            parameterItem.preinput = value;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 增加StructItem
        /// </summary>
        /// <param name="higherNode"></param>
        /// <param name="CID"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="indexS"></param>
        /// <param name="preinput"></param>
        /// <param name="note"></param>
        /// <param name="nodetype"></param>
        public void AddValueOfStructItem(List<object> higherNode, string CID, string type, string name, string indexS, string preinput, string note, string nodetype)
        {
            //创建structitem信息
            StructItem structitemInfo = new StructItem();

            structitemInfo.CID = CID;
            structitemInfo.type = type;
            structitemInfo.name = name;
            structitemInfo.index = indexS;
            structitemInfo.preinput = preinput;
            structitemInfo.note = note;
            structitemInfo.nodetype = nodetype;       
            //创建structitem信息
            List<object> parameList = new List<object>();
            structitemInfo.parameterList = parameList;
            //添加到队列中
            higherNode.Add(structitemInfo);
        }
        /// <summary>
        /// 增加ParameterItem
        /// </summary>
        /// <param name="higherNode"></param>
        /// <param name="CID"></param>
        /// <param name="type"></param>
        /// <param name="preinput"></param>
        /// <param name="name"></param>
        /// <param name="indexS"></param>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="note"></param>
        /// <param name="nodetype"></param>
        public void AddValueOfParameterItem(StructItem higherNode, string CID, string type, string preinput, string name, string indexS, string range, string value, string length, string note, string nodetype,string modified)
        {
            //创建parameterInfo信息
            Parameter parameterInfo = new Parameter();
            parameterInfo.CID = CID;
            parameterInfo.type = type;
            parameterInfo.preinput = preinput;
            parameterInfo.name = name;
            parameterInfo.index = indexS;
            parameterInfo.range = range;
            parameterInfo.value = value;
            parameterInfo.length = length;
            parameterInfo.note = note;
            parameterInfo.nodetype = nodetype;
            parameterInfo.modified = modified;
            //添加parameterInfo到队列中
            //ComRunDatas.structEntity.nodeList.LastOrDefault().parameterList.Add(parameterInfo);
            higherNode.parameterList.Add(parameterInfo);
        }

        /// <summary>
        /// 根据相同name去除重复项，保留最新项
        /// </summary>
        /// <param name="structEntity"></param>
        public void DistinctSameNameOfStructItem(StructEntity structEntity)
        {
            if (structEntity == null) return;
            else
            {
                structEntity.nodeList.Reverse();
                //distinct默认会保留第一个项目
                structEntity.nodeList = structEntity.nodeList.Distinct<object>(new StructItemByName()).ToList();
                structEntity.nodeList.Reverse();
            }

        }

    }
}

