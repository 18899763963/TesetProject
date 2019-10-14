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
                            ComData.structFunction.AddValueOfStructItem(ComData.structEntity.nodeList, Cid.ToString().PadLeft(4, '0'), type, name, indexS, preinput, node, nodetype);
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
                        //3.匹配结构体或结构体数组的定义,例如：
                        //OTN_USER_BOARD_INFO OTN_USER_BOARD_INFO_VAR;
                        //OTN_USER_BOARD_INFO OTN_USER_BOARD_INFO_VAR[board_num];
                        else if (Regex.IsMatch(line, @"[\w]+[\s]+[\w]+[\s]*(;|[\[]{1}[\S]+[\]]{1};)"))
                        {
                            string RegexStr3 = @"(?<structType>[\w]+)[\s]+(?<structName>[\w]+)[\s]*(;|[\[]{1}(?<ArrayNum>[\w]+)[\]]{1};)";
                            Match matchStr = Regex.Match(line, RegexStr3);
                            string structType = matchStr.Groups["structType"].ToString();
                            string structName = matchStr.Groups["structName"].ToString();
                            //删除同名结构体变量，将最后一个作为新值
                            if(ComData.defineEntities.Count>0)
                            {
                                DefineEntity matchDefineEntity = ComData.defineEntities.Where(i => i.name == structName).FirstOrDefault();
                                if (matchDefineEntity != null) { ComData.defineEntities.Remove(matchDefineEntity); }
                            }
                            if (matchStr.Groups["ArrayNum"] == null || matchStr.Groups["ArrayNum"].ToString() == "")
                            {
                                ComData.defineEntities.Add(new DefineEntity() { type = structType, name = structName });
                            }
                            else
                            {
                                //ComData.defineEntities.Add(new DefineEntity() { type = structType, name = structName+"["+ matchStr.Groups["ArrayNum"].ToString()+"]" });
                                ComData.defineEntities.Add(new DefineEntity() { type = structType, name = structName, ArrayNum = matchStr.Groups["ArrayNum"].ToString() });
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
                            string nodetype = "base";
                            bool isArrayNumber = false;
                            int arrayNumber = 0;
                            string RegexStr3 = @"(?<parametertype>[\S]+)[\s]+(?<parametername>[\S]+)[\s]*;[\s]*/+(?<parameternote>[\S]+)";
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
                                    preinput = "";
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
                                ComData.structFunction.AddValueOfParameterItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, Cid.ToString().PadLeft(4, '0'), type, preinput, name, indexS, range, value, length, note, nodetype);
                            }
                            else if(isArrayNumber == true)
                            {
                                for(int i=0;i<arrayNumber;i++)
                                {
                                    ComData.structFunction.AddValueOfParameterItem(ComData.structEntity.nodeList.LastOrDefault() as StructItem, Cid.ToString().PadLeft(4, '0'), type, preinput, name, i.ToString(), range, value, length, note, nodetype);
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


        public static void GetFileFromEntityByT4(string GenFileFullName)
        {

            if (ComData.sourceWorkPath == null && ComData.selectedSourceFileName == null && ComData.structEntity == null) return;
            FileStream fileStream = new FileStream(GenFileFullName, FileMode.Create);
            StreamWriter stringWriter = new StreamWriter(fileStream, Encoding.UTF8);
            //将数据写入到文件,使用T4模板
            RuntimeTextTemplate example = new RuntimeTextTemplate();
            string pageContent = example.TransformText();
            stringWriter.Write(pageContent);
            //关闭文件流
            stringWriter.Flush();
            stringWriter.Close();
        }


        public static void GetFileFromEntity(string GenFileFullName)
        {
            if (ComData.sourceWorkPath == null && ComData.selectedSourceFileName == null && ComData.structEntity == null) return;
            //1.将数据同类数据合并到字典中
            //Dictionary<string, FormatEntity> FormatEntityData = GetFormatEntityData(ComData.customStruct);
            //2.复制源文件到新文件中，并且将生成数据放入到其中
            //File.Copy(ComData.sourceWorkPath + ComData.headSourceFileName, GenFileFullName, true);
            //3.得到文件流,将数据添加到文件后面
            FileStream fileStream = new FileStream(GenFileFullName, FileMode.Create);
            StreamWriter stringWriter = new StreamWriter(fileStream, Encoding.UTF8);
            //将版本信息模板写入到输出文件中,使用T4模板
            //RuntimeTextTemplate example = new RuntimeTextTemplate();
            TestTextTemplate example = new TestTextTemplate();
            string pageContent = example.TransformText();
            stringWriter.Write(pageContent);
            //将具体数据写入
            foreach (string readLine in ComData.OutLines)
            {
                //写入复制内容的每一行
                stringWriter.Write(readLine.Substring(0, readLine.Length - 1) + "\r\n{\r\n");
                var List = ComData.customStruct.nodeList.GroupBy(x => (x as StructItem).type).Select(c => c.First()).ToList();
                foreach(StructItem Item in List)
                {
                    if (readLine.Contains(Item.type))
                    {
                        //截取指针名
                        int index1 = readLine.IndexOf('*');
                        int index2 = readLine.IndexOf(')');
                        string pointer = readLine.Substring(index1 + 1, index2 - index1 - 1) + "->";//指针名 
                                                                                                    //截取结构体类型名
                        index1 = readLine.IndexOf("OTN");
                        index2 = readLine.IndexOf("*");
                        string type = readLine.Substring(index1, index2 - index1 - 1);//结构体类型 
                        string indexName;
                        if (readLine.Contains("AAL_UINT8"))
                        {
                            index1 = readLine.IndexOf("AAL_UINT8");
                            index2 = readLine.IndexOf(",");
                            indexName = readLine.Substring(index1 + 10, index2 - index1 - 10);
                        }
                        else
                        {
                            indexName = "";
                        }
                        GenerateTextToFunction(type, pointer, indexName);
                    }
                }
                stringWriter.WriteLine(CommStr.Space + CommStr.Space + "return 0;");

             
                stringWriter.WriteLine("}\r\n");
            }

            //得到BoardNum数量
            string GetBoardNum()
            {
                var FirstList = ComData.customStruct.nodeList.FirstOrDefault();
                StructItem firstItem = FirstList as StructItem;
                var sItem = firstItem.parameterList.Where(x => (x as Parameter).name == "board_num").ToList();
                Parameter Item = sItem.FirstOrDefault() as Parameter;
                return Item.value;
            }
            //得到pointerName
            string GetPointerName(string input)
            {
                string substring = "";
                if (input.Contains("->"))
                {
                    substring = input.Substring(0, input.IndexOf('-'));
                }
                return substring;
            }
            //生成字符串入口函数
            void GenerateTextToFunction(string type, string pointer, string indexName)
            {

                //分类指定type的数据
                var StructList = ComData.customStruct.nodeList.Where(x => (x as StructItem).type == type).ToList();
                //添加校验项目					
                if (indexName != "")
                {
                    stringWriter.WriteLine("\tif(index >= {0})", GetBoardNum());
                    stringWriter.WriteLine("\t{");
                    stringWriter.WriteLine("\t\t" + GetPointerName(pointer) + " = NULL;");
                    stringWriter.WriteLine("\t\treturn -1;");
                    stringWriter.WriteLine("\t}");
                    stringWriter.WriteLine("\tswitch({0})", "index");
                }
                //遍历其中的项目		
                if (indexName == "") { stringWriter.WriteLine("\tswitch(0)"); }
                stringWriter.WriteLine("\t{");
                int i = 0;
                foreach (var StructItem in StructList)
                {
                    stringWriter.WriteLine("\t\tcase {0}:", i);
                    TraversalStructItem(StructItem, pointer, false, 0);
                    stringWriter.WriteLine("\t\tbreak;");
                    i++;
                }
                stringWriter.WriteLine("\t}");
                //stringWriter.WriteLine("\treturn 0;");
            }
            //将parameter struct对象分别放入不同队列
            List<Dictionary<string, List<object>>> GroupSameObject(List<object> objList)
            {
                List<Dictionary<string, List<object>>> listDic = new List<Dictionary<string, List<object>>>();
                Dictionary<string, List<object>> parameDic = new Dictionary<string, List<object>>() { };
                Dictionary<string, List<object>> structDic = new Dictionary<string, List<object>>() { };

                foreach (object oI in objList)
                {
                    if (oI is StructItem)
                    {
                        //添加StructItem到字典
                        StructItem pItem = oI as StructItem;
                        //如果初次添加
                        if (!structDic.ContainsKey(pItem.name))
                        {
                            //List<object> objList=new List<object>();
                            structDic[pItem.name] = new List<object>();
                        }
                        structDic[pItem.name].Add(oI);
                    }
                    else if (oI is Parameter)
                    {
                        //添加Parameter到字典
                        Parameter sItem = oI as Parameter;
                        //如果初次添加
                        if (!parameDic.ContainsKey(sItem.name))
                        {
                            //List<object> objList=new List<object>();
                            parameDic[sItem.name] = new List<object>();
                        }
                        parameDic[sItem.name].Add(oI);
                    }
                }
                listDic.Add(parameDic);
                listDic.Add(structDic);
                return listDic;

            }
            //遍历得到树形结构字符串
            void TraversalStructItem(object ObjItem, string Prefix, bool isArray, int indexValue)
            {
                if (ObjItem == null) return;
                if (ObjItem is StructItem)
                {
                    StructItem sItem = ObjItem as StructItem;
                    //第一个队列放structDic，第二个队列放parameDic
                    List<Dictionary<string, List<object>>> gruopObj = GroupSameObject(sItem.parameterList);
                    Dictionary<string, List<object>> parameDic = gruopObj[0];
                    Dictionary<string, List<object>> structDic = gruopObj[1];
                    //处理parameDic
                    foreach (string pName in parameDic.Keys)
                    {
                        //如果根据数量只有一个
                        List<object> paraList = parameDic[pName];
                        if (paraList.Count == 1)
                        {
                            //如果数量只有一个,但是原来定义为数组类型
                            Parameter m = paraList[0] as Parameter;
                            if (m.index != "") { TraversalStructItem(paraList[0], Prefix, true, 0); }
                            //如果不是数组(根据数量只有一个)
                            else { TraversalStructItem(paraList[0], Prefix, false, 0); }
                        }
                        //如果数量多于一个，则一定是数组
                        else if (paraList.Count > 1)
                        {
                            for (int i = 0; i < paraList.Count; i++)
                            {
                                TraversalStructItem(paraList[i], Prefix, true, i);
                            }
                        }
                    }
                    //处理structDic
                    foreach (string sName in structDic.Keys)
                    {
                        //如果数量只有一个
                        List<object> strucList = structDic[sName];
                        if (strucList.Count == 1)
                        {
                            //如果数量只有一个,但是原来定义为数组类型
                            StructItem m = strucList[0] as StructItem;

                            if (m.index != "")
                            {
                                string newPerfix = string.Format("{0}{1}[0]{2}", Prefix, sName, ".");
                                TraversalStructItem(strucList[0], newPerfix, true, 0);
                            }
                            //如果不是数组(根据数量只有一个)
                            else
                            {
                                string newPerfix = string.Format("{0}{1}{2}", Prefix, sName, ".");
                                TraversalStructItem(strucList[0], newPerfix, false, 0);
                            }
                        }
                        //如果数量多于一个，则一定是数组
                        else if (strucList.Count > 1)
                        {
                            for (int i = 0; i < strucList.Count; i++)
                            {
                                string newPerfix = string.Format("{0}{1}[{2}].", Prefix, sName, i);
                                TraversalStructItem(strucList[i], newPerfix, true, i);
                            }
                        }
                    }

                }
                else if (ObjItem is Parameter)
                {
                    Parameter pItem = ObjItem as Parameter;
                    if (!isArray)
                    {
                        //去除fpga_module这一项
                        if (pItem.name != "fpga_module")
                        {
                            //如果是字符串形式
                            if (pItem.name.Contains("*"))
                            {
                                stringWriter.WriteLine("\t\t{0}{1} = \"{2}\";\t//{3}", Prefix, pItem.name.Substring(1), pItem.value, pItem.note);
                            }
                            else { stringWriter.WriteLine("\t\t{0}{1} = {2};\t//{3}", Prefix, pItem.name, pItem.value, pItem.note); }
                        }
                    }
                    else if (isArray)
                    {
                        //如果是字符串形式
                        if (pItem.name.Contains("*"))
                        {
                            stringWriter.WriteLine("\t\t{0}{1}[{2}] = \"{3}\";\t//{4}", Prefix, pItem.name.Substring(1), indexValue, pItem.value, pItem.note);
                        }
                        else { stringWriter.WriteLine("\t\t{0}{1}[{2}] = {3};\t//{4}", Prefix, pItem.name, indexValue, pItem.value, pItem.note); }
                    }
                }
            }

            //7.关闭文件流
            stringWriter.Flush();
            stringWriter.Close();

        }

    }
}
