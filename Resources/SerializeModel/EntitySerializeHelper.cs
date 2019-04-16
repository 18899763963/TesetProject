using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace SmallManagerSpace.Resources
{
    /// <summary>
    /// XML序列化公共处理类
    /// </summary>
    public static class EntitySerializeHelper
    {
        /// <summary>
        /// 将实体对象转换成XML文件
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
        public static void XmlSerializeOnFile<T>(T obj)
        {
            try
            {
                //当在某个代码段中使用了类的实例，而希望无论因为什么原因，
                //只要离开了这个代码段就自动调用这个类实例的Dispose。 
                using (FileStream fs = new FileStream("serialiable.xml", FileMode.Create))
                {
                    Type t = obj.GetType();
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(fs, obj);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }
        /// <summary>
        /// 将实体对象转换成XML
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
        /// <param name="fileName">文件路径</param>
        public static void XmlSerializeOnString<T>(T obj, string fileName)
        {
            try
            {
                // Create an XmlTextWriter using a FileStream.
                Stream fs = new FileStream(fileName, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineChars = "\r\n";
                settings.Encoding = Encoding.UTF8;
                settings.IndentChars = "    ";
                using (XmlWriter writer = XmlWriter.Create(fs, settings))
                {
                    serializer.Serialize(writer, obj);
                    writer.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }
        /// <summary>
        /// 将XML文件转换成实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        public static T DESerializerOnFile<T>(string FileFullName) where T : class
        {
            try
            {
                 using (FileStream fs = new FileStream(FileFullName, FileMode.Open))
                {
               
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(fs) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将XML转换成实体对象异常", ex);
            }
        }

        public static T DESerializerFromFile<T>(string filePath, string xmlRootName = "structfile") where T : class
        {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                using (StringReader sr = new StringReader(xmlDoc.OuterXml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    return (xmldes.Deserialize(sr) as T);
                }
        }


        /// <summary>
        /// 将XML转换成实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strXML">XML</param>
        public static T DESerializerOnString<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将XML转换成实体对象异常", ex);
            }
        }
    }
}
