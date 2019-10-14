using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIModels
{
    class MenuItemObj
    {
        public void EnableToolStripItem(ToolStripMenuItem toolStripMenuItem)
        {
            toolStripMenuItem.Enabled = true;
        }
        public void DisableToolStripItem(ToolStripMenuItem toolStripMenuItem)
        {
            toolStripMenuItem.Enabled = false;
        }
    }
}
