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
        /// <summary>
        /// 是否保留上次的Enum和Struct,Define
        /// </summary>
        /// <param name="isInheritEnum">true:保留</param>
        /// <param name="isInheritStruct">true:保留</param>
        /// <param name="isInheritDefine">true:保留</param>
        public static void isInHeritData(bool isInheritEnum, bool isInheritStruct,bool isInheritDefine)
        {
            if (isInheritEnum == false && ComData.enumEntity!=null)
            {
                ComData.enumEntity.simpleTypes.Clear(); ;
            }
            if (isInheritStruct == false && ComData.structEntity != null)
            {
                ComData.structEntity.nodeList.Clear();
            }
            if (isInheritDefine == false && ComData.showEntities != null)
            {
                ComData.showEntities.Clear();
            }
        }
        /// <summary>
        /// 得到H文件的序列化数据
        /// </summary>
        /// <param name="FilePath"></param>
        public static void GetEntityFromFile(string FilePath)
        {
 
            //读取所有行，若包含AAL_INT32,则添加到集合中
            ComData.readAllLines = File.ReadAllLines(FilePath, Encoding.Default).ToList();
            ComData.OutLines.Clear();
            foreach (string item in ComData.readAllLines)
            {
                if (item.Contains("AAL_INT32"))
                {
                    ComData.OutLines.Add(item);
                }
            }
            //foreach
            //ComData.readAllLines.Add();
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
                            string note = "";
                            string nodetype = "struct";
                            string RegexStr3 = @"struct[\s]+(?<structtype>[\S]+)";
                            Match matc = Regex.Match(line, RegexStr3);
                            type = matc.Groups["structtype"].ToString();
                            //记录第一个 type="_otn_user_b_type_info"的子项
                            if (type.Equals("_otn_user_b_type_info")) { isStartCapture = true; }
                            if (isCaptured) { preinput = CapturedData; }
                            else { preinput = ""; }
                            name = "nameValue";
                            note = " ";

                            CapturedType = "isStruct";
                            //添加structitem数据到列表中
                            ComData.structFunction.AddValueOfStructItem(ComData.structEntity.nodeList, Cid.ToString().PadLeft(4, '0'), type, name, indexS, preinput, note, nodetype);
                            ProcessStep++;
                            Cid++;
                        }
                        //2.匹配字符串的typedef enum
                        else if (Regex.IsMatch(line, @"typedef[\s]+enum[\s]*"))
                        {
                            string baseValue = "xsd:hexBinary";
                            string lengthValue = "1";
                            string valueValue = "1";
                            enumAutoStep = 0;
                            //添加simpleTypeItem数据到列表中
                            ComData.enumFunction.addValueOfsimpleTypeItemWithout(baseValue, lengthValue, valueValue);
                            CapturedType = "isEnum";
                            ProcessStep++;
                        }
                        //3.匹配函数的定义,例如：
                        //AAL_INT32 otn_user_get_init_info( OTN_USER_INIT_INFO  *init_info );
                        else if (Regex.IsMatch(line, @"\w+\s+\w+\s*\(\s*\w+\s+\*{1}\s*\w+\s*\)\s*;"))
                        {
                            string RegexStr3 = @"(?<retType>\w+)\s+(?<funName>\w+)\s*\(\s*(?<argType>\w+)\s+\*{1}\s*(?<argName>\w+)\s*\)\s*;";
                            Match matchStr = Regex.Match(line, RegexStr3);
                            
                            string retTypeM = matchStr.Groups["retType"].ToString();
                            string funNameM = matchStr.Groups["funName"].ToString();
                            string argTypeM = matchStr.Groups["argType"].ToString();
                            string argNameM = matchStr.Groups["argName"].ToString();
                            //删除同名结构体变量，将最后一个作为新值
                            if (ComData.showEntities.Count > 0)//如果有相同则用第一个
                            {
                                ShowEntity matchShowEntity = ComData.showEntities.Where(i => i.argType == argTypeM).FirstOrDefault();
                                if (matchShowEntity == null)
                                {
                                    ComData.showEntities.Add(new ShowEntity() {retType=retTypeM,funName=funNameM,argType=argTypeM,argName=argNameM });
                                }
                            }else
                            {
                                ComData.showEntities.Add(new ShowEntity() { retType = retTypeM, funName = funNameM, argType = argTypeM, argName = argNameM });

                            }


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
                            ComData.structFunction.UpdateValueOfStructItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, "name", structType + "_VAR");
                            //修改parametertitem数据到列表中
                            ComData.structFunction.UpdateValueOfParameterItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, "preinput", "entry");
                            ProcessStep = 0;
                            CapturedType = "";
                        }
                        //4.匹配字符串{内容}
                        else if (Regex.IsMatch(line, @"[\S]+[\s]+[\S]+[\s]?;"))
                        {
                            string type = "";
                            string preinput = "";
                            string name = "";
                            string indexS = "";
                            string range = "";
                            string value = "1";
                            string length = "1";
                            string note = "";
                            string modified = "N";
                            string nodetype = "base";
                            bool isArrayNumber = false;
                            int arrayNumber = 0;
                            //此处的注释匹配改为可选 (;|[\[]{ 1} (?< ArrayNum >[\w] +)[\]]{ 1};)
                            string RegexStr3 = @"(?<parametertype>[\S]+)[\s]+(?<parametername>[\S]+)[\s]*;([\s]*/+(?<parameternote>[\w]+)|[\s]*)";
                            Match matchStr = Regex.Match(line, RegexStr3);
                            type = matchStr.Groups["parametertype"].ToString();
                            note = matchStr.Groups["parameternote"].ToString() ?? "";
                            length = GetLengthOfType(type).ToString();

                            string lineItem = matchStr.Groups["parametername"].ToString();
                            //(1)结构如*cfg_buf[switch_cfg_num]
                            if (lineItem.Contains("*") && lineItem.Contains("[") && lineItem.Contains("]"))
                            {
                                string RegexStr4 = @"(?<parameterpointer>[\*]*)[\s]*(?<parametername>[\S]+)[\s]*[\[]+(?<parameterarray>[\S]*)[\]]+";
                                Match matchString = Regex.Match(lineItem, RegexStr4);
                                name = "*" + matchString.Groups["parametername"].ToString();
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
                                if (type == "AAL_INT8")
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
                                arrayNumber = 0;
                                isArrayNumber = false;
                                if (int.TryParse(preinput, out arrayNumber))
                                {
                                    isArrayNumber = true;
                                    preinput = "invariant";
                                }
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
                            if (ComData.baseDictonary.ContainsKey(type))
                            {
                                range = ComData.baseDictonary[type].range;
                            }
                            if(isArrayNumber==false)
                            {
                                ComData.structFunction.AddValueOfParameterItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, Cid.ToString().PadLeft(4, '0'), type, preinput, name, indexS, range, value, length, note, nodetype, modified);
                            }
                            else if(isArrayNumber == true)
                            {
                                for(int i=0;i<arrayNumber;i++)
                                {
                                    ComData.structFunction.AddValueOfParameterItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, Cid.ToString().PadLeft(4, '0'), type, preinput, name, i.ToString(), range, value, length, note, nodetype, modified);
                                }
                            }
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
                                string RegexStr1 = @"(?<enValue>[\S]+)[\s]*=[\s]*(?<valueValue>[\S]*[^,\s])[\s]*,?[\s]*";
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
                                string RegexStr1 = @"(?<enValue>[\S]+[^,])[\s]*,?[\s]*";
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

        /// <summary>
        /// 通过t4模板得到cpp文件
        /// </summary>
        /// <param name="GenFileFullName">生成文件名</param>
        public static void GetCppFileFromEntityByT4(string GenFileFullName)
        {

            if (ComData.sourceWorkPath == null && ComData.selectedSourceFileName == null && ComData.customStruct == null) return;
            FileStream fileStream = new FileStream(GenFileFullName, FileMode.Create);
            StreamWriter stringWriter = new StreamWriter(fileStream, Encoding.UTF8);
            //将数据写入到文件,使用T4模板
            GetCppString example = new GetCppString();
            string pageContent = example.TransformText();
            stringWriter.Write(pageContent);
            //关闭文件流
            stringWriter.Flush();
            stringWriter.Close();
        }

        /// <summary>
        /// 通过t4模板得到h文件
        /// </summary>
        /// <param name="GenFileFullName">生成文件名</param>
        public static void GetHeaderFileFromEntityByT4(string GenFileFullName)
        {

            if (ComData.sourceWorkPath == null && ComData.selectedSourceFileName == null && ComData.enumEntity == null && ComData.structEntity == null) return;
            FileStream fileStream = new FileStream(GenFileFullName, FileMode.Create);
            StreamWriter stringWriter = new StreamWriter(fileStream, Encoding.UTF8);
            //将数据写入到文件,使用T4模板
            GetHeaderString example = new GetHeaderString();
            string pageContent = example.TransformText();
            stringWriter.Write(pageContent);
            //关闭文件流
            stringWriter.Flush();
            stringWriter.Close();
        }

    }
}
