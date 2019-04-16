using MasterDetailSample;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XMLFileClassSpace
{
    class XMLFileClass
    {
        //根据XSL文件生成新的XML文件
        public string MergerXMLFile(string inputXmlPath)
        {
            try
            {

                #region 使用嵌入资源
                ////获得正在运行类所在的名称空间
                //Type type = MethodBase.GetCurrentMethod().DeclaringType;
                //string _namespace = type.Namespace;
                ////获取当前主程序集
                //Assembly currentAssembly = Assembly.GetExecutingAssembly();
                ////资源的根名称
                //string resourceRootName = _namespace + ".ResourceTest";

                ////实例化资源管理类
                //ResourceManager resourceManager = new ResourceManager(resourceRootName, currentAssembly);

                //根据资源名获得资源对象


                //声明XslTransform类实例
                XslCompiledTransform xslt = new XslCompiledTransform();
                XsltSettings settings = XsltSettings.TrustedXslt;
                //XmlSecureResolver resolver = new XmlSecureResolver(new XmlUrlResolver(),http://localhost:821);
                settings.EnableDocumentFunction = true;
                //Load the XSL stylsheet into the XslCompiledTransform object xslt.Load(xslUrl, settings, new XmlUrlResolver());  
                #endregion
                //将resource中资源作为数据生成stream
                string Tamplate_XSL_String = Resource.Tamplate_XSL;
                string BaseEnumFile_String = Resource.Base_Enum;

                byte[] Tamplate_XSL_Array = Encoding.ASCII.GetBytes(Tamplate_XSL_String);
                byte[] BaseEnumFile_Array = Encoding.UTF8.GetBytes(BaseEnumFile_String);
                MemoryStream Tamplate_XSL_Stream = new MemoryStream(Tamplate_XSL_Array);
                MemoryStream BaseEnumFile_Stream = new MemoryStream(BaseEnumFile_Array);
                //创建节点集,作为参数传入XSLT
                XPathDocument document = new XPathDocument(BaseEnumFile_Stream);
                XPathNavigator navigator = document.CreateNavigator();
                XPathNodeIterator nodes = navigator.Select("/");
                //XSLT参数的使用
                //1.创建 XsltArgumentList 对象并使用 AddParam 方法添加参数。
                XsltArgumentList argList = new XsltArgumentList();
                argList.AddParam("baseFile", "", nodes);
                //2.从样式表调用参数。Transform()执行时发生
                //3.将 XsltArgumentList 对象传递给 Transform 方法。
                using (StreamReader rdr = new StreamReader(Tamplate_XSL_Stream))
                {
                    using (XmlReader xmlRdr = XmlReader.Create(rdr))
                    {
                        xslt.Load(xmlRdr, settings, new XmlUrlResolver());
                    }
                }
                //得到输入文件的路径
                int lastIndex = inputXmlPath.LastIndexOf("\\");
                string filePath = inputXmlPath.Substring(0, lastIndex);
                //取当前年月日时分秒
                System.DateTime currentTime = new System.DateTime();
                currentTime = System.DateTime.Now;
                //时间作为文件的尾缀
                string outputFilePath = filePath + "\\" + "GenXml" + currentTime.Minute.ToString() + ".xml";
                // Create an XmlWriter to write the output.             
                XmlWriter writer = XmlWriter.Create(outputFilePath);

                //转化源文件输出到输出文件outputFile
                //xslt.Transform(inputXmlPath, outputFilePath);//无参数的情况
                xslt.Transform(inputXmlPath, argList, writer);
                writer.Close();
                return outputFilePath;
            }
            catch
            {
                return null;
            }
        }
        public List<Array> ChangedRecord(List<Array> ListArray, string CID,string Name ,string Value)
        {
            string[] stringArray = new string[3] { CID, Name, Value };
            ListArray.Add(stringArray);
            return ListArray;
        }
        #region 得到文件的指定节点和块名

        public XElement GetStartElement(string StartElementName, string FilePath)
        {

            //检查输入参数
            if (StartElementName == null) { return null; }
            if (FilePath == null) { return null; }
            //加载xml文件 linq中的方法
            XDocument xDoc = XDocument.Load(FilePath);
            //获取根节点
            XElement root = xDoc.Root;
            //取指定元素节点
            XElement xElement = (from ele in root.Elements(StartElementName)
                                     // where ele.Value.Equals("Item1")
                                 select ele).FirstOrDefault();
            if (xElement == null) { return null; }
            else { return xElement; }
        }
        #endregion

        public List<DataSet> GetFillDataSet(XElement xelement)
        {
            if (xelement == null) { return null; }
            //此处进行一个数据Block一个数据集
            List<DataSet> ListDataSet = new List<DataSet>();
            //向表中添加数据
            try
            {
                if (xelement.Name.ToString() == "ConfigInfo")
                {
                    int index = 1;
                    //得到ConfigInfo的CID
                    XAttribute attributeConfgIn = xelement.Attribute("CID");
                    string attributeConfgInID = attributeConfgIn.Value;
                    foreach (XElement elementBlock in xelement.Elements())
                    {
                        //如果是Block 
                        if (elementBlock.Name.ToString() == "Block")
                        {
                            //定义数据集，得到表名
                            XAttribute attribute = elementBlock.Attribute("Type");
                            DataSet dataSetBlock = new DataSet(attribute.Value);
                            DataTable BlockTable = new DataTable("Block");
                            DataTable PathTable = new DataTable("Path");
                            DataTable ParameterTable = new DataTable("Parameter");
                            dataSetBlock.Tables.AddRange(new DataTable[] { BlockTable, PathTable, ParameterTable });
                            
                            //向表中添加数据
                            FillDatasetByTraversval(ref dataSetBlock, elementBlock, attributeConfgInID);
                            //设置表的列只读
                            BlockTable.Columns["PID"].ReadOnly = true;
                            BlockTable.Columns["CID"].ReadOnly = true;
                            BlockTable.Columns["PID"].ReadOnly = true;
                            BlockTable.Columns["CID"].ReadOnly = true;
                            PathTable.Columns["PID"].ReadOnly = true;
                            PathTable.Columns["CID"].ReadOnly = true;
                            ParameterTable.Columns["PID"].ReadOnly = true;
                            ParameterTable.Columns["CID"].ReadOnly = true;
                            //设置列顺序
                            BlockTable.Columns["PID"].SetOrdinal(0);
                            BlockTable.Columns["CID"].SetOrdinal(1);
                            PathTable.Columns["PID"].SetOrdinal(0);
                            PathTable.Columns["CID"].SetOrdinal(1);
                            ParameterTable.Columns["PID"].SetOrdinal(0);
                            ParameterTable.Columns["CID"].SetOrdinal(1);
                            //数据集队列添加数据集
                            ListDataSet.Add(dataSetBlock);
                            index++;
                        }
                    }

                }
                return ListDataSet;
            }
            catch
            {
                return null;
            }

        }
        public void FillDatasetByTraversval(ref DataSet dataSetReturn, XElement inputElement, string parentID)
        {
            //检查输入
            if (dataSetReturn == null || inputElement == null || parentID == null)
            {
                return;
            }
            //如果为Block元素            
            if (inputElement.Name.ToString() == "Block")
            {
                FillDataTable(ref dataSetReturn, inputElement, parentID);
                //跳入下一集Path 或者Parameter
                XAttribute attributeBlock = inputElement.Attribute("CID");
                parentID = attributeBlock.Value;
            }
            foreach (XElement element in inputElement.Elements())
            {
              
                string elementName = element.Name.ToString();
                if (element.Elements().Count() > 0)
                {
                    //如果为非终端节点 ->   Block,Path  
                    //得到属性的CID作为下一级的PID                  
                    if (elementName == "Path")
                    {
                      
                        FillDataTable(ref dataSetReturn, element, parentID);
                        XAttribute attribute = element.Attribute("CID");
                        string parentPathID = attribute.Value;
                        FillDatasetByTraversval(ref dataSetReturn, element, parentPathID);
                       
                    }
                }
                else
                {
                    //如果为终端节点 ->   Parameter           
                    if (elementName == "Parameter")
                    {
                        FillDataTable(ref dataSetReturn, element, parentID);
                    }
                }
            }
        }

        public void FillDataTable(ref DataSet dataSetReturn, XElement element, string ParentID)
        {
            //检查输入元素
            if (dataSetReturn == null || element == null || ParentID == null) return;
            //得到元素对应的数据表
            DataTable dataTable = dataSetReturn.Tables[element.Name.ToString()];
            DataRow datarow = dataTable.NewRow();
            DataColumn datacolumn = null;
            //增加ID列
            if (dataTable.Columns["PID"] == null)
            {
                DataColumn datacolumnPID = new DataColumn("PID", typeof(System.String));
                dataTable.Columns.Add(datacolumnPID);

            }
            //向行中添加数据
            foreach (XAttribute attribute in element.Attributes())
            {
                string name = attribute.Name.ToString();
                //没有该列则添加新的一列
                if (dataTable.Columns[name] == null)
                {
                    datacolumn = new DataColumn(name, typeof(System.String));
                    dataTable.Columns.Add(datacolumn);
                }
                //向行中添加数据

                datarow[name] = attribute.Value;

            }
            datarow["PID"] = ParentID;
            dataTable.Rows.Add(datarow);

        }

    }
}
