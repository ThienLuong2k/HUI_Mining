using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;

namespace HUIMining
{
    public partial class frmMain : Form
    {
        AlgoHUIMiner huiMiner;
        AlgoFHM fhm;
        private string dbName;
        private string fileCopy;
        private int row;
        private ExcelPackage package;
        private FileInfo info;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btn_run.Enabled = btn_refresh.Enabled = btn_importdata.Enabled = false;
            btnExcel.Enabled = false;
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
                bool imported;
                if (rdo_huiminer.Checked)
                {
                    fhm = null;
                    huiMiner = new AlgoHUIMiner();
                    imported = huiMiner.Input(txt_filename.Text, out cItem, out cTransaction);
                }
                else
                {
                    huiMiner = null;
                    fhm = new AlgoFHM();
                    imported = fhm.Input(txt_filename.Text, out cItem, out cTransaction);
                }
                if (imported)
                {
                    lbl_itemcount.Text = cItem.ToString();
                    lbl_transactioncount.Text = cTransaction.ToString();
                    MessageBox.Show("Nhập dữ liệu thành công.", "Thông báo");
                    // tạo tên file output
                    // lấy tên database: 1. bỏ đuôi .txt
                    string[] inputPath = txt_filename.Text.Split('.')[0].Split('\\');
                    // 2. bỏ phần đường dẫn tuyệt đối
                    dbName = inputPath[inputPath.Length - 1];
                    btn_run.Enabled = true;
                    btnExcel.Enabled = true;
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
                
                // lấy tên thuật toán
                string algo;
                if (rdo_huiminer.Checked)
                    algo = "HuiMiner";
                else
                    algo = "FHM";
                // kết hợp tất cả làm tên file output
                save.FileName = "HUIs_" + dbName + "_" + algo + "_" 
                    + DateTime.Now.ToString("ddMMyyyy HHmmss") + ".txt";
                if(save.ShowDialog() == DialogResult.OK)
                {
                    int HuiCount;
                    string fileout = save.FileName;
                    // tạo các biến đo lường
                    Stopwatch watch = new Stopwatch();
                    Process cp = Process.GetCurrentProcess();
                    double usedMemory;
                    if (huiMiner != null)
                    {
                        watch.Start();
                        huiMiner.RunAlgoHuiminer(txt_filename.Text, fileout, int.Parse(txt_minutil.Text));
                        watch.Stop();
                        HuiCount = huiMiner.huiCount;

                    }
                    else
                    {
                        watch.Start();
                        fhm.RunAlgoFhm(txt_filename.Text, fileout, int.Parse(txt_minutil.Text));
                        watch.Stop();
                        HuiCount = fhm.huiCount;
                    }
                    
                    usedMemory = cp.WorkingSet64; // đơn vị byte
                    //PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", cp.ProcessName);
                    //usedMemory = ramCounter.NextValue();
                    double usedMemoryMB = usedMemory / 1048576.0; // đơn vị megabyte
                    double timeCount = watch.ElapsedMilliseconds; // đơn vị ms
                    double memoryCount = Math.Round(usedMemoryMB, 2);
                    string textshow = "Thực hiện thành công.\n\n- Số tập hữu ích cao: " + HuiCount.ToString()
                    + "\n\n- Thời gian: " + timeCount
                    + " ms\n\n- Bộ nhớ: " + memoryCount + " MB\n\nGhi thông số vào file Excel ?";
                    DialogResult dr = MessageBox.Show(textshow, "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        if(info == null) {
                            // tạo file Excel luôn
                            Tao_Excel();
                        }
                        else if(package == null)
                        {
                            package = new ExcelPackage(info);
                        }
                        ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"];
                        // nếu ô minutil nằm cột bên trái chưa có giá trị --> ghi giá trị vào cả 2 cột trái và phải
                        if(sheet.Cells[row, 2].Value == null)
                        {
                            sheet.Cells[row, 2].Value = int.Parse(txt_minutil.Text);
                            sheet.Cells[row, 7].Value = int.Parse(txt_minutil.Text);
                        }
                        if (huiMiner != null)
                        {
                            
                            sheet.Cells[row, 3].Value = Math.Round(timeCount / 1000, 2); // cột C - đơn vị giây
                            sheet.Cells[row, 8].Value = memoryCount; // cột H
                        }
                        else
                        {
                            sheet.Cells[row, 4].Value = Math.Round(timeCount / 1000, 2); // cột D - đơn vị giây
                            sheet.Cells[row, 9].Value = memoryCount; // cột I
                        }
                        row++;
                        package.SaveAs(info);
                    }
                    System.GC.Collect();
                    Process.Start(save.FileName);
                    btn_refresh.Enabled = true;
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if(huiMiner != null)
            {
                huiMiner.Clear();
            }
            else
            {
                fhm.Clear();
            }
            System.GC.Collect();
            row = 7;
            group_algo.Enabled = true;
            txt_minutil.Text = "";
            lbl_itemcount.Text = lbl_transactioncount.Text = "0";
            btn_run.Enabled = btn_refresh.Enabled = false;
            rdo_huiminer.Checked = true;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Tao_Excel();
        }

        private void Tao_Excel()
        {
            DateTime now = DateTime.Now;
            // chuẩn bị tên file
            string fileGoc = @"D:\ket qua tim HUI\ThongSo.xlsx";
            fileCopy = @"D:\ket qua tim HUI\ThongSo_" + dbName + "_" + now.ToString("ddMMyyyy HHmmss") + ".xlsx";
            // copy file
            File.Copy(fileGoc, fileCopy);
            // tạo các đối tượng cần thiết
            info = new FileInfo(fileCopy);
            package = new ExcelPackage(info);
            row = 7;
            // insert tên database và ngày giờ tạo
            package.Workbook.Worksheets["Sheet1"].Cells["B1"].Value = dbName;
            package.Workbook.Worksheets["Sheet1"].Cells["B3"].Value = now.ToString("dd/MM/yyyy HH:mm:ss");
            package.SaveAs(info);
            // hiển thị lên textbox
            txtFileExcel.Text = fileCopy;
        }
    }
}
