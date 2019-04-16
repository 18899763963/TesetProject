using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallManagerSpace.Resources.GUIVsEntity
{
    class AdvTreeToEntity
    {
        //private static class CommStr
        //{
        //    public static string Enter = "\r\n";
        //    public static string Space = "  ";
        //    public static string Comma = ",";
        //    public static string Semicolon = ";";
        //    public static string Quotation = "\"";
        //    public static string ParenttheseOpen = "(";
        //    public static string ParentthsisClose = ")";
        //    public static string SquareBracketOpen = "[";
        //    public static string SquareBracketClose = "]";
        //    public static string BraceBracketOpen = "{";
        //    public static string BraceBracketClose = "}";
        //    public static string AngleBracketOpen = "<";
        //    public static string AngleBracketClose = ">";
        //}

        //private class CSoureFormat
        //{
        //    public int count;
        //    public List<string> StructData;
        //}
       
        //private void GetAdvTreeData(AdvTree advTree, Dictionary<string, CSoureFormat> CSourceStr)
        //{
        //    if (advTree == null && CSourceStr == null) return;
        //    foreach (Node node in advTree.Nodes)
        //    {
        //        TraversalAdvTreeToDict(node, CSourceStr);
        //    }
        //}
        //public void GennrateCSourceFile(AdvTree advTree)
        //{
        //    if (advTree == null && ComRunDatas.WorkPath == null && ComRunDatas.HeadSourceFileName == null) return;
        //    Dictionary<string, CSoureFormat> CSourceStr = new Dictionary<string, CSoureFormat>();
        //    //1.遍历AdvTree数据表,得到数据
        //    GetAdvTreeData(advTree, CSourceStr);
        //    //2.复制源文件到新文件中，并且将生成数据放入到其中
        //    string GenFileName = @"\GenCSource.h";
        //    string GenFileFullName = ComRunDatas.WorkPath + GenFileName;
        //    File.Copy(ComRunDatas.WorkPath + ComRunDatas.HeadSourceFileName, ComRunDatas.WorkPath + GenFileName, true);
        //    //3.得到文件流,将数据添加到文件后面
        //    FileStream fileStream = new FileStream(GenFileName, FileMode.Append);
        //    StreamWriter stringWriter = new StreamWriter(fileStream);
        //    stringWriter.Write("\r\n//******************************Automatic generation*********************************//\r\n");
        //    //4.将数据写入到文件
        //    //结构体数组
        //    //TEST_T gst[10] = { { }, { }, { }, { } };
        //    foreach (string keyName in CSourceStr.Keys)
        //    {
        //        //5.添加GST gst[10] =
        //        string defineString = keyName + CommStr.Space + keyName.ToLower() + CommStr.SquareBracketOpen + CSourceStr[keyName].count + CommStr.SquareBracketClose + " = ";
        //        stringWriter.Write(defineString);
        //        if (CSourceStr[keyName].count != 1) stringWriter.Write(CommStr.BraceBracketOpen);
        //        //6.添加{ { }, { }, { }, { } }
        //        int nextIndex = 0;
        //        int max = CSourceStr[keyName].StructData.Count();
        //        string preValue = string.Empty;
        //        string nextValue = string.Empty;
        //        foreach (string item in CSourceStr[keyName].StructData)
        //        {
        //            if (nextIndex < max - 1) { nextValue = CSourceStr[keyName].StructData[++nextIndex]; }
        //            stringWriter.Write(item);
        //            if (!item.Equals(CommStr.BraceBracketOpen) && !nextValue.Equals(CommStr.BraceBracketClose))
        //            {
        //                stringWriter.Write(CommStr.Comma);
        //            }
        //        }
        //        if (CSourceStr[keyName].count != 1) stringWriter.Write(CommStr.BraceBracketClose);
        //        stringWriter.Write(CommStr.Semicolon + CommStr.Enter);
        //    }
        //    stringWriter.Write("//****************************************************************************************//\r\n");
        //    //7.关闭文件流
        //    stringWriter.Flush();
        //    stringWriter.Close();

        //}

    }
}
