using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SmallManagerSpace.Resources
{

    /// <summary>
    /// 存放界面可编辑的项和函数实现项
    /// </summary>
    public class ShowEntity
    { 
        public string retType { get; set; }
        public string funName { get; set; }
        public string argType { get; set; }
        public string argName { get; set; }

    }
}
