using System.Windows.Forms;



namespace MasterDetailSample
{
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]public 
	partial class frmMain : System.Windows.Forms.Form
	{
		
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnglishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.主题配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.浅色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabItem4 = new DevComponents.DotNetBar.TabItem(this.components);
            this.OpenFileToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.SaveFileToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.CopyToolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.PasteToolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.DeleteToolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutToolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.默认ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.浅色ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.menuStrip1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Panel1.Size = new System.Drawing.Size(672, 27);
            this.Panel1.TabIndex = 20;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(672, 25);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileToolStripMenuItem,
            this.SaveFileToolStripMenuItem,
            this.toolStripSeparator4,
            this.ExitMainToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.FileToolStripMenuItem.Text = "文件(&F)";
            this.FileToolStripMenuItem.Click += new System.EventHandler(this.FileToolStripMenuItem_Click);
            // 
            // OpenFileToolStripMenuItem
            // 
            this.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem";
            this.OpenFileToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.OpenFileToolStripMenuItem.Text = "打开文件(&O)           Ctrl+O";
            this.OpenFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItem_Click);
            // 
            // SaveFileToolStripMenuItem
            // 
            this.SaveFileToolStripMenuItem.Name = "SaveFileToolStripMenuItem";
            this.SaveFileToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.SaveFileToolStripMenuItem.Text = "存储文件(&S)            Ctrl+S";
            this.SaveFileToolStripMenuItem.Click += new System.EventHandler(this.SaveFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(222, 6);
            // 
            // ExitMainToolStripMenuItem
            // 
            this.ExitMainToolStripMenuItem.Name = "ExitMainToolStripMenuItem";
            this.ExitMainToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.ExitMainToolStripMenuItem.Text = "退出(&X)                  Alt+F4";
            this.ExitMainToolStripMenuItem.Click += new System.EventHandler(this.ExitMainToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CutToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.EditToolStripMenuItem.Enabled = false;
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.EditToolStripMenuItem.Text = "编辑(&E)";
            // 
            // CutToolStripMenuItem
            // 
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.CutToolStripMenuItem.Text = "剪切";
            this.CutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.CopyToolStripMenuItem.Text = "复制";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.PasteToolStripMenuItem.Text = "粘贴";
            this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.DeleteToolStripMenuItem.Text = "删除    ";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LanguageToolStripMenuItem,
            this.主题配置ToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.ViewToolStripMenuItem.Text = "设置(&S)";
            // 
            // LanguageToolStripMenuItem
            // 
            this.LanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChineseToolStripMenuItem,
            this.EnglishToolStripMenuItem,
            this.DataToolStripMenuItem});
            this.LanguageToolStripMenuItem.Enabled = false;
            this.LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem";
            this.LanguageToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.LanguageToolStripMenuItem.Text = "语言配置";
            this.LanguageToolStripMenuItem.Visible = false;
            // 
            // ChineseToolStripMenuItem
            // 
            this.ChineseToolStripMenuItem.Checked = true;
            this.ChineseToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChineseToolStripMenuItem.Name = "ChineseToolStripMenuItem";
            this.ChineseToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ChineseToolStripMenuItem.Text = "中文";
            this.ChineseToolStripMenuItem.Click += new System.EventHandler(this.ChineseToolStripMenuItem_Click);
            // 
            // EnglishToolStripMenuItem
            // 
            this.EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem";
            this.EnglishToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.EnglishToolStripMenuItem.Text = "英文";
            this.EnglishToolStripMenuItem.Click += new System.EventHandler(this.EnglishToolStripMenuItem_Click);
            // 
            // DataToolStripMenuItem
            // 
            this.DataToolStripMenuItem.Name = "DataToolStripMenuItem";
            this.DataToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.DataToolStripMenuItem.Text = "数据";
            this.DataToolStripMenuItem.Click += new System.EventHandler(this.DataToolStripMenuItem_Click);
            // 
            // 主题配置ToolStripMenuItem
            // 
            this.主题配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.默认ToolStripMenuItem,
            this.浅色ToolStripMenuItem});
            this.主题配置ToolStripMenuItem.Name = "主题配置ToolStripMenuItem";
            this.主题配置ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.主题配置ToolStripMenuItem.Text = "主题配置(&C)";
            // 
            // 默认ToolStripMenuItem
            // 
            this.默认ToolStripMenuItem.Checked = true;
            this.默认ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.默认ToolStripMenuItem.Name = "默认ToolStripMenuItem";
            this.默认ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.默认ToolStripMenuItem.Text = "默认";
            this.默认ToolStripMenuItem.Click += new System.EventHandler(this.默认ToolStripMenuItem_Click);
            // 
            // 浅色ToolStripMenuItem
            // 
            this.浅色ToolStripMenuItem.Name = "浅色ToolStripMenuItem";
            this.浅色ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.浅色ToolStripMenuItem.Text = "浅色";
            this.浅色ToolStripMenuItem.Click += new System.EventHandler(this.浅色ToolStripMenuItem_Click);
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.ToolsToolStripMenuItem.Text = "工具(&T)";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.HelpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.AboutToolStripMenuItem.Text = "关于(&A)";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 511);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(672, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(657, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabItem2
            // 
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "2.命令行";
            // 
            // tabItem1
            // 
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "1.数据窗";
            // 
            // tabItem3
            // 
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "数据窗";
            // 
            // tabItem4
            // 
            this.tabItem4.Name = "tabItem4";
            this.tabItem4.Text = "命令行";
            // 
            // OpenFileToolStripButton1
            // 
            this.OpenFileToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFileToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileToolStripButton1.Image")));
            this.OpenFileToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileToolStripButton1.Name = "OpenFileToolStripButton1";
            this.OpenFileToolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.OpenFileToolStripButton1.Text = "OpenFileToolStripButton1";
            this.OpenFileToolStripButton1.ToolTipText = "打开文件";
            this.OpenFileToolStripButton1.Click += new System.EventHandler(this.OpenFileToolStripButton1_Click);
            // 
            // SaveFileToolStripButton1
            // 
            this.SaveFileToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveFileToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("SaveFileToolStripButton1.Image")));
            this.SaveFileToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveFileToolStripButton1.Name = "SaveFileToolStripButton1";
            this.SaveFileToolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.SaveFileToolStripButton1.Tag = "打开";
            this.SaveFileToolStripButton1.Text = "SaveFileToolStripButton1";
            this.SaveFileToolStripButton1.ToolTipText = "存储文件";
            this.SaveFileToolStripButton1.Click += new System.EventHandler(this.SaveFileToolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // CutToolStripButton1
            // 
            this.CutToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CutToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("CutToolStripButton1.Image")));
            this.CutToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutToolStripButton1.Name = "CutToolStripButton1";
            this.CutToolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.CutToolStripButton1.Text = "toolStripButton1";
            this.CutToolStripButton1.ToolTipText = "剪切";
            this.CutToolStripButton1.Click += new System.EventHandler(this.CutToolStripButton1_Click);
            // 
            // CopyToolStripButton2
            // 
            this.CopyToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyToolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("CopyToolStripButton2.Image")));
            this.CopyToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripButton2.Name = "CopyToolStripButton2";
            this.CopyToolStripButton2.Size = new System.Drawing.Size(24, 24);
            this.CopyToolStripButton2.Text = "toolStripButton2";
            this.CopyToolStripButton2.ToolTipText = "复制";
            this.CopyToolStripButton2.Click += new System.EventHandler(this.CopyToolStripButton2_Click);
            // 
            // PasteToolStripButton3
            // 
            this.PasteToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteToolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("PasteToolStripButton3.Image")));
            this.PasteToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripButton3.Name = "PasteToolStripButton3";
            this.PasteToolStripButton3.Size = new System.Drawing.Size(24, 24);
            this.PasteToolStripButton3.Text = "toolStripButton3";
            this.PasteToolStripButton3.ToolTipText = "粘贴";
            this.PasteToolStripButton3.Click += new System.EventHandler(this.PasteToolStripButton3_Click);
            // 
            // DeleteToolStripButton4
            // 
            this.DeleteToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteToolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("DeleteToolStripButton4.Image")));
            this.DeleteToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteToolStripButton4.Name = "DeleteToolStripButton4";
            this.DeleteToolStripButton4.Size = new System.Drawing.Size(24, 24);
            this.DeleteToolStripButton4.Text = "toolStripButton4";
            this.DeleteToolStripButton4.ToolTipText = "删除";
            this.DeleteToolStripButton4.Click += new System.EventHandler(this.DeleteToolStripButton4_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 23);
            // 
            // AboutToolStripButton7
            // 
            this.AboutToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AboutToolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("AboutToolStripButton7.Image")));
            this.AboutToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutToolStripButton7.Name = "AboutToolStripButton7";
            this.AboutToolStripButton7.Size = new System.Drawing.Size(24, 24);
            this.AboutToolStripButton7.Text = "toolStripButton7";
            this.AboutToolStripButton7.ToolTipText = "关于此软件";
            this.AboutToolStripButton7.Click += new System.EventHandler(this.AboutToolStripButton7_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileToolStripButton1,
            this.SaveFileToolStripButton1,
            this.toolStripSeparator2,
            this.CutToolStripButton1,
            this.CopyToolStripButton2,
            this.PasteToolStripButton3,
            this.DeleteToolStripButton4,
            this.toolStripSeparator6,
            this.toolStripSplitButton1,
            this.AboutToolStripButton7});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 27);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(672, 27);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.默认ToolStripMenuItem1,
            this.浅色ToolStripMenuItem1});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(36, 24);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            this.toolStripSplitButton1.ToolTipText = "主题";
            // 
            // 默认ToolStripMenuItem1
            // 
            this.默认ToolStripMenuItem1.Checked = true;
            this.默认ToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.默认ToolStripMenuItem1.Name = "默认ToolStripMenuItem1";
            this.默认ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.默认ToolStripMenuItem1.Text = "默认";
            this.默认ToolStripMenuItem1.Click += new System.EventHandler(this.默认ToolStripMenuItem1_Click);
            // 
            // 浅色ToolStripMenuItem1
            // 
            this.浅色ToolStripMenuItem1.Name = "浅色ToolStripMenuItem1";
            this.浅色ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.浅色ToolStripMenuItem1.Text = "浅色";
            this.浅色ToolStripMenuItem1.Click += new System.EventHandler(this.浅色ToolStripMenuItem1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(672, 457);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabControl1.TabIndex = 27;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Text = "tabControl1";
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(672, 533);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenCode Tools";
            this.TransparencyKey = System.Drawing.Color.Tan;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        private StatusStrip statusStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1;
        private System.ComponentModel.IContainer components;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem EditToolStripMenuItem;
        private ToolStripMenuItem ToolsToolStripMenuItem;
        private ToolStripMenuItem HelpToolStripMenuItem;
        private ToolStripMenuItem OpenFileToolStripMenuItem;
        private ToolStripMenuItem SaveFileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem ExitMainToolStripMenuItem;
        private ToolStripMenuItem CutToolStripMenuItem;
        private ToolStripMenuItem CopyToolStripMenuItem;
        private ToolStripMenuItem PasteToolStripMenuItem;
        private ToolStripMenuItem DeleteToolStripMenuItem;
        private ToolStripMenuItem AboutToolStripMenuItem;
        private ToolTip toolTip1;
        private ToolStripMenuItem ViewToolStripMenuItem;
        internal Panel Panel1;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private DevComponents.DotNetBar.TabItem tabItem4;
        private ToolStripButton OpenFileToolStripButton1;
        private ToolStripButton SaveFileToolStripButton1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton CutToolStripButton1;
        private ToolStripButton CopyToolStripButton2;
        private ToolStripButton PasteToolStripButton3;
        private ToolStripButton DeleteToolStripButton4;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton AboutToolStripButton7;
        private ToolStrip toolStrip1;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private Timer timer1;
        private System.Diagnostics.EventLog eventLog1;
        private ToolStripMenuItem LanguageToolStripMenuItem;
        private ToolStripMenuItem ChineseToolStripMenuItem;
        private ToolStripMenuItem EnglishToolStripMenuItem;
        private ToolStripMenuItem DataToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem 主题配置ToolStripMenuItem;
        private ToolStripMenuItem 默认ToolStripMenuItem;
        private ToolStripMenuItem 浅色ToolStripMenuItem;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem 默认ToolStripMenuItem1;
        private ToolStripMenuItem 浅色ToolStripMenuItem1;
    }
	
}
