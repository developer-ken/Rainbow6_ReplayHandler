using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rainbow6_ReplayHandler
{
    public partial class Wait : Form
    {
        public bool Shown = false;
        public Wait()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void Wait_Load(object sender, EventArgs e)
        {
            Shown = true;
        }
    }
}
