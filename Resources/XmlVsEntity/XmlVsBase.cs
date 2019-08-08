using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmallManagerSpace.Resources.XmlVsEntity
{
    class XmlVsBase
    {
    
        public XElement GetBaseRootElement(string FileFullName)
        {
            //Linq导入
            XDocument xDoc = XDocument.Load(FileFullName);
            //获取根节点
            XElement root = xDoc.Root;
            return root;
            // return  root.Elements();  
        }

        /// <summary>
        /// 通过文件根节点得到数据
        /// </summary>
        /// <param name="xRootElements">文件根节点</param>
        /// <returns>返回数据列表</returns>
        public Dictionary<string, BaseEntity> GetBaseElementString(XElement xRootElements)
        {
            XNamespace xsd = "http://www.w3.org/2001/XMLSchemaTest";
            //XNamespace xdo = "urn:pxp";
            //XNamespace ms = "urn:schemas-microsoft-com:xslt";
            //XNamespace stack = "urn:anything";
            //XNamespace xdb = "http://xmlns.oracle.com/xdb";
            //XNamespace w = "http://www.fiberhome.com.cn/board/control";

            Dictionary<string, BaseEntity> BaseDictionary = new Dictionary<string, BaseEntity>();
            if (xRootElements != null)
            {
                if (xRootElements.HasElements)
                {
                    foreach (XElement elementLevelOne in xRootElements.Elements())
                    {
                        BaseEntity basedata = new BaseEntity();

                        XAttribute xAttributeOne = elementLevelOne.Attribute("name");
                        basedata.name = xAttributeOne.Value;

                        if (elementLevelOne.HasElements)
                        {
                            foreach (XElement elementLevelTwo in elementLevelOne.Elements())
                            {
                                XAttribute xAttributeTwo = elementLevelTwo.Attribute("base");
                                basedata.type = xAttributeTwo.Value.Substring(4);
                                if (elementLevelTwo.HasElements)
                                {
                                    foreach (XElement elementLevelThree in elementLevelTwo.Elements())
                                    {
                                  
                                        if (elementLevelThree.Name == xsd+"length")
                                        {
                                            XAttribute xAttributeValue = elementLevelThree.Attribute("value");
                                            int resultInt = 1;
                                            if (int.TryParse(xAttributeValue.Value, out resultInt))
                                            {
                                                basedata.length = resultInt;
                                            }
                                        }
                                       else if (elementLevelThree.Name == xsd+"range")
                                        {
                                            XAttribute xAttributeRange = elementLevelThree.Attribute("value");
                                            if (xAttributeRange != null)
                                            {
                                                basedata.range = xAttributeRange.Value;
                                            }
                                        }

                                    }
                                }

                            }
                        }

                        BaseDictionary[basedata.name] = basedata;
                    }
                }
            }
            return BaseDictionary;
        }
    }

}
