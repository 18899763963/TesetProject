using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallManagerSpace.Resources.GUIVsEntity
{
    class TabalControlObj
    {

        //struct TabPageProperty
        //{
        //    public int index;
        //    public string languageType;
        //    public bool isFull;
        //};

        //void TabControl2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int PageSelectedIndex = tabControl2.SelectedIndex;

        //    if ((tabPageProperties[PageSelectedIndex]).isFull == false)
        //    {
        //        //得到选中页的内容
        //        string TabPageNameBlock = GetTabPageName(PageSelectedIndex);
        //        AdvTree CurrentAdvTree = GetAdvTree(PageSelectedIndex);
        //        AdvTreeSetting(CurrentAdvTree, TabPageNameBlock, PageSelectedIndex);
        //        //填充选中页的内容
        //        FullDataToAdvTreeFromXMLNode(CurrentAdvTree, xElementStartPublic, TabPageNameBlock, PageSelectedIndex);
        //        SetTabPageProperty(PageSelectedIndex, "cn", true);
        //    }
        //}
        //private void SetTabPageProperty(int indexStartZero, string languageType, bool isFull)
        //{            //添加空的TabPage到TableControl2
        //    //TabPageProperty tab = tabPageProperties[indexStartZero];
        //    //tab.index = indexStartZero;
        //    //tab.languageType = languageType;
        //    //tab.isFull = isFull;
        //    //tabPageProperties[indexStartZero] = tab;
        //}
        //private void CreatePageAndPageProperty()
        //{
        //    int PageIndex = 1;
        //    tabControl2.TabPages.Clear();
        //    tabPageProperties.Clear();
        //    if (ListBlockName != null)
        //    {
        //        foreach (string BlockName in ListBlockName)
        //        {//页属性
        //            TabPageProperty tabPageProperty = new TabPageProperty();
        //            tabPageProperty.index = PageIndex - 1;
        //            tabPageProperty.languageType = "cn";
        //            tabPageProperty.isFull = false;
        //            tabPageProperties.Add(tabPageProperty);
        //            //页控件
        //            TabPage tabPage = new TabPage(PageIndex.ToString() + "." + BlockName);
        //            AdvTree advTree = new AdvTree();
        //            tabPage.Controls.Add(advTree);
        //            tabPage.Dock = DockStyle.Fill;
        //            advTree.Dock = DockStyle.Fill;
        //            tabControl2.Controls.Add(tabPage);
        //            PageIndex++;
        //        }
        //        tabControl2.Refresh();
        //    }
        //    tabControl2.SelectedIndexChanged += new System.EventHandler(this.TabControl2_SelectedIndexChanged);
        //}
        ///// <summary>
        ///// 得到下表对应的页名
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //private string GetTabPageName(int index)
        //{
        //    //得到指定的advTree,BlockName,XElement
        //    string TabPageNameWithPerfix = tabControl2.TabPages[index].Text;
        //    string[] TabPageNameArray = TabPageNameWithPerfix.Split('.');
        //    return TabPageNameArray[1];
        //}
    }
}
