using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using SmallManagerSpace.Resources.FileStringADU;
using SmallManagerSpace.Resources.GUIVsEntity;
using SmallManagerSpace.Resources.XmlVsEntity;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SmallManagerSpace.Resources
{
    public enum StepProcess { InitComm = 1, ParserFileToEntity, EntityToXML, EntityToXLS, EntityToGUI }
    public static class ComRunDatas
    {
        static public Dictionary<string, BaseData> commonBaseDictonary = null;
        //static public Dictionary<string, EnumProgramData> CommonEnumDictonary = null;
        static public EnumFunction enumFunction = null;
        static public StructFunction structFunction = null;
        static public StructEntity structEntity = null;
        static public EnumEntity enumEntity = null;
        static public StepProcess stepNow = StepProcess.InitComm;
        static public string structItemsFileName = null;
        static public string enumItemsFileName = null;
        static public string eadSourceFileName = null;
        static public string saveCFileName = null;
        static public string saveWorkPath = null;
        static public string sourceWorkPath = null;
        static public string programStartPath = System.Windows.Forms.Application.StartupPath;
        static public bool isSuccessdLoadFile = false;
        static public TabControl tabControl1 = null;
        static public AdvTree advTree = null;
        static public string structBody = null;
        static public string publicPreinputName = null;
        static public Dictionary<string, int> registerPreinput = null;
        static public Dictionary<string, ElementStyle> nodeElementStyle = null;

        /// <summary>
        /// 初始化程序运行的公共数据
        /// </summary>
        static public void InitCommonData(TabControl tabControlObj, string pathFileName)
        {
            InitBaseData();
            InitEnumSturctContainer();
            InitPathAndFileData(pathFileName);
            InitTableControlData(tabControlObj);
            InitAdvTreeData();
        }
        /// <summary>
        /// 初始化程序运行的Base
        /// </summary>
        static public void InitBaseData()
        {
            //1从文件中得到base中各个元素的值
            XmlVsBase bassDataOperation = new XmlVsBase();
            XElement BaseElement = bassDataOperation.GetBaseRootElement();
            commonBaseDictonary = bassDataOperation.GetBaseElementString(BaseElement);
        }
        /// <summary>
        /// Enum Struct 公共数据建立数据容器对象
        /// </summary>
        static public void InitEnumSturctContainer()
        {
            enumFunction = new EnumFunction();
            enumFunction.CreatesimpleTypeInfo();
            structFunction = new StructFunction();
            structFunction.CreateConfigFileInfo();
        }
        static public void InitPathAndFileData(string PathFileName)
        {
            //1.解析xml文件路径
            FileStringOperation pathFileString = new FileStringOperation();
            sourceWorkPath = pathFileString.GetDirectionNameString(PathFileName);
            eadSourceFileName = pathFileString.GetFileNameString(PathFileName);
            structItemsFileName = "StructItems.xml";
            enumItemsFileName ="EnumItems.xml";
        }

        static public void InitAdvTreeData()
        {
            advTree = new AdvTree();
            nodeElementStyle = new Dictionary<string, ElementStyle>();
            registerPreinput = new Dictionary<string, int>();
            structBody = "StructList";
            publicPreinputName = "board_num";
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
