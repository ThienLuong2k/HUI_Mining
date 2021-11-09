using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace HUIMining
{
    public partial class frmMain : Form
    {
        List<Itemset> HUIs;
        XuLy xl = new XuLy();
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
                bool imported = xl.NhapDL(txt_filename.Text, out int cItem, out int cTransaction);
                if(imported)
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
                
                Stopwatch watch = new Stopwatch();
                Process cp = Process.GetCurrentProcess();
                double usedMemory;
                if(rdo_huiminer.Checked)
                {
                    watch.Start();
                    HUIs = xl.RunAlgoHuiminer(txt_filename.Text, int.Parse(txt_minutil.Text));
                    watch.Stop();
                }
                else
                {
                    watch.Start();
                    HUIs = xl.RunAlgoFhm(txt_filename.Text, int.Parse(txt_minutil.Text));
                    watch.Stop();
                }
                usedMemory = cp.WorkingSet64; // byte
                //PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", cp.ProcessName);
                //usedMemory = ramCounter.NextValue();
                double usedMemoryMB = usedMemory / 1048576.0; // megabyte
                list_hui.Items.Clear();
                if(HUIs != null)
                {
                    foreach (Itemset set in HUIs)
                    {
                        string setName = "";
                        foreach (int item in set.Name)
                        {
                            setName = setName + item.ToString() + ", ";
                        }
                        ListViewItem i = new ListViewItem(setName);
                        i.SubItems.Add(set.utility.ToString());
                        list_hui.Items.Add(i);
                    }
                }
                lbl_showtimer.Text = watch.ElapsedMilliseconds.ToString() + " ms";
                lbl_showmemory.Text = usedMemoryMB.ToString() + " MB";
                lbl_huicount.Text = list_hui.Items.Count.ToString();
                btn_refresh.Enabled = true;
                btn_export.Enabled = true;
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            list_hui.Items.Clear();
            xl.Refresh();
            System.GC.Collect();
            
            txt_filename.Text = txt_minutil.Text = "";
            lbl_huicount.Text = lbl_itemcount.Text = lbl_transactioncount.Text = "0";
            lbl_showmemory.Text = lbl_showtimer.Text = "0";
            btn_run.Enabled = btn_refresh.Enabled = btn_importdata.Enabled = false;
            btn_export.Enabled = false;
            rdo_huiminer.Checked = true;
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            char[] splitChar = { '_', '.'};
            string database = txt_filename.Text.Split(splitChar)[0];
            string algo;
            if (rdo_fhm.Checked)
                algo = "FHM";
            else
                algo = "HUI-Miner";
            //string filename = "HUIs_" + database + "_" + algo + ".txt";
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text files | *.txt";
            //save.FileName += filename;
            if(save.ShowDialog() == DialogResult.OK)
            {
                // đề phòng trường hợp người dùng sửa tên file trước
                string filename = save.FileName;
                if (xl.PrintResult(filename, HUIs, int.Parse(txt_minutil.Text)))
                {
                    DialogResult dr = MessageBox.Show("Xuất ra file thành công. Mở file ngay ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        Process.Start(filename);
                    }
                }
                else
                {
                    MessageBox.Show("Xuất ra file không thành công.", "Thông báo");
                }
            }
        }
    }
}
