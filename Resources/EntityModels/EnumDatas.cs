using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmallManagerSpace.Resources
{
    public class Enumration
    {
        public string en { get; set; }
        public string cn { get; set; }
        public string value { get; set; }
        public override string ToString()
        {
            return this.value;
        }
    }
    public class EnumData
    {
        public string name { get; set; }
        public string type { get; set; }
        public int length { get; set; }
        public List<Enumration> EnumrationList =null;
    }


}
