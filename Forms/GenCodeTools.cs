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
        delegate void DisplayDataGuiViaEntity(string n);
        delegate void GenerationXlsFromXml(string n);
        delegate void GenerationXmlFromEntity(string WorkSpace,string StructFileName,string EnumFileName);
        delegate void InitCommonData(TabControl tabControlInstance, string n);
        delegate void MainProcess(string n);
        public void MainProcessInstance(string PathFileName)
        {
            //1.����base�ļ�,��ʼ��AdvTree,TabControl,·���ַ��ȹ�������
            if (ComRunDatas.stepNow.Equals(StepProcess.InitComm))
            {
                InitCommonData InitCommonData = new InitCommonData(ComRunDatas.InitCommonData);
                InitCommonData(tabControl1, PathFileName);
                ComRunDatas.stepNow = StepProcess.ParserFileToEntity;
            }    
            //2.�����H/XML�ļ�����ת��OBJ����
            if (ComRunDatas.stepNow.Equals(StepProcess.ParserFileToEntity))
            {   //a.�����Hͷ�ļ�->Obj
                if (PathFileName.Contains(".h"))
                {
                    GenerationEntityFromHeader GenerationEntityFromHeader = new GenerationEntityFromHeader(GetEntityFromHeaderFile);
                    GenerationEntityFromHeader(PathFileName);
                    //��obj������������л���xml�ļ���
                    GenerationXmlFromEntity GenerationXmlFromEntity = new GenerationXmlFromEntity(SerialStructEnumEntityToXml);
                    GenerationXmlFromEntity(ComRunDatas.programStartPath, ComRunDatas.structItemsFileName, ComRunDatas.enumItemsFileName);
                    ComRunDatas.stepNow = StepProcess.EntityToGUI;
                }
                //b.�����XMLͷ�ļ�->OBJ
                else if (PathFileName.Contains(".xml"))
                {
                    GenerationEntityFromXml GenerationEntityFromXml = new GenerationEntityFromXml(GetEntityFromXmlFile);
                    GenerationEntityFromXml(PathFileName);
                    ComRunDatas.stepNow = StepProcess.EntityToGUI;
                }
            }
            //3.OBJ�������ɽ���
            if (ComRunDatas.stepNow.Equals(StepProcess.EntityToGUI))
            {
                DisplayDataGuiViaEntity displayDataGuiViaOBJ = new DisplayDataGuiViaEntity(DisplayDataGuiViaOBJ);
                displayDataGuiViaOBJ(PathFileName);
            }
            ComRunDatas.stepNow = StepProcess.InitComm;
        }


        private void GetEntityFromHeaderFile(string InputFilePath)
        {
            //1.�����ļ�����
            EntityVsFile.GetEntityFromFile(InputFilePath);
        }
        private void GetEntityFromXmlFile(string InputFilePath)
        {
            //1.����XML�ļ����ݵ�Entity
            ComRunDatas.structEntity = ComRunDatas.structFunction.XmlDeSerializeToStructObj(ComRunDatas.sourceWorkPath, ComRunDatas.structItemsFileName);
        }
        private void SerialStructEnumEntityToXml(string WorkSpace, string StructFileName,string EnumFileName)
        {
             ComRunDatas.structFunction.XmlSerializeToStructFile(WorkSpace, StructFileName);
             ComRunDatas.enumFunction.XmlSerializeToEnumFile(WorkSpace,EnumFileName);
        }
        private void DisplayDataGuiViaOBJ(string InputFilePath)
        {
            EntityToAdvTree entityToAdvTree = new EntityToAdvTree();
            entityToAdvTree.FullDataToAdvTreeFromXMLObj();
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new ThreadStart(() =>
            {
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = "E:\\";
                    openFileDialog.Filter = "h�ļ�(*.h)|*.h|xml�ļ�(*.xml)|*.xml";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 1;
                    //���������׶�
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
        /// �������нڵ㣬����.h�ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //�����ļ����� 
                //saveFileDialog1.Filter = "c�ļ�|*.c|��־�ļ�|*.xml";
                saveFileDialog1.Filter = "c�ļ�|*.c";

                //����Ĭ���ļ�������ʾ˳�� 
                saveFileDialog1.FilterIndex = 1;
                //����Ի����Ƿ�����ϴδ򿪵�Ŀ¼ 
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if(saveFileDialog1.FileName.ToLower().Contains(".c"))
                    {
                        FileStringFunction pathFileStringADU = new FileStringFunction();
                        ComRunDatas.saveWorkPath = pathFileStringADU.GetDirectionNameString(saveFileDialog1.FileName);
                        ComRunDatas.saveCFileName = pathFileStringADU.GetFileNameString(saveFileDialog1.FileName); //��ȡ�ļ���������·��;
                        AdvTree CurrentAdvTree = ComRunDatas.advTree;
                        if (CurrentAdvTree != null)
                        {   //1.����ǰ��������ת��Ϊ����
                            AdvTreeToEntity advTreeToEntity = new AdvTreeToEntity();
                            advTreeToEntity.GetEntityByAdvTreeNode(CurrentAdvTree);
                            //2.����ǰ�������ݷ���header�ļ���                            
                            string GenFileFullName = ComRunDatas.saveWorkPath +@"\"+ ComRunDatas.saveCFileName;
                            EntityVsFile.GetFileFromEntity(GenFileFullName);
                            //3.���ļ��ṹ���е��������������ֵ�滻
                            FileStringFunction fileStringADU = new FileStringFunction();
                            fileStringADU.ReplaceStringOnFile(GenFileFullName, ComRunDatas.registerPreinput);
                            //4.��obj������������л���xml�ļ���
                            GenerationXmlFromEntity GenerationXmlFromEntity = new GenerationXmlFromEntity(SerialStructEnumEntityToXml);
                            GenerationXmlFromEntity(ComRunDatas.programStartPath, ComRunDatas.structItemsFileName, ComRunDatas.enumItemsFileName);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
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
            //            //�õ�ѡ��ҳ������
            //            string TabPageNameBlock = GetTabPageName(PageSelectedIndex);
            //            AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
            //            AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, PageSelectedIndex);
            //            //���ѡ��ҳ������
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
            //            //�õ�ѡ��ҳ������
            //            string TabPageNameBlock = GetTabPageName(PageSelectedIndex);
            //            AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
            //            AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, PageSelectedIndex);
            //            //���ѡ��ҳ������
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
            //            //�õ�ѡ��ҳ������
            //            string TabPageNameBlock = GetTabPageName(PageSelectedIndex);
            //            AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
            //            AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, PageSelectedIndex);
            //            //���ѡ��ҳ������
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
            //    textBox1.SelectedText = "";//�ٰѵ�ǰѡȡ�����������,��ǰ��ʵ�ּ��й�����.
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
            //    textBox1.SelectedText = "";//�ѵ�ǰѡȡ�����������
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

