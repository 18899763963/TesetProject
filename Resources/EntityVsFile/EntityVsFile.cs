using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SmallManagerSpace.Resources.TextTemplate;

namespace SmallManagerSpace.Resources
{
    static class EntityVsFile
    {
        private static class CommStr
        {
            public static string Enter = "\r\n";
            public static string Space = "  ";
            public static string Comma = ",";
            public static string Semicolon = ";";
            public static string Quotation = "\"";
            public static string ParenttheseOpen = "(";
            public static string ParentthsisClose = ")";
            public static string SquareBracketOpen = "[";
            public static string SquareBracketClose = "]";
            public static string BraceBracketOpen = "{";
            public static string BraceBracketClose = "}";
            public static string AngleBracketOpen = "<";
            public static string AngleBracketClose = ">";
        }

        //用于存放读取头文件的输出部分格式
        static List<string> OutLine = new List<string>();
        private static bool IsString(string InputName)
        {
            bool isString = false;
            if (InputName.Contains("AAL_UINT8"))
            {
                isString = true;
            }
            return isString;
        }
        private static bool IsArray(string InputName)
        {
            bool isArray = false;
            if (InputName.Contains('[') && InputName.Contains(']'))
            {
                isArray = true;
            }
            return isArray;
        }
        private static string GetArrayName(string InputName)
        {
            string arrayName = null;
            if (IsArray(InputName))
            {
                arrayName = InputName.Substring(0, InputName.IndexOf('['));
            }
            return arrayName;
        }
     
        /// <summary>
        /// 得到匹配的基本类型，enum类型的长度
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        private static int GetLengthOfType(string Type)
        {
            int Length = 0;
            //匹配基本类型的长度
            if ((ComData.baseDictonary != null) && (ComData.baseDictonary.ContainsKey(Type)))
            {
                Length = ComData.baseDictonary[Type].length;
            }
            //匹配enum类型的长度
            else if ((ComData.enumEntity != null) && (ComData.enumEntity.simpleTypes.Where(x => x.name == Type).Count() != 0))
            {
                simpleType matchSimpleType = ComData.enumEntity.simpleTypes.Where(x => x.name == Type).First();
                Length = int.Parse(matchSimpleType.length);
            }
            ////匹配struct类型的长度
            //else if ((ComRunDatas.structEntity != null) && (ComRunDatas.structEntity.nodeList.Where(x => x.type == Type).Count() != 0))
            //{
            //    StructItem matchStructItem = ComRunDatas.structEntity.nodeList.Where(x => x.type == Type).First();
            //    foreach (Parameter parameter in matchStructItem.parameterList)
            //    {
            //        string paraType = parameter.type;
            //        Length += GetLengthOfType(paraType);
            //    }
            //}
            return Length;
        }
        private static List<string> GeParamterDataList(List<Parameter> ParameterList)
        {
            List<string> ParameterDataList = new List<string>();
            ParameterDataList.Add(CommStr.BraceBracketOpen);
            string arrayNameLatest = null;
            string arrayNameCurrent = null;
            foreach (Parameter parameter in ParameterList)
            {
                arrayNameCurrent = GetArrayName(parameter.name);
                //0.如果是字符串
                if (IsString(parameter.type))
                {
                    ParameterDataList.Add(CommStr.Quotation + parameter.value + CommStr.Quotation);
                }
                //1.如果是首次捕捉到数组名
                else if (arrayNameCurrent != null && arrayNameLatest == null)
                {
                    arrayNameLatest = arrayNameCurrent;
                    ParameterDataList.Add(CommStr.BraceBracketOpen);
                    ParameterDataList.Add(parameter.value);

                }
                //2.如果是中间次捕捉到数组名
                else if (arrayNameCurrent == arrayNameLatest && arrayNameCurrent != null && arrayNameLatest != null)
                {
                    arrayNameLatest = arrayNameCurrent;
                    ParameterDataList.Add(parameter.value);
                }
                //3.如果是最后一次捕捉到数组名
                else if (arrayNameCurrent == null && arrayNameLatest != null)
                {
                    arrayNameLatest = arrayNameCurrent;
                    ParameterDataList.Add(parameter.value);
                    ParameterDataList.Add(CommStr.BraceBracketClose);
                }
                //4.如果不是数组
                else
                {
                    arrayNameLatest = arrayNameCurrent;
                    ParameterDataList.Add(parameter.value);
                }
            }//如果前一次是数组，但是没有结尾 -->中间次捕捉到数组名
            if (arrayNameCurrent == arrayNameLatest && arrayNameCurrent != null && arrayNameLatest != null)
            {
                arrayNameLatest = arrayNameCurrent;
                ParameterDataList.Add(CommStr.BraceBracketClose);
            }
            ParameterDataList.Add(CommStr.BraceBracketClose);
            return ParameterDataList;
        }

  
        public static void GetEntityFromFile(string FilePath)
        {
            //读取所有行，若包含AAL_INT32,则添加到集合中
            string[] readOutputLine = File.ReadAllLines(FilePath, Encoding.Default);
            foreach (string item in readOutputLine)
            {
                if (item.Contains("AAL_INT32"))
                {
                    OutLine.Add(item);
                }
            }

            StreamReader sr = new StreamReader(FilePath, Encoding.Default);
            String line;
            int Cid = 1;
            //匹配第一行
            string CapturedType = "";
            int enumAutoStep = 0;
            int ProcessStep = 0;
            //捕获type="_otn_user_b_type_info" 子项的值
            bool isCaptured = false;
            bool isStartCapture = false;
            string CapturedData = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                if (CapturedType.Equals(""))
                {
                    if (ProcessStep == 0)
                    {
                        //1.匹配字符串的typedef struct
                        if (Regex.IsMatch(line, @"typedef[\s]+struct[\s]+"))
                        {
                            string type = "";
                            string name = "";
                            string indexS = "";
                            string preinput = "";
                            string node = "";
                            string nodetype = "struct";
                            string RegexStr3 = @"struct[\s]+(?<structtype>[\S]+)";
                            Match matc = Regex.Match(line, RegexStr3);
                            type = matc.Groups["structtype"].ToString();
                            //记录第一个 type="_otn_user_b_type_info"的子项
                            if (type.Equals("_otn_user_b_type_info")) { isStartCapture = true; }
                            if (isCaptured) { preinput = CapturedData; }
                            else { preinput = ""; }
                            name = "nameValue";
                            node = " ";
                            CapturedType = "isStruct";

                            //添加structitem数据到列表中
                            ComData.structFunction.AddValueOfStructItem(ComData.structEntity.nodeList, Cid.ToString().PadLeft(4, '0'), type, name,indexS, preinput, node, nodetype);
                            ProcessStep++;
                            Cid++;
                        }
                        //2.匹配字符串的typedef enum
                        else if (Regex.IsMatch(line, @"typedef[\s]+enum[\s]*"))
                        {
                            string baseValue = "xsd:hexBinary";
                            string lengthValue = "1";
                            string valueValue = "1";
                            //添加simpleTypeItem数据到列表中
                            ComData.enumFunction.addValueOfsimpleTypeItemWithout(baseValue, lengthValue, valueValue);
                            CapturedType = "isEnum";
                            ProcessStep++;
                        }
                    }
                }
                else if (CapturedType.Equals("isStruct"))
                {
                    //2.匹配字符串的{
                    if (ProcessStep == 1)
                    {
                        if (Regex.IsMatch(line, @"{"))
                        {
                            ProcessStep++;
                        }
                    }
                    else if (ProcessStep == 2)
                    {
                        //3.匹配字符串的}
                        if (Regex.IsMatch(line, @"}"))
                        {

                            string RegexStr3 = @"}[\s]*(?<structType>[\S]+)[\s]*;";
                            string structType = "";
                            Match matc = Regex.Match(line, RegexStr3);
                            structType = matc.Groups["structType"].ToString();
                            //修改structitem数据到列表中
                            ComData.structFunction.UpdateValueOfStructItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, "type", structType);
                            ComData.structFunction.UpdateValueOfStructItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, "name", structType+"_VAR");
                            //修改parametertitem数据到列表中
                            ComData.structFunction.UpdateValueOfParameterItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, "preinput", "entry");
                            ProcessStep = 0;
                            CapturedType = "";
                        }     //4.匹配字符串{内容}
                        else if (Regex.IsMatch(line, @"[\S]+[\s]+[\S]+[\s]?;"))
                        {
                            string type = "";
                            // string vartype = "base";
                            string preinput = "";
                            string name = "";
                            string indexS = "";
                            string range = "";
                            string value = "1";
                            string length = "1";
                            string note = "";
                            string nodetype = "base";
                            string RegexStr3 = @"(?<parametertype>[\S]+)[\s]+(?<parametername>[\S]+)[\s]*;[\s]*/+(?<parameternote>[\S]+)";
                            Match matchStr = Regex.Match(line, RegexStr3);
                            type = matchStr.Groups["parametertype"].ToString();
                            note = matchStr.Groups["parameternote"].ToString()??"";
                            length = GetLengthOfType(type).ToString();

                            string lineItem = matchStr.Groups["parametername"].ToString();
                            //(1)结构如*cfg_buf[switch_cfg_num]
                            if (lineItem.Contains("*") && lineItem.Contains("[") && lineItem.Contains("]"))
                            {
                                string RegexStr4 = @"(?<parameterpointer>[\*]*)[\s]*(?<parametername>[\S]+)[\s]*[\[]+(?<parameterarray>[\S]*)[\]]+";
                                Match matchString = Regex.Match(lineItem, RegexStr4);
                                name = "*" + matchString.Groups["parametername"].ToString() ;
                                preinput = matchString.Groups["parameterarray"].ToString();
                                value = "{" + value + "}";
                                // vartype = "pointer* array[0]";
                            }
                            //(2)结构如*cfg_buf
                            else if (lineItem.Contains("*") && !lineItem.Contains("[") && !lineItem.Contains("]"))
                            {
                                string RegexStr4 = @"(?<parameterpointer>[\*]*)[\s]*(?<parametername>[\S]+)";
                                Match matchString = Regex.Match(lineItem, RegexStr4);
                                Console.WriteLine("parameterpointer:{0},parametername:{1}", matchString.Groups["parameterpointer"].ToString(), matchString.Groups["parametername"].ToString());
                                name = "*" + matchString.Groups["parametername"].ToString();
                                if (type == "AAL_UINT8")
                                {
                                    value = "DefaultString";
                                }

                            }
                            //(3)结构如cfg_buf[switch_cfg_num]
                            else if (!lineItem.Contains("*") && lineItem.Contains("[") && lineItem.Contains("]"))
                            {
                                string RegexStr4 = @"(?<parametername>[\S]+)[\s]*[\[]+(?<parameterarray>[\S]*)[\]]+";
                                Match matchString = Regex.Match(lineItem, RegexStr4);
                                Console.WriteLine("parametername:{0},parameterarray:{1}", matchString.Groups["parametername"].ToString(), matchString.Groups["parameterarray"].ToString());
                                name = matchString.Groups["parametername"].ToString();
                                preinput = matchString.Groups["parameterarray"].ToString();
                                indexS = "0";
                            }
                            //(4)结构如cfg_buf
                            else if (!lineItem.Contains("*") && !lineItem.Contains("[") && !lineItem.Contains("]"))
                            {
                                Console.WriteLine("parametername:{0}", lineItem);
                                name = lineItem;
                            }
                            if (name.Equals("board_num") && isStartCapture.Equals(true)) { isCaptured = true; CapturedData = "board_num"; }
                            //匹配范围
                            if(ComData.baseDictonary.ContainsKey(type))
                            {
                                range = ComData.baseDictonary[type].range;
                            }
                            ComData.structFunction.AddValueOfParameterItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem,Cid.ToString().PadLeft(4, '0'), type, preinput, name,indexS, range, value, length, note, nodetype);
                            Cid++;
                        }
                    }
                }
                else if (CapturedType.Equals("isEnum"))
                {
                    //2.匹配字符串的{
                    if (ProcessStep == 1)
                    {
                        if (Regex.IsMatch(line, @"{"))
                        {
                            ProcessStep++;
                        }
                    }
                    else if (ProcessStep == 2)
                    {
                        //3.匹配字符串的}
                        if (Regex.IsMatch(line, @"}"))
                        {
                            //更新   string nameValue = "";的值
                            string nameValue = "";
                            //  string RegexStr1 = @"}[\s]*(?<nameValue>[\S]+)[\s]*;";
                            string RegexStr1 = @"}[\s]*(?<nameValue>[\S]+)[\s]*;";
                            Match matc = Regex.Match(line, RegexStr1);
                            nameValue = matc.Groups["nameValue"].ToString();
                            ComData.enumFunction.updateValueOfsimpleTypeItem("name", nameValue);
                            ProcessStep = 0;
                            CapturedType = "";
                        }
                        else if (Regex.IsMatch(line, @"[\S]+[\s]*[\=]?[\S]*[\s]?,?"))
                        {
                            //得到匹配的数据
                            string enValue = "";
                            string cnValue = "";
                            string valueValue = "";
                            //添加simpleTypeItem数据到列表中
                            //(1)结构如OTN_USER_PORT_RATE_NULL = 0,
                            if (line.Contains("="))
                            {
                                string RegexStr1 = @"(?<enValue>[\S]+)[\s]*=[\s]*(?<valueValue>[\S]*),";
                                Match matc1 = Regex.Match(line, RegexStr1);
                                Console.WriteLine("parametertype:{0},parametername:{1}", matc1.Groups["enValue"].ToString(), matc1.Groups["valueValue"].ToString());
                                enValue = matc1.Groups["enValue"].ToString();
                                cnValue = matc1.Groups["enValue"].ToString();
                                valueValue = matc1.Groups["valueValue"].ToString().PadLeft(2, '0');
                                bool result = Int32.TryParse(valueValue, out enumAutoStep);
                                if (result)
                                {
                                    enumAutoStep++;
                                }
                                ComData.enumFunction.addValueOfEnumOfsimpleTypeItem(enValue, cnValue, valueValue);
                            }//(2)结构如OTN_USER_PORT_RATE_2G5,
                            else
                            {
                                string RegexStr1 = @"(?<enValue>[\S]+[^,])[\s]*,?";
                                Match matc1 = Regex.Match(line, RegexStr1);
                                Console.WriteLine("parametertype:{0}", matc1.Groups["enValue"].ToString());
                                enValue = matc1.Groups["enValue"].ToString();
                                cnValue = matc1.Groups["enValue"].ToString();
                                valueValue = enumAutoStep.ToString().PadLeft(2, '0');
                                ComData.enumFunction.addValueOfEnumOfsimpleTypeItem(enValue, cnValue, valueValue);
                                enumAutoStep++;
                            }
                        }
                    }
                }
            }
            sr.Close();
        }

        public static void GetFileFromEntity(string GenFileFullName)
        {
            if (ComData.sourceWorkPath == null && ComData.headSourceFileName == null && ComData.structEntity == null) return;
            //1.将数据同类数据合并到字典中
            //Dictionary<string, FormatEntity> FormatEntityData = GetFormatEntityData(ComData.customStruct);
            //2.复制源文件到新文件中，并且将生成数据放入到其中
            //File.Copy(ComData.sourceWorkPath + ComData.headSourceFileName, GenFileFullName, true);
            //3.得到文件流,将数据添加到文件后面
            FileStream fileStream = new FileStream(GenFileFullName, FileMode.Create);
            StreamWriter stringWriter = new StreamWriter(fileStream, Encoding.UTF8);
            //将数据写入到文件,使用T4模板
            RuntimeTextTemplate example = new RuntimeTextTemplate();
            string pageContent = example.TransformText();
            stringWriter.Write(pageContent);

            //foreach (string readLine in OutLine)
            //{
            //    //写入复制内容的每一行
            //    stringWriter.Write(readLine.Substring(0, readLine.Length - 1) + "\r\n{\r\n");
            //    var List = ComData.customStruct.nodeList.GroupBy(x => (x as StructItem).type).Select(c => c.First()).ToList();
            //    foreach (StructItem Item in List)
            //    {
            //        if (readLine.Contains(Item.type))
            //        {
            //            //截取指针名
            //            int index1 = readLine.IndexOf('*');
            //            int index2 = readLine.IndexOf(')');
            //            string HeadInfoString = readLine.Substring(index1 + 1, index2 - index1 - 1);//指针名 
            //            StructItem nestStructItem;//嵌套的结构体
            //            String ParameterName, ParameterValue;//参数名，参数值
            //            var RepeatList = ComData.customStruct.nodeList.Where(x => (x as StructItem).type == Item.type).ToList();
            //            if (RepeatList.Count > 1)
            //            {
            //                stringWriter.Write(CommStr.Space + CommStr.Space + "switch(" + RepeatList.Count + ")\r\n" + CommStr.Space + CommStr.Space + "{\r\n");
            //                int i = 1;
            //                foreach (StructItem RepeatItem in RepeatList)
            //                {
            //                    stringWriter.Write(CommStr.Space + CommStr.Space + CommStr.Space + CommStr.Space + "case " + i + ":\r\n");
            //                    for (int j = 0; j < RepeatItem.parameterList.Count; j++)
            //                    {
            //                        if (RepeatItem.parameterList[j] is StructItem)
            //                        {
            //                            nestStructItem = Item.parameterList[j] as StructItem;
            //                            for (int m = 0; m < nestStructItem.parameterList.Count; m++)
            //                            {
            //                                ParameterName = (nestStructItem.parameterList[m] as Parameter).name;
            //                                ParameterValue = (nestStructItem.parameterList[m] as Parameter).value;
            //                                stringWriter.Write(CommStr.Space + CommStr.Space + CommStr.Space + CommStr.Space + HeadInfoString + "->" + nestStructItem.name + "."  + ParameterName + " = " + ParameterValue + ";\r\n");
            //                            }
            //                        }
            //                        else if (RepeatItem.parameterList[j] is Parameter)
            //                        {
            //                            ParameterName = (RepeatItem.parameterList[j] as Parameter).name;
            //                            ParameterValue = (RepeatItem.parameterList[j] as Parameter).value;
            //                            stringWriter.Write(CommStr.Space + CommStr.Space + CommStr.Space + CommStr.Space + HeadInfoString + "->" + ParameterName + " = " + ParameterValue + ";\r\n");
            //                        }
            //                    }
            //                    i++;
            //                }
            //                stringWriter.Write(CommStr.Space + CommStr.Space + "}\r\n");
            //            }
            //            else
            //            {
            //                for (int j = 0; j < Item.parameterList.Count; j++)
            //                {
            //                    if (Item.parameterList[j] is StructItem)
            //                    {
            //                        nestStructItem = Item.parameterList[j] as StructItem;
            //                        for (int m = 0; m < nestStructItem.parameterList.Count; m++)
            //                        {
            //                            ParameterName = (nestStructItem.parameterList[m] as Parameter).name;
            //                            ParameterValue = (nestStructItem.parameterList[m] as Parameter).value;
            //                            stringWriter.Write(CommStr.Space + CommStr.Space + HeadInfoString + "->" + nestStructItem.name + "." + ParameterName + " = " + ParameterValue + ";\r\n");
            //                        }
            //                    }
            //                    else if (Item.parameterList[j] is Parameter)
            //                    {

            //                        ParameterName = (Item.parameterList[j] as Parameter).name;
            //                        ParameterValue = (Item.parameterList[j] as Parameter).value;
            //                        stringWriter.Write(CommStr.Space + CommStr.Space + HeadInfoString + "->" + ParameterName + " = " + ParameterValue + ";\r\n");
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    stringWriter.Write(CommStr.Space + CommStr.Space + "return 0;\r\n}\r\n\r\n");
            //}
            //stringWriter.Write("//****************************************************************************************//\r\n");
            //7.关闭文件流
            stringWriter.Flush();
            stringWriter.Close();

        }

    }
}
