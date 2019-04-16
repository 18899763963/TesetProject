using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIModels
{
    class PropertyGridObj
    {
        //为DockPanelLeft窗体添加PropertyGrid控件
        //PropertyGridObj propertyGridObj = new PropertyGridObj();
        //dockPanelSideRight.Controls.Add(propertyGridObj);
        //InitPropertyGrid();
        PropertyGrid propertyGridObj = null;
        public void InitPropertyGrid(Control ParentControl)
        {
            propertyGridObj = new PropertyGrid();
            propertyGridObj.Location = new Point(0, 0);
            propertyGridObj.Anchor = AnchorStyles.Left;
            propertyGridObj.Dock = DockStyle.Fill;
            ParentControl.Controls.Add(propertyGridObj);
        }
        //设置PropertyGrid控件的值，自定义属性
        public void SelectPropertyObject()
        {
            UserProperty propertyUser = new UserProperty();
            propertyUser.AppName = "TesApp";
            propertyGridObj.SelectedObject = propertyUser;
        }
        class UserProperty
        {
            private string _AppName;
            private string _AppPath;
            private Point _Location;// = new Point(0, 0);
            private Size _Size = new Size(0, 0);
            private Color _BackColor;
            private Color _ForeColor;
            private Font _Font;
            private String _Text;

            [CategoryAttribute("常规"), DescriptionAttribute("应用程序名称"), ReadOnlyAttribute(true)]
            public string AppName
            {
                get
                {
                    return _AppName;
                }
                set
                {
                    _AppName = value;
                }
            }
            [CategoryAttribute("常规"), DescriptionAttribute("应用程序路径"), ReadOnlyAttribute(true)]
            public string AppPath
            {
                get
                {
                    return _AppPath;
                }
                set
                {
                    _AppPath = value;
                }
            }

            [CategoryAttribute("布局"), DescriptionAttribute("位置"), ReadOnlyAttribute(false)]
            public Point Location
            {
                get
                {
                    return _Location;
                }
                set
                {
                    _Location = value;
                }
            }
            [CategoryAttribute("布局"), DescriptionAttribute("尺寸"), ReadOnlyAttribute(false)]
            public Size Size
            {
                get
                {
                    return _Size;
                }
                set
                {
                    _Size = value;
                }
            }

            [CategoryAttribute("外观"), DescriptionAttribute("背景色"), ReadOnlyAttribute(false)]
            public Color BackColor
            {
                get
                {
                    return _BackColor;
                }
                set
                {
                    _BackColor = value;
                }
            }
            [CategoryAttribute("外观"), DescriptionAttribute("前景色"), ReadOnlyAttribute(false)]
            public Color ForeColor
            {
                get
                {
                    return _ForeColor;
                }
                set
                {
                    _ForeColor = value;
                }
            }
            [CategoryAttribute("外观"), DescriptionAttribute("文本")]
            public String Text
            {
                get
                {
                    return _Text;
                }
                set
                {
                    _Text = value;
                }
            }
            [CategoryAttribute("外观"), DescriptionAttribute("字体")]
            public Font Font
            {
                get
                {
                    return _Font;
                }
                set
                {
                    _Font = value;
                }
            }
        }
    }
}
