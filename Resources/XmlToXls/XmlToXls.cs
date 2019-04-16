using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SmallManagerSpace.Resources
{
    class XmlToXls
    {

        static private void SetXlsProtectFile(Workbook workbook)
        {
            workbook.Protect("pwd111");//设置保护加密的 密码 ：pwd111
        }
        static private void SetXlsProtectSheet(Worksheet sheet)
        {
            sheet.Protect("test", SheetProtectionType.All);
        }
        static private void SetXlsProtectCellOfSheet(Worksheet sheet)
        {

            sheet.Range["A1"].Text = "锁定";
            sheet.Range["B1"].Text = "未锁定";

            sheet.Range["A1"].Style.Locked = true;
            sheet.Range["B1"].Style.Locked = false;

            //一定要对工作表进行保护，才能生效
            sheet.Protect("test", SheetProtectionType.All);
        }
        static private void SetXlsValueCellOfSheet(Worksheet sheet, int Row, int Column)
        {

            sheet.Range[Row, Column].Text = "我是第1行的第2个单元格";
        }
        static private void SetXlsValueCellOfSheet(Worksheet sheet, String Row, String Column)
        {
            sheet.Range[Row + Column].Text = "我是A1单元格";
        }
        static private string GetXlsValueCellOfSheet(Worksheet sheet, String Row, String Column)
        {

            return sheet.Range[Row + Column].Text;
        }
        static private void SetXlsValueTypeCellOfSheet(Worksheet sheet, String Row, String Column)
        {
            sheet.Range["A1"].Text = "我是A1单元格";
            sheet.Range[1, 2].Text = "我是第1行的第2个单元格";
            sheet.Range["A3"].NumberValue = 100.23;
            sheet.Range["A4"].DateTimeValue = DateTime.Now;
            sheet.Range["A5"].BooleanValue = true;
            //对一定范围内的单元格进行字体控制
            sheet.Range["A1:B10"].Style.Font.FontName = "微软雅黑";//字体名称
            sheet.Range["A1:B10"].Style.Font.Size = 20;//字体大小
            sheet.Range["A1:B10"].Style.Font.Underline = FontUnderlineType.DoubleAccounting;//下划线类型
        }
        static private void MergeCellOfSheet(Worksheet sheet, String Row, String Column)
        {
            //将A5-B6的单元格合并
            sheet.Range[Row + Column].Merge();
            //将某一行全部合并
            sheet.Rows[7].Merge();
        }
        static private void GroupByRowsOfSheet(Worksheet sheet, String Row, String Column)
        {
            sheet.GroupByRows(2, 9, true);//最后1个bool参数是默认是否折叠
        }
        static private void OpenXls(string FilePath)
        {
            System.Diagnostics.Process.Start(FilePath);
        }
        static private void FindAllStringOfSheet(Worksheet sheet)
        {
            CellRange[] ranges = sheet.FindAllString("test", false, false);
            //循环找到的单元格
            foreach (CellRange range in ranges)
            {
                range.Text = "修改后";
                range.Style.Color = Color.Yellow;
            }
        }
        static private void SetFilterOfSheet(Worksheet sheet)
        {
            //创建过滤器
            sheet.ListObjects.Create("Table", sheet.Range[1, 1, sheet.LastRow, sheet.LastColumn]);
            sheet.ListObjects[0].BuiltInTableStyle = TableBuiltInStyles.TableStyleLight9;
        }
        static private void ClearFilterOfSheet(Worksheet sheet)
        {
            sheet.AutoFilters.Clear();
        }

        static private void InsertArrayOfSheet(Worksheet sheet, object[,] objectArray)
        {
            int maxRow = 10;
            int maxCol = 5;
            //生成测试数据数组
            object[,] myarray = new object[maxRow + 1, maxCol + 1];
            bool[,] isred = new bool[maxRow + 1, maxCol + 1];
            for (int i = 0; i <= maxRow; i++)
            {
                for (int j = 0; j <= maxCol; j++)
                {
                    myarray[i, j] = i + j;
                    if ((int)myarray[i, j] > 8)
                        isred[i, j] = true;
                }
            }
            //将数组插入到sheet中，后面参数是起始的行和列号
            sheet.InsertArray(objectArray, 1, 1);
        }

        //保护sheet
        static private void SetXlsProperty(Workbook workbook)
        {
            //修改文档属性信息，这样在发布的时候，可以通过文档显示公司以及文件人的信息
            workbook.DocumentProperties.Author = "Bran.Wang";            //作者
            workbook.DocumentProperties.Subject = "测试文件属性";   //主题
            workbook.DocumentProperties.Title = "测试Excel文件";    //标题
            workbook.DocumentProperties.Company = "FiberHome";     //单位
            workbook.DocumentProperties.Comments = "自动生成";  //评论
            workbook.DocumentProperties.Keywords = "H文件生成的XlS ";   //关键词
            workbook.DocumentProperties.CreatedTime = DateTime.Now; //创建时间
        }
        public static void GenerateToObjFromXls(StructDatas structfileObj, string WorkPath, string XlsxFileName)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(WorkPath + XlsxFileName);
            workbook.SaveAsXml(WorkPath + "result.xml");
            //保存到物理路径
            workbook.SaveToFile(WorkPath + XlsxFileName, ExcelVersion.Version2013);
        }
        public static void GenerateToXlsFromObj(StructDatas structfileObj, string WorkPath, string XlsxFileName)
        {
            //新建Workbook
            Workbook workbook = new Workbook();
            //设置xls属性
            SetXlsProperty(workbook);
            //得到第一个Sheet页
            Worksheet sheet = workbook.Worksheets[0];
            int row = 1;
            int column = 1;
            //int startRow = 1;
            //int lastRow = 1;
            foreach (structitem structitemObj in structfileObj.structitemlist)
            {
                //1.将structItemObj数据添加到节点中
                FullDataToXlsByStructitem(structitemObj, sheet, row, column);
                row++;
                //2.将structItemObj. List<parameter> 添加到节点中
                // startRow = row;
                foreach (parameter parameterObj in structitemObj.parameterlist)
                {

                    FullDataToXlsByParameter(parameterObj, sheet, row, column + 1);
                    row++;
                }
                //lastRow = row;
                //3.折叠parameter中的数据
                //  sheet.GroupByRows(startRow, lastRow, true);//最后1个bool参数是默认是否折叠
            }
            //保存到物理路径
            workbook.SaveToFile(WorkPath + XlsxFileName, ExcelVersion.Version2013);
        }
        public static void FullDataToXlsByStructitem(structitem structitemObj, Worksheet sheet, int row, int column)
        {
            sheet.Range[row, column++].Text = structitemObj.CID;
            sheet.Range[row, column++].Text = structitemObj.type;
            sheet.Range[row, column++].Text = structitemObj.name;
            sheet.Range[row, column++].Text = structitemObj.preinput;
            sheet.Range[row, column++].Text = structitemObj.note;
        }
        public static void FullDataToXlsByParameter(parameter parameterObj, Worksheet sheet, int row, int column)
        {
            sheet.Range[row, column++].Text = parameterObj.CID;
            sheet.Range[row, column++].Text = parameterObj.type;
            sheet.Range[row, column++].Text = parameterObj.preinput;
            sheet.Range[row, column++].Text = parameterObj.range;
            sheet.Range[row, column++].Text = parameterObj.value;
            sheet.Range[row, column++].Text = parameterObj.length;
            sheet.Range[row, column++].Text = parameterObj.note;

        }
    }
}
