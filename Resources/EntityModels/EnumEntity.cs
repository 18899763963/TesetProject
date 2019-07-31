using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SmallManagerSpace.Resources
{
    [XmlRootAttribute("schema", IsNullable = false)]
    public class EnumEntity
    {
        [XmlArrayAttribute("simpleTypes")]
        public List<simpleType> simpleTypes { get; set; }
    }

    public class EnumFunction
    {
        public  void CreatesimpleTypeInfo()
        {
            //创建simpleTypeInfo信息
            ComRunDatas.enumEntity = new EnumEntity();
            ComRunDatas.enumEntity.simpleTypes = new List<simpleType>();
        }
        /// <summary>
        ///  将对象数据序列化
        /// </summary>
        public void XmlSerializeToEnumFile(string WorkPath, string EnumItemsFileName)
        {
            if (ComRunDatas.structEntity != null)
            {
                string FileName = WorkPath + EnumItemsFileName;
                EntitySerialize.XmlSerializeOnString(ComRunDatas.enumEntity, FileName);
            }
        }

        /// <summary>
        ///  将xml文件化为对象数据序列
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="EnumItemsFileName"></param>
        /// <returns></returns>
        public EnumEntity XmlDeSerializeToStructObj(string Path, string EnumItemsFileName)
        {            
            return EntitySerialize.DESerializerOnFile<EnumEntity>(Path + EnumItemsFileName);
        }
        /// <summary>
        /// 添加子项目到对象实体中
        /// </summary>
        /// <param name="enValue"></param>
        /// <param name="cnValue"></param>
        /// <param name="valueValue"></param>
        public void addValueOfEnumOfsimpleTypeItem(string enValue, string cnValue, string valueValue)
        {
            simpleType simpleTypeItem = ComRunDatas.enumEntity.simpleTypes.LastOrDefault();
            enumerationValue simpleTypeValueItem = new enumerationValue();
            simpleTypeValueItem.cn = enValue;
            simpleTypeValueItem.en = cnValue;
            simpleTypeValueItem.value = valueValue;
            simpleTypeItem.enumerationValues.Add(simpleTypeValueItem);
        }
        /// <summary>
        ///  添加数据到simpleType，除enumeration部分
        /// </summary>
        public  void addValueOfsimpleTypeItemWithout(string baseValue, string lengthValue, string valueValue)
        {
            simpleType simpleTypeItem = new simpleType();
            //初始化类成员
            simpleTypeItem.baseType = baseValue;
            simpleTypeItem.length = lengthValue;
            List<enumerationValue> simpleTypeValues = new List<enumerationValue>();
            simpleTypeItem.enumerationValues = simpleTypeValues;
            ComRunDatas.enumEntity.simpleTypes.Add(simpleTypeItem);
        }

        /// <summary>
        ///  修改simpleTypeItem数据
        /// </summary>
        public void updateValueOfsimpleTypeItem(string key, string value)
        {
            simpleType simpleTypeItem = ComRunDatas.enumEntity.simpleTypes.LastOrDefault();
            if (key.Equals("name"))
            {
                simpleTypeItem.name = value;
            }
        }

    }
    public class simpleType
    {
        [XmlAttribute("name")]
        public string name { get; set; }
        [XmlAttribute("baseType")]
        public string baseType { get; set; }
        [XmlAttribute("length")]
        public string length { get; set; }
        [XmlArrayAttribute("enumerationItems")]
        public List<enumerationValue> enumerationValues { get; set; }
    }
    public class enumerationValue
    {
        [XmlAttribute("en")]
        public string en { get; set; }
        [XmlAttribute("cn")]
        public string cn { get; set; }
        [XmlAttribute("value")]
        public string value { get; set; }
    }

}
