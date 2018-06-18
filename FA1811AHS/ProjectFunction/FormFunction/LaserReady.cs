using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FA1811AHS
{
    /// <summary>
    /// 雷射準備倒數
    /// </summary>
    public partial class LaserReady : Form
    {
        private int waittime = 14;

        public LaserReady()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "雷射準備中...........(" + waittime + ")";
            waittime--;
            if (waittime == 0)
                this.Dispose();
        }
    }
}