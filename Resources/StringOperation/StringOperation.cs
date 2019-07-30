using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmallManagerSpace.Resources
{
    class StringOperation
    {
        XNamespace w3Space = "http://www.w3.org/2001/XMLSchema";
        XNamespace FiberHomeSpace = "http://www.fiberhome.com.cn/board/control";
        public XElement GetEnumRootElement()
        {
            #region 使用嵌入资源    
#if InternalFile
            //将resource中资源作为数据生成stream
            string EnumString = SmallManagerSpace.Raws.Resource.Enum;
            byte[] EnumBytes = Encoding.Unicode.GetBytes(EnumString);
            MemoryStream EnumStream = new MemoryStream(EnumBytes);
            //Linq导入
            XDocument xDoc = XDocument.Load(EnumStream);
#endif
            #endregion
            #region 使用外部资源
            string startUpPath = Application.StartupPath;
            string enumFileName = "enum.xsd";
            //Linq导入
            XDocument xDoc = XDocument.Load(startUpPath+"\\"+ enumFileName);
            #endregion
            //获取根节点
            XElement root = xDoc.Root;
            return root;
            // return  root.Elements();  
        }
        public Dictionary<string, string> GetEnumSpecElementString(XElement xRootElements, string SimpleTypeName, string LanguageType)
        {

            Dictionary<string, string> NodeValue = null;
            //取指定元素节点schema 
            XElement xElementSelc = (from ele in xRootElements.Elements()
                                     where (ele.Attribute("name").Value.Equals(SimpleTypeName))
                                     select ele).FirstOrDefault();
            if (xElementSelc != null)
            {
                var enumerationElem = xElementSelc.Element(w3Space + "restriction").Elements(w3Space + "enumeration");
                NodeValue = new Dictionary<string, string>();
                foreach (var nodevar in enumerationElem)
                {
                    if (LanguageType == "cn")
                    {
                        NodeValue[nodevar.Attribute("value").Value] = nodevar.Attribute(FiberHomeSpace + "cn").Value;
                    }
                    else if (LanguageType == "en")
                    {
                        NodeValue[nodevar.Attribute("value").Value] = nodevar.Attribute(FiberHomeSpace + "en").Value;
                    }
                }
            }
            return NodeValue;
        }
        public Dictionary<string, string> GetEnumSpan(string SpanRootString)
        {
            Dictionary<string, string> SpanDictionary = new Dictionary<string, string>();
            if (SpanRootString.Contains("[") && SpanRootString.Contains("]"))
            {
                string SpanString = SpanRootString.Substring(1, SpanRootString.Length - 2);
                //字符串不包含",-"
                if (!SpanString.Contains(",") && !SpanString.Contains("-"))
                {
                    SpanDictionary[SpanString] = SpanString;
                }
                //字符串仅仅包含","
                else if (SpanString.Contains(",") && !SpanString.Contains("-"))
                {
                    string[] arrayList = SpanString.Split(',');
                    for (int index = 0; index < arrayList.Count(); index++)
                    {
                        SpanDictionary[arrayList[index]] = arrayList[index];
                    }

                }
                //字符串仅仅包含"-"
                else if (!SpanString.Contains(",") && SpanString.Contains("-"))
                {
                    string[] arrayList = SpanString.Split('-');
                    int downNum = int.Parse(arrayList[0], System.Globalization.NumberStyles.HexNumber);
                    int upNum = int.Parse(arrayList[1], System.Globalization.NumberStyles.HexNumber);
                    int stringLen = arrayList[0].Length;
                    for (int index = downNum; index < upNum + 1; index++)
                    {
                        string FormatString = "X" + stringLen;
                        string HexString = (Convert.ToUInt64(index)).ToString(FormatString);
                        SpanDictionary[HexString] = HexString;
                    }
                }
                //字符串都包含",-"
                else
                {
                    string[] arrayList = SpanString.Split(',');
                    int typeLength = 0;
                    for (int times = 0; times < arrayList.Count(); times++)
                    {
                        string[] subItem = arrayList[times].Split('-');
                        if (subItem.Count() == 2)
                        {
                            int downNum = int.Parse(subItem[0], System.Globalization.NumberStyles.HexNumber);
                            int upNum = int.Parse(subItem[1], System.Globalization.NumberStyles.HexNumber);
                            typeLength = subItem[0].Length;
                            string FormatString = String.Format("0:X{0}", typeLength);
                            for (int startNum = downNum; startNum < upNum + 1; startNum++)
                            {
                                string HexString = String.Format("{" + FormatString + "}", startNum);
                                SpanDictionary[HexString] = HexString;
                            }
                        }
                        else if ((subItem.Count() == 1))
                        {
                            SpanDictionary[subItem[0]] = subItem[0];
                        }
                    }

                }
            }
            return SpanDictionary;

        }
        public Dictionary<string, string> GetDictIntersect(Dictionary<string, string> dictEnum, Dictionary<string, string> dictSpan)
        {
            Dictionary<string, string> returnDict = new Dictionary<string, string>();
            if (dictEnum != null && dictSpan != null)
            {
                foreach (string key in dictSpan.Keys)
                {
                    if (dictEnum.ContainsKey(key))
                    {
                        returnDict[key] = dictEnum[key];
                    }
                }
            }
            if (returnDict.Keys.Count == 0)
            { returnDict = null; }
            return returnDict;
        }
        public string GetComboxItemString(ComboxItem[] comboBoxItems, string SpecString)
        {
            string returnString = null;
            for (int index = 0; index < comboBoxItems.Count(); index++)
            {
                if ((comboBoxItems[index]).Values == SpecString)
                {
                    returnString = (comboBoxItems[index]).Text;
                    break;
                }
            }
            if (returnString == null) returnString = (comboBoxItems[0]).Text;
            return returnString;
        }
        public ComboxItem[] DictionaryToComboxItem(Dictionary<string, string> Dict)
        {

            ComboxItem[] values = new ComboxItem[Dict.Count];
            for (int index = 0; index < Dict.Count; index++)
            {
                var item = Dict.ElementAt(index);
                values[index] = new ComboxItem(item.Value, item.Key);

            }
            return values;
        }
        public class ComboxItem
        {
            private string text;
            private string values;
            public string Text
            {
                get { return this.text; }
                set { this.text = value; }
            }
            public string Values
            {
                get { return this.values; }
                set { this.values = value; }
            }
            public ComboxItem(string _Text, string _Values)
            {
                Text = _Text;
                Values = _Values;
            }

            public override string ToString()
            {
                return Text;
            }
        }
    }

}
