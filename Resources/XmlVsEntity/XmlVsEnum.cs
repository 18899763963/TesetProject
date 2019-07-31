using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmallManagerSpace.Resources.XmlVsEntity
{
    class XmlVsEnum
    {
    //    string enumFileName = Application.StartupPath + "\\" + "enum.xsd";
    //    public XElement GetEnumRootElement()
    //    {

    //        //Linq导入
    //        XDocument xDoc = XDocument.Load(enumFileName);
    //        //获取根节点
    //        XElement root = xDoc.Root;
    //        return root;

    //    }
    //    public Dictionary<string, EnumProgramData> GetEnumElementString(XElement xRootElements)
    //    {
    //        XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
    //        XNamespace xdo = "urn:pxp";
    //        XNamespace ms = "urn:schemas-microsoft-com:xslt";
    //        XNamespace stack = "urn:anything";
    //        XNamespace xdb = "http://xmlns.oracle.com/xdb";
    //        XNamespace w = "http://www.fiberhome.com.cn/board/control";
    //        Dictionary<string, EnumProgramData> EnumDictionary = new Dictionary<string, EnumProgramData>();
    //        if (xRootElements != null)
    //        {
    //            if (xRootElements.HasElements)
    //            {
    //                foreach (XElement elementLevelOne in xRootElements.Elements())
    //                {
    //                    EnumProgramData enumData = new EnumProgramData();

    //                    XAttribute xAttributeOne = elementLevelOne.Attribute("name");
    //                    enumData.name = xAttributeOne.Value;
    //                    if (elementLevelOne.HasElements)
    //                    {
    //                        foreach (XElement elementLevelTwo in elementLevelOne.Elements())
    //                        {
    //                            XAttribute xAttributeTwo = elementLevelTwo.Attribute("base");
    //                            enumData.type = xAttributeTwo.Value.Substring(4);
    //                            if (elementLevelTwo.HasElements)
    //                            {
    //                                foreach (XElement elementLevelThree in elementLevelTwo.Elements())
    //                                {
    //                                    if (elementLevelThree.Name.Equals(xsd + "length"))
    //                                    {
    //                                        int resultInt = 1;
    //                                        if (int.TryParse(elementLevelThree.Attribute("value").Value, out resultInt))
    //                                        {
    //                                            enumData.length = resultInt;
    //                                        }
    //                                    }
    //                                    else if (elementLevelThree.Name.Equals(xsd + "enumeration"))
    //                                    {
    //                                        if (enumData.EnumrationList == null)
    //                                        {
    //                                            enumData.EnumrationList = new List<Enumration>();
    //                                        }
    //                                        Enumration EnumrationItem = new Enumration();
    //                                        EnumrationItem.en = elementLevelThree.Attribute(w + "en").Value;
    //                                        EnumrationItem.cn = elementLevelThree.Attribute(w + "cn").Value;
    //                                        EnumrationItem.value = elementLevelThree.Attribute("value").Value;
    //                                        enumData.EnumrationList.Add(EnumrationItem);
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }

    //                    EnumDictionary[enumData.name] = enumData;
    //                }
    //            }
    //        }
    //        return EnumDictionary;
    //    }
    }
}
