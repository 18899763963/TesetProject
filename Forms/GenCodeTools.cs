using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using SmallManagerSpace.Forms;
using SmallManagerSpace.Resources;
using SmallManagerSpace.Resources.FileStringADU;
using SmallManagerSpace.Resources.GUIVsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using TabControl = DevComponents.DotNetBar.TabControl;

namespace MasterDetailSample
{
    public partial class frmMain
    {
        private static frmMain defaultInstance;
        public static frmMain Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new frmMain();
                    defaultInstance.FormClosed += new FormClosedEventHandler(defaultInstance_FormClosed);
                }
                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }
        }
        public frmMain()
        {
            InitializeComponent();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.skinEngine1.SkinFile = Application.StartupPath + @"\Skins\WarmColor1.ssk"; //8
            Sunisoft.IrisSkin.SkinEngine se = null;
            se = new Sunisoft.IrisSkin.SkinEngine();
            se.SkinAllForm = true;
        }
        static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        delegate void GenerationEntityFromHeader(string n);
        delegate void GenerationEntityFromXml(string n);
        delegate void DisplayDataGuiViaString(string n);
        delegate void DisplayDataGuiViaEntity(StructEntity structEntity);
        delegate void GenerationXlsFromXml(string n);
        delegate void GenerationXmlFromEntity(string Type,string FileFullName);
        delegate void InitCommonData(TabControl tabControlInstance, string n);
        delegate void MainProcess(string n);
        public void MainProcessInstance(string PathFileName)
        {
         
            //1.加载base文件,初始化AdvTree,TabControl,路径字符等公共变量
            if (ComData.stepNow.Equals(Step.InitComm))
            {
                InitCommonData InitCommonData = new InitCommonData(ComData.InitCommonData);
                InitCommonData(tabControl1, PathFileName);
                ComData.stepNow = Step.ParserFileToEntity;
            }    
            //2.如果是H/XML文件，则转化OBJ对象
            if (ComData.stepNow.Equals(Step.ParserFileToEntity))
            {   //a.如果是H头文件->Obj
                if (PathFileName.Contains(".h"))
                {
                    GenerationEntityFromHeader GenerationEntityFromHeader = new GenerationEntityFromHeader(GetEntityFromHeaderFile);
                    GenerationEntityFromHeader(PathFileName);
                    //将obj对象的数据序列化到xml文件中
                    GenerationXmlFromEntity GenerationXmlFromEntity = new GenerationXmlFromEntity(SerialEntityToXml);
                    GenerationXmlFromEntity("enum", ComData.programStartPath+ComData.enumItemsFileName);
                    GenerationXmlFromEntity("struct", ComData.programStartPath+ComData.structItemsFileName);
                    ComData.stepNow = Step.EntityToCustomEntity;
                }
                //b.如果是XML头文件->OBJ
                else if (PathFileName.Contains(".xml"))
                {
                    GenerationEntityFromXml GenerationEntityFromXml = new GenerationEntityFromXml(GetEntityFromXmlFile);
                    GenerationEntityFromXml(PathFileName);
                    ComData.stepNow = Step.EntityToCustomEntity;
                }
            }
            //3.Entity转换CustomEntity
            if (ComData.stepNow.Equals(Step.EntityToCustomEntity))
            {
                DefineEntityFuncion defineEntityFuncion = new DefineEntityFuncion();
                List<DefineEntity> defineEntitys = defineEntityFuncion.CreateDefineEntity();
                StructFunction structFunction = new StructFunction();
                structFunction.CreateCustomStruct(defineEntitys);
                //将obj对象的数据序列化到xml文件中
                GenerationXmlFromEntity GenerationXmlFromEntity = new GenerationXmlFromEntity(SerialEntityToXml);
                GenerationXmlFromEntity("customstruct", ComData.programStartPath + ComData.customItemsFileName);
                ComData.stepNow = Step.EntityToGUI;
            }
            //4.CustomEntity对象生成界面
            if (ComData.stepNow.Equals(Step.EntityToGUI))
            {
                DisplayDataGuiViaEntity displayDataGuiViaOBJ = new DisplayDataGuiViaEntity(DisplayDataGuiViaOBJ);
                displayDataGuiViaOBJ(ComData.customStruct);
            }
            ComData.stepNow = Step.InitComm;
        }


        private void GetEntityFromHeaderFile(string InputFilePath)
        {
            //1.解析文件内容
            EntityVsFile.GetEntityFromFile(InputFilePath);
        }
        private void GetEntityFromXmlFile(string InputFilePath)
        {
            //1.解析XML文件内容到Entity
            ComData.structEntity = ComData.structFunction.XmlDeSerializeToStructObj(ComData.sourceWorkPath, ComData.structItemsFileName);
        }
        /// <summary>
        /// 序列化对象到xml文件中
        /// </summary>
        /// <param name="Type">对象类型</param>
        /// <param name="FileFullName">存放文件地址</param>
        private void SerialEntityToXml(string Type, string FileFullName)
        {
            switch (Type)
            {
                case "enum":
                    EntitySerialize.XmlSerializeOnString(ComData.enumEntity, FileFullName);
                    break;
                case "struct":
                    EntitySerialize.XmlSerializeOnString(ComData.structEntity, FileFullName);
                    break;
                case "customstruct":
                    EntitySerialize.XmlSerializeOnString(ComData.customStruct, FileFullName);
                    break;
                default:
                    break;
            }      
        }


        private void DisplayDataGuiViaOBJ(StructEntity inputEntity)
        {
            EntityToAdvTree entityToAdvTree = new EntityToAdvTree();
            entityToAdvTree.FullDataToAdvTreeFromXMLObj(inputEntity);
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new ThreadStart(() =>
            {
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = "E:\\";
                    openFileDialog.Filter = "h文件(*.h)|*.h|xml文件(*.xml)|*.xml";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 1;
                    //定义程序处理阶段
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string SelectedPathSource = openFileDialog.FileName;
                        MainProcess MainProcess = new MainProcess(MainProcessInstance);
                        MainProcess(SelectedPathSource);
                    }
                }
            }
             ));
        }
        private void OpenFileToolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileToolStripMenuItem_Click(sender, e);
        }


        /// <summary>
        /// 遍历树中节点，生成.h文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //设置文件类型 
                //saveFileDialog1.Filter = "c文件|*.c|日志文件|*.xml";
                saveFileDialog1.Filter = "c文件|*.c";

                //设置默认文件类型显示顺序 
                saveFileDialog1.FilterIndex = 1;
                //保存对话框是否记忆上次打开的目录 
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if(saveFileDialog1.FileName.ToLower().Contains(".c"))
                    {
                        FileStringFunction pathFileStringADU = new FileStringFunction();
                        ComData.saveWorkPath = pathFileStringADU.GetDirectionNameString(saveFileDialog1.FileName);
                        ComData.saveCFileName = pathFileStringADU.GetFileNameString(saveFileDialog1.FileName); //获取文件名，不带路径;
                        AdvTree CurrentAdvTree = ComData.advTree;
                        if (CurrentAdvTree != null)
                        {   //1.将当前树的数据转换为对象
                            AdvTreeToEntity advTreeToEntity = new AdvTreeToEntity();
                            advTreeToEntity.GetEntityByAdvTreeNode(CurrentAdvTree);
                            //*********************************need to rework************************************//
                            //2.将当前树的数据放入header文件中                            
                            string GenFileFullName = ComData.saveWorkPath +@"\"+ ComData.saveCFileName;
                            EntityVsFile.GetFileFromEntity(GenFileFullName);
                            //3.将文件结构体中的数组变量用数组值替换
                            FileStringFunction fileStringADU = new FileStringFunction();
                            fileStringADU.ReplaceStringOnFile(GenFileFullName, ComData.EntryVar);
                            //********************************************************************//
                            //4.将obj对象的数据序列化到xml文件中
                            GenerationXmlFromEntity GenerationXmlFromEntity = new GenerationXmlFromEntity(SerialEntityToXml);
                            GenerationXmlFromEntity("customstruct", ComData.programStartPath+ComData.customItemsFileName);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                return;
            }
        }


        private void ChineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ChineseToolStripMenuItem.Checked = true;
            //EnglishToolStripMenuItem.Checked = false;
            //DataToolStripMenuItem.Checked = false;
            //if (tabPageProperties != null)
            //{
            //    if (tabPageProperties.Count > 0)
            //    {
            //        for (int index = 0; index < tabPageProperties.Count(); index++)
            //        {
            //            SetTabPageProperty(index, "cn", false);
            //        }
            //        int PageSelectedIndex = tabControl2.SelectedIndex;
            //        if ((tabPageProperties[PageSelectedIndex]).isFull == false)
            //        {
            //            //得到选中页的内容
            //            string TabPageNameBlock = GetTabPageName(PageSelectedIndex);
            //            AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
            //            AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, PageSelectedIndex);
            //            //填充选中页的内容
            //            FullDataToAdvTreeFromXMLNode(CurrentAdvTree, xElementStartPublic, TabPageNameBlock, PageSelectedIndex);
            //            SetTabPageProperty(PageSelectedIndex, "cn", true);
            //        }
            //    }
            //}
        }
        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ChineseToolStripMenuItem.Checked = false;
            //EnglishToolStripMenuItem.Checked = true;
            //DataToolStripMenuItem.Checked = false;
            //if (tabPageProperties != null)
            //{
            //    if (tabPageProperties.Count > 0)
            //    {
            //        for (int index = 0; index < tabPageProperties.Count(); index++)
            //        {
            //            SetTabPageProperty(index, "en", false);
            //        }
            //        int PageSelectedIndex = tabControl2.SelectedIndex;
            //        if ((tabPageProperties[PageSelectedIndex]).isFull == false)
            //        {
            //            //得到选中页的内容
            //            string TabPageNameBlock = GetTabPageName(PageSelectedIndex);
            //            AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
            //            AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, PageSelectedIndex);
            //            //填充选中页的内容
            //            FullDataToAdvTreeFromXMLNode(CurrentAdvTree, xElementStartPublic, TabPageNameBlock, PageSelectedIndex);
            //            SetTabPageProperty(PageSelectedIndex, "en", true);
            //        }
            //    }
            //}
        }
        private void DataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ChineseToolStripMenuItem.Checked = false;
            //EnglishToolStripMenuItem.Checked = false;
            //DataToolStripMenuItem.Checked = true;
            //if (tabPageProperties != null)
            //{
            //    if (tabPageProperties.Count > 0)
            //    {
            //        for (int index = 0; index < tabPageProperties.Count(); index++)
            //        {
            //            SetTabPageProperty(index, "data", false);
            //        }
            //        int PageSelectedIndex = tabControl2.SelectedIndex;
            //        if ((tabPageProperties[PageSelectedIndex]).isFull == false)
            //        {
            //            //得到选中页的内容
            //            string TabPageNameBlock = GetTabPageName(PageSelectedIndex);
            //            AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
            //            AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, PageSelectedIndex);
            //            //填充选中页的内容
            //            FullDataToAdvTreeFromXMLNode(CurrentAdvTree, xElementStartPublic, TabPageNameBlock, PageSelectedIndex);
            //            SetTabPageProperty(PageSelectedIndex, "data", true);
            //        }
            //    }
            //}

        }
        private void SaveFileToolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileToolStripMenuItem_Click(sender, e);
        }
        private void CollapseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CollapseToolStripButton1_Click(sender, e);
        }
        private void CollapseToolStripButton1_Click(object sender, EventArgs e)
        {
            //int PageSelectedIndex = tabControl2.SelectedIndex;
            //AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
            //if (CurrentAdvTree != null)
            //{
            //    CurrentAdvTree.CollapseAll();
            //}
        }
        private void ExpandToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ExpandToolStripButton2_Click(sender, e);
        }
        private void ExpandToolStripButton2_Click(object sender, EventArgs e)
        {

        }
        private void ExpandNodeChild(Node nodeFather)
        {
            if (nodeFather.Expanded != true)
            {
                nodeFather.Expanded = true;
            }
            if (nodeFather.HasChildNodes == true)
            {
                foreach (Node node in nodeFather.Nodes)
                {
                    ExpandNodeChild(node);
                }
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().CloseMainWindow();
        }
        private void ExitMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().CloseMainWindow();
        }
        private void CutToolStripButton1_Click(object sender, EventArgs e)
        {
            //    textBox1.Copy();
            //    textBox1.SelectedText = "";//再把当前选取的内容清除掉,当前就实现剪切功能了.
        }
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CutToolStripButton1_Click(sender, e);
        }
        private void CopyToolStripButton2_Click(object sender, EventArgs e)
        {
            //if (textBox1.Enabled == true)
            //{
            //    textBox1.Copy();
            //}
        }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CopyToolStripButton2_Click(sender, e);
        }
        private void PasteToolStripButton3_Click(object sender, EventArgs e)
        {
            //if (textBox1.Enabled == true)
            //{
            //    textBox1.Paste();
            //}
        }
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PasteToolStripButton3_Click(sender, e);
        }
        private void DeleteToolStripButton4_Click(object sender, EventArgs e)
        {
            //if (textBox1.Enabled == true)
            //{
            //    textBox1.SelectedText = "";//把当前选取的内容清除掉
            //}
        }
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DeleteToolStripButton4_Click(sender, e);
        }
        private void AboutToolStripButton7_Click(object sender, EventArgs e)
        {
            //TipMessageBoxcs aboutMessage = new TipMessageBoxcs();
            //aboutMessage.ShowDialog();
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AboutToolStripButton7_Click(sender, e);

        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

