using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SmallManagerSpace.Resources
{
    class EventLogClass
    {
        ///// <summary> 
        ///// 日志文件记录 
        ///// </summary> 
        ///// <param name="logName">日志描述</param>
        ///// <param name="msg">写入信息</param> 
        //public static void WriteMsg(string logName, string msg)
        //{
        //    try
        //    {
        //        string path = Path.Combine("./log");
        //        if (!Directory.Exists(path))//判断是否有该文件 
        //            Directory.CreateDirectory(path);
        //        string logFileName = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";//生成日志文件 
        //        if (!File.Exists(logFileName))//判断日志文件是否为当天
        //        {
        //            FileStream fs;
        //            fs = File.Create(logFileName);//创建文件
        //            fs.Close();
        //        }
        //        StreamWriter writer = File.AppendText(logFileName);//文件中添加文件流

        //        writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + logName + "\r\n" + msg);
        //        writer.WriteLine("--------------------------------分割线--------------------------------");
        //        writer.Flush();
        //        writer.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        string path = Path.Combine("./log");
        //        if (!Directory.Exists(path))
        //            Directory.CreateDirectory(path);
        //        string logFileName = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
        //        if (!File.Exists(logFileName))//判断日志文件是否为当天
        //        {
        //            FileStream fs;
        //            fs = File.Create(logFileName);//创建文件
        //            fs.Close();
        //        }
        //        StreamWriter writer = File.AppendText(logFileName);//文件中添加文件流
        //        writer.WriteLine(DateTime.Now.ToString("日志记录错误HH:mm:ss") + "\r\n " + e.Message + " " + msg);
        //        writer.WriteLine("--------------------------------分割线--------------------------------");
        //        writer.Flush();
        //        writer.Close();
        //    }
        //}
        ///*****第二种*******/
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="logtype">日志类型</param>
        ///// <param name="source">来源</param>
        ///// <param name="message">结果</param>
        ///// <param name="detail">详细信息</param>
        //public static void Write(string logtype, string source, string message, string detail)
        //{
        //    try
        //    {
        //        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        //        Directory.CreateDirectory(path);
        //        string str2 = Path.Combine(path, DateTime.Today.ToString("yyyyMMdd") + ".log");
        //        string str3 = "********************************" + DateTime.Now.ToString() + "********************************";
        //        using (FileStream stream = new FileStream(str2, FileMode.Append, FileAccess.Write, FileShare.Read))
        //        {
        //            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
        //            {
        //                writer.WriteLine(str3);
        //                writer.WriteLine("Type: " + logtype);
        //                writer.WriteLine("Source: " + source);
        //                writer.WriteLine("Message: " + message);
        //                writer.WriteLine("Detail: " + detail);
        //                string str4 = "";
        //                writer.WriteLine(str4.PadLeft(str3.Length, '*'));
        //                writer.WriteLine();
        //                writer.Flush();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        public static void SaveErrorEventLog(string message, string soruce = "FiberHomeDiskToolsApp", string logname = "UserEventLog")
        {

            if (!EventLog.SourceExists(soruce))
            {
                EventLog.CreateEventSource(soruce, logname);
            }
            EventLog.WriteEntry(soruce, message, EventLogEntryType.Error);

        }
        public static void SaveInfoEventLog(string message, string soruce = "FiberHomeDiskToolsApp", string logname = "UserEventLog")
        {
            EventLog log = new EventLog();
            log.Source = soruce;
            if (!EventLog.SourceExists(soruce))
            {
                EventLog.CreateEventSource(soruce, logname);
            }
            EventLog.WriteEntry(soruce, message, EventLogEntryType.Information);

        }
    }
}
