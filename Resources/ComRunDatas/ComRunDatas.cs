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
        static public EnumOfSourceFileDataOperation enumOfSourceFileDataOperation = null;
        static public StructOfSourceFileDataOperation structOfSourceFileDataOperation = null;
        static public StepProcess StepNow = StepProcess.InitComm;
        static public StructOfSourceFileDatas StructOfSourceFileEntity = null;
        static public EnumOfSourceFileDatas EnumOfSourceFileEntity = null;        
        static public string StructItemsOfSourceFileName = null;
        static public string EnumItemsOfSourceFileName = null;
        static public string HeadSourceFileName = null;
        static public string SinkCFileName = null;
        static public string SinkWorkPath = null;
        static public string SourceWorkPath = null;
        static public bool isSuccessdLoadFile = false;
        static public TabControl tabControl1 = null;
        static public AdvTree advTree = null;
        static public string StructBody = null;
        static public string PublicPreinputName = null;
        static public Dictionary<string, int> RegisterPreinput = null;
        static public Dictionary<string, ElementStyle> NodeElementStyle = null;

 



        /// <summary>
        /// 初始化程序运行的公共数据
        /// </summary>
        static public void BussnessInitCommonData(TabControl tabControlObj,string PathFileName)
        {
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
            enumOfSourceFileDataOperation  = new EnumOfSourceFileDataOperation();
            enumOfSourceFileDataOperation.CreatesimpleTypeInfo();
            structOfSourceFileDataOperation = new StructOfSourceFileDataOperation();
            structOfSourceFileDataOperation.CreateConfigFileInfo();
        }
        static public void InitPathAndFileData(string PathFileName)
        {
            //1.解析xml文件路径
            PathFileStringADU pathFileStringADU = new PathFileStringADU();
            SourceWorkPath            = pathFileStringADU.GetDirectionNameString(PathFileName);
            HeadSourceFileName  = pathFileStringADU.GetFileNameString(PathFileName);
            StructItemsOfSourceFileName = "SourceFileToStructItems.xml";
            EnumItemsOfSourceFileName   = "SourceFileToEnumItems.xml";
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
