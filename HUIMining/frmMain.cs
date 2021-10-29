using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HUIMining
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btn_run.Enabled = btn_refresh.Enabled = false;
            rdo_huiminer.Checked = true;
        }

        private void txt_minutil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txt_minutil_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txt_minutil.Text) && !String.IsNullOrEmpty(txt_filename.Text))
            {
                btn_run.Enabled = true;
            }
        }
    }
}
