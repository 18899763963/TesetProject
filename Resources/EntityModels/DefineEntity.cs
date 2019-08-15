using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SmallManagerSpace.Resources
{

    /// <summary>
    /// 存放用户自定义数据
    /// </summary>
    public class DefineEntity
    {
        public string type { get; set; }
        public string name { get; set; }
    }
}
