using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SmallManagerSpace.Resources
{
    class PathFileStringADU
    {
         public string GetDirectionNameString(string inputFullPath)
        {
            //得到输入文件的路径
            int lastIndex = inputFullPath.LastIndexOf("\\");
            string PathName = inputFullPath.Substring(0, lastIndex + 1);
            return PathName;
        }
        public string GetFileNameString(string inputFullPath)
        {
            //得到输入文件的路径
            int lastIndex = inputFullPath.LastIndexOf("\\");
            int fullLen = inputFullPath.Length;
            int FileNameLen = fullLen - lastIndex - 1;
            string FileName = inputFullPath.Substring(lastIndex + 1, FileNameLen);
            return FileName;
        }
        //static public void FileStreamWrite(string inputData)
        //{
        //    string filePath = Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + ".txt";
        //    if (File.Exists(filePath))
        //        File.Delete(filePath);
        //    FileStream fs = new FileStream(filePath, FileMode.Create);
        //    //获得字节数组
        //    byte[] data = System.Text.Encoding.Default.GetBytes(inputData);
        //    //开始写入
        //    fs.Write(data, 0, data.Length);
        //    //清空缓冲区、关闭流
        //    fs.Flush();
        //    fs.Close();
        //}
        //static public void FileWriteAllLines(string inputData)
        //{
        //    string filePath = Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + ".txt";
        //    if (File.Exists(filePath))
        //        File.Delete(filePath);

        //    //如果文件不存在，则创建；存在则覆盖
        //    //该方法写入字符数组换行显示
        //    string[] lines = { "first line", "second line", "third line", "第四行" };
        //    File.WriteAllLines(@"C:\testDir\test.txt", lines, Encoding.UTF8);
        //}
        //static public void FileWriteAllText(string inputData,string fileName)
        //{
        //  string currentName=  inputData.ToString();//是值转换为字符串

        //    //string filePath = Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + nameof(inputData) + ".xml";
        //    if (File.Exists(fileName))
        //        File.Delete(fileName);
        //    //如果文件不存在，则创建；存在则覆盖
        //    File.WriteAllText(fileName, inputData, Encoding.UTF8);
        //}
        //static public void StreamWriterWrite(string[] inputData)
        //{
        //    string filePath = Directory.GetCurrentDirectory() + "\\" + Process.GetCurrentProcess().ProcessName + ".txt";
        //    if (File.Exists(filePath))
        //        File.Delete(filePath);
        //    //在将文本写入文件前，处理文本行
        //    //StreamWriter一个参数默认覆盖
        //    //StreamWriter第二个参数为false覆盖现有文件，为true则把文本追加到文件末尾
        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\testDir\test2.txt", true))
        //    {
        //        foreach (string line in inputData)
        //        {
        //            if (!line.Contains("second"))
        //            {
        //                file.Write(line);//直接追加文件末尾，不换行
        //                file.WriteLine(line);// 直接追加文件末尾，换行 
        //            }
        //        }
        //    }
        //}


    }
}
