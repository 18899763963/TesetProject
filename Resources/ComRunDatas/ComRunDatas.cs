using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using SmallManagerSpace.Resources.FileStringADU;
using SmallManagerSpace.Resources.GUIModels;
using SmallManagerSpace.Resources.GUIVsEntity;
using SmallManagerSpace.Resources.XmlVsEntity;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SmallManagerSpace.Resources
{
    public enum Step { InitComm = 1, ParserFileToEntity, EntityToXML, EntityToXLS, EntityToGUI, EntityToCustomEntity }
    public static class ComData
    {
        static public Dictionary<string, BaseEntity> baseDictonary = null;
        static public EnumFunction enumFunction = null;
        static public StructFunction structFunction = null;
        static public StructEntity structEntity = null;
        static public StructEntity customStruct = null;
        static public StructEntity importStruct = null;
        static public EnumEntity enumEntity = null;
        static public List<DefineEntity> defineEntities =new  List<DefineEntity>();
        static public Step stepNow = Step.InitComm;
        static public string baseItemsFileName = "BaseItems.xsd";
        static public string enumItemsFileName = "EnumItems.xml";
        static public string structItemsFileName = "StructItems.xml";
        //static public string importItemsFileName = "ImportItems.xml";
        static public string customItemsFileName = "CustomItems"+DateTime.Now.ToString("yyyyMMddHHmm")+".xml";
        static public string selectedSourceFileName = null;
        static public string importedSourceFileName = null;
        static public string saveCFileName = null;
        static public string saveWorkPath = null;
        static public string sourceWorkPath = null;
        static public string importedWorkPath = null;
        static public string programStartPath = System.Windows.Forms.Application.StartupPath + "\\";
        static public bool isSuccessdLoadFile = false;
        static public TabControl tabControl1 = null;
        static public AdvTree advTree = null;
        static public string structBody = null;
        static public Dictionary<string, int> EntryVar = null;
        static public Dictionary<string, int> customEntryVar = null;
        static public Dictionary<string, ElementStyle> nodeElementStyle = null;
        static public int skinIndex = 0;//AdvTree颜色主题编号
        static public List<string> readAllLines = new List<string>();
        static public List<string> OutLines = new List<string>();
        /// <summary>
        /// 初始化导入融合的公共数据
        /// </summary>
        static public void InitImportData()
        {
            defineEntities.Clear();
        }
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
        /// 初始化程序运行的公共数据
        /// </summary>
        static public void InitImportFileData(string pathFileName)
        {

            InitImportPathAndFileData(pathFileName);
        }
        /// <summary>
        /// 初始化程序运行的Base
        /// </summary>
        static public void InitBaseData()
        {
            //1从文件中得到base中各个元素的值
            XmlVsBase bassDataOperation = new XmlVsBase();
            XElement BaseElement = bassDataOperation.GetBaseRootElement(ComData.programStartPath + ComData.baseItemsFileName);
            baseDictonary = bassDataOperation.GetBaseElementString(BaseElement);
        }
        /// <summary>
        /// Enum Struct 公共数据建立数据容器对象
        /// </summary>
        static public void InitEnumSturctContainer()
        {
            defineEntities.Clear();
            structEntity = null;
            customStruct = null;
            enumFunction = new EnumFunction();
            enumFunction.CreatesimpleTypeInfo();
            structFunction = new StructFunction();
            structFunction.CreateStructEntity();
        }
        static public void InitPathAndFileData(string PathFileName)
        {
            //1.解析xml文件路径
            FileStringFunction pathFileString = new FileStringFunction();
            sourceWorkPath = pathFileString.GetDirectionNameString(PathFileName);
            selectedSourceFileName = pathFileString.GetFileNameString(PathFileName);
        }
        /// <summary>
        /// 初始化导入合并文件路径
        /// </summary>
        /// <param name="PathFileName"></param>
        static public void InitImportPathAndFileData(string PathFileName)
        {
            //1.解析xml文件路径
            FileStringFunction pathFileString = new FileStringFunction();
            importedWorkPath = pathFileString.GetDirectionNameString(PathFileName);
            importedSourceFileName = pathFileString.GetFileNameString(PathFileName);
        }

        static public void InitAdvTreeData()
        {
            advTree = new AdvTree();
            nodeElementStyle = new Dictionary<string, ElementStyle>();

            EntryVar = new Dictionary<string, int>();
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
