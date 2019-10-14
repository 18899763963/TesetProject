using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallManagerSpace.Resources.GUIModels
{
    class MenuButtonObj
    {
        public void EnableToolStripButton(ToolStripButton toolStripButton)
        {
            toolStripButton.Enabled = true;
        }
        public void DisableToolStripButton(ToolStripButton toolStripButton)
        {
            toolStripButton.Enabled = false;
        }
    }
}
