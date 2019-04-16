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
        #region 窗体初始化
        public frmMain()
        {
            InitializeComponent();
            SetTelnetAvailableBoolFalse();
            //Added to support default instance behavour in C#
            if (defaultInstance == null)
                defaultInstance = this;

        }
        //定义数据表
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

        #region XML模块->变量
        #endregion
        #region XML模块->函数
        public void ClearFields()
        {
            panelView.Controls.Clear();
            Refresh();
        }
        //没有显示数据
        #region SuperGridControl模块
        //单元格文本变更事件
        private void superGridControl1_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {

            SuperGridControl_getRow( sender, e);
        }
        //显示表
        //数据绑定完成事件
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {

            GridPanel panel = e.GridPanel;
            //显示行号
            panel.ShowRowGridIndex = true;
            if (panel.DataMember == "qtlsdmx")
            {
                double count = 0;
                foreach (GridElement item in panel.Rows)
                {
                    GridRow row = item as GridRow;
                    count += Convert.ToDouble(row["je"].Value);
                }

                panel.Footer.Text = string.Format("<font size='9' famaly='宋体'>总金额:<font color='Green'>{0}</font></font>", count);
            }
        }

        //设置样式
        private void SuperGridContorl_setting(SuperGridControl superGridControl1)
        {
            //不显示Filter           
            //superGridControl1.PrimaryGrid.Filter.Visible = false;
            // 控制表格只能选中单行
            superGridControl1.PrimaryGrid.MultiSelect = false;
            superGridControl1.PrimaryGrid.InitialSelection = RelativeSelection.Row;
            //只能选中一个单元格，而不是一行单元格
            superGridControl1.PrimaryGrid.SelectionGranularity = SelectionGranularity.Row;
            //是否显示序列号
           // superGridControl1.PrimaryGrid.ShowRowHeaders = false;
            //自动填满表格
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.Fill;
            //允许单元格拖动成为集合组
           // superGridControl1.PrimaryGrid.ColumnHeader.AllowSelection = true;
            //superGridControl1.PrimaryGrid.GroupByRow.Visible = false;

            GridPanel panel = superGridControl1.PrimaryGrid;
            //设置表格自动展开
            //panel.AutoExpandSetGroup = false;
            //设置表格中文字的位置居中     
            superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;

            //显示行号
            //superGridControl1.PrimaryGrid.ShowRowGridIndex = true ;
            //设置行号的起始值（默认值为0）
            //superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            //允许调整行头的宽度
            superGridControl1.PrimaryGrid.AllowRowHeaderResize = true;
            //允许显示行头
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            //注册单元格变更事件
            superGridControl1.CellValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValueChangedEventArgs>(this.superGridControl1_CellValueChanged);
            //让列头显示筛选图标
            // superGridControl1.PrimaryGrid.EnableFiltering = false;
            //superGridControl1.PrimaryGrid.EnableColumnFiltering = false;
            //显示Filter
            //superGridControl1.PrimaryGrid.Filter.Visible = false;
            //允许按列分组
            //this.superGridControl1.PrimaryGrid.GroupByRow.Visible = false;

            //在列头出显示图标
            // .gridColumn1.HeaderStyles.Default.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            //设置展开和收缩图标
            //this.superGridControl1.PrimaryGrid.CollapseImage = global::HRMS.Properties.Resources.BugRight;
            //this.superGridControl1.PrimaryGrid.ExpandImage = global::HRMS.Properties.Resources.BugUp;
        }
        public void  SuperGridControl_getRow(object sender, GridCellValueChangedEventArgs e)
        {   //类型转换
            SuperGridControl superGridControl1 = sender as SuperGridControl;
            if (superGridControl1 != null)  //在if判断a是否为null
            {
                //设置选中单元格背景颜色
              //  CellStyles.Default.Background.Color1 = Color.Red;
                //获取选中单元格的值
                SelectedElementCollection SelectedCellValue = superGridControl1.GetSelectedCells();
                // 获取选中单元格行的信息
                SelectedElementCollection SelectedRowValue = superGridControl1.GetSelectedRows() ;
                // 获取选中单元格列的信息
                SelectedElementCollection SelectedColumnValue = superGridControl1.GetSelectedColumns();
             
                    foreach (GridCell gridCell in SelectedCellValue.GetCells())
                    {
                     string   cellValue = gridCell.Value.ToString();
                    }
                
            }          
        }
        //SuperGridControl单元格可以选择指定值
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
            //gd.Name = "第1";
            //gd.HeaderText = "第1";

            //superGridControl1.PrimaryGrid.Columns.Add(gd);
            //gd = new GridColumn();
            //gd.Name = "第2";
            //gd.HeaderText = "第2";
            //superGridControl1.PrimaryGrid.Columns.Add(gd);
            //gd = new GridColumn();
            //gd.Name = "第3";
            //gd.HeaderText = "第3";
            //superGridControl1.PrimaryGrid.Columns.Add(gd);
        }

        private void SuperGridControl_DisplayData(string inputXmlPath)
        {
                 
            XMLFileClass xmlFileClass = new XMLFileClass();
            //合并文件XML文件，并生成新的XML
            string GenXmlPath = xmlFileClass.MergerXMLFile(inputXmlPath);
            //生成XML文件出错
            if (GenXmlPath == null)
            {
                MessageBox.Show("Error generating XML file.");
                toolStripStatusLabel1.Text = "Error generating XML file";
                return;
            }
            string StartEnementName = "ConfigInfo";
            XElement xElement = xmlFileClass.GetStartElement(StartEnementName, GenXmlPath);
            //读取XML文件出错
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
                    //添加数据页，将页名设置为表名
                    TabPage tabPage = new TabPage(dataSetName);
                    tabPage.Text = dataSetName;
                    //设置dataset中数据关系
                    dataSet.Relations.Add("1", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                    dataSet.Relations.Add("2", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                    dataSet.Relations.Add("3", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                    dataSet.Relations.Add("4", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                    //添加superGridControl
                    SuperGridControl superGridControl = new SuperGridControl();
                    superGridControl.PrimaryGrid.DataSource = dataSet;
                    //设置superGridControl属性
                    SuperGridContorl_setting(superGridControl);
                    //数据绑定
                    // superGridControl.DataBindings
                    superGridControl.Dock = DockStyle.Fill;
                    //数据页中添加SuperGridControl
                    tabPage.Controls.Add(superGridControl);
                    //数据选项表中添加数据页
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
        #region DataGridView 模块
        public void DataGridView_DisplayData(string xmlpathSource)
        {
            try
            {
                //清楚显示的数据
                Refresh();
                XMLFileClass xmlFileClass = new XMLFileClass();
                //合并文件XML文件，并生成新的XML
                string GenXmlPath = xmlFileClass.MergerXMLFile(xmlpathSource);
                //生成XML文件出错
                if (GenXmlPath == null)
                {
                    MessageBox.Show("Error generating XML file.");
                    toolStripStatusLabel1.Text = "Error generating XML file";
                    return;
                }
                string StartEnementName = "ConfigInfo";
                XElement xElement = xmlFileClass.GetStartElement(StartEnementName, GenXmlPath);
                //读取XML文件出错
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
                        //添加数据页，将页名设置为表名
                        TabPage tabPage = new TabPage(dataSetName);
                        tabPage.Text = dataSetName;
                        //设置dataset中数据关系
                        dataSet.Relations.Add("1", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("2", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        dataSet.Relations.Add("3", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("4", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        //添加DataGridView
                        DataGridView dataGridView = new DataGridView();
                        //数据添加
                        dataGridView.DataSource = dataSet;
                        //数据绑定
                        // superGridControl.DataBindings
                        dataGridView.Dock = DockStyle.Fill;
                        //数据页中添加dataGridView
                        tabPage.Controls.Add(dataGridView);
                        //数据选项表中添加数据页
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
        #region DataGrid模块
        public void DataGrid_DisplayData(string xmlpathSource)
        {
            try
            {
                //清楚显示的数据
                
                Refresh();
                XMLFileClass xmlFileClass = new XMLFileClass();
                //合并文件XML文件，并生成新的XML
                string GenXmlPath = xmlFileClass.MergerXMLFile(xmlpathSource);
                //生成XML文件出错
                if (GenXmlPath == null)
                {
                    MessageBox.Show("Error generating XML file.");
                    toolStripStatusLabel1.Text = "Error generating XML file";
                    return;
                }
                string StartEnementName = "ConfigInfo";
                XElement xElement = xmlFileClass.GetStartElement(StartEnementName, GenXmlPath);
                //读取XML文件出错
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
                        //添加数据页，将页名设置为表名
                        TabPage tabPage = new TabPage(dataSetName);
                        tabPage.Text = dataSetName;
                        //设置dataset中数据关系
                        dataSet.Relations.Add("1", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("2", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        dataSet.Relations.Add("3", dataSet.Tables["Path"].Columns["CID"], dataSet.Tables["Path"].Columns["PID"], false);
                        dataSet.Relations.Add("4", dataSet.Tables["Block"].Columns["CID"], dataSet.Tables["Parameter"].Columns["PID"], false);
                        //添加DataGridView
                        DataGrid dataGrid = new DataGrid();
                        //数据添加
                        dataGrid.DataSource = dataSet;
                        //数据绑定
                        // superGridControl.DataBindings
                        dataGrid.Dock = DockStyle.Fill;
                        //数据页中添加dataGrid
                        tabPage.Controls.Add(dataGrid);
                        //数据选项表中添加数据页
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
                    //注意这里写路径时要用c:\\而不是c:\
                    openFileDialog.InitialDirectory = "E:\\";
                    // openFileDialog.Filter = "XML文件|*.xml|C#文件|*.cs|所有文件|*.*";
                    openFileDialog.Filter = "XML文件|*.xml";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 1;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        xmlpathSource = openFileDialog.FileName;
                        //1.数据展示SuperGridControl---可以展示复杂的关系，但是需要控件
                        SuperGridControl_DisplayData(xmlpathSource);
                        //2.数据展示DataGridView---无法展示复杂的关系
                        // DisplayData_DataGridView(xmlpathSource);
                        //3.数据展示DataGrid---可以展示复杂的关系，但是外观需要优化
                        //DisplayData_DataGrid(xmlpathSource);
                    }
                }
            }
             ));
        }
        #endregion

        #region TelNet模块->变量
        private TelnetClass myTelnet;
        private string ipStr;
        private string portStr;


        #endregion
        #region TelNet模块->函数
        private void toolStripButtonLogin_Click(object sender, EventArgs e)
        {
            (new Thread(() =>
            {
                this.BeginInvoke(new ThreadStart(() =>
                {
                    //获取IP,Port
                    toolStripTextBoxIP_TextChanged(sender, e);
                    toolStripTextBoxPort_TextChanged(sender, e);
                }));
                //验证IP,Port是否正确
                if (string.IsNullOrEmpty(toolStripTextBoxIP.Text) || string.IsNullOrEmpty(toolStripTextBoxPort.Text))
                {
                    MessageBox.Show("Please enter the full IP and port number !");

                    this.BeginInvoke(new ThreadStart(() =>
                    {
                        this.SetTelnetAvailableBoolFalse();
                    }));

                    return;
                }
                //处理login
                if (this.toolStripButtonLogin.Text == "Login")
                {
                    //ping主机
                    if (!TryPing(this.ipStr, 10))
                    {
                        MessageBox.Show("Ping " + this.ipStr + " Timeout!");

                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolFalse();
                        }));
                        return;
                    }
                    //建议类
                    myTelnet = new TelnetClass(ipStr, int.Parse(portStr));
                    //建立TCP连接
                    if (!myTelnet.Connect())
                    {//连接不成功
                        MessageBox.Show("Telnet Connect Fail !");
                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolFalse();
                        }));
                        return;
                    }
                    //登陆成功
                    if (!myTelnet.login())
                    {   //关闭套接字
                        myTelnet.close();
                        MessageBox.Show("Telnet Login Fail !");
                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolFalse();
                        }));
                        return;
                    }
                    //成功登陆，设置相关标识位
                    this.BeginInvoke(new ThreadStart(() =>
                    {
                        this.SetTelnetAvailableBoolTrue();
                        //清楚交互命令行
                        richTextBox_echo.Clear();
                        //放一个箭头
                        richTextBox_echo.AppendText("->");

                    }));
                }
                //处理logout
                else
                {
                    if (myTelnet != null && !myTelnet.logout())
                    {//释放不资源成功
                        this.BeginInvoke(new ThreadStart(() =>
                        {
                            this.SetTelnetAvailableBoolTrue();
                        }));
                        return;
                    }

                    this.BeginInvoke(new ThreadStart(() =>
                    {//释放资源成功
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
                //启动输入线程 
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

                //启动显示线程-->显示新增加文本
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
