using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace HUIMining
{
    public partial class frmMain : Form
    {
        XuLy xl = new XuLy();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btn_run.Enabled = btn_refresh.Enabled = btn_importdata.Enabled = false;
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
                
                int cItem, cTransaction;
                bool imported = xl.NhapDL(txt_filename.Text, out cItem, out cTransaction);
                if (imported)
                {
                    lbl_itemcount.Text = cItem.ToString();
                    lbl_transactioncount.Text = cTransaction.ToString();
                    MessageBox.Show("Nhập dữ liệu thành công.", "Thông báo");
                    btn_run.Enabled = true;
                    group_algo.Enabled = false;
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
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Text files | *.txt";
                // tạo tên file output
                // lấy tên database: 1. bỏ đuôi .txt
                string[] inputPath = txt_filename.Text.Split('.')[0].Split('\\');
                // 2. bỏ phần đường dẫn tuyệt đối
                string dbName = inputPath[inputPath.Length - 1];
                // lấy tên thuật toán
                string algo;
                if (rdo_huiminer.Checked)
                    algo = "HuiMiner";
                else
                    algo = "FHM";
                // kết hợp tất cả làm tên file output
                save.FileName = "HUIs_" + dbName + "_" + algo + "_" 
                    + DateTime.Now.ToString("ddmmyyyy HHmmss") + ".txt";
                if(save.ShowDialog() == DialogResult.OK)
                {
                    int HuiCount;
                    string fileout = save.FileName;
                    // tạo các biến đo lường
                    Stopwatch watch = new Stopwatch();
                    Process cp = Process.GetCurrentProcess();
                    double usedMemory;
                    if (rdo_huiminer.Checked)
                    {
                        watch.Start();
                        xl.RunAlgoHuiminer(txt_filename.Text, fileout, int.Parse(txt_minutil.Text));
                        watch.Stop();
                        
                    }
                    else
                    {
                        watch.Start();
                        xl.RunAlgoFhm(txt_filename.Text, fileout, int.Parse(txt_minutil.Text));
                        watch.Stop();
                    }
                    HuiCount = xl.HuiCount;
                    usedMemory = cp.WorkingSet64; // byte
                    //PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", cp.ProcessName);
                    //usedMemory = ramCounter.NextValue();
                    double usedMemoryMB = usedMemory / 1048576.0; // megabyte
                    string textshow = "Thực hiện thành công.\n\n- Số tập hữu ích cao: " + HuiCount.ToString()
                    + "\n\n- Thời gian: " + watch.ElapsedMilliseconds.ToString()
                    + " ms\n\n- Bộ nhớ: " + usedMemoryMB.ToString() + " MB\n\nMở file ngay?";
                    DialogResult dr = MessageBox.Show(textshow, "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        Process.Start(save.FileName);
                    }
                    btn_refresh.Enabled = true;
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            xl.Refresh();
            System.GC.Collect();

            group_algo.Enabled = true;
            txt_minutil.Text = "";
            lbl_itemcount.Text = lbl_transactioncount.Text = "0";
            btn_run.Enabled = btn_refresh.Enabled = false;
            rdo_huiminer.Checked = true;
        }
    }
}
