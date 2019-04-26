﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SmallManagerSpace.Resources
{
    [XmlRootAttribute("structDatas", IsNullable = false)]
    public class StructOfSourceFileDatas
    {
        [XmlArrayAttribute("structitems")]
        public List<StructItem> structItemList { get; set; }

    }
    public class StructOfSourceFileDataOperation
    {
        /// <summary>
        ///  创建对象configfile
        /// </summary>
        public  void CreateConfigFileInfo()
        {
            //创建configfile信息
            ComRunDatas.StructOfSourceFileEntity = new StructOfSourceFileDatas();
            ComRunDatas.StructOfSourceFileEntity.structItemList = new List<StructItem>();
        }
        /// <summary>
        ///  将对象数据序列化
        /// </summary>
        public void XmlSerializeToStructFile(string WorkPath, string StructItemsFileName)
        {
            if (ComRunDatas.StructOfSourceFileEntity != null)
            {
                string FileName = WorkPath + StructItemsFileName;
                EntitySerializeHelper.XmlSerializeOnString(ComRunDatas.StructOfSourceFileEntity, FileName);
            }
        }
        /// <summary>
        ///  修改StructItem中数据
        /// </summary>
        public void UpdateValueOfStructItem(string key, string value)
        {
            StructItem structitemlast = ComRunDatas.StructOfSourceFileEntity.structItemList.LastOrDefault();
            //找到最后一个structitem项目
            if (key.Equals("name"))
            {//由于实际上是一个引用值，即指针，因此修改的值会回传到源
                structitemlast.name = value;
            }
        }

        public  void AddValueOfStructItem(string CID, string type, string name, string preinput, string note)
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
            ComRunDatas.StructOfSourceFileEntity.structItemList.Add(structitemInfo);
        }
        public  StructOfSourceFileDatas XmlDeSerializeToStructObj(string WorkPath, string StructItemsFileName)
        {
            return EntitySerializeHelper.DESerializerOnFile<StructOfSourceFileDatas>(WorkPath + StructItemsFileName);
        }
        /// <summary>
        ///  修改ParameterItem中数据
        /// </summary>
        public  void UpdateValueOfParameterItem(string key, string value)
        {
            //1.找到最后一个structitem项目
            StructItem structitemlast = ComRunDatas.StructOfSourceFileEntity.structItemList.LastOrDefault();
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
            ComRunDatas.StructOfSourceFileEntity.structItemList.LastOrDefault().parameterList.Add(parameterInfo);
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
