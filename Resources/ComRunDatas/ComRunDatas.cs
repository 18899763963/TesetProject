using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using SmallManagerSpace.Resources.GUIVsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace SmallManagerSpace.Resources
{
    public enum StepProcess { InitComm = 1, ParserFileToEntity, EntityToXML, EntityToXLS, EntityToGUI }
    public static class ComRunDatas
    {
        static public Dictionary<string, BaseData> CommonBaseDictonary = null;
        static public Dictionary<string, EnumData> CommonEnumDictonary = null;
        static public EnumUserDataOperation CommonEnumUserDatasOperation = null;
        static public StructDataOperation CommonStructDataOperation = null;
        static public StepProcess StepNow = StepProcess.InitComm;
        static public StructDatas StructEntity = null;
        static public EnumUserDatas EnumEntity = null;        
        static public string StructItemsFileName = null;
        static public string EnumItemsFileName = null;
        static public string HeadSourceFileName = null;
        static public string WorkPath = null;
        static public bool isSuccessdLoadFile = false;
        static public TabControl tabControl1 = null;
        static public AdvTree advTree = null;
        static public string StructBody = null;
        static public string PublicPreinputName = null;
        static public Dictionary<string, int> RegisterPreinput = null;
        static public Dictionary<string, ElementStyle> NodeElementStyle = null;
        static public DataTable dataTableEntity = null;
        //——————————————————————————————————
        //static public frmMain defaultInstance;
        //XElement xElementStartPublic = null;
        //TransformFileClass xmlFileClass = null;
        //XElement xEnumElements = null;
        //List<string> ListBlockName = null;
        //List<TabPageProperty> tabPageProperties = new List<TabPageProperty>();
        //List<ChangedNode> changedNodeList = new List<ChangedNode>();
        //Dictionary<string, string> structblockDic;
        //Dictionary<string, string> parameterDic;

        public class Student
        {
            private int studentNo;

            public int StudentNo
            {
                get { return studentNo; }
                set { studentNo = value; }
            }

            private string studentName;

            public string StudentName
            {
                get { return studentName; }
                set { studentName = value; }
            }

            private string sex;

            public string Sex
            {
                get { return sex; }
                set { sex = value; }
            }
        }
        static public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            DataColumn dcNo = new DataColumn("StudentNo", typeof(Int32));
            DataColumn dcName = new DataColumn("StudentName", typeof(string));
            DataColumn dcSex = new DataColumn("Sex", typeof(string));
            dt.Columns.Add(dcNo);
            dt.Columns.Add(dcName);
            dt.Columns.Add(dcSex);
            dt.Rows.Add(new object[] { 1, "测试", "男" });
            dt.Rows.Add(new object[] { 2, "开发", "男" });
            dt.Rows.Add(new object[] { 3, "会计", "男" });
            return dt;
        }
        //——————————————————————————————————

        /// <summary>
        /// 初始化程序运行的公共数据
        /// </summary>
        static public void BussnessInitCommonData(TabControl tabControlObj,string PathFileName)
        {
            dataTableEntity = GetDataTable();
            InitBaseAndEnumData();
            InitPathAndFileData(PathFileName);       
            InitTableControlData(tabControlObj);
            InitAdvTreeData();
        }
        /// <summary>
        /// 初始化程序运行的Base,Enum公共数据
        /// </summary>
        static public void InitBaseAndEnumData()
        {
            //1.得到base中各个元素的值
            BaseDataOperation bassDataOperation = new BaseDataOperation();
            XElement BaseElement = bassDataOperation.GetBaseRootElement();
            CommonBaseDictonary = bassDataOperation.GetBaseElementString(BaseElement);

            //2.得到enum中各个元素的值
            EnumDataOperation enumDataOperation = new EnumDataOperation();
            XElement EnumElement = enumDataOperation.GetEnumRootElement();
            CommonEnumDictonary = enumDataOperation.GetEnumElementString(EnumElement);

            //3.建立数据容器对象
            CommonEnumUserDatasOperation  = new EnumUserDataOperation();
            CommonEnumUserDatasOperation.CreatesimpleTypeInfo();
            CommonStructDataOperation = new StructDataOperation();
            CommonStructDataOperation.CreateConfigFileInfo();
        }
        static public void InitPathAndFileData(string PathFileName)
        {
            //1.解析xml文件路径
            PathFileStringADU pathFileStringADU = new PathFileStringADU();
            WorkPath            = pathFileStringADU.GetDirectionNameString(PathFileName);
            HeadSourceFileName  = pathFileStringADU.GetFileNameString(PathFileName);
            StructItemsFileName = "StructItems.xml";
            EnumItemsFileName   = "EnumItems.xml";
        }
        static public void InitAdvTreeData()
        {
            advTree = new AdvTree();
            NodeElementStyle = new Dictionary<string, ElementStyle>();
            RegisterPreinput = new Dictionary<string, int>();
            StructBody = "StructList";
            PublicPreinputName = "board_num";
            AdvTreeObj advTreeObj = new AdvTreeObj();
            advTreeObj.InitAdvTreeDatas();
            
        }
        static public void InitTableControlData(TabControl tabControlObj)
        {
            tabControl1 = tabControlObj;
            tabControl1.Controls.Clear();
        }
    }
    
}
