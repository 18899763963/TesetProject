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

    /// <summary>
    /// 通过Name判断两个项目是否相等
    /// </summary>
    public class EnumEqualityByName : IEqualityComparer<object>
    {
        public bool Equals(object x, object y)
        {
            simpleType ox = x as simpleType;
            simpleType oy = y as simpleType;
            return ox.name == oy.name;
        }
        public int GetHashCode(object obj)
        {
            simpleType objx = obj as simpleType;
            return (objx == null) ? 0 : objx.name.ToString().GetHashCode();
        }
    }
    public class EnumFunction
    {
        public  void CreatesimpleTypeInfo()
        {
            //创建simpleTypeInfo信息
            ComData.enumEntity = new EnumEntity();
            ComData.enumEntity.simpleTypes = new List<simpleType>();
        }

        /// <summary>
        ///  将xml文件化为对象数据序列
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="EnumItemsFileName"></param>
        /// <returns></returns>
        public EnumEntity XmlDeSerializeToEnumObj(string Path, string EnumItemsFileName)
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
            simpleType simpleTypeItem = ComData.enumEntity.simpleTypes.LastOrDefault();
            EnumValue simpleTypeValueItem = new EnumValue();
            simpleTypeValueItem.cn = enValue;
            simpleTypeValueItem.en = cnValue;
            simpleTypeValueItem.value = valueValue;
            simpleTypeItem.EnumValues.Add(simpleTypeValueItem);
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
            List<EnumValue> simpleTypeValues = new List<EnumValue>();
            simpleTypeItem.EnumValues = simpleTypeValues;
            ComData.enumEntity.simpleTypes.Add(simpleTypeItem);
        }

        /// <summary>
        ///  修改simpleTypeItem数据
        /// </summary>
        public void updateValueOfsimpleTypeItem(string key, string value)
        {
            simpleType simpleTypeItem = ComData.enumEntity.simpleTypes.LastOrDefault();
            if (key.Equals("name"))
            {
                simpleTypeItem.name = value;
            }
        }
        /// <summary>
        /// 根据相同name去除重复项，保留最新项
        /// </summary>
        /// <param name="enumEntity"></param>
        public void DistinctSameNameOfEnumEntity(EnumEntity enumEntity)
        {
            if (enumEntity == null) return;
            else
            {
                enumEntity.simpleTypes.Reverse();
                //distinct默认会保留第一个项目
                enumEntity.simpleTypes = enumEntity.simpleTypes.Distinct<simpleType>(new EnumEqualityByName()).ToList();
                enumEntity.simpleTypes.Reverse();
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
        [XmlElement("enumvalue")]
        public List<EnumValue> EnumValues { get; set; }
    }
    public class EnumValue
    {
        [XmlAttribute("en")]
        public string en { get; set; }
        [XmlAttribute("cn")]
        public string cn { get; set; }
        [XmlAttribute("value")]
        public string value { get; set; }
    }

}
