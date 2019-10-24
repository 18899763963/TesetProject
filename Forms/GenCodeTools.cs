using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using SmallManagerSpace.Forms;
using SmallManagerSpace.Resources;
using SmallManagerSpace.Resources.FileStringADU;
using SmallManagerSpace.Resources.GUIModels;
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
        Sunisoft.IrisSkin.SkinEngine skin = new Sunisoft.IrisSkin.SkinEngine();
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

        /// <summary>
        /// 加载主题皮肤
        /// </summary>
        /// <param name="skinname">皮肤文件名</param>
        private void loadSkin(string skinname)
        {
            //this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            //this.skinEngine1.SkinFile = Application.StartupPath + @"\Skins\" + skinname; //8
            //Sunisoft.IrisSkin.SkinEngine se = null;
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinAllForm = true;
            skin.SkinFile = Application.StartupPath + @"\Skins\" + skinname;
            //skin.SkinAllForm = true;
            //skin.Active = true;
        }

        public frmMain()
        {
            InitializeComponent();
            //this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            //this.skinEngine1.SkinFile = Application.StartupPath + @"\Skins\WarmColor1.ssk"; //8
            //Sunisoft.IrisSkin.SkinEngine se = null;
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinAllForm = true;
            loadSkin("WarmColor1.ssk");
        }
        static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        delegate void DisplayDataGuiViaString(string n);
        delegate void GenerationXlsFromXml(string n);
        delegate void GenerationXmlFromEntity(string Type, string FileFullName);
        delegate void InitCommonData(TabControl tabControlInstance, string stringFileFullName);
        delegate void ImportFileProcess(string n);
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
            //2.如果是H头文件，则转换CstructEntity,再转换CustomEntity
            if (ComData.stepNow.Equals(Step.ParserFileToEntity) && PathFileName.Contains(".h"))
            {
                //如果是H头文件转换structEntity
                GetEntityFromHeaderFile(PathFileName);
                //根据相同name去除重复项，保留最新项
                StructFunction structFunction = new StructFunction();
                EnumFunction enumFunction = new EnumFunction();
                enumFunction.DistinctSameNameOfEnumEntity(ComData.enumEntity);
                structFunction.DistinctSameNameOfStructItem(ComData.structEntity);
                //structEntity转换CustomEntity                  
                ComData.customStruct = structFunction.CreateCustomStruct(ComData.defineEntities);
                //将OBJ对象的数据序列化到xml文件中
                EntitySerialize.XmlSerializeOnString(ComData.enumEntity, ComData.programStartPath + ComData.enumItemsFileName);
                EntitySerialize.XmlSerializeOnString(ComData.structEntity, ComData.programStartPath + ComData.structItemsFileName);
                ComData.stepNow = Step.EntityToGUI;

            }
            //3.如果是XML文件，则转化CustomEntity
            else if (ComData.stepNow.Equals(Step.ParserFileToEntity) && PathFileName.Contains(".xml"))
            {
                //转换enum文件的内容
                ComData.enumEntity = ComData.enumFunction.XmlDeSerializeToEnumObj(ComData.programStartPath, ComData.enumItemsFileName);
                //解析XML文件内容到Entity                         
                ComData.customStruct = GetSructEntityFromXmlFileDistinctNameIndex(ComData.sourceWorkPath, ComData.selectedSourceFileName);
                ComData.stepNow = Step.EntityToGUI;
            }
            //4.CustomEntity对象生成界面
            if (ComData.stepNow.Equals(Step.EntityToGUI))
            {
                //按name 升序排序
                ComData.customStruct.nodeList.Sort(new StructSortByNameIndex());
                DisplayDataGuiViaOBJ(ComData.customStruct);
            }
            ComData.stepNow = Step.InitComm;
            //打开添加文件按钮
            MenuButtonObj menubt = new MenuButtonObj();
            menubt.EnableToolStripButton(this.AddFileAToolStripButton1);
            MenuItemObj menuit = new MenuItemObj();
            menuit.EnableToolStripItem(this.AddFileToolStripMenuItem2);
        }

        /// <summary>
        /// 添加文件到主界面的实例
        /// </summary>
        /// <param name="PathFileName">文件路径名称</param>
        public void ImportFileProcessInstance(string PathFileName)
        {
            //0.导入先将图形界面中的内容序列化到实体      
            if (ComData.advTree != null)
            {   //1.将当前树的数据转换为对象
                AdvTreeToEntity advTreeToEntity = new AdvTreeToEntity();
                ComData.customStruct = advTreeToEntity.GetEntityByAdvTreeNode(ComData.advTree);
            }
            //1.加载base文件,初始化路径字符等公共变量
            if (ComData.stepNow.Equals(Step.InitComm))
            {
                ComData.InitImportPathAndFileData(PathFileName);
                ComData.InitImportData();
                ComData.stepNow = Step.ParserFileToEntity;
            }
            ////2.如果是H头文件->StructOBJ对象-->CustomOBJ对象
            //if (ComData.stepNow.Equals(Step.ParserFileToEntity) && PathFileName.Contains(".h"))
            //{
            //    //如果是H头文件->StructOBJ对象
            //    GetEntityFromHeaderFile(PathFileName);
            //    //去除结构体中相同元素项目（匹配：name,保留方法：存储最新）
            //    StructFunction structFunction = new StructFunction();
            //    EnumFunction enumFunction = new EnumFunction();
            //    enumFunction.DistinctSameNameOfEnumEntity(ComData.enumEntity);
            //    structFunction.DistinctSameNameOfStructItem(ComData.structEntity);
            //    //structEntity转换CustomEntity               
            //    ComData.importStruct = structFunction.CreateCustomStruct(ComData.defineEntities);
            //    //合并importStruct到customStruct，保留最新项目
            //    if (IsContainsSameItemByName(ComData.importStruct, ComData.customStruct))
            //    {
            //        //产生对话框
            //    }
            //    ComData.customStruct = MergeStructByNameIndexForNewXml(ComData.importStruct, ComData.customStruct, true);
            //    //按name 升序排序
            //    ComData.customStruct.nodeList.Sort(new StructSortByNameIndex());
            //    //将OBJ对象的数据序列化到xml文件中
            //    EntitySerialize.XmlSerializeOnString(ComData.enumEntity, ComData.programStartPath + ComData.enumItemsFileName);
            //    EntitySerialize.XmlSerializeOnString(ComData.structEntity, ComData.programStartPath + ComData.structItemsFileName);
            //    ComData.stepNow = Step.EntityToGUI;
            //}
            //2.如果是xml文件->importOBJ对象-->CustomOBJ对象
            if (ComData.stepNow.Equals(Step.ParserFileToEntity) && PathFileName.Contains(".xml"))
            {
                //转换enum文件的内容                  
                ComData.enumEntity = ComData.enumFunction.XmlDeSerializeToEnumObj(ComData.programStartPath, ComData.enumItemsFileName);
                //解析XML文件内容到Entity       
                ComData.importStruct = GetSructEntityFromXmlFileDistinctNameIndex(ComData.importedWorkPath, ComData.importedSourceFileName);
                //合并importStruct到customStruct，保留最新项目
                if (IsContainsSameItemByName(ComData.importStruct, ComData.customStruct))
                {
                    //产生对话框
                }
                ComData.customStruct = MergeStructByNameIndexForNewXml(ComData.importStruct, ComData.customStruct, true);
                //按name 升序排序
                ComData.customStruct.nodeList.Sort(new StructSortByNameIndex());
                ComData.stepNow = Step.EntityToGUI;
            }
            //3.CustomEntity对象生成界面
            if (ComData.stepNow.Equals(Step.EntityToGUI))
            {
                DisplayDataGuiViaOBJ(ComData.customStruct);
            }
            ComData.stepNow = Step.InitComm;
        }


        private void GetEntityFromHeaderFile(string InputFilePath)
        {
            //1.解析文件内容
            EntityVsFile.GetEntityFromFile(InputFilePath);
        }
        /// <summary>
        /// 是否包含同名name的结构体类型
        /// </summary>
        /// <param name="newStruct"></param>
        /// <param name="oldSturct"></param>
        /// <returns></returns>
        private bool IsContainsSameItemByName(StructEntity newStruct, StructEntity oldSturct)
        {
            bool result = false;
            if (newStruct != null && oldSturct != null)
            {
                if (newStruct.nodeList.Intersect<object>(oldSturct.nodeList, new StructEqualityByName()).Count<object>() > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        private void SetMaxBoardNumForNewStruct(StructEntity newStruct, StructEntity oldSturct)
        {
            string newStructIndexValue = newStruct.nodeList.Max(i => (i as StructItem).index);
            string oldStructIndexValue = oldSturct.nodeList.Max(i => (i as StructItem).index);
            switch (oldStructIndexValue.CompareTo(newStructIndexValue))
            {
                //front<rear;
                case -1:
                    //newStruct.nodeList.Where(i => (i as StructItem).type.Equals("OTN_USER_B_TYPE_INFO")).FirstOrDefault();
                    break;
                //front=rear;
                case 0:
                    //oldSturct.nodeList.Where(i => (i as StructItem).type.Equals("OTN_USER_B_TYPE_INFO")).FirstOrDefault();
                    break;
                //front>rear;
                case 1:
                    object oldObj = oldSturct.nodeList.Where(i => (i as StructItem).type.Equals("OTN_USER_B_TYPE_INFO")).FirstOrDefault();
                   // object newObj = newStruct.nodeList.Where(i => (i as StructItem).type.Equals("OTN_USER_B_TYPE_INFO")).FirstOrDefault();
                    //(newStruct.nodeList.Where(i => (i as StructItem).type.Equals("OTN_USER_B_TYPE_INFO")).FirstOrDefault() as StructItem)=;
                    foreach(object newObj in newStruct.nodeList)
                    {
                        if(newObj is StructItem)
                        {
                            StructItem nO = newObj as StructItem;
                            StructItem oO = oldObj as StructItem;
                            if (nO.type.Equals("OTN_USER_B_TYPE_INFO"))
                            {
                                nO.parameterList = oO.parameterList;
                            }        
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 更具name,index保留尾项法合并结构体
        /// </summary>
        /// <param name="newStruct">新结构体项</param>
        /// <param name="oldSturct">旧结构体项</param>
        /// <param name="isSaveNew">true:相同项目，则保留newStruct;false:相同项目，则保留oldSturct</param>
        /// <returns></returns>
        private StructEntity MergeStructByNameIndexForNewXml(StructEntity newStruct, StructEntity oldSturct, bool isSaveNew)
        {
            StructEntity mergeStruct = new StructEntity();
            if (newStruct != null && oldSturct != null)
            {
                if (isSaveNew)
                {
                    //获取最大board_num结构体
                    SetMaxBoardNumForNewStruct(newStruct, oldSturct);
                    //留重复的元素尾元素
                    oldSturct.nodeList.AddRange(newStruct.nodeList);
                    oldSturct.nodeList.Reverse();
                    oldSturct.nodeList = oldSturct.nodeList.Distinct<object>(new StructEqualityByNameIndex()).ToList<object>();
                    oldSturct.nodeList.Reverse();
                    mergeStruct.nodeList = oldSturct.nodeList;
                }
                else
                {
                    //留重复的元素头元素
                    oldSturct.nodeList.AddRange(newStruct.nodeList);
                    oldSturct.nodeList = oldSturct.nodeList.Distinct<object>(new StructEqualityByNameIndex()).ToList<object>();
                    mergeStruct.nodeList = oldSturct.nodeList;
                }

            }
            return mergeStruct;
        }


        ///// <summary>
        ///// 得到结构体中有相同项目结构体
        ///// </summary>
        ///// <param name="newStruct">新结构体项</param>
        ///// <param name="oldSturct">旧结构体项</param>
        ///// <param name="isSaveNew">true:相同项目，则保留newStruct;false:相同项目，则保留oldSturct</param>
        ///// <returns></returns>
        //private StructEntity MergeStructEntityFromNewXml(StructEntity newStruct, StructEntity oldSturct, bool isSaveNew)
        //{
        //    StructEntity mergeStruct = new StructEntity();
        //    if (newStruct != null && oldSturct != null)
        //    {
        //        if (isSaveNew)
        //        {
        //            foreach (object ob in newStruct.nodeList)
        //            {
        //                //不含相同项目，则添加到oldStruct列表结尾
        //                if (!oldSturct.nodeList.Contains<object>(ob, new StructEqualityByName()))
        //                {
        //                    oldSturct.nodeList.Add(ob);
        //                }
        //                //含有相同项目，则将newStruct相同名的子项替换oldStruct的子项处
        //                else
        //                {
        //                    if (ob is StructItem)
        //                    {
        //                        StructItem sI = ob as StructItem;
        //                        //此处的筛选是同一份内容嘛？
        //                        //List<object> sameObjectAtOldSide = oldSturct.nodeList.Where(i => i.Equals(ob)).ToList<object>();
        //                        List<object> sameObjectAtOldSide = oldSturct.nodeList.FindAll(i => (i as StructItem).name == sI.name);
        //                        //更换其中的parameterList项目
        //                        foreach (object obItem in sameObjectAtOldSide)
        //                        {
        //                            (obItem as StructItem).parameterList = sI.parameterList;
        //                        }
        //                    }
        //                }
        //            }
        //            mergeStruct.nodeList = oldSturct.nodeList;
        //        }
        //        else
        //        {

        //            foreach (object ob in newStruct.nodeList)
        //            {
        //                //不含相同项目，则添加到oldStruct列表结尾
        //                if (!oldSturct.nodeList.Contains<object>(ob, new StructEqualityByName()))
        //                {
        //                    oldSturct.nodeList.Add(ob);
        //                }
        //            }
        //            mergeStruct.nodeList = oldSturct.nodeList;
        //        }

        //    }
        //    return mergeStruct;
        //}


        private StructEntity GetSructEntityFromXmlFileDistinctNameIndex(string path, string InputFilePath)
        {
            //1.解析XML文件内容到Entity
            StructEntity structEntity = ComData.structFunction.XmlDeSerializeToStructObj(path, InputFilePath);
            //留重复的元素尾元素
            structEntity.nodeList.Reverse();
            structEntity.nodeList = structEntity.nodeList.Distinct<object>(new StructEqualityByNameIndex()).ToList<object>();
            structEntity.nodeList.Reverse();
            return structEntity;
        }

        private void DisplayDataGuiViaOBJ(StructEntity inputEntity)
        {
            EntityToAdvTree entityToAdvTree = new EntityToAdvTree();
            entityToAdvTree.FullDataToAdvTreeFromXMLObj(inputEntity);
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
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
                //saveFileDialog1.Filter = "cpp文件|*.c|日志文件|*.xml";
                saveFileDialog1.Filter = "cpp文件|*.cpp";
                saveFileDialog1.FileName = "fhapp_otn_user_auto" + DateTime.Now.ToString("yyyyMMddHHmm");

                //设置默认文件类型显示顺序 
                saveFileDialog1.FilterIndex = 1;
                //保存对话框是否记忆上次打开的目录 
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName.ToLower().Contains(".c"))
                    {
                        FileStringFunction pathFileStringADU = new FileStringFunction();
                        ComData.saveWorkPath = pathFileStringADU.GetDirectionNameString(saveFileDialog1.FileName);
                        ComData.saveCFileName = pathFileStringADU.GetFileNameString(saveFileDialog1.FileName); //获取文件名，不带路径;
                        AdvTree CurrentAdvTree = ComData.advTree;
                        if (CurrentAdvTree != null)
                        {   //1.将当前树的数据转换为对象
                            AdvTreeToEntity advTreeToEntity = new AdvTreeToEntity();
                            ComData.customStruct = advTreeToEntity.GetEntityByAdvTreeNode(CurrentAdvTree);
                            StructFunction sF = new StructFunction();
                            ComData.customEntryVar = sF.GetEntryVarValue(ComData.customStruct);
                            //*********************************************************************//
                            //2.将当前树的数据放入cpp文件中                            
                            string GenCppFileFullName = ComData.saveWorkPath + @"\" + ComData.saveCFileName;
                            EntityVsFile.GetCppFileFromEntityByT4(GenCppFileFullName);
                            //3.将当前树的数据放入header文件中 
                            string GenHeaderFileFullName = ComData.saveWorkPath + @"\" + ComData.saveCFileName.Replace(".cpp", ".h");
                            EntityVsFile.GetHeaderFileFromEntityByT4(GenHeaderFileFullName);
                            //********************************************************************//
                            //4.将obj对象的数据序列化到xml文件中
                            EntitySerialize.XmlSerializeOnString(ComData.enumEntity, ComData.programStartPath + ComData.enumItemsFileName);
                            EntitySerialize.XmlSerializeOnString(ComData.structEntity, ComData.programStartPath + ComData.structItemsFileName);
                            EntitySerialize.XmlSerializeOnString(ComData.customStruct, ComData.saveWorkPath + ComData.customItemsFileName);

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
        private void DefaultColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                loadSkin("WarmColor1.ssk");
                DefaultToolStripMenuItem.Checked = true;
                DefaultToolStripMenuItem1.Checked = true;
                LithtColorToolStripMenuItem.Checked = false;
                LightColorToolStripMenuItem1.Checked = false;
                ComData.skinIndex = 0;
                AdvTreeObj advTreeObj = new AdvTreeObj();
                advTreeObj.AdvTreeSkinSet(Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), Color.AntiqueWhite);
            }
            catch { }
        }

        private void LightColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                loadSkin("PageColor1.ssk");
                LithtColorToolStripMenuItem.Checked = true;
                LightColorToolStripMenuItem1.Checked = true;
                DefaultToolStripMenuItem.Checked = false;
                DefaultToolStripMenuItem1.Checked = false;
                ComData.skinIndex = 1;
                AdvTreeObj advTreeObj = new AdvTreeObj();
                advTreeObj.AdvTreeSkinSet(Color.FromArgb(252, 252, 252), Color.FromArgb(225, 236, 233), Color.FromArgb(242, 242, 242));
            }
            catch { }
        }

        private void DefaultColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DefaultColorToolStripMenuItem_Click(sender, e);
        }

        private void LightColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LightColorToolStripMenuItem_Click(sender, e);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                SaveFileToolStripMenuItem.PerformClick();
            }
            else if (e.KeyCode == Keys.O && e.Control)
            {
                OpenFileToolStripMenuItem.PerformClick();
            }
            else if (e.KeyCode == Keys.F4 && e.Alt)
            {
                ExitMainToolStripMenuItem.PerformClick();
            }
        }

        private void AddFileAToolStripButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "E:\\";
            //openFileDialog.Filter = "h文件(*.h)|*.h|xml文件(*.xml)|*.xml";
            openFileDialog.Filter = "xml文件(*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            //定义程序处理阶段
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string SelectedPathSource = openFileDialog.FileName;
                ImportFileProcess importFileProcess = new ImportFileProcess(ImportFileProcessInstance);
                importFileProcess(SelectedPathSource);
            }
        }

        private void AddFileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddFileAToolStripButton1_Click(sender, e);
        }
    }
}

