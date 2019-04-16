﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace SmallManagerSpace.Resources
{
    /// <summary>
    /// 用于存储base文件中的数据
    /// </summary>
    public class BaseData
    {
        public string name { get; set; }
        public string type { get; set; }
        public int length { get; set; }
    }
    public class BaseDataOperation
    {
        public XElement GetBaseRootElement()
        {
            string startUpPath = Application.StartupPath;
            string enumFileName = "base.xsd";
            //Linq导入
            XDocument xDoc = XDocument.Load(startUpPath + "\\" + enumFileName);
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
        public Dictionary<string,BaseData> GetBaseElementString(XElement xRootElements)
        {
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XNamespace xdo = "urn:pxp";
            XNamespace ms = "urn:schemas-microsoft-com:xslt";
            XNamespace stack = "urn:anything";
            XNamespace xdb = "http://xmlns.oracle.com/xdb";
            XNamespace w = "http://www.fiberhome.com.cn/board/control";

            Dictionary<string, BaseData> BaseDictionary = new Dictionary<string, BaseData>();
            if (xRootElements != null)
            {
                if (xRootElements.HasElements)
                {
                    foreach (XElement elementLevelOne in xRootElements.Elements())
                    {
                        BaseData basedata = new BaseData();

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
                                        XAttribute xAttributeThree = elementLevelThree.Attribute("value");
                                        int resultInt = 1;
                                        if (int.TryParse(xAttributeThree.Value, out resultInt))
                                        {
                                            basedata.length = resultInt;
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
