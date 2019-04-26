﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        private class FormatEntity
        {
            public int count;
            public List<string> StructData;
        }
        private static bool IsString(string InputName)
        {
            bool isString = false;
            if (InputName.Contains("AAL_SINT8"))
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
        private static Dictionary<string, FormatEntity> GetFormatEntityData(StructOfSourceFileDatas StructEntity)
        {
            Dictionary<string, FormatEntity> keyValuePairs = new Dictionary<string, FormatEntity>();
            foreach (StructItem structItem in StructEntity.structItemList)
            {
                //1.如果字典不包含该元素,则添加该元素到字典
                if (!keyValuePairs.ContainsKey(structItem.name))
                {
                    //1初始化结构体变量
                    keyValuePairs[structItem.name] = new FormatEntity();
                    keyValuePairs[structItem.name].StructData = new List<string>();
                    keyValuePairs[structItem.name].count = 0;
                    //2.将parameter中的value值添加到
                    List<string> ParamterDataList = GeParamterDataList(structItem.parameterList);
                    keyValuePairs[structItem.name].StructData.AddRange(ParamterDataList);
                    keyValuePairs[structItem.name].count++;
                }  //2.如果字典包含该元素,则添加该元素的数据到字典
                else
                {
                    //1.将parameter中的value值添加到
                    List<string> ParamterDataList = GeParamterDataList(structItem.parameterList);
                    keyValuePairs[structItem.name].StructData.AddRange(ParamterDataList);
                    keyValuePairs[structItem.name].count++;
                }
            }
            return keyValuePairs;
        }
        public static void GetEntityFromFile(string FilePath)
        {
            StreamReader sr = new StreamReader(FilePath, Encoding.Default);
            String line;
            int CID = 1;
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
                            string preinput = "";
                            string node = "";
                            string RegexStr3 = @"struct[\s]+(?<structtype>[\S]+)";
                            Match matc = Regex.Match(line, RegexStr3);
                            type = matc.Groups["structtype"].ToString();
                            //记录第一个 type="_otn_user_b_type_info"的子项
                            if (type.Equals("_otn_user_b_type_info")) { isStartCapture = true; }
                            if (isCaptured) { preinput = CapturedData; }
                            else { preinput = "Null"; }
                            name = "nameValue";
                            node = " ";
                            CapturedType = "isStruct";
                            //添加structitem数据到列表中
                            ComRunDatas.structOfSourceFileDataOperation.AddValueOfStructItem(CID.ToString().PadLeft(4, '0'), type, name, preinput, node);
                            ProcessStep++;
                            CID++;
                        }
                        //2.匹配字符串的typedef enum
                        else if (Regex.IsMatch(line, @"typedef[\s]+enum[\s]*"))
                        {
                            string baseValue = "xsd:hexBinary";
                            string lengthValue = "1";
                            string valueValue = "1";
                            //添加simpleTypeItem数据到列表中
                            ComRunDatas.enumOfSourceFileDataOperation.addValueOfsimpleTypeItemWithout(baseValue, lengthValue, valueValue);
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

                            string RegexStr3 = @"}[\s]*(?<structname>[\S]+)[\s]*;";
                            string structname = "";
                            Match matc = Regex.Match(line, RegexStr3);
                            Console.WriteLine("structname:{0}", matc.Groups["structname"].ToString());
                            structname = matc.Groups["structname"].ToString();
                            //修改structitem数据到列表中
                            ComRunDatas.structOfSourceFileDataOperation.UpdateValueOfStructItem("name", structname);
                            //修改parametertitem数据到列表中
                            ComRunDatas.structOfSourceFileDataOperation.UpdateValueOfParameterItem("preinput", "entry");
                            ProcessStep = 0;
                            CapturedType = "";
                        }     //4.匹配字符串{内容}
                        else if (Regex.IsMatch(line, @"[\S]+[\s]+[\S]+[\s]?;"))
                        {
                            string type = "";
                            // string vartype = "base";
                            string preinput = "Null";
                            string name = "";
                            string range = "[01-05]";
                            string value = "1";
                            string length = "1";
                            string note = "";
                            string RegexStr3 = @"(?<parametertype>[\S]+)[\s]+(?<parametername>[\S]+)[\s]*;[\s]*/+(?<parameternote>[\S]+)";
                            Match matc1 = Regex.Match(line, RegexStr3);
                            type = matc1.Groups["parametertype"].ToString();
                            note = matc1.Groups["parameternote"].ToString();
                            string lineItem = matc1.Groups["parametername"].ToString();
                            //(1)结构如*cfg_buf[switch_cfg_num]
                            if (lineItem.Contains("*") && lineItem.Contains("[") && lineItem.Contains("]"))
                            {
                                string RegexStr4 = @"(?<parameterpointer>[\*]*)[\s]*(?<parametername>[\S]+)[\s]*[\[]+(?<parameterarray>[\S]*)[\]]+";
                                Match matc2 = Regex.Match(lineItem, RegexStr4);
                                Console.WriteLine("parameterpointer:{0},parametername:{1},parameterarray:{2}", matc2.Groups["parameterpointer"].ToString(), matc2.Groups["parametername"].ToString(), matc2.Groups["parameterarray"].ToString());
                                name = "*" + matc2.Groups["parametername"].ToString() + "[0]";
                                preinput = matc2.Groups["parameterarray"].ToString();
                                // vartype = "pointer* array[0]";
                            }
                            //(2)结构如*cfg_buf
                            else if (lineItem.Contains("*") && !lineItem.Contains("[") && !lineItem.Contains("]"))
                            {
                                string RegexStr4 = @"(?<parameterpointer>[\*]*)[\s]*(?<parametername>[\S]+)";
                                Match matc2 = Regex.Match(lineItem, RegexStr4);
                                Console.WriteLine("parameterpointer:{0},parametername:{1}", matc2.Groups["parameterpointer"].ToString(), matc2.Groups["parametername"].ToString());
                                name = "*" + matc2.Groups["parametername"].ToString();
                                if (type == "AAL_SINT8")
                                {
                                    value = "DefString";
                                }
                                // vartype = "pointer *";
                            }
                            //(3)结构如cfg_buf[switch_cfg_num]
                            else if (!lineItem.Contains("*") && lineItem.Contains("[") && lineItem.Contains("]"))
                            {
                                string RegexStr4 = @"(?<parametername>[\S]+)[\s]*[\[]+(?<parameterarray>[\S]*)[\]]+";
                                Match matc2 = Regex.Match(lineItem, RegexStr4);
                                Console.WriteLine("parametername:{0},parameterarray:{1}", matc2.Groups["parametername"].ToString(), matc2.Groups["parameterarray"].ToString());
                                name = matc2.Groups["parametername"].ToString() + "[0]";
                                preinput = matc2.Groups["parameterarray"].ToString();
                                //vartype = "array[0]";
                            }
                            //(4)结构如cfg_buf
                            else if (!lineItem.Contains("*") && !lineItem.Contains("[") && !lineItem.Contains("]"))
                            {
                                Console.WriteLine("parametername:{0}", lineItem);
                                name = lineItem;
                            }
                            if (name.Equals("board_num") && isStartCapture.Equals(true)) { isCaptured = true; CapturedData = "board_num"; }
                            ComRunDatas.structOfSourceFileDataOperation.AddValueOfParameterItem(CID.ToString().PadLeft(4, '0'), type, preinput, name, range, value, length, note);
                            CID++;
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
                            ComRunDatas.enumOfSourceFileDataOperation.updateValueOfsimpleTypeItem("name", nameValue);
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
                                ComRunDatas.enumOfSourceFileDataOperation.addValueOfEnumOfsimpleTypeItem(enValue, cnValue, valueValue);
                            }//(2)结构如OTN_USER_PORT_RATE_2G5,
                            else
                            {
                                string RegexStr1 = @"(?<enValue>[\S]+)[\s]*,?";
                                Match matc1 = Regex.Match(line, RegexStr1);
                                Console.WriteLine("parametertype:{0}", matc1.Groups["enValue"].ToString());
                                enValue = matc1.Groups["enValue"].ToString();
                                cnValue = matc1.Groups["enValue"].ToString();
                                valueValue = enumAutoStep.ToString().PadLeft(2, '0');
                                ComRunDatas.enumOfSourceFileDataOperation.addValueOfEnumOfsimpleTypeItem(enValue, cnValue, valueValue);
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
            if (ComRunDatas.SourceWorkPath == null && ComRunDatas.HeadSourceFileName == null && ComRunDatas.StructOfSourceFileEntity == null) return;
            //1.将数据同类数据合并到字典中
            Dictionary<string, FormatEntity> FormatEntityData = GetFormatEntityData(ComRunDatas.StructOfSourceFileEntity);
            //2.复制源文件到新文件中，并且将生成数据放入到其中
            File.Copy(ComRunDatas.SourceWorkPath + ComRunDatas.HeadSourceFileName, GenFileFullName, true);
            //3.得到文件流,将数据添加到文件后面
            FileStream fileStream = new FileStream(GenFileFullName, FileMode.Append);
            StreamWriter stringWriter = new StreamWriter(fileStream);
            //stringWriter.Write("\r\n//******************************Automatic generation*********************************//\r\n");
            //添加头文件
            //stringWriter.Write("#include " + ComRunDatas.HeadSourceFileName);

            stringWriter.Write("\r\n//******************************Automatic generation*********************************//\r\n");
            //4.将数据写入到文件
            //结构体数组
            //eg: TEST_T gst[10] = { { }, { }, { }, { } };
            foreach (string keyName in FormatEntityData.Keys)
            {
                //5.添加GST gst[10] =
                string defineString = keyName + CommStr.Space + keyName.ToLower() + CommStr.SquareBracketOpen + FormatEntityData[keyName].count + CommStr.SquareBracketClose + " = ";
                stringWriter.Write(defineString);
                if (FormatEntityData[keyName].count != 1) stringWriter.Write(CommStr.BraceBracketOpen);
                //6.添加{ { }, { }, { }, { } }
                int nextIndex = 0;
                int max = FormatEntityData[keyName].StructData.Count();
                string preValue = string.Empty;
                string nextValue = string.Empty;
                foreach (string item in FormatEntityData[keyName].StructData)
                {
                    if (nextIndex < max - 1) { nextValue = FormatEntityData[keyName].StructData[++nextIndex]; }
                    stringWriter.Write(item);
                    if (!item.Equals(CommStr.BraceBracketOpen) && !nextValue.Equals(CommStr.BraceBracketClose))
                    {
                        stringWriter.Write(CommStr.Comma);
                    }
                }
                if (FormatEntityData[keyName].count != 1) stringWriter.Write(CommStr.BraceBracketClose);
                stringWriter.Write(CommStr.Semicolon + CommStr.Enter);
            }
            stringWriter.Write("//****************************************************************************************//\r\n");
            //7.关闭文件流
            stringWriter.Flush();
            stringWriter.Close();

        }

    }
}
