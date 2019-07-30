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
       
    }
}
