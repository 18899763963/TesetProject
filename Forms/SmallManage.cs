using System.Collections.Generic;
using System;
using System.Linq;
using System.Drawing;
using System.Data;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using TelnetNameSpace;
using System.Xml.Schema;
using System.Xml;
using XMLFileClassSpace;

using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Rendering;
using DevComponents.DotNetBar.SuperGrid;

namespace MasterDetailSample
{
    public partial class frmMain
    {
        #region �����ʼ��
        public frmMain()
        {
            InitializeComponent();
            SetTelnetAvailableBoolFalse();
            //Added to support default instance behavour in C#
            if (defaultInstance == null)
                defaultInstance = this;

        }
        //�������ݱ�
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
        static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }
        #endregion

        #region XMLģ��->����
        #endregion
        #region XMLģ��->����
        public void ClearFields()
        {
            panelView.Controls.Clear();
            Refresh();
        }
        //û����ʾ����
        #region SuperGridControlģ��
        //��Ԫ���ı�����¼�
        private void superGridControl1_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {

            SuperGridControl_getRow( sender, e);
        }
        //��ʾ��
        //���ݰ�����¼�
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {

            GridPanel panel = e.GridPanel;
            //��ʾ�к�
            panel.ShowRowGridIndex = true;
            if (panel.DataMember == "qtlsdmx")
            {
                double count = 0;
                foreach (GridElement item in panel.Rows)
                {
                    GridRow row = item as GridRow;
                    count += Convert.ToDouble(row["je"].Value);
                }

                panel.Footer.Text = string.Format("<font size='9' famaly='����'>�ܽ��:<font color='Green'>{0}</font></font>", count);
            }
        }

        //������ʽ
        private void SuperGridContorl_setting(SuperGridControl superGridControl1)
        {
            //����ʾFilter           
            //superGridControl1.PrimaryGrid.Filter.Visible = false;
            // ���Ʊ��ֻ��ѡ�е���
            superGridControl1.PrimaryGrid.MultiSelect = false;
            superGridControl1.PrimaryGrid.InitialSelection = RelativeSelection.Row;
            //ֻ��ѡ��һ����Ԫ�񣬶�����һ�е�Ԫ��
            superGridControl1.PrimaryGrid.SelectionGranularity = SelectionGranularity.Row;
            //�Ƿ���ʾ���к�
           // superGridControl1.PrimaryGrid.ShowRowHeaders = false;
            //�Զ��������
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.Fill;
            //����Ԫ���϶���Ϊ������
           // superGridControl1.PrimaryGrid.ColumnHeader.AllowSelection = true;
            //superGridControl1.PrimaryGrid.GroupByRow.Visible = false;

            GridPanel panel = superGridControl1.PrimaryGrid;
            //���ñ���Զ�չ��
            //panel.AutoExpandSetGroup = false;
            //���ñ�������ֵ�λ�þ���     
            superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;

            //��ʾ�к�
            //superGridControl1.PrimaryGrid.ShowRowGridIndex = true ;
            //�����кŵ���ʼֵ��Ĭ��ֵΪ0��
            //superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            //���������ͷ�Ŀ��
            superGridControl1.PrimaryGrid.AllowRowHeaderResize = true;
            //������ʾ��ͷ
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            //ע�ᵥԪ�����¼�
            superGridControl1.CellValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValueChangedEventArgs>(this.superGridControl1_CellValueChanged);
            //����ͷ��ʾɸѡͼ��
            // superGridControl1.PrimaryGrid.EnableFiltering = false;
            //superGridControl1.PrimaryGrid.EnableColumnFiltering = false;
            //��ʾFilter
            //superGridControl1.PrimaryGrid.Filter.Visible = false;
            //�����з���
            //this.superGridControl1.PrimaryGrid.GroupByRow.Visible = false;

            //����ͷ����ʾͼ��
            // .gridColumn1.HeaderStyles.Default.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            //����չ��������ͼ��
            //this.superGridControl1.PrimaryGrid.CollapseImage = global::HRMS.Properties.Resources.BugRight;
            //this.superGridControl1.PrimaryGrid.ExpandImage = global::HRMS.Properties.Resources.BugUp;
        }
        public void  SuperGridControl_getRow(object sender, GridCellValueChangedEventArgs e)
        {   //����ת��
            SuperGridControl superGridControl1 = sender as SuperGridControl;
            if (superGridControl1 != null)  //��if�ж�a�Ƿ�Ϊnull
            {
                //����ѡ�е�Ԫ�񱳾���ɫ
              //  CellStyles.Default.Background.Color1 = Color.Red;
                //��ȡѡ�е�Ԫ���ֵ
                SelectedElementCollection SelectedCellValue = superGridControl1.GetSelectedCells();
                // ��ȡѡ�е�Ԫ���е���Ϣ
                SelectedElementCollection SelectedRowValue = superGridControl1.GetSelectedRows() ;
                // ��ȡѡ�е�Ԫ���е���Ϣ
                SelectedElementCollection SelectedColumnValue = superGridControl1.GetSelectedColumns();
             
                    foreach (GridCell gridCell in SelectedCellValue.GetCells())
                    {
                     string   cellValue = gridCell.Value.ToString();
                    }
                
            }          
        }
        //SuperGridControl��Ԫ�����ѡ��ָ��ֵ
        public void SuperGridControl_addRow()
        {
            //GridRow Dfr = superGridControl1.PrimaryGrid.NewRow();
            //Dfr[0].Value = "1";
            //Dfr[1].Value = "2";
            //Dfr[2].Value = "3";
            //superGridControl1.PrimaryGrid.Rows.Add(Dfr);
            //Dfr = superGridControl1.PrimaryGrid.NewRow();
            //Dfr[0].Value = "2";
            //Dfr[1].Value = "3";
            //Dfr[2].Value = "4";

            //superGridControl1.PrimaryGrid.Rows.Add(Dfr);
            //Dfr = superGridControl1.PrimaryGrid.NewRow();
            //Dfr[0].Value = "3";

            //superGridControl1.PrimaryGrid.Rows.Add(Dfr);
            //Dfr = superGridControl1.PrimaryGrid.NewRow();
            //Dfr[0].Value = "4";

            //superGridControl1.PrimaryGrid.Rows.Add(Dfr);
            //Dfr = superGridControl1.PrimaryGrid.NewRow();
            //Dfr[0].Value = "5";
            //superGridControl1.PrimaryGrid.Rows.Add(Dfr);

        }
        public void SuperGridControl_addColumn()
        {
            //GridColumn gd = new GridColumn();
            //gd.Name = "��1";
            //gd.HeaderText = "��1";

            //superGridControl1.PrimaryGrid.Columns.Add(gd);
            //gd = new GridColumn();
            //gd.Name = "��2";
            //gd.HeaderText = "��2";
            //superGridControl1.PrimaryGrid.Columns.Add(gd);
            //gd = new GridColumn();
            //gd.Name = "��3";
            //gd.HeaderText = "��3";
            //superGridControl1.PrimaryGrid.Columns.Add(gd);
        }

        private void SuperGridControl_DisplayData(string inputXmlPath)
        {
                 
            XMLFileClass xmlFileClass = new XMLFileClass();
            //�ϲ��ļ�XML�ļ����������µ�XML
            string GenXmlPath = xmlFileClass.MergerXMLFile(inputXmlPath);
            //����XML�ļ�����
            if (GenXmlPath == null)
            {
                MessageBox.Show("Error generating XML file.");
                toolStripStatusLabel1.Text = "Error generating XML file";
                return;
            }
            string StartEnementName = "ConfigInfo";
            XElement xElement = xmlFileClass.GetStartElement(StartEnementName, GenXmlPath);
            //��ȡXML�ļ�����
            if (xElement == null)
            {
                MessageBox.Show("The file does not contain the word 'ConfigInfo'.");
                toolStripStatusLabel1.Text = "Error importing file . ";
                return;
            }
            List<DataSet> ListDataSet = xmlFileClass.GetFillDataSet(xElement);
            if (ListDataSet != null)
            {
                for (int i = 0; i < ListDataSet.Count; i++)
                {
                    DataSet dataSet = ListDataSet[i];
                    string dataSetName = dataSet.DataSetName;
                    dataSetName = (i + 1).ToString() + "." + dataSetName;
                    //�������ҳ����ҳ������Ϊ����
                    TabPage tabPage = new TabPage(dataSetName);
                    tabPage.Text = dataSetName;
                    //����dataset�����ݹ�ϵ
                    dataSet.Relations.Add("1", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                    dataSet.Relations.Add("2", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                    dataSet.Relations.Add("3", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                    dataSet.Relations.Add("4", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                    //���superGridControl
                    SuperGridControl superGridControl = new SuperGridControl();
                    superGridControl.PrimaryGrid.DataSource = dataSet;
                    //����superGridControl����
                    SuperGridContorl_setting(superGridControl);
                    //���ݰ�
                    // superGridControl.DataBindings
                    superGridControl.Dock = DockStyle.Fill;
                    //����ҳ�����SuperGridControl
                    tabPage.Controls.Add(superGridControl);
                    //����ѡ������������ҳ
                    tabControl1.Controls.Add(tabPage);

                    toolStripStatusLabel1.Text = "Success importing file .";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + "Error importing file . ";
            }
        }

        #endregion
        #region DataGridView ģ��
        public void DataGridView_DisplayData(string xmlpathSource)
        {
            try
            {
                //�����ʾ������
                Refresh();
                XMLFileClass xmlFileClass = new XMLFileClass();
                //�ϲ��ļ�XML�ļ����������µ�XML
                string GenXmlPath = xmlFileClass.MergerXMLFile(xmlpathSource);
                //����XML�ļ�����
                if (GenXmlPath == null)
                {
                    MessageBox.Show("Error generating XML file.");
                    toolStripStatusLabel1.Text = "Error generating XML file";
                    return;
                }
                string StartEnementName = "ConfigInfo";
                XElement xElement = xmlFileClass.GetStartElement(StartEnementName, GenXmlPath);
                //��ȡXML�ļ�����
                if (xElement == null)
                {
                    MessageBox.Show("The file does not contain the word 'ConfigInfo'.");
                    toolStripStatusLabel1.Text = "Error importing file . ";
                    return;
                }
                List<DataSet> ListDataSet = xmlFileClass.GetFillDataSet(xElement);
                if (ListDataSet != null)
                {
                    for (int i = 0; i < ListDataSet.Count; i++)
                    {
                        DataSet dataSet = ListDataSet[i];
                        string dataSetName = dataSet.DataSetName;
                        dataSetName = (i + 1).ToString() + "." + dataSetName;
                        //�������ҳ����ҳ������Ϊ����
                        TabPage tabPage = new TabPage(dataSetName);
                        tabPage.Text = dataSetName;
                        //����dataset�����ݹ�ϵ
                        dataSet.Relations.Add("1", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("2", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        dataSet.Relations.Add("3", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("4", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        //���DataGridView
                        DataGridView dataGridView = new DataGridView();
                        //�������
                        dataGridView.DataSource = dataSet;
                        //���ݰ�
                        // superGridControl.DataBindings
                        dataGridView.Dock = DockStyle.Fill;
                        //����ҳ�����dataGridView
                        tabPage.Controls.Add(dataGridView);
                        //����ѡ������������ҳ
                        tabControl1.Controls.Add(tabPage);

                    }
                    toolStripStatusLabel1.Text = "Success importing file .";
                }

            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.ToString();

            }
        }
        #endregion
        #region DataGridģ��
        public void DataGrid_DisplayData(string xmlpathSource)
        {
            try
            {
                //�����ʾ������
                
                Refresh();
                XMLFileClass xmlFileClass = new XMLFileClass();
                //�ϲ��ļ�XML�ļ����������µ�XML
                string GenXmlPath = xmlFileClass.MergerXMLFile(xmlpathSource);
                //����XML�ļ�����
                if (GenXmlPath == null)
                {
                    MessageBox.Show("Error generating XML file.");
                    toolStripStatusLabel1.Text = "Error generating XML file";
                    return;
                }
                string StartEnementName = "ConfigInfo";
                XElement xElement = xmlFileClass.GetStartElement(StartEnementName, GenXmlPath);
                //��ȡXML�ļ�����
                if (xElement == null)
                {
                    MessageBox.Show("The file does not contain the word 'ConfigInfo'.");
                    toolStripStatusLabel1.Text = "Error importing file . ";
                    return;
                }
                List<DataSet> ListDataSet = xmlFileClass.GetFillDataSet(xElement);
                if (ListDataSet != null)
                {
                    for (int i = 0; i < ListDataSet.Count; i++)
                    {
                        DataSet dataSet = ListDataSet[i];
                        string dataSetName = dataSet.DataSetName;
                        dataSetName = (i + 1).ToString() + "." + dataSetName;
                        //�������ҳ����ҳ������Ϊ����
                        TabPage tabPage = new TabPage(dataSetName);
                        tabPage.Text = dataSetName;
                        //����dataset�����ݹ�ϵ
                        dataSet.Relations.Add("1", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("2", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        dataSet.Relations.Add("3", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("4", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        //���DataGridView
                        DataGrid dataGrid = new DataGrid();
                        //�������
                        dataGrid.DataSource = dataSet;
                        //���ݰ�
                        // superGridControl.DataBindings
                        dataGrid.Dock = DockStyle.Fill;
                        //����ҳ�����dataGrid
                        tabPage.Controls.Add(dataGrid);
                        //����ѡ������������ҳ
                        tabControl1.Controls.Add(tabPage);
                    }
                    toolStripStatusLabel1.Text = "Success importing file .";
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.ToString();
            }
        }
        #endregion
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new ThreadStart(() =>
            {
                {
                    string xmlpathSource;
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    //ע������д·��ʱҪ��c:\\������c:\
                    openFileDialog.InitialDirectory = "E:\\";
                    // openFileDialog.Filter = "XML�ļ�|*.xml|C#�ļ�|*.cs|�����ļ�|*.*";
                    openFileDialog.Filter = "XML�ļ�|*.xml";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 1;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        xmlpathSource = openFileDialog.FileName;
                        //1.����չʾSuperGridControl---����չʾ���ӵĹ�ϵ��������Ҫ�ؼ�
                        SuperGridControl_DisplayData(xmlpathSource);
                        //2.����չʾDataGridView---�޷�չʾ���ӵĹ�ϵ
                        // DisplayData_DataGridView(xmlpathSource);
                        //3.����չʾDataGrid---����չʾ���ӵĹ�ϵ�����������Ҫ�Ż�
                        //DisplayData_DataGrid(xmlpathSource);
                    }
                }
            }
             ));
        }
        #endregion

        #region TelNetģ��->����
        private TelnetClass myTelnet;
        private string ipStr;
        private string portStr;


        #endregion
        #region TelNetģ��->����
        private void toolStripButtonLogin_Click(object sender, EventArgs e)
        {
            (new Thread(() =>
            {
                this.BeginInvoke(new ThreadStart(() =>
                {
                    //��ȡIP,Port
                    toolStripTextBoxIP_TextChanged(sender, e);
                    toolStripTextBoxPort_TextChanged(sender, e);
                }));
                //��֤IP,Port�Ƿ���ȷ
                if (string.IsNullOrEmpty(toolStripTextBoxIP.Text) || string.IsNullOrEmpty(toolStripTextBoxPort.Text))
                {
                    MessageBox.Show("Please enter the full IP and port number !");

                    this.BeginInvoke(new ThreadStart(() =>
                    {
                        this.SetTelnetAvailableBoolFalse();
                    }));

                    return;
                }
                //����login
                if (this.toolStripButtonLogin.Text == "Login")
                {
                    //ping����
                    if (!TryPing(this.ipStr, 10))
                    {
                        MessageBox.Show("Ping " + this.ipStr + " Timeout!");

                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolFalse();
                        }));
                        return;
                    }
                    //������
                    myTelnet = new TelnetClass(ipStr, int.Parse(portStr));
                    //����TCP����
                    if (!myTelnet.Connect())
                    {//���Ӳ��ɹ�
                        MessageBox.Show("Telnet Connect Fail !");
                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolFalse();
                        }));
                        return;
                    }
                    //��½�ɹ�
                    if (!myTelnet.login())
                    {   //�ر��׽���
                        myTelnet.close();
                        MessageBox.Show("Telnet Login Fail !");
                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolFalse();
                        }));
                        return;
                    }
                    //�ɹ���½��������ر�ʶλ
                    this.BeginInvoke(new ThreadStart(() =>
                    {
                        this.SetTelnetAvailableBoolTrue();
                        //�������������
                        richTextBox_echo.Clear();
                        //��һ����ͷ
                        richTextBox_echo.AppendText("->");

                    }));
                }
                //����logout
                else
                {
                    if (myTelnet != null && !myTelnet.logout())
                    {//�ͷŲ���Դ�ɹ�
                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolTrue();
                        }));
                        return;
                    }

                    this.BeginInvoke(new ThreadStart(() =>
                    {//�ͷ���Դ�ɹ�
                        this.SetTelnetAvailableBoolFalse();
                    }));
                }


            })).Start();
        }
        private bool TryPing(string ipstr, int timeout)
        {
            Ping pi = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            PingReply reply = pi.Send(ipstr, 1000 * timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void AppendMessage(string str)
        {
            this.BeginInvoke(new ThreadStart(() =>
            {
                this.richTextBox_echo.AppendText(str + "\n");
                this.richTextBox_echo.SelectionStart = this.richTextBox_echo.TextLength;
                this.richTextBox_echo.ScrollToCaret();
            }));
        }
        private void AppendMessage_green(string str)
        {
            this.BeginInvoke(new ThreadStart(() =>
            {
                this.richTextBox_echo.AppendText(str + "\n");
                this.richTextBox_echo.Select(this.richTextBox_echo.Text.Length - str.Length - 1, str.Length);
                this.richTextBox_echo.SelectionColor = Color.Green;
                this.richTextBox_echo.ScrollToCaret();
            }));
        }
        private void AppendMessage_red(string str)
        {
            this.BeginInvoke(new ThreadStart(() =>
            {
                this.richTextBox_echo.AppendText(str + "\n");
                this.richTextBox_echo.Select(this.richTextBox_echo.Text.Length - str.Length - 1, str.Length);
                this.richTextBox_echo.SelectionColor = Color.Red;
                this.richTextBox_echo.ScrollToCaret();
            }));
        }
        public static string GetReturnValueFormEcho(string str)
        {
            string[] strs = str.Split(new string[] { "," + Environment.NewLine + "value =" }, StringSplitOptions.None)[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return strs[strs.Length - 1].Trim();
        }
        private void SetTelnetAvailableBoolFalse()
        {

            this.richTextBox_echo.Enabled = false;
            this.toolStripButtonLogin.Text = "Login";
            this.toolStripButtonLogin.BackColor = Color.Red;
        }
        private void SetTelnetAvailableBoolTrue()
        {

            this.richTextBox_echo.Enabled = true;
            this.toolStripButtonLogin.Text = "Logout";
            this.toolStripButtonLogin.BackColor = Color.LimeGreen;
        }

        private void toolStripTextBoxIP_TextChanged(object sender, EventArgs e)
        {
            this.ipStr = this.toolStripTextBoxIP.Text.Trim();
        }
        private void toolStripTextBoxPort_TextChanged(object sender, EventArgs e)
        {
            this.portStr = this.toolStripTextBoxPort.Text.Trim();
        }
        public void richTextBox_echo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //���������߳� 
                (new Thread(() =>
                {
                    this.BeginInvoke(new ThreadStart(() =>
                    {
                        string[] sLines = richTextBox_echo.Lines;
                        if (sLines.Count() > 10) richTextBox_echo.Clear();
                        myTelnet.telnet_write_line_add(sLines[sLines.Count() - 2].Remove(0, 2));
                        this.toolStripStatusLabel1.Text = (sLines[sLines.Count() - 2].Remove(0, 2));
                    }
                     ));
                })).Start();

                //������ʾ�߳�-->��ʾ�������ı�
                (new Thread(() =>
                {
                    this.BeginInvoke(new ThreadStart(() =>
                     {
                         if (myTelnet != null)
                         {
                             richTextBox_echo.AppendText(myTelnet.buf_get_all());
                         }
                         // else richTextBox_echo.AppendText("->\r\n");

                     }
                     ));
                }
                )).Start();
            }

        }




        #endregion

    }

}
