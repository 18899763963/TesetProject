using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmallManagerSpace.Resources.FileStringADU
{
    class FileStringADU
    {
        public void ReplaceStringOnFile(string FileName, Dictionary<string,int> PreinputDictionary)
        {
            //1.得到文件流
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs,Encoding.GetEncoding("GB2312"));
            string FileBuffer = sr.ReadToEnd();
            foreach(string key in PreinputDictionary.Keys)
            {
                string OldValue="["+key+"]";
                string NewValue = "[" + PreinputDictionary[key].ToString() + "]";      
                FileBuffer = FileBuffer.Replace(OldValue, NewValue);
            }       
            sr.Close();
            fs.Close();
            FileStream fs2 = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs2, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(FileBuffer);
            sw.Close();
            fs2.Close();
        }
        public void AddFunctionInterface(string FileName, Dictionary<string, int> PreinputDictionary)
        {

        }
    }
}
