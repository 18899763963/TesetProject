using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SmallManagerSpace.Resources
{
    static class HeadFileToEntity
    {
        public static void GetEntityFromHeadFile(string FilePath)
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
                            if (type.Equals("_otn_user_b_type_info")){  isStartCapture = true;}
                            if (isCaptured){ preinput = CapturedData; }
                            else { preinput = "Null";}
                            name = "nameValue";                            
                            node = " ";                        
                            CapturedType = "isStruct";
                            //添加structitem数据到列表中
                            ComRunDatas.CommonStructDataOperation.AddValueOfStructItem(CID.ToString().PadLeft(4,'0'), type, name, preinput, node);
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
                            ComRunDatas.CommonEnumUserDatasOperation.addValueOfsimpleTypeItemWithout(baseValue, lengthValue,valueValue);
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
                            ComRunDatas.CommonStructDataOperation.UpdateValueOfStructItem("name", structname);
                            //修改parametertitem数据到列表中
                            ComRunDatas.CommonStructDataOperation.UpdateValueOfParameterItem("preinput", "entry");
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
                                name ="*"+ matc2.Groups["parametername"].ToString();
                                if (type== "AAL_SINT8")                             
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
                            if (name.Equals("board_num")&&isStartCapture.Equals(true)) { isCaptured = true; CapturedData = "board_num";  }
                            ComRunDatas.CommonStructDataOperation.AddValueOfParameterItem(CID.ToString().PadLeft(4, '0'), type, preinput, name, range, value, length, note);
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
                            string RegexStr1 = @"}[\s]*(?<nameValue>[\S])+[\s]*;";
                            Match matc = Regex.Match(line, RegexStr1);
                            nameValue = matc.Groups["nameValue"].ToString();
                            ComRunDatas.CommonEnumUserDatasOperation.updateValueOfsimpleTypeItem("name", nameValue);
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
                                valueValue = matc1.Groups["valueValue"].ToString().PadLeft(2,'0');
                                bool result = Int32.TryParse(valueValue, out enumAutoStep);
                                if (result)
                                {
                                    enumAutoStep++;
                                }
                                ComRunDatas.CommonEnumUserDatasOperation.addValueOfEnumOfsimpleTypeItem(enValue, cnValue, valueValue);
                            }//(2)结构如OTN_USER_PORT_RATE_2G5,
                            else 
                            {
                                string RegexStr1 = @"(?<enValue>[\S]+)[\s]*,?";
                                Match matc1 = Regex.Match(line, RegexStr1);
                                Console.WriteLine("parametertype:{0}", matc1.Groups["enValue"].ToString());
                                enValue = matc1.Groups["enValue"].ToString();
                                cnValue = matc1.Groups["enValue"].ToString();
                                valueValue = enumAutoStep.ToString().PadLeft(2,'0');
                                ComRunDatas.CommonEnumUserDatasOperation.addValueOfEnumOfsimpleTypeItem(enValue, cnValue, valueValue);
                                enumAutoStep++;
                            }
                        }
                    }
                }
            }
            sr.Close();
        }

    }
}
