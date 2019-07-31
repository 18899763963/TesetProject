using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SmallManagerSpace.Resources
{
    [XmlRootAttribute("structDatas", IsNullable = false)]
    public class StructEntity
    {
        [XmlArrayAttribute("structitems")]
        public List<StructItem> structItemList { get; set; }

    }
    public class StructFunction
    {
        /// <summary>
        ///  创建对象configfile
        /// </summary>
        public  void CreateConfigFileInfo()
        {
            //创建configfile信息
            ComRunDatas.structEntity = new StructEntity();
            ComRunDatas.structEntity.structItemList = new List<StructItem>();
        }
        /// <summary>
        ///  将对象数据序列化为xml文件
        /// </summary>
        public void XmlSerializeToStructFile(string WorkPath, string StructItemsFileName)
        {
            if (ComRunDatas.structEntity != null)
            {
                string FileName = WorkPath + StructItemsFileName;
                EntitySerialize.XmlSerializeOnString(ComRunDatas.structEntity, FileName);
            }
        }
        /// <summary>
        ///  将xml文件化为对象数据序列
        /// </summary>
        /// </summary>
        /// <param name="WorkPath"></param>
        /// <param name="StructItemsFileName"></param>
        /// <returns></returns>
        public StructEntity XmlDeSerializeToStructObj(string Path, string StructItemsFileName)
        {
            return EntitySerialize.DESerializerOnFile<StructEntity>(Path + StructItemsFileName);
        }
        /// <summary>
        ///  修改StructItem中数据
        /// </summary>
        public void UpdateValueOfStructItem(string key, string value)
        {
            StructItem structitemlast = ComRunDatas.structEntity.structItemList.LastOrDefault();
            //找到最后一个structitem项目
            if (key.Equals("name"))
            {//由于实际上是一个引用值，即指针，因此修改的值会回传到源
                structitemlast.name = value;
            }
        }


        /// <summary>
        ///  修改ParameterItem中数据
        /// </summary>
        public  void UpdateValueOfParameterItem(string key, string value)
        {
            //1.找到最后一个structitem项目
            StructItem structitemlast = ComRunDatas.structEntity.structItemList.LastOrDefault();
            //2.遍历structitemlast中项目
            List<string> PreinputRecord = new List<string>();
            if (key.Equals("preinput"))
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
        /// <summary>
        /// 增加StructItem
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="preinput"></param>
        /// <param name="note"></param>
        public void AddValueOfStructItem(string CID, string type, string name, string preinput, string note)
        {
            //创建structitem信息
            StructItem structitemInfo = new StructItem();
            structitemInfo.CID = CID;
            structitemInfo.type = type;
            structitemInfo.name = name;
            structitemInfo.preinput = preinput;
            structitemInfo.note = note;
            //创建structitem信息
            List<Parameter> parameList = new List<Parameter>();
            structitemInfo.parameterList = parameList;
            //添加到队列中
            ComRunDatas.structEntity.structItemList.Add(structitemInfo);
        }
        /// <summary>
        /// 增加ParameterItem
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="type"></param>
        /// <param name="preinput"></param>
        /// <param name="name"></param>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="note"></param>
        public void AddValueOfParameterItem(string CID, string type , string preinput, string name, string range, string value, string length, string note)
        {
            //创建parameterInfo信息
            Parameter parameterInfo = new Parameter();
            parameterInfo.CID = CID;
            parameterInfo.type = type;
            parameterInfo.preinput = preinput;
            parameterInfo.name = name;
            parameterInfo.range = range;
            parameterInfo.value = value;
            parameterInfo.length = length;
            parameterInfo.note = note;
            //添加parameterInfo到队列中
            ComRunDatas.structEntity.structItemList.LastOrDefault().parameterList.Add(parameterInfo);
        }
    }
    public class StructItem
    {
        [XmlAttribute("CID")]
        public string CID { get; set; }

        [XmlAttribute("type")]
        public string type { get; set; }

        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("preinput")]
        public string preinput { get; set; }

        [XmlAttribute("note")]
        public string note { get; set; }
         [XmlArrayAttribute("parameters")]
        public List<Parameter> parameterList { get; set; }
    }
    [XmlRootAttribute("parameter", IsNullable = false)]
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
        [XmlAttribute("range")]
        public string range { get; set; }
        [XmlAttribute("value")]
        public string value { get; set; }
        [XmlAttribute("length")]
        public string length { get; set; }
        [XmlAttribute("note")]
        public string note { get; set; }
    }
}
