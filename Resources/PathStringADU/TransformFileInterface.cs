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
using System.Threading;
using SmallManagerSpace;
using System.Windows.Forms;
namespace TransformFileSpace
{
    //public class ChangedNode
    //{
    //    public string CID;
    //    public string Name;
    //    public string Value;
    //    public string Length;
    //}
    //class TransformFileClass
    //{
    //    #region 属性块
    //    public struct BlockNode
    //    {
    //        public List<string> arrayList;
    //        public int arrayListLen;
    //        public string blockIndex;
    //    }
    //    #endregion
    //    #region 函数块
    //    public string MergerXMLFile(string inputXmlPath)
    //    {
    //        //根据XSL文件生成新的XML文件
    //        try
    //        {
    //            #region 使用嵌入资源
    //            //声明XslTransform类实例
    //            XslCompiledTransform xslt = new XslCompiledTransform();
    //            XsltSettings settings = XsltSettings.TrustedXslt;
    //            //XmlSecureResolver resolver = new XmlSecureResolver(new XmlUrlResolver(),http://localhost:821);
    //            settings.EnableDocumentFunction = true;
    //            //Load the XSL stylsheet into the XslCompiledTransform object xslt.Load(xslUrl, settings, new XmlUrlResolver());  
    //            #endregion
    //            //将resource中资源作为数据生成stream
    //            string Tamplate_XSL_String = SmallManagerSpace.Raws.Resource.Tamplate_XSL;
    //            string BaseEnumFile_String = SmallManagerSpace.Raws.Resource.Base_Enum;
    //            byte[] Tamplate_XSL_Array = Encoding.ASCII.GetBytes(Tamplate_XSL_String);
    //            byte[] BaseEnumFile_Array = Encoding.UTF8.GetBytes(BaseEnumFile_String);
    //            MemoryStream Tamplate_XSL_Stream = new MemoryStream(Tamplate_XSL_Array);
    //            MemoryStream BaseEnumFile_Stream = new MemoryStream(BaseEnumFile_Array);
    //            //创建节点集,作为参数传入XSLT
    //            XPathDocument document = new XPathDocument(BaseEnumFile_Stream);
    //            XPathNavigator navigator = document.CreateNavigator();
    //            XPathNodeIterator nodes = navigator.Select("/");
    //            //XSLT参数的使用
    //            //1.创建 XsltArgumentList 对象并使用 AddParam 方法添加参数。
    //            XsltArgumentList argList = new XsltArgumentList();
    //            argList.AddParam("baseFile", "", nodes);
    //            //2.从样式表调用参数。Transform()执行时发生
    //            //3.将 XsltArgumentList 对象传递给 Transform 方法。
    //            using (StreamReader rdr = new StreamReader(Tamplate_XSL_Stream))
    //            {
    //                using (XmlReader xmlRdr = XmlReader.Create(rdr))
    //                {
    //                    xslt.Load(xmlRdr, settings, new XmlUrlResolver());
    //                }
    //            }
    //            //将输出文件放到原文件相同位置
    //            string outputFilePath = GetFileFullNameWithFixedSuffix(inputXmlPath, "GenXml", ".xml");
    //            // Create an XmlWriter to write the output.             
    //            XmlWriter writer = XmlWriter.Create(outputFilePath);
    //            //转化源文件输出到输出文件outputFile
    //            //xslt.Transform(inputXmlPath, outputFilePath);//无参数的情况
    //            xslt.Transform(inputXmlPath, argList, writer);
    //            writer.Close();
    //            return outputFilePath;
    //        }
    //        catch (Exception ex)
    //        {
    //            System.Windows.Forms.MessageBox.Show(ex.ToString());
    //            return null;
    //        }
    //    }

    //    public string GetFileFullNameWithFixedSuffix(string inputFullPath, string fileName, string fileType)
    //    {
    //        //得到输入文件的路径
    //        int lastIndex = inputFullPath.LastIndexOf("\\");
    //        string filePath = inputFullPath.Substring(0, lastIndex + 1);
    //        //文件的尾缀
    //        string outputFullFilePath = filePath + fileName + fileType;
    //        return outputFullFilePath;
    //    }
    //    public string GetFileFullNameWithSuffixTime(string inputFullPath, string fileName, string fileType)
    //    {
    //        //得到输入文件的路径
    //        int lastIndex = inputFullPath.LastIndexOf("\\");
    //        string filePath = inputFullPath.Substring(0, lastIndex + 1);
    //        //取当前时间
    //        System.DateTime currentTime = new System.DateTime();
    //        currentTime = System.DateTime.Now;
    //        //时间作为文件的尾缀
    //        string outputFullFilePath = filePath + fileName + currentTime.Minute.ToString() + fileType;
    //        return outputFullFilePath;
    //    }
    //    public List<string> GetStartElementChildName(XElement StartElementName, string ChildNodeName)
    //    {
    //        List<string> ListBlockName = new List<string>();
    //        foreach (XElement xElementChild in StartElementName.Elements())
    //        {
    //            if(xElementChild.Name.ToString()== ChildNodeName)
    //            {
    //                ListBlockName.Add(xElementChild.Name.ToString());
    //            }              
    //        }
    //        return ListBlockName;
    //    }

    //    public XElement GetRootElement(string FilePath, string StartElementName)
    //    {
    //        //检查输入参数
    //        if (StartElementName == null) { return null; }
    //        if (FilePath == null) { return null; }
    //        try
    //        {
    //            //加载xml文件 linq中的方法
    //            XDocument xDoc = XDocument.Load(FilePath);
    //            //获取根节点
    //            XElement root = xDoc.Root;
    //            ////取指定元素节点
    //            XElement xelement = (from ele in root.Elements()
    //                                 where ele.Name.ToString().Equals(StartElementName)
    //                                 select ele).FirstOrDefault<XElement>();

    //            return xelement;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //    public XElement GetRootElement(string FilePath)
    //    {
    //        //检查输入参数
    //        if (FilePath == null) { return null; }
    //        try
    //        {
    //            //加载xml文件 linq中的方法
    //            XDocument xDoc = XDocument.Load(FilePath);
    //            //获取根节点
    //            XElement root = xDoc.Root;
                
    //            return root;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //    public XElement GetStartElement(string FilePath,string StartElementName)
    //    {
    //        //检查输入参数
    //        if (StartElementName == null) { return null; }
    //        if (FilePath == null) { return null; }
    //        try
    //        {
    //            //加载xml文件 linq中的方法
    //            XDocument xDoc = XDocument.Load(FilePath);
    //            //获取根节点
    //            XElement root = xDoc.Root;
    //            ////取指定元素节点
    //            //XElement xelement = (from ele in root.Elements()
    //            //                     where ele.Name.ToString().Equals(StartElementName)
    //            //                     select ele).FirstOrDefault<XElement>();

    //            return root;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //    public List<BlockNode> getBlockData(string inputXmlPath, string StartEnementName)
    //    {
    //        if (inputXmlPath == null) return null;
    //        XElement xElement = GetStartElement(StartEnementName, inputXmlPath);
    //        //读取XML文件出错
    //        if (xElement == null)
    //        {
    //            return null;
    //        }
    //        //泛型列表
    //        List<BlockNode> blockNodeList = new List<BlockNode>();
    //        //遍历XML文件,得到文件中default的值和长度
    //        foreach (XElement element in xElement.Elements())
    //        {
    //            BlockNode blockNode = new BlockNode();
    //            List<string> listString = new List<string>();
    //            XAttribute xAttribute = element.Attribute("index");
    //            blockNode.arrayList = listString;
    //            blockNode.arrayListLen = 0;
    //            blockNode.blockIndex = xAttribute.Value;
    //            if (element.Name.ToString() == "Block")
    //            {
    //                FillArrayListByTraversval(ref blockNode, element);
    //                blockNodeList.Add(blockNode);
    //            }
    //        }
    //        //检查得到default的值和长度是否一致
    //        for (int index = 0; index < blockNodeList.Count(); index++)
    //        {
    //            List<string> arrayList = blockNodeList[index].arrayList;
    //            int statisticLen = blockNodeList[index].arrayListLen;
    //            int arrayListLength = 0;
    //            for (int indexChild = 0; indexChild < arrayList.Count; indexChild++)
    //            {
    //                arrayListLength = arrayListLength + arrayList[indexChild].ToString().Length;
    //            }
    //            //如果长度不同
    //            if (statisticLen != (arrayListLength / 2)) return null;
    //        }
    //        return blockNodeList;
    //    }
    //    public List<DataSet> GetFillDataSet(XElement xelement)
    //    {
    //        if (xelement == null) { return null; }
    //        //此处进行一个数据Block一个数据集
    //        List<DataSet> ListDataSet = new List<DataSet>();
    //        //向表中添加数据
    //        try
    //        {
    //            if (xelement.Name.ToString() == "ConfigInfo")
    //            {
    //                int index = 1;
    //                //得到ConfigInfo的CID
    //                XAttribute attributeConfgIn = xelement.Attribute("CID");
    //                string attributeConfgInID = attributeConfgIn.Value;
    //                foreach (XElement elementBlock in xelement.Elements())
    //                {
    //                    //如果是Block 
    //                    if (elementBlock.Name.ToString() == "Block")
    //                    {
    //                        //定义数据集，得到表名
    //                        XAttribute attribute = elementBlock.Attribute("Type");
    //                        DataSet dataSetBlock = new DataSet(attribute.Value);
    //                        DataTable BlockTable = new DataTable("Block");
    //                        DataTable PathTable = new DataTable("Path");
    //                        DataTable ParameterTable = new DataTable("Parameter");
    //                        dataSetBlock.Tables.AddRange(new DataTable[] { BlockTable, PathTable, ParameterTable });
    //                        //向表中添加数据
    //                        FillDatasetByTraversval(ref dataSetBlock, elementBlock, attributeConfgInID);
    //                        //设置数据集中的表
    //                        Mutex mutex = new Mutex();
    //                        mutex.WaitOne();
    //                        SettingTable(ref dataSetBlock);
    //                        mutex.ReleaseMutex();
    //                        //数据集队列添加数据集
    //                        ListDataSet.Add(dataSetBlock);
    //                        index++;
    //                    }
    //                }
    //            }
    //            return ListDataSet;
    //        }
    //        catch (Exception ex)
    //        {
    //            System.Windows.Forms.MessageBox.Show(ex.ToString());
    //            return null;
    //        }
    //    }
    //    public static byte[] HexStringToByteArray(string s)
    //    {
    //        s = s.Replace(" ", "");
    //        byte[] buffer = new byte[s.Length / 2];
    //        for (int i = 0; i < s.Length; i += 2)
    //            buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
    //        return buffer;
    //    }
    //    public bool GennrateBinFile(  string inputXmlPath, string DiskCodeStringValue, string DiskAddressStringValue)
    //    {
    //        if (inputXmlPath == null || DiskCodeStringValue == null || DiskAddressStringValue == null) return false;
    //        //1.定义变量
    //        List<BlockNode> blockNodeList = null;
    //        FileStream fs = null;
    //        BinaryWriter bw = null;
    //        //遍历XML数据表,得到数据
    //        blockNodeList = getBlockData(inputXmlPath, "ConfigInfo");
    //        //检查返回结果
    //        if (blockNodeList == null) return false;
    //        try
    //        {
    //            //新建一个文件
    //            /**************************************************************/
    //            string FileName = "conf_data";
    //            string FinleType = ".bin";
    //            //2.将输出文件放到原文件相同位置
    //            string outputFullFilePath = GetFileFullNameWithFixedSuffix(inputXmlPath, FileName, FinleType);
    //            fs = new FileStream(outputFullFilePath, FileMode.Create);
    //            // sw = new StreamWriter(fs);
    //            bw = new BinaryWriter(fs);
    //            //进度条加
    //            //3.序列化要写入的数据
    //            /**************************************************************/
    //            List<byte> listString = new List<byte>();
    //            //进度条加
    //            ////3.1.盘代码：            0-3字节 盘代码 4个字节-------通过用户手动输入
    //            //listString.Add(DiskCodeStringValue); 
    //            byte[] DiskCodeStringValueHex = HexStringToByteArray(DiskCodeStringValue);
    //            listString.AddRange(DiskCodeStringValueHex);
    //            ////3.2.盘地址（框+槽）：   4-5字节 盘地址 2个字节-------通过用户手动输入
    //            byte[] DiskAddressStringValueHex = HexStringToByteArray(DiskAddressStringValue);
    //            listString.AddRange(DiskAddressStringValueHex);
    //            //进度条加
    //            ////3.3.配置扩展类型(填01)：6  字节 配置扩展类型 1个字节-------暂时默认配1
    //            byte ConfigTypeHex = 0x01;
    //            listString.Add(ConfigTypeHex);
    //            //进度条加
    //            ////3.4.子配置块个数      ：7  字节 子配置块个数 1个字节-------xml中配置块的总个数，这里定义为n个配置块
    //            byte BlockCountHex = (byte)blockNodeList.Count();
    //            listString.Add(BlockCountHex);
    //            //块0起始地址
    //            int LastestBlockOffsetLen = 0;
    //            ////3.5.配置描述块        ：8 -（8 + 25 * n）字节 配置描述块 25个字节------描述配置信息，非配置数据，具体如下：
    //            //进度条加
    //            for (int index = 0; index < blockNodeList.Count(); index++)
    //            {
    //                //3.5.1:0-1字节 FH ASCII码 2个字节------固定写ASCII码”FH”
    //                byte[] CampanyIDHex = System.Text.Encoding.Default.GetBytes("FH");
    //                listString.AddRange(CampanyIDHex);
    //                //3.5.2:2  字节 配置块ID  1个字节------配置块ID,xml中读取
    //                string BlockID = blockNodeList[index].blockIndex;
    //                byte[] BlockIDHex = HexStringToByteArray(BlockID);
    //                listString.AddRange(BlockIDHex);
    //                //3.5.3:(Problem) 3-6字节 配置块长度4个字节------配置数据长度（不是这里的配置描述数据）
    //                int BlockLength = blockNodeList[index].arrayListLen;
    //                byte[] BlockLengthHex = BitConverter.GetBytes(BlockLength);
    //                Array.Reverse(BlockLengthHex);
    //                listString.AddRange(BlockLengthHex);
    //                //3.5.4 7-10字节 配置块1的偏移 4个字节
    //                int BlockOffsetLen = LastestBlockOffsetLen;
    //                byte[] BlockOffsetLenHex = BitConverter.GetBytes(BlockOffsetLen);
    //                Array.Reverse(BlockOffsetLenHex);
    //                listString.AddRange(BlockOffsetLenHex);
    //                LastestBlockOffsetLen = BlockLength + LastestBlockOffsetLen;
    //                //3.5.5:11 字节 压缩类型 1个字节------固定写0
    //                byte CompressionTypeHex = 0x00;
    //                listString.Add(CompressionTypeHex);
    //                //3.5.6:12  字节 是否强制执行 1个字节------固定写0
    //                byte isEnforceExecutionHex = 0x00;
    //                listString.Add(isEnforceExecutionHex);
    //                //3.5.7: 13 - 16字节 CRC校验 4个字节 ------ 固定写0
    //                int CRCData = 0;
    //                byte[] CRCDataHex = BitConverter.GetBytes(CRCData);
    //                listString.AddRange(CRCDataHex);
    //                //3.5.8:17 - 24字节 备用字节 8个字节------ - 固定写80 00 00 00 00 00 00 00
    //                byte[] StandbyHex = { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
    //                listString.AddRange(StandbyHex);
    //            }
    //            //进度条加
    //            //3.6.配置块数据：8 + 25 * n - 结束 所有配置块数据，完全按照协议格式，协议ID从小到大(与上面协议描述块顺序相同)来组
    //            foreach (BlockNode blockNode in blockNodeList)
    //            {
    //                foreach (string stringItem in blockNode.arrayList)
    //                {   //得到数据ASCII
    //                    //byte[] stringItemHex = System.Text.Encoding.Default.GetBytes(stringItem);
    //                    //得到数据HEX
    //                    byte[] stringItemHex = HexStringToByteArray(stringItem);
    //                    listString.AddRange(stringItemHex);
    //                }
    //            }
    //            //进度条加
    //            /**************************************************************/
    //            //4.将数据写入文件
    //            foreach (byte byteItem in listString)
    //            {
    //                bw.Write(byteItem);
    //            }
    //            //进度条加
    //            bw.Flush();
    //            bw.Close();
    //            return true;
    //            /**************************************************************/
    //        }
    //        catch
    //        {
    //            if (fs != null || bw != null)
    //            {
    //                bw.Close();
    //                fs.Close();
    //            }
    //            return false;
    //        }
    //    }
    //    public bool UpdateFileNode(string inputXmlPath, List<ChangedNode> changedNodes)
    //    {
    //        //1.输入检查
    //        if (inputXmlPath == null || changedNodes == null) return false;
    //        //2.定位到文档configInfo节点
    //        //加载xml文件 linq中的方法
    //        XDocument xDoc = XDocument.Load(inputXmlPath);
    //        XElement root = xDoc.Root;
    //        //取指定元素节点
    //        XElement xElement = (from ele in root.Elements("ConfigInfo")
    //                             select ele).FirstOrDefault();
    //        if (xElement == null) { return false; }
    //        try
    //        {
    //            //3.遍历修改文件
    //            foreach (ChangedNode changedNode in changedNodes)
    //            {
    //                var targetNode = from Node in xElement.Descendants("Parameter")
    //                                 where (string)Node.Attribute("CID") == changedNode.CID
    //                                 select Node;
    //                foreach (var i in targetNode) { i.Attribute(changedNode.Name).Value = changedNode.Value; }
    //            }
    //            xDoc.Save(inputXmlPath);
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            System.Windows.Forms.MessageBox.Show(ex.ToString());
    //            return false;
    //        }
    //    }
    //    public void FillArrayListByTraversval(ref BlockNode blockNode, XElement inputElement)
    //    {
    //        //得到每个Block//Parameter下面的Default值
    //        //检查输入
    //        if (inputElement == null)
    //        {
    //            return;
    //        }
    //        foreach (XElement element in inputElement.Elements())
    //        {
    //            string elementName = element.Name.ToString();
    //            if (element.Elements().Count() > 0)
    //            {
    //                //如果为非终端节点 ->   Block,Path       
    //                if (elementName == "Block")
    //                {
    //                    FillArrayListByTraversval(ref blockNode, element);
    //                }
    //                if (elementName == "Path")
    //                {
    //                    //如果path的上一个目录为中Data="Line_Number"，且Default="0000",则跳过Path中的内容
    //                    XElement elementBefore = element.ElementsBeforeSelf("Parameter").LastOrDefault();
    //                    bool isSkipPath = false;
    //                    if (elementBefore != null)
    //                    {
    //                        XAttribute attribute1 = elementBefore.Attribute("Data");
    //                        XAttribute attribute2 = elementBefore.Attribute("default");
    //                        if ((attribute1.Value == "Line_Number" && attribute2.Value == "0000") || (attribute1.Value == "Mask_Config_Item_Number" && attribute2.Value == "00000000"))
    //                        {
    //                            isSkipPath = true;
    //                        }
    //                    }
    //                    //得到default的值      
    //                    if (!isSkipPath)
    //                    {
    //                        FillArrayListByTraversval(ref blockNode, element);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                //如果为终端节点 ->   Parameter           
    //                if (elementName == "Parameter")
    //                {
    //                    XAttribute attributeDef = null;
    //                    XAttribute attributeLen = null;
    //                    //得到default的值
    //                    attributeDef = element.Attribute("default");
    //                    //得到length的值
    //                    attributeLen = element.Attribute("length");
    //                    int attributeLenInt = Convert.ToInt32(attributeLen.Value);

    //                    if ((attributeLenInt*2)>attributeDef.Value.Length)
    //                    {
    //                        string attributedefvalue = string.Empty;
    //                        attributedefvalue= attributeDef.Value.PadLeft(attributeLenInt*2, '0');                      
    //                        blockNode.arrayList.Add(attributedefvalue);
    //                        blockNode.arrayListLen = blockNode.arrayListLen + attributeLenInt;
    //                    }else
    //                    {
    //                        blockNode.arrayList.Add(attributeDef.Value);
    //                        blockNode.arrayListLen = blockNode.arrayListLen + attributeLenInt;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    public void SettingTable(ref DataSet dataSet)
    //    {
    //        foreach (DataTable dataTable in dataSet.Tables)
    //        {
    //            if (dataTable.TableName == "Parameter")
    //            {
    //                dataTable.Columns["PID"].ReadOnly = true;
    //                dataTable.Columns["CID"].ReadOnly = true;
    //                dataTable.Columns["Data"].ReadOnly = true;
    //                dataTable.Columns["type"].ReadOnly = true;
    //                dataTable.Columns["base"].ReadOnly = true;
    //                dataTable.Columns["length"].ReadOnly = true;
    //                dataTable.Columns["value"].ReadOnly = true;
    //                dataTable.Columns["PID"].SetOrdinal(0);
    //                dataTable.Columns["CID"].SetOrdinal(1);
    //                dataTable.Columns["Data"].SetOrdinal(2);
    //                dataTable.Columns["type"].SetOrdinal(3);
    //                dataTable.Columns["base"].SetOrdinal(4);
    //                dataTable.Columns["length"].SetOrdinal(5);
    //                dataTable.Columns["value"].SetOrdinal(6);
    //                dataTable.Columns["default"].SetOrdinal(7);
    //            }
    //            else if (dataTable.TableName == "Path")
    //            {
    //                dataTable.Columns["PID"].ReadOnly = true;
    //                dataTable.Columns["CID"].ReadOnly = true;
    //                dataTable.Columns["Data"].ReadOnly = true;
    //                dataTable.Columns["PID"].SetOrdinal(0);
    //                dataTable.Columns["CID"].SetOrdinal(1);
    //                dataTable.Columns["Data"].SetOrdinal(2);
    //            }
    //            else if (dataTable.TableName == "Block")
    //            {
    //                dataTable.Columns["PID"].ReadOnly = true;
    //                dataTable.Columns["CID"].ReadOnly = true;
    //                dataTable.Columns["Type"].ReadOnly = true;
    //                dataTable.Columns["index"].ReadOnly = true;
    //                dataTable.Columns["PID"].SetOrdinal(0);
    //                dataTable.Columns["CID"].SetOrdinal(1);
    //                dataTable.Columns["Type"].SetOrdinal(2);
    //                dataTable.Columns["index"].SetOrdinal(3);
    //            }
    //        }
    //    }
    //    public void FillDatasetByTraversval(ref DataSet dataSetReturn, XElement inputElement, string parentID)
    //    {
    //        //检查输入
    //        if (dataSetReturn == null || inputElement == null || parentID == null)
    //        {
    //            return;
    //        }
    //        //如果为Block元素            
    //        if (inputElement.Name.ToString() == "Block")
    //        {
    //            FillDataTable(ref dataSetReturn, inputElement, parentID);
    //            //跳入下一集Path 或者Parameter
    //            XAttribute attributeBlock = inputElement.Attribute("CID");
    //            parentID = attributeBlock.Value;
    //        }
    //        foreach (XElement element in inputElement.Elements())
    //        {
    //            string elementName = element.Name.ToString();
    //            if (element.Elements().Count() > 0)
    //            {
    //                //如果为非终端节点 ->   Block,Path  
    //                //得到属性的CID作为下一级的PID                  
    //                if (elementName == "Path")
    //                {
    //                    FillDataTable(ref dataSetReturn, element, parentID);
    //                    XAttribute attribute = element.Attribute("CID");
    //                    string parentPathID = attribute.Value;
    //                    FillDatasetByTraversval(ref dataSetReturn, element, parentPathID);
    //                }
    //            }
    //            else
    //            {
    //                //如果为终端节点 ->   Parameter           
    //                if (elementName == "Parameter")
    //                {
    //                    FillDataTable(ref dataSetReturn, element, parentID);
    //                }
    //            }
    //        }
    //    }
    //    public void FillDataTable(ref DataSet dataSetReturn, XElement element, string ParentID)
    //    {
    //        //检查输入元素
    //        if (dataSetReturn == null || element == null || ParentID == null) return;
    //        //得到元素对应的数据表
    //        DataTable dataTable = dataSetReturn.Tables[element.Name.ToString()];
    //        DataRow datarow = dataTable.NewRow();
    //        DataColumn datacolumn = null;
    //        //增加ID列
    //        if (dataTable.Columns["PID"] == null)
    //        {
    //            DataColumn datacolumnPID = new DataColumn("PID", typeof(System.String));
    //            dataTable.Columns.Add(datacolumnPID);
    //        }
    //        //向行中添加数据
    //        foreach (XAttribute attribute in element.Attributes())
    //        {
    //            string name = attribute.Name.ToString();
    //            //如果属性名称为visible,则跳过该属性，以便不将该属性放入表中
    //            if (name == "visible")
    //            {
    //                continue;
    //            }
    //            //没有该列则添加新的一列
    //            if (dataTable.Columns[name] == null)
    //            {
    //                datacolumn = new DataColumn(name, typeof(System.String));
    //                dataTable.Columns.Add(datacolumn);
    //            }
    //            //向行中添加数据
    //            datarow[name] = attribute.Value;
    //        }
    //        datarow["PID"] = ParentID;
    //        dataTable.Rows.Add(datarow);
    //    }
    //    #endregion
    //}
}
