using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace HUIMining
{
    public partial class frmMain : Form
    {
        XuLy xuly = new XuLy();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btn_run.Enabled = btn_refresh.Enabled = btn_importdata.Enabled = false;
            btn_export.Enabled = false;
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

        private void btn_importdata_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_filename.Text))
            {
                MessageBox.Show("Chưa chọn file txt !", "Thông báo");
                btn_choosefile.Focus();
            }
            else
            {
                if(xuly.NhapDuLieu(txt_filename.Text, out int cItem, out int cTransaction))
                {
                    lbl_itemcount.Text = cItem.ToString();
                    lbl_transactioncount.Text = cTransaction.ToString();
                    MessageBox.Show("Nhập dữ liệu thành công.", "Thông báo");
                    btn_run.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Nhập dữ liệu thất bại.", "Thông báo");
                }
            }
        }

        private void btn_choosefile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All text file (*.txt)| *.txt";
            DialogResult dr = dialog.ShowDialog();
            if(dr == DialogResult.OK)
            {
                txt_filename.Text = dialog.FileName;
                btn_importdata.Enabled = true;
            }
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_minutil.Text))
            {
                MessageBox.Show("Chưa nhập minutil !", "Thông báo");
            }
            else
            {
                List<Itemset> HUIs;
                Stopwatch watch = new Stopwatch();
                //Process currentProcess = Process.GetCurrentProcess();
                long usedMemory;
                if(rdo_huiminer.Checked)
                {
                    watch.Start();
                    HUIs = xuly.RunAlgo(1, txt_filename.Text, int.Parse(txt_minutil.Text));
                    watch.Stop();
                }
                else
                {
                    watch.Start();
                    HUIs = xuly.RunAlgo(2, txt_filename.Text, int.Parse(txt_minutil.Text));
                    watch.Stop();
                }
                //usedMemory = currentProcess.WorkingSet64; // byte
                //double usedMemoryMB = usedMemory / 1048576.0; // megabyte
                foreach(Itemset set in HUIs)
                {
                    string setName = "";
                    foreach(int item in set.Name)
                    {
                        setName = setName + item.ToString() + ", ";
                    }
                    ListViewItem i = new ListViewItem(setName);
                    i.SubItems.Add(set.utility.ToString());
                    list_hui.Items.Add(i);
                }
                lbl_showtimer.Text = watch.ElapsedMilliseconds.ToString() + " ms";
                //lbl_showmemory.Text = usedMemoryMB.ToString() + " MB";
                lbl_huicount.Text = list_hui.Items.Count.ToString();
                btn_refresh.Enabled = true;
                btn_export.Enabled = true;
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            System.GC.Collect();
            list_hui.Items.Clear();
            xuly.Refresh();
            txt_filename.Text = txt_minutil.Text = "";
            lbl_huicount.Text = lbl_itemcount.Text = lbl_transactioncount.Text = "0";
            lbl_showmemory.Text = lbl_showtimer.Text = "0";
            btn_run.Enabled = btn_refresh.Enabled = btn_importdata.Enabled = false;
            btn_export.Enabled = false;
            rdo_huiminer.Checked = true;
        }
    }
}
